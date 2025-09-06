using System;
using System.Linq;
using UnityEngine;
using QFramework;
using Random = UnityEngine.Random;

namespace ProjectSurvivor
{
    public partial class SimpleSword : ViewController
    {
        private float mCurrentSeconds = 0;



        private void Update()
        {
            mCurrentSeconds += Time.deltaTime;

            if (mCurrentSeconds >= Global.SimpleAbilityDuration.Value)
            {
                mCurrentSeconds = 0;

                var countTimes = Global.SuperSword.Value ? 2 : 1;
                var damageTimes = Global.SuperSword.Value ? Random.Range(2, 3 + 1) : 1;
                var distanceTimes = Global.SuperSword.Value ? 2 : 1;
                var enemies = FindObjectsByType<Enemy>(FindObjectsInactive.Exclude, FindObjectsSortMode.None);

                foreach (var enemy in enemies
                             .OrderBy(e => e.Direction2DFrom(Player.Default).magnitude)
                             .Where(e => e.Direction2DFrom(Player.Default).magnitude <
                                         Global.SimpleSwordRange.Value * distanceTimes)
                             .Take((Global.SimpleSwordCount.Value + Global.AdditionalFlyThingCount.Value) * countTimes))
                {
                    Sword.Instantiate()
                        .Position(enemy.Position() + Vector3.left * 0.25f)
                        .Show()
                        .Self(self =>
                        {
                            var selfCache = self;
                            selfCache.OnTriggerEnter2DEvent(collider2D =>
                            {
                                var hurtBox = collider2D.GetComponent<HitHurtBox>();
                                if (hurtBox)
                                {
                                    if (hurtBox.Owner.CompareTag("Enemy"))
                                    {
                                        DamageSystem.CalculateDamage(Global.SimpleAbilityDamage.Value * damageTimes,
                                            hurtBox.Owner.GetComponent<Enemy>());
                                    }
                                }
                            }).UnRegisterWhenGameObjectDestroyed(gameObject);

                            // 劈砍动画
                            ActionKit
                                .Sequence()
                                .Callback(() => { selfCache.enabled = false; })
                                .Parallel(p =>
                                {
                                    p.Lerp(0, 10, 0.2f,
                                        (z) => selfCache.LocalEulerAnglesZ(z));

                                    p.Append(ActionKit.Sequence()
                                        .Lerp(0, 1.25f, 0.1f, scale => selfCache.LocalScale(scale))
                                        .Lerp(1.25f, 1, 0.1f, scale => selfCache.LocalScale(scale))
                                    );
                                })
                                .Callback(() => { selfCache.enabled = true; })
                                .Parallel(p =>
                                {
                                    p.Lerp(10, -180, 0.2f, z => selfCache.LocalEulerAnglesZ(z));
                                    p.Append(ActionKit.Sequence()
                                        .Lerp(1, 1.25f, 0.1f, scale => selfCache.LocalScale(scale))
                                        .Lerp(1.25f, 1f, 0.1f, scale => selfCache.LocalScale(scale))
                                    );
                                })
                                .Callback(() => { selfCache.enabled = false; })
                                .Lerp(-180, 0, 0.3f, z =>
                                {
                                    selfCache.LocalEulerAnglesZ(z)
                                        .LocalScale(z.Abs() / 180);
                                })
                                .Start(this, () => { selfCache.DestroyGameObjGracefully(); });
                        });
                }
            }
        }
    }
}