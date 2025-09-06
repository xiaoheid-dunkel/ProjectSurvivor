using System;
using System.Linq;
using QAssetBundle;
using UnityEngine;
using QFramework;
using Random = UnityEngine.Random;

namespace ProjectSurvivor
{
    public partial class SimpleKnife : ViewController
    {
        private float mCurrentSeconds = 0;

        private void Update()
        {
            mCurrentSeconds += Time.deltaTime;

            if (mCurrentSeconds >= Global.SimpleKnifeDuration.Value)
            {
                mCurrentSeconds = 0;
                
                if (Player.Default)
                {
                    var enemies = FindObjectsByType<Enemy>(FindObjectsInactive.Exclude, FindObjectsSortMode.None)
                        .OrderBy(enemy => Player.Default.Distance2D(enemy))
                        .Take(Global.SimpleKnifeCount.Value + Global.AdditionalFlyThingCount.Value);

                    var i = 0;
                    foreach (var enemy in enemies)
                    {
                        if (i < 4)
                        {
                            ActionKit.DelayFrame(11 * i, () => AudioKit.PlaySound(Sfx.KNIFE))
                                .StartGlobal();
                            i++;
                        }

                        if (enemy)
                        {
                            Knife.Instantiate()
                                .Position(this.Position())
                                .Show()
                                .Self(self =>
                                {
                                    var selfCache = self;
                                    var direction = enemy.NormalizedDirection2DFrom(Player.Default);
                                    self.transform.up = direction;
                                    var rigidbody2D = self.GetComponent<Rigidbody2D>();
                                    rigidbody2D.linearVelocity = direction * 10;
                                    var attackCount = 0;
                                    self.OnTriggerEnter2DEvent(collider =>
                                    {
                                        var hurtBox = collider.GetComponent<HitHurtBox>();
                                        if (hurtBox)
                                        {
                                            if (hurtBox.Owner.CompareTag("Enemy"))
                                            {

                                                var damageTimes = Global.SuperKnife.Value ? Random.Range(2, 3 + 1) : 1;
                                                DamageSystem.CalculateDamage(Global.SimpleKnifeDamage.Value * damageTimes,
                                                    hurtBox.Owner.GetComponent<Enemy>());
                                                attackCount++;

                                                if (attackCount >= Global.SimpleKnifeAttackCount.Value)
                                                {
                                                    selfCache.DestroyGameObjGracefully();
                                                }
                                            }
                                        }
                                    }).UnRegisterWhenGameObjectDestroyed(self);

                                    ActionKit.OnUpdate.Register(() =>
                                    {
                                        if (Player.Default)
                                        {
                                            if ((Player.Default.Distance2D(selfCache)) > 20)
                                            {
                                                self.DestroyGameObjGracefully();
                                            }
                                        }
                                    }).UnRegisterWhenGameObjectDestroyed(self);
                                });
                        }
                    }
                }
            }
        }
    }
}