/****************************************************************************
 * 2023.7 LIANGXIEWIN
 ****************************************************************************/

using System.Linq;
using QAssetBundle;
using UnityEngine.UI;
using QFramework;

namespace ProjectSurvivor
{
	public partial class CoinUpgradePanel : UIElement,IController
	{
		private void Awake()
		{
			CoinUpgradeItemTemplate.Hide();

			Global.Coin.RegisterWithInitValue(coin =>
			{

				CoinText.text = "金币:" + coin;

			}).UnRegisterWhenGameObjectDestroyed(gameObject);
			
			
			foreach (var coinUpgradeItem in this.GetSystem<CoinUpgradeSystem>().Items.Where(item=>!item.UpgradeFinish))
			{
				CoinUpgradeItemTemplate.InstantiateWithParent(CoinUpgradeItemRoot)
					.Self(self =>
					{
						var itemCache = coinUpgradeItem;
						self.GetComponentInChildren<Text>().text =
							coinUpgradeItem.Description + $" {coinUpgradeItem.Price} 金币";
						self.onClick.AddListener(() =>
						{
							itemCache.Upgrade();
							AudioKit.PlaySound("AbilityLevelUp");
						});
						var selfCache = self;

						coinUpgradeItem.OnChanged.Register(() =>
						{
							if (itemCache.ConditionCheck())
							{
								selfCache.Show();
							}
							else
							{
								selfCache.Hide();
							}

						}).UnRegisterWhenGameObjectDestroyed(selfCache);

						if (itemCache.ConditionCheck())
						{
							selfCache.Show();
						}
						else
						{
							selfCache.Hide();
						}

						Global.Coin.RegisterWithInitValue(coin =>
						{
							if (coin >= itemCache.Price)
							{
								selfCache.interactable = true;
							}
							else
							{
								selfCache.interactable = false;
							}

						}).UnRegisterWhenGameObjectDestroyed(self);
					});
			}


			
			BtnClose.onClick.AddListener(() =>
			{
				AudioKit.PlaySound(Sfx.BUTTONCLICK);
				this.Hide();
			});
		}

		protected override void OnBeforeDestroy()
		{
		}

		public IArchitecture GetArchitecture()
		{
			return Global.Interface;
		}
	}
}