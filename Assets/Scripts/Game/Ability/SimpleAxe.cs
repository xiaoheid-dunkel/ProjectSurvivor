using System;
using UnityEngine;
using QFramework;

namespace ProjectSurvivor
{
    public partial class SimpleAxe : ViewController
    {
        void Start()
        {
            // Code Here
        }

        private float mCurrentSeconds = 0;

        private void Update()
        {
            mCurrentSeconds += Time.deltaTime;


            if (mCurrentSeconds >= 1.0f)
            {
                Axe.Instantiate()
                    .Show()
                    .Position(this.Position())
                    .Self(self =>
                    {
                        var rigidbody2D = self.GetComponent<Rigidbody2D>();

                        var randomX = RandomUtility.Choose(-8, -5, -3, 3, 5, 8);
                        var randomY = RandomUtility.Choose(3, 5, 8);
                        rigidbody2D.linearVelocity = new Vector2(randomX, randomY);

                        self.OnTriggerEnter2DEvent(collider =>
                        {
                            var hurtBox = collider.GetComponent<HitHurtBox>();
                            if (hurtBox)
                            {
                                if (hurtBox.Owner.CompareTag("Enemy"))
                                {
                                    hurtBox.Owner.GetComponent<Enemy>().Hurt(2);
                                }
                            }
                        }).UnRegisterWhenGameObjectDestroyed(self);

                        ActionKit.OnUpdate.Register(() =>
                        {
                            if (Player.Default)
                            {
                                if (Player.Default.Position().y - self.Position().y > 15)
                                {
                                    self.DestroyGameObjGracefully();
                                }
                            }

                        }).UnRegisterWhenGameObjectDestroyed(self);
                    });

                mCurrentSeconds = 0;
            }
        }
    }
}