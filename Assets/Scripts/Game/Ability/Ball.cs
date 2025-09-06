using QAssetBundle;
using UnityEngine;
using QFramework;
using Random = UnityEngine.Random;

namespace ProjectSurvivor
{
	public partial class Ball : ViewController
	{
		void Start()
		{
            // Set initial random velocity for the ball
            SelfRigidbody2D.linearVelocity =
                // Random direction vector
                new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f)) *
                // Random speed magnitude (based on global basketball speed value)
                Random.Range(Global.BasketBallSpeed.Value - 2, Global.BasketBallSpeed.Value + 2);;
            // Register super basketball event, scale up ball when unlocked
            Global.SuperBasketBall.RegisterWithInitValue(unlocked =>
			{
				if (unlocked)
				{
                    // Scale ball up to 3 times
                    this.LocalScale(3);
				}
			}).UnRegisterWhenGameObjectDestroyed(gameObject);
            // Register hurt box trigger event
            HurtBox.OnTriggerEnter2DEvent(collider =>
			{
                // Get hit hurt box component from collider
                var hurtBox = collider.GetComponent<HitHurtBox>();
				if (hurtBox)
				{
                    // Check if collider owner is an enemy
                    if (hurtBox.Owner.CompareTag("Enemy"))
					{
                        // Get enemy interface
                        var enemy = hurtBox.Owner.GetComponent<IEnemy>();
                        // Calculate damage multiplier (random 2-3x in super basketball mode, otherwise 1x)
                        var damageTimes = Global.SuperBasketBall.Value ? Random.Range(2, 3 + 1) : 1;
                        // Calculate and apply damage
                        DamageSystem.CalculateDamage(Global.BasketBallDamage.Value * damageTimes,enemy);
                        // 50% chance to knock back enemy
                        if (Random.Range(0, 1f) < 0.5f && collider && collider.attachedRigidbody &&
						    Player.Default)
						{
                            // Set enemy velocity (combination of directions from ball and player)
                            collider.attachedRigidbody.linearVelocity =
                                // Opposite direction from ball
                                collider.NormalizedDirection2DFrom(this) * 5 +
                                // Opposite direction from player
                                collider.NormalizedDirection2DFrom(Player.Default) * 10;
						}
					}
				}

			}).UnRegisterWhenGameObjectDestroyed(gameObject);
		}
        // Collision enter 2D event handler
        private void OnCollisionEnter2D(Collision2D other)
		{
            // Get collision normal
            var normal = other.GetContact(0).normal;
            // Determine how to bounce based on collision normal
            if (normal.x > normal.y)
			{
                // Horizontal collision dominant, keep X velocity, randomize Y velocity
                var rb = SelfRigidbody2D;
				rb.linearVelocity = new Vector2(rb.linearVelocity.x,
                    // Keep Y direction sign but randomize magnitude
                    Mathf.Sign(rb.linearVelocity.y) * Random.Range(0.5f, 1.5f) *
                    // Randomize based on global basketball speed value
                    Random.Range(Global.BasketBallSpeed.Value - 2, Global.BasketBallSpeed.Value + 2));
                // Random rotation speed
                rb.angularVelocity = Random.Range(-360f, 360f);
			}
			else
			{
                // Vertical collision dominant, keep Y velocity, randomize X velocity
                var rb = SelfRigidbody2D;
                rb.linearVelocity =
                    new Vector2(
                        // Keep X direction sign but randomize magnitude
                        Mathf.Sign(rb.linearVelocity.x) * Random.Range(0.5f, 1.5f) * Random.Range(
                            Global.BasketBallSpeed.Value - 2, Global.BasketBallSpeed.Value + 2),
                        rb.linearVelocity.y);
                // Random rotation speed
                rb.angularVelocity = Random.Range(-360f, 360f);
			}
            // Play ball collision sound effect
            AudioKit.PlaySound(Sfx.BALL);
		}
	}
}
