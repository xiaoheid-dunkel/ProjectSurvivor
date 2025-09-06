using QFramework;
using UnityEngine;

namespace ProjectSurvivor
{
    // Global architecture class, inheriting from Architecture<Global>, using singleton pattern
    public class Global : Architecture<Global>
    {
        // Editor-only code: Add menu item to clear all data
#if UNITY_EDITOR
        [UnityEditor.MenuItem("Tool/Clear All Data")]
        public static void ClearAllData()
        {
            PlayerPrefs.DeleteAll();
        }
        #endif
        
        #region Model

        public static BindableProperty<int> HP = new BindableProperty<int>(3);
        public static BindableProperty<int> MaxHP = new BindableProperty<int>(3);

        /// <summary>
        /// 经验值
        /// </summary>
        public static BindableProperty<int> Exp = new BindableProperty<int>(0);

        public static BindableProperty<int> Coin = new BindableProperty<int>(0);

        public static BindableProperty<int> Level = new BindableProperty<int>(1);

        public static BindableProperty<float> CurrentSeconds = new BindableProperty<float>(0);

        public static BindableProperty<bool> SimpleSwordUnlocked = new BindableProperty<bool>(false);
        public static BindableProperty<float> SimpleAbilityDamage = new(Config.InitSimpleSwordDamage);
        public static BindableProperty<float> SimpleAbilityDuration = new(Config.InitSimpleSwordDuration);
        public static BindableProperty<int> SimpleSwordCount = new(Config.InitSimpleSwordCount);
        public static BindableProperty<float> SimpleSwordRange = new(Config.InitSimpleSwordRange);

        public static BindableProperty<bool> BombUnlocked = new(false);
        public static BindableProperty<float> BombDamage = new(Config.InitBombDamage);
        public static BindableProperty<float> BombPercent = new(Config.InitBombPercent);
        
        public static BindableProperty<bool> SimpleKnifeUnlocked = new BindableProperty<bool>(false);
        public static BindableProperty<float> SimpleKnifeDamage = new(Config.InitSimpleKnifeDamage);
        public static BindableProperty<float> SimpleKnifeDuration = new(Config.InitSimpleKnifeDuration);
        public static BindableProperty<int> SimpleKnifeCount = new(Config.InitSimpleKnifeCount);
        public static BindableProperty<int> SimpleKnifeAttackCount = new BindableProperty<int>(1);

        public static BindableProperty<bool> RotateSwordUnlocked = new BindableProperty<bool>(false);
        public static BindableProperty<float> RotateSwordDamage = new(Config.InitRotateSwordDamage); 
        public static BindableProperty<int> RotateSwordCount = new(Config.InitRotateSwordCount); 
        public static BindableProperty<float> RotateSwordSpeed = new(Config.InitRotateSwordSpeed); 
        public static BindableProperty<float> RotateSwordRange = new(Config.InitRotateSwordRange);

        public static BindableProperty<bool> BasketBallUnlocked = new BindableProperty<bool>(false);
        public static BindableProperty<float> BasketBallDamage = new(Config.InitBasketBallDamage);
        public static BindableProperty<float> BasketBallSpeed = new(Config.InitBasketBallSpeed);
        public static BindableProperty<int> BasketBallCount = new(Config.InitBasketBallCount);
        
        public static BindableProperty<float> CriticalRate = new(Config.InitCriticalRate);
        public static BindableProperty<float> DamageRate = new(1);
        public static BindableProperty<int> AdditionalFlyThingCount = new(0);
        public static BindableProperty<float> MovementSpeedRate = new(1.0f);
        public static BindableProperty<float> CollectableArea = new(Config.InitCollectableArea);
        public static BindableProperty<float> AdditionalExpPercent = new(0);

        public static BindableProperty<bool> SuperKnife = new(false);
        public static BindableProperty<bool> SuperSword = new(false);
        public static BindableProperty<bool> SuperRotateSword = new(false);
        public static BindableProperty<bool> SuperBomb = new(false);
        public static BindableProperty<bool> SuperBasketBall = new(false);
        
        
        public static BindableProperty<float> ExpPercent = new BindableProperty<float>(0.3f);
        public static BindableProperty<float> CoinPercent = new BindableProperty<float>(0.05f);

        #endregion

        [RuntimeInitializeOnLoadMethod]
        public static void AutoInit()
        {
            AudioKit.PlaySoundMode = AudioKit.PlaySoundModes.IgnoreSameSoundInGlobalFrames;
            ResKit.Init();
            UIKit.Root.SetResolution(1920, 1080, 1);

            Global.MaxHP.Value = PlayerPrefs.GetInt(nameof(MaxHP), 3);
            HP.Value = MaxHP.Value;
            Global.Coin.Value = PlayerPrefs.GetInt("coin", 0);

            Global.ExpPercent.Value = PlayerPrefs.GetFloat(nameof(ExpPercent), 0.4f);
            Global.CoinPercent.Value = PlayerPrefs.GetFloat(nameof(CoinPercent), 0.1f);

            Global.Coin.Register(coin => { PlayerPrefs.SetInt(nameof(coin), coin); });

            Global.ExpPercent.Register(expPercent => { PlayerPrefs.SetFloat(nameof(expPercent), expPercent); });

            Global.CoinPercent.Register(coinPercent => { PlayerPrefs.SetFloat(nameof(CoinPercent), coinPercent); });

            Global.MaxHP.Register(maxHP => { PlayerPrefs.SetInt(nameof(MaxHP), maxHP); });

            var _ = Interface;
        }

        public static void ResetData()
        {
            HP.Value = MaxHP.Value;
            Exp.Value = 0;
            Level.Value = 1;
            CurrentSeconds.Value = 0;

            SimpleSwordUnlocked.Value = false;
            SimpleAbilityDamage.Value = Config.InitSimpleSwordDamage;
            SimpleAbilityDuration.Value = Config.InitSimpleSwordDuration;
            SimpleSwordRange.Value = Config.InitSimpleSwordRange;
            SimpleSwordCount.Value = Config.InitSimpleSwordCount;

            BombPercent.Value = Config.InitBombPercent;
            BombUnlocked.Value = false;
            BombDamage.Value = Config.InitBombDamage;
            
            SimpleKnifeUnlocked.Value = false;
            SimpleKnifeDamage.Value = Config.InitSimpleKnifeDamage;
            SimpleKnifeDuration.Value = Config.InitSimpleKnifeDuration;
            SimpleKnifeCount.Value = Config.InitSimpleKnifeCount;
            SimpleKnifeAttackCount.Value = 1;

            RotateSwordUnlocked.Value = false;
            RotateSwordDamage.Value = Config.InitRotateSwordDamage;
            RotateSwordCount.Value = Config.InitRotateSwordCount;
            RotateSwordSpeed.Value = Config.InitRotateSwordSpeed;
            RotateSwordRange.Value = Config.InitRotateSwordRange;

            BasketBallUnlocked.Value = false;
            BasketBallDamage.Value = Config.InitBasketBallDamage;
            BasketBallCount.Value = Config.InitBasketBallCount;
            BasketBallSpeed.Value = Config.InitBasketBallSpeed;
            
            CriticalRate.Value = Config.InitCriticalRate;
            DamageRate.Value = 1;
            AdditionalFlyThingCount.Value = 0;
            MovementSpeedRate.Value = 1.0f;
            CollectableArea.Value = Config.InitCollectableArea;
            AdditionalExpPercent.Value = 0;

            SuperKnife.Value = false;
            SuperBomb.Value = false;
            SuperRotateSword.Value = false;
            SuperSword.Value = false;
            SuperBasketBall.Value = false;
            
            EnemyGenerator.EnemyCount.Value = 0;
            Interface.GetSystem<ExpUpgradeSystem>().ResetData();
        }

        public static int ExpToNextLevel()
        {
            return Level.Value * 5;
        }

        public static void GeneratePowerUp(GameObject gameObject,bool genTreasureChest)
        {
            if (genTreasureChest)
            {
                PowerUpManager.Default.TreasureChest
                    .Instantiate()
                    .Position(gameObject.Position())
                    .Show();
                return;
            }
            var percent = Random.Range(0, 1f);

            if (percent < ExpPercent.Value + AdditionalExpPercent.Value)
            {
                // 掉落经验值;
                PowerUpManager.Default.Exp.Instantiate()
                    .Position(gameObject.Position())
                    .Show();

                return;
            }

            percent = Random.Range(0, 1f);

            if (percent < CoinPercent.Value)
            {
                PowerUpManager.Default.Coin.Instantiate()
                    .Position(gameObject.Position())
                    .Show();

                return;
            }

            percent = Random.Range(0, 1f);

            if (percent < 0.1f && !Object.FindObjectOfType<HP>())
            {
                PowerUpManager.Default.HP.Instantiate()
                    .Position(gameObject.Position())
                    .Show();

                return;
            }

            if (BombUnlocked.Value && !Object.FindObjectOfType<Bomb>())
            {
                percent = Random.Range(0, 1f);

                if (percent < BombPercent.Value)
                {
                    PowerUpManager.Default.Bomb.Instantiate()
                        .Position(gameObject.Position())
                        .Show();

                    return;
                }
            }

            percent = Random.Range(0, 1f);

            if (percent < 0.1f && !Object.FindObjectOfType<GetAllExp>())
            {
                PowerUpManager.Default.GetAllExp.Instantiate()
                    .Position(gameObject.Position())
                    .Show();

                return;
            }
        }

        protected override void Init()
        {
            // 注册模块的操作
            // XXX Model
            this.RegisterSystem(new SaveSystem());
            this.RegisterSystem(new CoinUpgradeSystem());
            this.RegisterSystem(new ExpUpgradeSystem());
            this.RegisterSystem(new AchievementSystem());
        }
    }
}