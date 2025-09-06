using UnityEngine;

namespace ProjectSurvivor
{
    public class DamageSystem
    {
        public static void CalculateDamage(float baseDamage, IEnemy enemy, int maxNormalDamage = 2,
            float criticalDamageTimes = 5)
        {
            baseDamage *= Global.DamageRate.Value;
            if (Random.Range(0, 1.0f) < Global.CriticalRate.Value)
            {
                enemy.Hurt(baseDamage * Random.Range(2f,criticalDamageTimes),false,true); 
            }
            else
            {
                enemy.Hurt(baseDamage + Random.Range(-1, maxNormalDamage));
            }
        }
    }
}