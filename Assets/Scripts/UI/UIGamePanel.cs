using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace ProjectSurvivor
{
    public class UIGamePanelData : UIPanelData
    {
    }

    public partial class UIGamePanel : UIPanel
    {
        public static EasyEvent FlashScreen = new EasyEvent();

        public static EasyEvent OpenTreasurePanel = new EasyEvent();

        protected override void OnInit(IUIData uiData = null)
        {
            mData = uiData as UIGamePanelData ?? new UIGamePanelData();
            // please add init code here

            EnemyGenerator.EnemyCount.RegisterWithInitValue(enemyCount => 
            { EnemyCountText.text = "敌人:" + enemyCount; })
                .UnRegisterWhenGameObjectDestroyed(gameObject);

            Global.CurrentSeconds.RegisterWithInitValue(currentSeconds =>
            {
                if (Time.frameCount % 30 == 0)
                {
                    var currentSecondsInt = Mathf.FloorToInt(currentSeconds);
                    var seconds = currentSecondsInt % 60;
                    var minutes = currentSecondsInt / 60;
                    TimeText.text = "时间:" + $"{minutes:00}:{seconds:00}";
                }
            }).UnRegisterWhenGameObjectDestroyed(gameObject);

            Global.Exp.RegisterWithInitValue(exp => 
            { ExpValue.fillAmount = exp / (float)Global.ExpToNextLevel(); })
                .UnRegisterWhenGameObjectDestroyed(gameObject);

            Global.Level.RegisterWithInitValue(lv => { LevelText.text = "等级:" + lv; })
                .UnRegisterWhenGameObjectDestroyed(gameObject);

            ExpUpgradePanel.Hide();
            Global.Level.Register(lv =>
            {
                Time.timeScale = 0;
                ExpUpgradePanel.Show();
                AudioKit.PlaySound("LevelUp");
            }).UnRegisterWhenGameObjectDestroyed(gameObject);

            Global.Exp.RegisterWithInitValue(exp =>
            {
                if (exp >= Global.ExpToNextLevel())
                {
                    Global.Exp.Value -= Global.ExpToNextLevel();
                    Global.Level.Value++;
                }
            }).UnRegisterWhenGameObjectDestroyed(gameObject);


            var enemyGenerator = FindObjectOfType<EnemyGenerator>();
            ActionKit.OnUpdate.Register(() =>
            {
                Global.CurrentSeconds.Value += Time.deltaTime;

                if (enemyGenerator.LastWave && enemyGenerator.CurrentWave == null &&
                    EnemyGenerator.EnemyCount.Value == 0)
                {
                    this.CloseSelf();
                    UIKit.OpenPanel<UIGamePassPanel>();
                }
            }).UnRegisterWhenGameObjectDestroyed(gameObject);


            Global.Coin.RegisterWithInitValue(coin => { CoinText.text = "金币:" + coin; })
                .UnRegisterWhenGameObjectDestroyed(gameObject);

            FlashScreen.Register(() =>
            {
                ActionKit
                    .Sequence()
                    .Lerp(0, 0.5f, 0.1f,
                        alpha => ScreenColor.ColorAlpha(alpha))
                    .Lerp(0.5f, 0, 0.2f,
                        alpha => ScreenColor.ColorAlpha(alpha),
                        () => ScreenColor.ColorAlpha(0))
                    .Start(this);
            }).UnRegisterWhenGameObjectDestroyed(gameObject);

            OpenTreasurePanel.Register(() =>
            {
                Time.timeScale = 0.0f;
                TreasureChestPanel.Show();
            }).UnRegisterWhenGameObjectDestroyed(gameObject);
        }

        protected override void OnOpen(IUIData uiData = null)
        {
        }

        protected override void OnShow()
        {
        }

        protected override void OnHide()
        {
        }

        protected override void OnClose()
        {
        }
    }
}