using System;
using UnityEngine;
using QFramework;

namespace ProjectSurvivor
{
    public partial class Exp : PowerUp
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.GetComponent<CollectableArea>())
            {
                FlyingToPlayer = true;
            }
        }
        
        protected override void Execute()
        {
            AudioKit.PlaySound("Exp");
            Global.Exp.Value++;
            this.DestroyGameObjGracefully();
        }
        
        protected override Collider2D Collider2D => selfCollider2D;
    }
}