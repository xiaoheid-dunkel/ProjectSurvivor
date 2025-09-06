using System;
using System.Collections.Generic;
using UnityEngine;
using QFramework;
using Random = UnityEngine.Random;

namespace ProjectSurvivor
{
    public partial class RotateSword : ViewController
    {
        private List<Collider2D> mSwords = new List<Collider2D>();


        void Start()
        {
            // Code Here


            Sword.Hide();

            void CreateSword()
            {
                mSwords.Add(Sword.InstantiateWithParent(this)
                    .Self(self =>
                    {
                        self.OnTriggerEnter2DEvent(collider =>
                        {
                            var hurtBox = collider.GetComponent<HitHurtBox>();
                            if (hurtBox)
                            {
                                if (hurtBox.Owner.CompareTag("Enemy"))
                                {
                                    var damageTimes = Global.SuperRotateSword.Value ? Random.Range(2, 3 + 1) : 1;
                                    
                                    DamageSystem.CalculateDamage(Global.RotateSwordDamage.Value * damageTimes,
                                        hurtBox.Owner.GetComponent<Enemy>());

                                    if (Random.Range(0, 1.0f) < 0.5f)
                                    {
                                        collider.attachedRigidbody.linearVelocity =
                                            collider.NormalizedDirection2DFrom(self) * 5 +
                                            collider.NormalizedDirection2DFrom(Player.Default) * 10;
                                    }
                                }
                            }
                        }).UnRegisterWhenGameObjectDestroyed(self);
                    })
                    .Show()
                );
            }


            void CreateSwords()
            {
                var toAddCount = Global.RotateSwordCount.Value + Global.AdditionalFlyThingCount.Value - mSwords.Count;
                for (var i = 0; i < toAddCount; i++)
                {
                    CreateSword();
                }

                UpdateCirclePos();
            }
            Global.RotateSwordCount.Or(Global.AdditionalFlyThingCount)
                .Register(CreateSwords)
                .UnRegisterWhenGameObjectDestroyed(gameObject);

            Global.RotateSwordRange.Register((range) => { UpdateCirclePos(); })
                .UnRegisterWhenGameObjectDestroyed(gameObject);
            
            CreateSwords();
        }


        void UpdateCirclePos()
        {
            var radius = Global.RotateSwordRange.Value;
            var durationDegrees = 360 / mSwords.Count;

            for (var i = 0; i < mSwords.Count; i++)
            {
                var circleLocalPos = new Vector2(Mathf.Cos(durationDegrees * i * Mathf.Deg2Rad),
                                         Mathf.Sin(durationDegrees * i * Mathf.Deg2Rad)) *
                                     radius;
                mSwords[i].LocalPosition(circleLocalPos.x, circleLocalPos.y)
                    .LocalEulerAnglesZ(durationDegrees * i - 90);
            }
        }

        private void Update()
        {
            var speed = Global.SuperRotateSword.Value
                ? Time.frameCount * 10
                : Global.RotateSwordSpeed.Value * Time.frameCount;

            this.LocalEulerAnglesZ(-speed);
        }
    }
}