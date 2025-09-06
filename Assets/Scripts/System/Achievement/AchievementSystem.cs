using System.Collections.Generic;
using System.Linq;
using QFramework;
using UnityEngine;

namespace ProjectSurvivor
{
    public class AchievementSystem : AbstractSystem
    {
        public AchievementItem Add(AchievementItem item)
        {
            Items.Add(item);
            return item;
        }
        
        protected override void OnInit()
        {
            var saveSystem = this.GetSystem<SaveSystem>();
            
            Add(new AchievementItem()
                    .WithKey("3_minutes")
                    .WithName("坚持三分钟")
                    .WithDescription("坚持 3 分钟\n奖励 1000 金币")
                    .WithIconName("achievement_time_icon")
                    .Condition(() => Global.CurrentSeconds.Value >= 60 * 3)
                    // .Condition(() => Global.CurrentSeconds.Value >= 10)
                    .OnUnlocked(_ => { Global.Coin.Value += 1000; }))
                .Load(saveSystem);
            
            Add(new AchievementItem()
                    .WithKey("5_minutes")
                    .WithName("坚持五分钟")
                    .WithDescription("坚持 5 分钟\n奖励 1000 金币")
                    .WithIconName("achievement_time_icon")
                    .Condition(() => Global.CurrentSeconds.Value >= 60 * 5)
                    .OnUnlocked(_ => { Global.Coin.Value += 1000; }))
                .Load(saveSystem);
            
            Add(new AchievementItem()
                    .WithKey("10_minutes")
                    .WithName("坚持十分钟")
                    .WithDescription("坚持 10 分钟\n奖励 1000 金币")
                    .WithIconName("achievement_time_icon")
                    .Condition(() => Global.CurrentSeconds.Value >= 60 * 10)
                    .OnUnlocked(_ => { Global.Coin.Value += 1000; }))
                .Load(saveSystem);
            
            Add(new AchievementItem()
                    .WithKey("15_minutes")
                    .WithName("坚持 15 分钟")
                    .WithDescription("坚持 10 分钟\n奖励 1000 金币")
                    .WithIconName("achievement_time_icon")
                    .Condition(() => Global.CurrentSeconds.Value >= 60 * 15)
                    .OnUnlocked(_ => { Global.Coin.Value += 1000; }))
                .Load(saveSystem);
            
            Add(new AchievementItem()
                    .WithKey("lv30")
                    .WithName("30 级")
                    .WithDescription("第一次升级到 30 级\n奖励 1000 金币")
                    .WithIconName("achievement_level_icon")
                    .Condition(() => Global.Level.Value >= 30)
                    .OnUnlocked(_ => { Global.Coin.Value += 1000; }))
                .Load(saveSystem);
            
            Add(new AchievementItem()
                    .WithKey("lv50")
                    .WithName("50 级")
                    .WithDescription("第一次升级到 50 级\n奖励 1000 金币")
                    .WithIconName("achievement_level_icon")
                    .Condition(() => Global.Level.Value >= 50)
                    .OnUnlocked(_ => { Global.Coin.Value += 1000; }))
                .Load(saveSystem);
            
            Add(new AchievementItem()
                    .WithKey("first_time_paired_ball")
                    .WithName("合成后的篮球")
                    .WithDescription("第一次解锁合成后的篮球\n奖励 1000 金币")
                    .WithIconName("paired_ball_icon")
                    .Condition(() => Global.SuperBasketBall.Value)
                    .OnUnlocked(_ => { Global.Coin.Value += 1000; }))
                .Load(saveSystem);
            
            Add(new AchievementItem()
                    .WithKey("first_time_paired_bomb")
                    .WithName("合成后的炸弹")
                    .WithDescription("第一次解锁合成后的炸弹\n奖励 1000 金币")
                    .WithIconName("paired_bomb_icon")
                    .Condition(() => Global.SuperBomb.Value)
                    .OnUnlocked(_ => { Global.Coin.Value += 1000; }))
                .Load(saveSystem);
            
            Add(new AchievementItem()
                    .WithKey("first_time_paired_sword")
                    .WithName("合成后的剑")
                    .WithDescription("第一次解锁合成后的剑\n奖励 1000 金币")
                    .WithIconName("paired_simple_sword_icon")
                    .Condition(() => Global.SuperSword.Value)
                    .OnUnlocked(_ => { Global.Coin.Value += 1000; }))
                .Load(saveSystem);
            
            Add(new AchievementItem()
                    .WithKey("first_time_paired_knife")
                    .WithName("合成后的飞刀")
                    .WithDescription("第一次解锁合成后的飞刀\n奖励 1000 金币")
                    .WithIconName("paired_simple_knife_icon")
                    .Condition(() => Global.SuperKnife.Value)
                    .OnUnlocked(_ => { Global.Coin.Value += 1000; }))
                .Load(saveSystem);
            
            Add(new AchievementItem()
                    .WithKey("first_time_paired_circle")
                    .WithName("合成后的守卫剑")
                    .WithDescription("第一次解锁合成后的守卫剑\n奖励 1000 金币")
                    .WithIconName("paired_rotate_sword_icon")
                    .Condition(() => Global.SuperRotateSword.Value)
                    .OnUnlocked(_ => { Global.Coin.Value += 1000; }))
                .Load(saveSystem);
            
            Add(new AchievementItem()
                    .WithKey("first_time_paired_circle")
                    .WithName("全部能力升级")
                    .WithDescription("全部能力升级完成\n奖励 1000 金币")
                    .WithIconName("achievement_all_icon")
                    .Condition(() => ExpUpgradeSystem.AllUnlockedFinish)
                    .OnUnlocked(_ => { Global.Coin.Value += 1000; }))
                .Load(saveSystem);
            
            ActionKit.OnUpdate.Register(() =>
            {
                if (Time.frameCount % 10 == 0)
                {
                    foreach (var achievementItem in Items.Where(achievementItem =>
                                 !achievementItem.Unlocked && achievementItem.ConditionCheck()))
                    {
                        achievementItem.Unlock(saveSystem);
                    }
                }
            });
        }
        public List<AchievementItem> Items = new List<AchievementItem>();

        public static EasyEvent<AchievementItem> OnAchievementUnlocked = new EasyEvent<AchievementItem>();
    }
}