using System;
using QAssetBundle;
using UnityEngine;
using QFramework;

namespace ProjectSurvivor
{
	
	public partial class Enemy : ViewController,IEnemy
	{
		public float HP = 3;

		public float MovementSpeed = 2.0f;

		public Color DissolveColor = Color.yellow;

		public bool TreasureChestEnemy = false;
		
		private void Start()
		{
			EnemyGenerator.EnemyCount.Value++;
		}

		private void OnDestroy()
		{
			EnemyGenerator.EnemyCount.Value--;
		}

		private void FixedUpdate()
		{
			if (!mIgnoreHurt)
			{
				if (Player.Default)
				{
					var direction = (Player.Default.transform.position - transform.position).normalized;

					SelfRigidbody2D.linearVelocity = direction * MovementSpeed;
				}
				else
				{
					SelfRigidbody2D.linearVelocity = Vector2.zero;
				}
			}
		}

		private void Update()
		{
			
			if (HP <= 0)
			{
				Global.GeneratePowerUp(gameObject,TreasureChestEnemy);
				FxController.Play(Sprite,DissolveColor);
				AudioKit.PlaySound(Sfx.ENEMYDIE);
				this.DestroyGameObjGracefully();
			}
		}

		private bool mIgnoreHurt = false;
		public void Hurt(float value,bool force = false,bool critical = false)
		{
			if (mIgnoreHurt && !force) return;
			mIgnoreHurt = true;
			
			SelfRigidbody2D.linearVelocity = Vector2.zero;
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
