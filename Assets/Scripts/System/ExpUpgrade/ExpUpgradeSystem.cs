using System.Collections.Generic;
using System.Linq;
using QFramework;
using UnityEngine;

namespace ProjectSurvivor
{
    public class ExpUpgradeSystem : AbstractSystem
    {
        public List<ExpUpgradeItem> Items { get; } = new List<ExpUpgradeItem>();
        public static bool AllUnlockedFinish = false;
        
        public static void CheckAllUnlockedFinish()
        {
            AllUnlockedFinish = Global.Interface.GetSystem<ExpUpgradeSystem>().Items
                .All(i => i.UpgradeFinish);
        }

        public Dictionary<string, ExpUpgradeItem> Dictionary = new();
        
        public Dictionary<string, string> Pairs = new Dictionary<string, string>()
        {
            { "simple_sword", "simple_critical" },
            { "simple_bomb", "simple_fly_count" },
            { "simple_knife", "damage_rate" },
            { "basket_ball", "movement_speed_rate" },
            { "rotate_sword", "simple_exp" },
            
            { "simple_critical", "simple_sword" },
            { "simple_fly_count", "simple_bomb" },
            { "damage_rate", "simple_knife" },
            { "movement_speed_rate", "basket_ball" },
            { "simple_exp", "rotate_sword" },
        };
        
        public Dictionary<string, BindableProperty<bool>> PairedProperties =
            new()
            {
                { "simple_sword", Global.SuperSword },
                { "simple_bomb", Global.SuperBomb },
                { "simple_knife", Global.SuperKnife },
                { "basket_ball", Global.SuperBasketBall },
                { "rotate_sword", Global.SuperRotateSword },

                // simple_exp
                // simple_collectable_area
            };
        

        public ExpUpgradeItem Add(ExpUpgradeItem item)
        {
            Items.Add(item);
            return item;
        }

        protected override void OnInit()
        {
            Debug.Log("OnInit");
            ResetData();

            Global.Level.Register(_ =>
            {
                Debug.Log("Level Up");
                Roll();
            });
        }

        public void ResetData()
        {
            Items.Clear();

            Add(new ExpUpgradeItem(true)
                .WithKey("simple_sword")
                .WithName("剑")
                .WithIconName("simple_sword_icon")
                .WithPairedName("合成后的剑")
                .WithPairedIconName("paired_simple_sword_icon")
                .WithPairedDescription("攻击力翻倍 攻击范围翻倍")
                .WithMaxLevel(10)
                .WithDescription(lv =>
                {
                    return lv switch
                    {
                        1 => $"剑Lv{lv}:攻击身边的敌人",
                        2 => $"剑Lv{lv}:\n攻击力+3 数量+2",
                        3 => $"剑Lv{lv}:\n攻击力+2 间隔-0.25s",
                        4 => $"剑Lv{lv}:\n攻击力+2 间隔-0.25s",
                        5 => $"剑Lv{lv}:\n攻击力+3 数量+2",
                        6 => $"剑Lv{lv}:\n范围+1 间隔-0.25s",
                        7 => $"剑Lv{lv}:\n攻击力+3 数量+2",
                        8 => $"剑Lv{lv}:\n攻击力+2 范围+1",
                        9 => $"剑Lv{lv}:\n攻击力+3 间隔-0.25s",
                        10 => $"剑Lv{lv}:\n攻击力+3 数量+2",
                        _ => null
                    };
                })
                .OnUpgrade((_, level) =>
                {
                    switch (level)
                    {
                        case 1:
                            Global.SimpleSwordUnlocked.Value = true;
                            break;
                        case 2:
                            Global.SimpleAbilityDamage.Value += 3;
                            Global.SimpleSwordCount.Value += 2;
                            break;
                        case 3:
                            Global.SimpleAbilityDamage.Value += 2;
                            Global.SimpleAbilityDuration.Value -= 0.25f;
                            break;
                        case 4:
                            Global.SimpleAbilityDamage.Value += 2;
                            Global.SimpleAbilityDuration.Value -= 0.25f;
                            break;
                        case 5:
                            Global.SimpleAbilityDamage.Value += 3;
                            Global.SimpleSwordCount.Value += 2;
                            break;
                        case 6:
                            Global.SimpleSwordRange.Value++;
                            Global.SimpleAbilityDuration.Value -= 0.25f;
                            break;
                        case 7:
                            Global.SimpleAbilityDamage.Value += 3;
                            Global.SimpleSwordCount.Value += 2;
                            break;
                        case 8:
                            Global.SimpleAbilityDamage.Value += 2;
                            Global.SimpleSwordRange.Value++;
                            break;
                        case 9:
                            Global.SimpleAbilityDamage.Value += 3;
                            Global.SimpleAbilityDuration.Value -= 0.25f;
                            break;
                        case 10:
                            Global.SimpleAbilityDamage.Value += 3;
                            Global.SimpleSwordCount.Value += 2;
                            break;
                    }
                })
            );

            Add(new ExpUpgradeItem(true)
                .WithKey("rotate_sword")
                .WithName("守卫剑")
                .WithIconName("rotate_sword_icon")
                .WithPairedName("合成后的守卫剑")
                .WithPairedIconName("paired_rotate_sword_icon")
                .WithPairedDescription("攻击力翻倍 旋转速度翻倍")
                .WithMaxLevel(10)
                .WithDescription(lv =>
                {
                    return lv switch
                    {
                        1 => $"守卫剑Lv{lv}: \n环绕主角身边的剑",
                        2 => $"守卫剑Lv{lv}: \n数量 + 1 攻击力 + 1",
                        3 => $"守卫剑Lv{lv}: \n攻击力 + 2 速度+25%",
                        4 => $"守卫剑Lv{lv}: \n速度+50%",
                        5 => $"守卫剑Lv{lv}: \n数量 + 1 攻击力 + 1",
                        6 => $"守卫剑Lv{lv}: \n攻击力 + 2 速度+25%",
                        7 => $"守卫剑Lv{lv}: \n数量 + 1 攻击力 + 1",
                        8 => $"守卫剑Lv{lv}: \n攻击力 + 2 速度+25%",
                        9 => $"守卫剑Lv{lv}: \n数量 + 1 攻击力 + 1",
                        10 => $"守卫剑Lv{lv}: \n攻击力 + 2 速度+25%",
                        _ => null
                    };
                })
                .OnUpgrade((_, level) =>
                {
                    switch (level)
                    {
                        case 1:
                            Global.RotateSwordUnlocked.Value = true;
                            break;
                        case 2:
                            Global.RotateSwordCount.Value++;
                            Global.RotateSwordDamage.Value++;
                            break;
                        case 3:
                            Global.RotateSwordDamage.Value++;
                            Global.RotateSwordDamage.Value++;
                            Global.RotateSwordSpeed.Value *= 1.25f;
                            break;
                        case 4:
                            Global.RotateSwordSpeed.Value *= 1.5f;
                            break;
                        case 5:
                            Global.RotateSwordCount.Value++;
                            Global.RotateSwordDamage.Value++;
                            break;
                        case 6:
                            Global.RotateSwordDamage.Value++;
                            Global.RotateSwordDamage.Value++;
                            Global.RotateSwordSpeed.Value *= 1.25f;
                            break;
                        case 7:
                            Global.RotateSwordCount.Value++;
                            Global.RotateSwordDamage.Value++;
                            break;
                        case 8:
                            Global.RotateSwordDamage.Value++;
                            Global.RotateSwordDamage.Value++;
                            Global.RotateSwordSpeed.Value *= 1.25f;
                            break;
                        case 9:
                            Global.RotateSwordCount.Value++;
                            Global.RotateSwordDamage.Value++;
                            break;
                        case 10:
                            Global.RotateSwordDamage.Value++;
                            Global.RotateSwordDamage.Value++;
                            Global.RotateSwordSpeed.Value *= 1.25f;
                            break;
                    }
                })
            );

            Add(new ExpUpgradeItem(false)
                .WithKey("simple_bomb")
                .WithName("炸弹")
                .WithIconName("bomb_icon")
                .WithPairedName("合成后的炸弹")
                .WithPairedIconName("paired_bomb_icon")
                .WithPairedDescription("每隔 15 秒爆炸一次")
                .WithMaxLevel(10)
                .WithDescription(lv =>
                {
                    return lv switch
                    {
                        1 => $"炸弹Lv{lv}:\n攻击全部敌人(怪物掉落)",
                        2 => $"炸弹Lv{lv}:\n掉落概率+5% 攻击力+5",
                        3 => $"炸弹Lv{lv}:\n掉落概率+5% 攻击力+5",
                        4 => $"炸弹Lv{lv}:\n掉落概率+5% 攻击力+5",
                        5 => $"炸弹Lv{lv}:\n掉落概率+5% 攻击力+5",
                        6 => $"炸弹Lv{lv}:\n掉落概率+5% 攻击力+5",
                        7 => $"炸弹Lv{lv}:\n掉落概率+5% 攻击力+5",
                        8 => $"炸弹Lv{lv}:\n掉落概率+5% 攻击力+5",
                        9 => $"炸弹Lv{lv}:\n掉落概率+5% 攻击力+5",
                        10 => $"炸弹Lv{lv}:\n掉落概率+10% 攻击力+5",
                        _ => null
                    };
                })
                .OnUpgrade((_, level) =>
                {
                    switch (level)
                    {
                        case 1:
                            Global.BombUnlocked.Value = true;
                            break;
                        case 2:
                            Global.BombDamage.Value += 5;
                            Global.BombPercent.Value += 0.05f;
                            break;
                        case 3:
                            Global.BombDamage.Value += 5;
                            Global.BombPercent.Value += 0.05f;
                            break;
                        case 4:
                            Global.BombDamage.Value += 5;
                            Global.BombPercent.Value += 0.05f;
                            break;
                        case 5:
                            Global.BombDamage.Value += 5;
                            Global.BombPercent.Value += 0.05f;
                            break;
                        case 6:
                            Global.BombDamage.Value += 5;
                            Global.BombPercent.Value += 0.05f;
                            break;
                        case 7:
                            Global.BombDamage.Value += 5;
                            Global.BombPercent.Value += 0.05f;
                            break;
                        case 8:
                            Global.BombDamage.Value += 5;
                            Global.BombPercent.Value += 0.05f;
                            break;
                        case 9:
                            Global.BombDamage.Value += 5;
                            Global.BombPercent.Value += 0.05f;
                            break;
                        case 10:
                            Global.BombDamage.Value += 5;
                            Global.BombPercent.Value += 0.1f;
                            break;
                    }
                })
            );

            Add(new ExpUpgradeItem(true)
                .WithKey("simple_knife")
                .WithName("飞刀")
                .WithIconName("simple_knife_icon")
                .WithPairedName("合成后的飞刀")
                .WithPairedIconName("paired_simple_knife_icon")
                .WithPairedDescription("攻击力翻倍")
                .WithMaxLevel(10)
                .WithDescription(lv =>
                {
                    return lv switch
                    {
                        1 => $"飞刀Lv{lv}:\n向最近的敌人发射一把飞到",
                        2 => $"飞刀Lv{lv}:\n攻击力+3 数量+2",
                        3 => $"飞刀Lv{lv}:\n间隔-0.1s 攻击力+1 数量+1",
                        4 => $"飞刀Lv{lv}:\n间隔-0.1s 穿透+1 数量+1",
                        5 => $"飞刀Lv{lv}:\n攻击力+3 数量+1",
                        6 => $"飞刀Lv{lv}:\n间隔-0.1s 数量+1",
                        7 => $"飞刀Lv{lv}:\n间隔-0.1s 穿透+1 数量+1",
                        8 => $"飞刀Lv{lv}:\n攻击力+3 数量+1",
                        9 => $"飞刀Lv{lv}:\n间隔-0.1s 数量+1",
                        10 => $"飞刀Lv{lv}:\n攻击力+3 数量+1",
                        _ => null
                    };
                })
                .OnUpgrade((_, level) =>
                {
                    switch (level)
                    {
                        case 1:
                            Global.SimpleKnifeUnlocked.Value = true;
                            break;
                        case 2:
                            Global.SimpleKnifeDamage.Value += 3;
                            Global.SimpleKnifeCount.Value += 2;
                            break;
                        case 3:
                            Global.SimpleKnifeDuration.Value -= 0.1f;
                            Global.SimpleKnifeDamage.Value += 2;
                            Global.SimpleKnifeCount.Value++;
                            break;
                        case 4:
                            Global.SimpleKnifeDuration.Value -= 0.1f;
                            Global.SimpleKnifeAttackCount.Value++;
                            Global.SimpleKnifeCount.Value++;
                            break;
                        case 5:
                            Global.SimpleKnifeDamage.Value += 3;
                            Global.SimpleKnifeCount.Value++;
                            break;
                        case 6:
                            Global.SimpleKnifeDuration.Value -= 0.1f;
                            Global.SimpleKnifeCount.Value++;
                            break;
                        case 7:
                            Global.SimpleKnifeDuration.Value -= 0.1f;
                            Global.SimpleKnifeAttackCount.Value++;
                            Global.SimpleKnifeCount.Value++;
                            break;
                        case 8:
                            Global.SimpleKnifeDamage.Value += 3;
                            Global.SimpleKnifeCount.Value++;
                            break;
                        case 9:
                            Global.SimpleKnifeDuration.Value -= 0.1f;
                            Global.SimpleKnifeCount.Value++;
                            break;
                        case 10:
                            Global.SimpleKnifeDamage.Value += 3;
                            Global.SimpleKnifeCount.Value++;
                            break;
                    }
                })
            );


            Add(new ExpUpgradeItem(true)
                .WithKey("basket_ball")
                .WithName("篮球")
                .WithIconName("ball_icon")
                .WithPairedName("合成后的篮球")
                .WithPairedIconName("paired_ball_icon")
                .WithPairedDescription("攻击力翻倍 体积变大")
                .WithMaxLevel(10)
                .WithDescription(lv =>
                {
                    return lv switch
                    {
                        1 => $"篮球Lv{lv}:\n在屏幕内反弹的篮球",
                        2 => $"篮球Lv{lv}:\n攻击力+3",
                        3 => $"篮球Lv{lv}:\n数量+1",
                        4 => $"篮球Lv{lv}:\n攻击力+3",
                        5 => $"篮球Lv{lv}:\n数量+1",
                        6 => $"篮球Lv{lv}:\n攻击力+3",
                        7 => $"篮球Lv{lv}:\n速度+20%",
                        8 => $"篮球Lv{lv}:\n攻击力+3",
                        9 => $"篮球Lv{lv}:\n速度+20%",
                        10 => $"篮球Lv{lv}:\n数量+1",
                        _ => null
                    };
                })
                .WithMaxLevel(10)
                .OnUpgrade((_, level) =>
                {
                    switch (level)
                    {
                        case 1:
                            Global.BasketBallUnlocked.Value = true;
                            break;
                        case 2:
                            Global.BasketBallDamage.Value += 3;
                            break;
                        case 3:
                            Global.BasketBallCount.Value++;
                            break;
                        case 4:
                            Global.BasketBallDamage.Value += 3;
                            break;
                        case 5:
                            Global.BasketBallCount.Value++;
                            break;
                        case 6:
                            Global.BasketBallDamage.Value += 3;
                            break;
                        case 7:
                            Global.BasketBallSpeed.Value *= 1.2f;
                            break;
                        case 8:
                            Global.BasketBallDamage.Value += 3;
                            break;
                        case 9:
                            Global.BasketBallSpeed.Value *= 1.2f;
                            break;
                        case 10:
                            Global.BasketBallCount.Value++;
                            break;
                    }
                })
            );


            Add(new ExpUpgradeItem(false)
                .WithKey("simple_critical")
                .WithName("暴击")
                .WithIconName("critical_icon")
                .WithMaxLevel(5)
                .WithDescription(lv =>
                {
                    return lv switch
                    {
                        1 => $"暴击Lv{lv}:\n每次伤害 15% 概率暴击",
                        2 => $"暴击Lv{lv}:\n每次伤害 28% 概率暴击",
                        3 => $"暴击Lv{lv}:\n每次伤害 43% 概率暴击",
                        4 => $"暴击Lv{lv}:\n每次伤害 50% 概率暴击",
                        5 => $"暴击Lv{lv}:\n每次伤害 80% 概率暴击",
                        _ => null
                    };
                })
                .OnUpgrade((_, lv) =>
                {
                    switch (lv)
                    {
                        case 1:
                            Global.CriticalRate.Value = 0.15f;
                            break;
                        case 2:
                            Global.CriticalRate.Value = 0.28f;
                            break;
                        case 3:
                            Global.CriticalRate.Value = 0.43f;
                            break;
                        case 4:
                            Global.CriticalRate.Value = 0.5f;
                            break;
                        case 5:
                            Global.CriticalRate.Value = 0.8f;
                            break;
                    }
                }));
     
            Add(new ExpUpgradeItem(false)
                .WithKey("damage_rate")
                .WithName("伤害率")
                .WithIconName("damage_icon")
                .WithMaxLevel(5)
                .WithDescription(lv =>
                {
                    return lv switch
                    {
                        1 => $"伤害率Lv{lv}:\n增加 20% 额外伤害",
                        2 => $"伤害率Lv{lv}:\n增加 40% 额外伤害",
                        3 => $"伤害率Lv{lv}:\n增加 60% 额外伤害",
                        4 => $"伤害率Lv{lv}:\n增加 80% 额外伤害",
                        5 => $"伤害率Lv{lv}:\n增加 100% 额外伤害",

                        _ => null
                    };
                })
                .OnUpgrade((_, lv) =>
                {
                    switch (lv)
                    {
                        case 1:
                            Global.DamageRate.Value = 1.2f;
                            break;
                        case 2:
                            Global.DamageRate.Value = 1.4f;
                            break;
                        case 3:
                            Global.DamageRate.Value = 1.6f;
                            break;
                        case 4:
                            Global.DamageRate.Value = 1.8f;
                            break;
                        case 5:
                            Global.DamageRate.Value = 2f;
                            break;
                    }
                }));
  
            Add(new ExpUpgradeItem(false)
                .WithKey("simple_fly_count")
                .WithIconName("fly_icon")
                .WithName("飞射物")
                .WithMaxLevel(3)
                .WithDescription(lv =>
                {
                    return lv switch
                    {
                        1 => $"飞射物Lv{lv}:\n额外增加 1 个飞射物",
                        2 => $"飞射物Lv{lv}:\n额外增加 2 个飞射物",
                        3 => $"飞射物Lv{lv}:\n额外增加 3 个飞射物",
                        _ => null
                    };
                })
                .OnUpgrade((_, lv) =>
                {
                    switch (lv)
                    {
                        case 1:
                            Global.AdditionalFlyThingCount.Value++;
                            break;
                        case 2:
                            Global.AdditionalFlyThingCount.Value++;
                            break;
                        case 3:
                            Global.AdditionalFlyThingCount.Value++;
                            break;
                    }
                }));

            Add(new ExpUpgradeItem(false)
                .WithKey("movement_speed_rate")
                .WithName("移动速度")
                .WithIconName("movement_icon")
                .WithMaxLevel(5)
                .WithDescription(lv =>
                {
                    return lv switch
                    {
                        1 => $"移动速度Lv{lv}:\n增加 25% 移动速度",
                        2 => $"移动速度Lv{lv}:\n增加 50% 移动速度",
                        3 => $"移动速度Lv{lv}:\n增加 75% 移动速度",
                        4 => $"移动速度Lv{lv}:\n增加 100% 移动速度",
                        5 => $"移动速度Lv{lv}:\n增加 150% 移动速度",

                        _ => null
                    };
                })
                .OnUpgrade((_, lv) =>
                {
                    switch (lv)
                    {
                        case 1:
                            Global.MovementSpeedRate.Value = 1.25f;
                            break;
                        case 2:
                            Global.MovementSpeedRate.Value = 1.5f;
                            break;
                        case 3:
                            Global.MovementSpeedRate.Value = 1.75f;
                            break;
                        case 4:
                            Global.MovementSpeedRate.Value = 2f;
                            break;
                        case 5:
                            Global.MovementSpeedRate.Value = 2.5f;
                            break;
                    }
                }));

            Add(new ExpUpgradeItem(false)
                .WithKey("simple_collectable_area")
                .WithName("拾取范围")
                .WithIconName("collectable_icon")
                .WithMaxLevel(3)
                .WithDescription(lv =>
                {
                    return lv switch
                    {
                        1 => $"拾取范围Lv{lv}:\n额外增加 100% 范围",
                        2 => $"拾取范围Lv{lv}:\n额外增加 200% 范围",
                        3 => $"拾取范围Lv{lv}:\n额外增加 300% 范围",
                        _ => null
                    };
                })
                .OnUpgrade((_, lv) =>
                {
                    switch (lv)
                    {
                        case 1:
                            Global.CollectableArea.Value = 2f;
                            break;
                        case 2:
                            Global.CollectableArea.Value = 3f;
                            break;
                        case 3:
                            Global.CollectableArea.Value = 4f;
                            break;
                    }
                }));

            Add(new ExpUpgradeItem(false)
                .WithKey("simple_exp")
                .WithName("经验值")
                .WithIconName("exp_icon")
                .WithMaxLevel(5)
                .WithDescription(lv =>
                {
                    return lv switch
                    {
                        1 => $"经验值Lv{lv}:\n额外增加 5% 掉落概率",
                        2 => $"经验值Lv{lv}:\n额外增加 8% 掉落概率",
                        3 => $"经验值Lv{lv}:\n额外增加 12% 掉落概率",
                        4 => $"经验值Lv{lv}:\n额外增加 17% 掉落概率",
                        5 => $"经验值Lv{lv}:\n额外增加 25% 掉落概率",
                        _ => null
                    };
                })
                .OnUpgrade((_, lv) =>
                {
                    switch (lv)
                    {
                        case 1:
                            Global.AdditionalExpPercent.Value = 0.05f;
                            break;
                        case 2:
                            Global.AdditionalExpPercent.Value = 0.08f;
                            break;
                        case 3:
                            Global.AdditionalExpPercent.Value = 0.12f;
                            break;
                        case 4:
                            Global.AdditionalExpPercent.Value = 0.17f;
                            break;
                        case 5:
                            Global.AdditionalExpPercent.Value = 0.25f;
                            break;
                    }
                }));

            Dictionary = Items.ToDictionary(i => i.Key);
        }

        public void Roll()
        {
            foreach (var expUpgradeItem in Items)
            {
                expUpgradeItem.Visible.Value = false;
            }

            var list = Items.Where(item => !item.UpgradeFinish).ToList();

            if (list.Count >= 4)
            {
                list.GetAndRemoveRandomItem().Visible.Value = true;
                list.GetAndRemoveRandomItem().Visible.Value = true;
                list.GetAndRemoveRandomItem().Visible.Value = true;
                list.GetAndRemoveRandomItem().Visible.Value = true;
            }
            else
            {
                foreach (var item in list)
                {
                    item.Visible.Value = true;
                }
            }
        }
    }
}