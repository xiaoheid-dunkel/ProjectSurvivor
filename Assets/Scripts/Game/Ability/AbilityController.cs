using System.Linq;
using UnityEngine;
using QFramework;

namespace ProjectSurvivor
{
    // Partial class declaration, inheriting from ViewController and IController interface
    public partial class AbilityController : ViewController,IController
	{
		void Start()
		{
            // Code Here
            // Register simple sword unlock event and show simple sword when unlocked
            Global.SimpleSwordUnlocked.RegisterWithInitValue(unlocked =>
			{
				if (unlocked)
				{
					SimpleSword.Show();
				}

			}).UnRegisterWhenGameObjectDestroyed(gameObject);
			
			Global.SimpleKnifeUnlocked.RegisterWithInitValue(unlocked =>
			{
				if (unlocked)
				{
					SimpleKnife.Show();
				}

			}).UnRegisterWhenGameObjectDestroyed(gameObject);
			
			Global.RotateSwordUnlocked.RegisterWithInitValue(unlocked =>
			{
				if (unlocked)
				{
					RotateSword.Show();
				}

			}).UnRegisterWhenGameObjectDestroyed(gameObject);
			
			Global.BasketBallUnlocked.RegisterWithInitValue(unlocked =>
			{
				if (unlocked)
				{
					BasketBallAbility.Show();
				}

			}).UnRegisterWhenGameObjectDestroyed(gameObject);


            // 随机升级一个
            // Get all weapon-type upgrade items from the experience upgrade system
            this.GetSystem<ExpUpgradeSystem>().Items.Where(item=>item.IsWeapon)
				.ToList()
				.GetRandomItem().Upgrade();

			Global.SuperBomb.RegisterWithInitValue(unlocked =>
			{
				if (unlocked)
				{
					SuperBomb.Show();
				}
			}).UnRegisterWhenGameObjectDestroyed(gameObject);
		}

		public IArchitecture GetArchitecture()
		{
			return Global.Interface;
		}
	}
}
