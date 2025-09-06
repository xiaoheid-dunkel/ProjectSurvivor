using System;
using UnityEngine;
using QFramework;

namespace ProjectSurvivor
{
	public partial class FxController : ViewController
	{

		private static FxController mDefault;

		private void Awake()
		{
			mDefault = this;
		}

		private void OnDestroy()
		{
			mDefault = null;
		}
		//Play special efect
		public static void Play(SpriteRenderer sprite, Color dissolveColor)
		{
			mDefault.EnemyDieFx.Instantiate()
				.Position(sprite.Position())
				.LocalScale(sprite.Scale())
				.Self(s =>
				{
					s.GetComponent<Dissolve>().DissolveColor = dissolveColor;
					s.sprite = sprite.sprite;
				})
				.Show();
		}
	}
}
