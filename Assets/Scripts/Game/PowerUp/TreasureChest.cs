using UnityEngine;
using QFramework;

namespace ProjectSurvivor
{
	public partial class TreasureChest : GameplayObject
	{
		private void OnTriggerEnter2D(Collider2D other)
		{
			if (other.GetComponent<CollectableArea>())
			{
				UIGamePanel.OpenTreasurePanel.Trigger();
				
				AudioKit.PlaySound("TreasureChest");
				
				this.DestroyGameObjGracefully();
			}
		}

		protected override Collider2D Collider2D => SelfCollider2D;
	}
}
