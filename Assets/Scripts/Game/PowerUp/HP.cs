using UnityEngine;
using QFramework;

namespace ProjectSurvivor
{
	public partial class HP : PowerUp
	{
		private void OnTriggerEnter2D(Collider2D other)
		{
			if (other.GetComponent<CollectableArea>())
			{
				if (Global.HP.Value == Global.MaxHP.Value)
				{
					
				}
				else
				{
					FlyingToPlayer = true;
				}
			}
		}

		protected override Collider2D Collider2D => SelfCollider2D;
		protected override void Execute()
		{
			AudioKit.PlaySound("HP");
			Global.HP.Value++;
			this.DestroyGameObjGracefully();
		}
	}
}
