/****************************************************************************
 * 2023.9 LIANGXIEWIN
 ****************************************************************************/

using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace ProjectSurvivor
{
	public partial class TreasureChestPanel
	{
		[SerializeField] public UnityEngine.UI.Image Icon;
		[SerializeField] public UnityEngine.UI.Button BtnSure;
		[SerializeField] public UnityEngine.UI.Text Content;

		public void Clear()
		{
			Icon = null;
			BtnSure = null;
			Content = null;
		}

		public override string ComponentName
		{
			get { return "TreasureChestPanel";}
		}
	}
}
