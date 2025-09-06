/****************************************************************************
 * 2023.9 LIANGXIEWIN
 ****************************************************************************/

using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace ProjectSurvivor
{
	public partial class AchivementPanel
	{
		[SerializeField] public UnityEngine.UI.Button BtnClose;
		[SerializeField] public UnityEngine.UI.Button AchivementItemTemplate;
		[SerializeField] public RectTransform AchivementItemRoot;

		public void Clear()
		{
			BtnClose = null;
			AchivementItemTemplate = null;
			AchivementItemRoot = null;
		}

		public override string ComponentName
		{
			get { return "AchivementPanel";}
		}
	}
}
