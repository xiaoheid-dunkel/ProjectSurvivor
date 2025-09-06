using System;
using UnityEngine;
using QFramework;

namespace ProjectSurvivor
{
	public partial class EnemyMiniBoss : ViewController,IEnemy
	{
		public enum States
		{
			FlowingPlayer,
			Warning, // 警戒状态
			Dash, // 冲向主角
			Wait, // 等待
		}

		public FSM<States> FSM = new FSM<States>();

		public float HP = 50;
		public float MovementSpeed = 2.0f;

		private void Start()
		{
			EnemyGenerator.EnemyCount.Value++;
			
			FSM.State(States.FlowingPlayer)
				.OnFixedUpdate(() =>
				{
					if (Player.Default)
					{
						var direction = (Player.Default.transform.position - transform.position).normalized;

						SelfRigidbody2D.linearVelocity = direction * MovementSpeed;

						if ((Player.Default.transform.Position() - transform.Position()).magnitude <= 15)
						{
							FSM.ChangeState(States.Warning);
						}
					}
					else
					{
						SelfRigidbody2D.linearVelocity = Vector2.zero;
					}
					
					
				});
			FSM.State(States.Warning)
				.OnEnter(() =>
				{
					SelfRigidbody2D.linearVelocity = Vector2.zero;
				})
				.OnUpdate(() =>
				{
					// 21 ~ 3
					var frames = 3 + (60 * 3 - FSM.FrameCountOfCurrentState) / 10;

					if (FSM.FrameCountOfCurrentState / frames % 2 == 0)
					{
						Sprite.color = Color.red;
					}
					else
					{
						Sprite.color = Color.white;
					}
					
					if (FSM.FrameCountOfCurrentState >= 60 * 3)
					{
						FSM.ChangeState(States.Dash);
					}
				})
				.OnExit(() =>
				{
					Sprite.color = Color.white;
				});

			var dashStartPos = Vector3.zero;
			var dashStartDistanceToPlayer = 0f;
			FSM.State(States.Dash)
				.OnEnter(() =>
				{
					var direction = (Player.Default.Position() - transform.Position()).normalized;
					SelfRigidbody2D.linearVelocity = direction * 15;
					dashStartPos = transform.Position();
					dashStartDistanceToPlayer = (Player.Default.Position() - transform.Position()).magnitude;
				})
				.OnUpdate(() =>
				{
					var distance = (transform.Position() - dashStartPos).magnitude;

					if (distance >= dashStartDistanceToPlayer + 5)
					{
						FSM.ChangeState(States.Wait);
					}
				});

			FSM.State(States.Wait)
				.OnEnter(() =>
				{
					SelfRigidbody2D.linearVelocity = Vector2.zero;
				})
				.OnUpdate(() =>
				{
					if (FSM.FrameCountOfCurrentState >= 30)
					{
						FSM.ChangeState(States.FlowingPlayer);
					}
				});
			
			FSM.StartState(States.FlowingPlayer);
			
		}

		private void OnDestroy()
		{
			EnemyGenerator.EnemyCount.Value--;
		}
		private bool mIgnoreHurt = false;
		private void Update()
		{
			FSM.Update();


			if (HP <= 0)
			{
				Global.GeneratePowerUp(gameObject,true);
				this.DestroyGameObjGracefully();
			}
		}

		private void FixedUpdate()
		{
			FSM.FixedUpdate();
		}


		public void Hurt(float value, bool force = false,bool critical = false)
		{
			if (mIgnoreHurt && !force) return;
			
			FloatingTextController.Play(transform.position,value.ToString("0"),critical);
			
			Sprite.color = Color.red;
			AudioKit.PlaySound("Hit");
			ActionKit.Delay(0.2f,() =>
			{
				HP -= value;
				Sprite.color = Color.white;
				mIgnoreHurt = false;
			}).Start(this);
		}

		public void SetSpeedScale(float speedScale)
		{
			MovementSpeed *= speedScale;
		}

		public void SetHPScale(float hpScale)
		{
			HP *= hpScale;
		}
	}
}
