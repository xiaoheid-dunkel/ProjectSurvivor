/****************************************************************************
 * 2023.7 LIANGXIEWIN
 ****************************************************************************/

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using QFramework;
using UnityEngine.U2D;

namespace ProjectSurvivor
{
	public partial class ExpUpgradePanel : UIElement,IController
	{
		private ResLoader mResLoader;
		private void Awake()
		{
			mResLoader = ResLoader.Allocate();
			var iconAtlas = mResLoader.LoadSync<SpriteAtlas>("icon");
			var expUpgradeSystem = this.GetSystem<ExpUpgradeSystem>();

			foreach (var expUpgradeItem in expUpgradeSystem.Items)
			{
				BtnExpUpgradeItemTemplate.InstantiateWithParent(UpgradeRoot)
					.Self(self =>
					{
						var itemCache = expUpgradeItem;

						self.transform.Find("Icon").GetComponent<Image>().sprite =
							iconAtlas.GetSprite(itemCache.IconName);
						
						self.onClick.AddListener(() =>
						{
							Time.timeScale = 1.0f;
							itemCache.Upgrade();
							this.Hide();
							AudioKit.PlaySound("AbilityLevelUp");
						});
						var selfCache = self;
						itemCache.Visible.RegisterWithInitValue(visible =>
						{
							if (visible)
							{
								self.GetComponentInChildren<Text>().text =
									expUpgradeItem.Description;
								selfCache.Show();
								var pairedUpgradeName = selfCache.transform.Find("PairedUpgradeName");
								if (expUpgradeSystem.Pairs.TryGetValue(itemCache.Key, out var pairedName))
								{
									var pairedItem = expUpgradeSystem.Dictionary[pairedName];
									if (pairedItem.CurrentLevel.Value > 0 && itemCache.CurrentLevel.Value == 0)
									{
										pairedUpgradeName.Find("Icon").GetComponent<Image>().sprite =
											iconAtlas.GetSprite(pairedItem.IconName);
										pairedUpgradeName.Show();
									}
									else
									{
										pairedUpgradeName.Hide();
									}
								}
								else
								{
									pairedUpgradeName.Hide();
								}
							}
							else
							{
								selfCache.Hide();
							}

						}).UnRegisterWhenGameObjectDestroyed(selfCache);

						itemCache.CurrentLevel.RegisterWithInitValue(lv =>
						{
							selfCache.GetComponentInChildren<Text>().text =
								itemCache.Description;
						}).UnRegisterWhenGameObjectDestroyed(gameObject);
					});
			}
			
		}

		protected override void OnBeforeDestroy()
		{
			mResLoader.Recycle2Cache();
			mResLoader = null;
		}

		public IArchitecture GetArchitecture()
		{
			return Global.Interface;
		}
	}
}