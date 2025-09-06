using System;
using UnityEngine;
using QFramework;

namespace ProjectSurvivor
{
	public partial class SuperBomb : ViewController
	{
		private float mCurrentSeconds = 0;


		private void Update()
		{
			mCurrentSeconds += Time.deltaTime;
			if (mCurrentSeconds >= 15)
			{
				mCurrentSeconds = 0;
				Bomb.Execute();
			}
		}
	}
}
