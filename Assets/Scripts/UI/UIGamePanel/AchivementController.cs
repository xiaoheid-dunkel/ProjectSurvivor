/****************************************************************************
 * 2023.9 LIANGXIEWIN
 ****************************************************************************/

using QFramework;
using UnityEngine;
using UnityEngine.U2D;

namespace ProjectSurvivor
{
	public partial class AchivementController : UIElement
	{
		ResLoader mResLoader = ResLoader.Allocate();

		private void Awake()
		{
			var originLocalPosY = AchivementItem.LocalPositionY();

			var iconAtlas = mResLoader.LoadSync<SpriteAtlas>("icon");
			AchievementSystem.OnAchievementUnlocked.Register(item =>
			{
				 Title.text = $"<b>成就 {item.Name} 达成!</b>";
				 Description.text = item.Description;
				 var sprite = iconAtlas.GetSprite(item.IconName);
				 Icon.sprite = sprite;
				 AchivementItem.Show();

				 AchivementItem.LocalPositionY(-200);

				 AudioKit.PlaySound("Achievement");
				 
				 ActionKit.Sequence()
					 .Lerp(-200, originLocalPosY, 0.3f, (y) => AchivementItem.LocalPositionY(y))
					 .Delay(2)
					 .Lerp(originLocalPosY, -200, 0.3f, (y) => AchivementItem.LocalPositionY(y), () =>
					 {
						 AchivementItem.Hide();
					 })
					 .Start(this);

			}).UnRegisterWhenGameObjectDestroyed(gameObject);
		}

		protected override void OnBeforeDestroy()
		{
			mResLoader.Recycle2Cache();
			mResLoader = null;
		}
	}
}