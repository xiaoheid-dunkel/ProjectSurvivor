using QAssetBundle;
using QFramework;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ProjectSurvivor
{
	public class UIGameStartPanelData : UIPanelData
	{
	}
	
	public partial class UIGameStartPanel : UIPanel,IController
	{
		protected override void OnInit(IUIData uiData = null)
		{
			mData = uiData as UIGameStartPanelData ?? new UIGameStartPanelData();
			// please add init code here


			Time.timeScale = 1.0f;
			BtnStartGame.onClick.AddListener(() =>
			{
				AudioKit.PlaySound(Sfx.BUTTONCLICK);
				Global.ResetData();
				this.CloseSelf();
				SceneManager.LoadScene("Game");
			});
			
			BtnCoinUpgrade.onClick.AddListener(() =>
			{
				AudioKit.PlaySound(Sfx.BUTTONCLICK);
				CoinUpgradePanel.Show();
			});

			BtnAchivement.onClick.AddListener(() =>
			{
				AudioKit.PlaySound(Sfx.BUTTONCLICK);
				AchivementPanel.Show();
			});
			
			
			this.GetSystem<CoinUpgradeSystem>().Say();
			
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

		public IArchitecture GetArchitecture()
		{
			return Global.Interface;
		}
	}
}
