/****************************************************************************
 * 2023.9 LIANGXIEWIN
 ****************************************************************************/

using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace ProjectSurvivor
{
	public partial class ExpUpgradePanel
	{
		[SerializeField] public UnityEngine.UI.Button BtnExpUpgradeItemTemplate;
		[SerializeField] public UnityEngine.UI.Image Icon;
		[SerializeField] public RectTransform UpgradeRoot;

		public void Clear()
		{
			BtnExpUpgradeItemTemplate = null;
			Icon = null;
			UpgradeRoot = null;
		}

		public override string ComponentName
		{
			get { return "ExpUpgradePanel";}
		}
	}
}
