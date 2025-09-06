/****************************************************************************
 * 2023.9 LIANGXIEWIN
 ****************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using QFramework;
using UnityEngine.U2D;
using Random = UnityEngine.Random;

namespace ProjectSurvivor
{
	public partial class TreasureChestPanel : UIElement,IController
	{
		ResLoader mResLoader = ResLoader.Allocate();

		private void Awake()
		{
			BtnSure.onClick.AddListener(() =>
			{
				Time.timeScale = 1.0f;
				this.Hide();
			});
		}

		private void OnEnable()
		{
			// 1. 判断是否有匹配的 没合成的 
			// 2. 判断是否有没升完级的
			var expUpgradeSystem = this.GetSystem<ExpUpgradeSystem>();

			var matchedPairedItems = expUpgradeSystem.Items.Where(item =>
			{
				if (item.CurrentLevel.Value >= 7)
				// if (item.CurrentLevel.Value >= 1 && item.PairedName.IsNotNullAndEmpty())
				{
					var containsInPair = expUpgradeSystem.Pairs.ContainsKey(item.Key);
					var pairedItemKey = expUpgradeSystem.Pairs[item.Key];
					var pairedItemStartUpgrade = expUpgradeSystem.Dictionary[pairedItemKey].CurrentLevel.Value > 0;
					var pairedUnlocked = expUpgradeSystem.PairedProperties[item.Key].Value;

					return containsInPair && pairedItemStartUpgrade && !pairedUnlocked;
				}

				return false;
			});

			if (matchedPairedItems.Any())
			{
				var item = matchedPairedItems.ToList().GetRandomItem();
				Content.text = "<b>" + item.PairedName + "</b>\n" + item.PairedDescription;

				while (!item.UpgradeFinish)
				{
					item.Upgrade();
				}

				Icon.sprite = mResLoader.LoadSync<SpriteAtlas>("icon")
					.GetSprite(item.PairedIconName);
				Icon.Show();
				
				expUpgradeSystem.PairedProperties[item.Key].Value = true;
			}
			else
			{
				var upgradeItems = expUpgradeSystem.Items.Where(item => item.CurrentLevel.Value > 0 && !item.UpgradeFinish).ToList();

				if (upgradeItems.Any())
				{
					var item = upgradeItems.GetRandomItem();
					Content.text = item.Description;
					
					Icon.sprite = mResLoader.LoadSync<SpriteAtlas>("icon")
						.GetSprite(item.IconName);
					Icon.Show();
					
					item.Upgrade();
				}
				else
				{
					if (Global.HP.Value < Global.MaxHP.Value)
					{
						if (Random.Range(0, 1.0f) < 0.2f)
						{
							Content.text = "恢复 1 血量";
							AudioKit.PlaySound("HP");
							Global.HP.Value++;
							Icon.Hide();
							return;
						}
					}
                    
					Content.text = "增加 50 金币";
					Global.Coin.Value += 50;
					Icon.Hide();
				}
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