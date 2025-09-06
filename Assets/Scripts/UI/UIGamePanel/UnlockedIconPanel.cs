/****************************************************************************
 * 2023.9 LIANGXIEWIN
 ****************************************************************************/

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using QFramework;
using UnityEngine.U2D;

namespace ProjectSurvivor
{
    public partial class UnlockedIconPanel : UIElement, IController
    {
        private Dictionary<string, System.Tuple<ExpUpgradeItem, Image>> mUnlockedKeys =
            new Dictionary<string, System.Tuple<ExpUpgradeItem, Image>>();

        ResLoader mResLoader = ResLoader.Allocate();

        private void Awake()
        {
            UnlockedIconTemplate.Hide();

            var iconAtlas = mResLoader.LoadSync<SpriteAtlas>("Icon");

            foreach (var expUpgradeItem in this.GetSystem<ExpUpgradeSystem>().Items)
            {
                var cachedItem = expUpgradeItem;
                expUpgradeItem.CurrentLevel.RegisterWithInitValue(level =>
                {
                    if (level > 0)
                    {
                        if (mUnlockedKeys.ContainsKey(cachedItem.Key))
                        {
                        }
                        else
                        {
                            UnlockedIconTemplate.InstantiateWithParent(UnlockedIconRoot)
                                .Self(self =>
                                {
                                    self.sprite = iconAtlas.GetSprite(cachedItem.IconName);
                                    mUnlockedKeys.Add(cachedItem.Key,
                                        new System.Tuple<ExpUpgradeItem, Image>(cachedItem, self));
                                })
                                .Show();
                        }
                    }
                }).UnRegisterWhenGameObjectDestroyed(gameObject);
            }

            Global.SuperKnife.Register(unlocked =>
            {
                if (unlocked)
                {
                    if (mUnlockedKeys.ContainsKey("simple_knife"))
                    {
                        var item = mUnlockedKeys["simple_knife"].Item1;
                        var sprite = iconAtlas.GetSprite(item.PairedIconName);
                        mUnlockedKeys["simple_knife"].Item2.sprite = sprite;
                    }
                }
            }).UnRegisterWhenGameObjectDestroyed(gameObject);
            
            Global.SuperRotateSword.Register(unlocked =>
            {
                if (unlocked)
                {
                    if (mUnlockedKeys.ContainsKey("rotate_sword"))
                    {
                        var item = mUnlockedKeys["rotate_sword"].Item1;
                        var sprite = iconAtlas.GetSprite(item.PairedIconName);
                        mUnlockedKeys["rotate_sword"].Item2.sprite = sprite;
                    }
                }
            }).UnRegisterWhenGameObjectDestroyed(gameObject);
            
            Global.SuperBasketBall.Register(unlocked =>
            {
                if (unlocked)
                {
                    if (mUnlockedKeys.ContainsKey("basket_ball"))
                    {
                        var item = mUnlockedKeys["basket_ball"].Item1;
                        var sprite = iconAtlas.GetSprite(item.PairedIconName);
                        mUnlockedKeys["basket_ball"].Item2.sprite = sprite;
                    }
                }
            }).UnRegisterWhenGameObjectDestroyed(gameObject);
            
            Global.SuperBomb.Register(unlocked =>
            {
                if (unlocked)
                {
                    if (mUnlockedKeys.ContainsKey("simple_bomb"))
                    {
                        var item = mUnlockedKeys["simple_bomb"].Item1;
                        var sprite = iconAtlas.GetSprite(item.PairedIconName);
                        mUnlockedKeys["simple_bomb"].Item2.sprite = sprite;
                    }
                }
            }).UnRegisterWhenGameObjectDestroyed(gameObject);
            
            Global.SuperSword.Register(unlocked =>
            {
                if (unlocked)
                {
                    if (mUnlockedKeys.ContainsKey("simple_sword"))
                    {
                        var item = mUnlockedKeys["simple_sword"].Item1;
                        var sprite = iconAtlas.GetSprite(item.PairedIconName);
                        mUnlockedKeys["simple_sword"].Item2.sprite = sprite;
                    }
                }
            }).UnRegisterWhenGameObjectDestroyed(gameObject);
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