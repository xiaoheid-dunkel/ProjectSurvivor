using UnityEngine;
using QFramework;

namespace ProjectSurvivor
{
	public partial class Bomb : GameplayObject
	{

		public static void Execute()
		{
			foreach (var enemyObj in GameObject.FindGameObjectsWithTag("Enemy"))
			{
				var enemy = enemyObj.GetComponent<Enemy>();

				if (enemy && enemy.gameObject.activeSelf)
				{
					DamageSystem.CalculateDamage(Global.BombDamage.Value,enemy);
				}
			}

			AudioKit.PlaySound("Bomb");
			UIGamePanel.FlashScreen.Trigger();
			CameraController.Shake();
		}
		
		private void OnTriggerEnter2D(Collider2D other)
		{
			if (other.GetComponent<CollectableArea>())
			{
				Execute();

				this.DestroyGameObjGracefully();
			}
		}

		protected override Collider2D Collider2D => SelfCollider2D;
	}
}
