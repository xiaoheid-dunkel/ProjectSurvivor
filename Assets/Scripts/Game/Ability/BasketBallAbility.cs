using System.Collections.Generic;
using UnityEngine;
using QFramework;

namespace ProjectSurvivor
{
	public partial class BasketBallAbility : ViewController
	{
		private List<Ball> mBalls = new List<Ball>();

		void Start()
		{
			void CreateBall()
			{
				mBalls.Add(Ball.Instantiate()
					.SyncPosition2DFrom(this)
					.Show());
			}

			void CreateBalls()
			{
				var ballCount2Create =
					Global.BasketBallCount.Value + Global.AdditionalFlyThingCount.Value - mBalls.Count;

				for (var i = 0; i < ballCount2Create; i++)
				{
					CreateBall();
				}
			}
			
			Global.BasketBallCount.Or(Global.AdditionalFlyThingCount)
				.Register(CreateBalls)
				.UnRegisterWhenGameObjectDestroyed(gameObject);
			
			CreateBalls();
		}
	}
}
