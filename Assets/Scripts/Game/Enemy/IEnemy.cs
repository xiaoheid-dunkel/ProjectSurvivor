namespace ProjectSurvivor
{
    public interface IEnemy
    {
        void Hurt(float value, bool force = false,bool critical = false);

        void SetSpeedScale(float speedScale);
        void SetHPScale(float hpScale);
    }
}