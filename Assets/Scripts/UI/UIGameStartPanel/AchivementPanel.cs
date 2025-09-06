/****************************************************************************
 * 2023.9 LIANGXIEWIN
 ****************************************************************************/

using System.Linq;
using QAssetBundle;
using QFramework;
using UnityEngine.U2D;
using UnityEngine.UI;

namespace ProjectSurvivor
{
    public partial class AchivementPanel : UIElement, IController
    {
        ResLoader mResLoader = ResLoader.Allocate();

        private void Awake()
        {
            AchivementItemTemplate.Hide();

            var iconAtlas = mResLoader.LoadSync<SpriteAtlas>("icon");

            foreach (var achievementItem in this.GetSystem<AchievementSystem>().Items
                         .OrderByDescending(item => item.Unlocked))
            {
                AchivementItemTemplate.InstantiateWithParent(AchivementItemRoot)
                    .Self(s =>
                    {
                        s.GetComponentInChildren<Text>().text = "<b>" + achievementItem.Name +
                                                                (achievementItem.Unlocked
                                                                    ? "<color=green>【已完成】</color>"
                                                                    : "") + "</b>\n" +
                                                                achievementItem.Description;
                        var sprite = iconAtlas.GetSprite(achievementItem.IconName);
                        s.transform.Find("Icon").GetComponent<Image>().sprite = sprite;

                    })
                    .Show();
            }
            
            BtnClose.onClick.AddListener(() =>
            {
                AudioKit.PlaySound(Sfx.BUTTONCLICK);
                this.Hide();
            });
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