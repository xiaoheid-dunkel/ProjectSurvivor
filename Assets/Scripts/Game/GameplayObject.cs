using QFramework;
using UnityEngine;

namespace ProjectSurvivor
{
    public abstract class GameplayObject : ViewController
    {
        public bool InScreen { get; set; }
        
        protected abstract Collider2D Collider2D { get; }
        
        private void OnBecameVisible()
        {
            Collider2D.enabled = true;
            InScreen = true;
        }
        
        private void OnBecameInvisible()
        {
            Collider2D.enabled = false;
            InScreen = false;
        }
    }

}