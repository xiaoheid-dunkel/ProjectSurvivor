using UnityEngine;
using QFramework;

namespace ProjectSurvivor
{
	public partial class Coin : PowerUp
	{
		private void OnTriggerEnter2D(Collider2D other)
		{
			if (other.GetComponent<CollectableArea>())
			{
				FlyingToPlayer = true;
			}
		}

		protected override Collider2D Collider2D => SelfCollider2D;
		protected override void Execute()
		{
			AudioKit.PlaySound("Coin");
			Global.Coin.Value++;
			this.DestroyGameObjGracefully();
		}
	}
}
