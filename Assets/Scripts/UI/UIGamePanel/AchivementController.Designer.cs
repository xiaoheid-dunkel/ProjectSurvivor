/****************************************************************************
 * 2023.9 LIANGXIEWIN
 ****************************************************************************/

using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace ProjectSurvivor
{
	public partial class AchivementController
	{
		[SerializeField] public UnityEngine.UI.Image AchivementItem;
		[SerializeField] public UnityEngine.UI.Text Description;
		[SerializeField] public UnityEngine.UI.Text Title;
		[SerializeField] public UnityEngine.UI.Image Icon;

		public void Clear()
		{
			AchivementItem = null;
			Description = null;
			Title = null;
			Icon = null;
		}

		public override string ComponentName
		{
			get { return "AchivementController";}
		}
	}
}
