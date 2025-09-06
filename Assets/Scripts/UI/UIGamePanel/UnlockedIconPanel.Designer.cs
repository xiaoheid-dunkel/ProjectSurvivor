/****************************************************************************
 * 2023.9 LIANGXIEWIN
 ****************************************************************************/

using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace ProjectSurvivor
{
	public partial class UnlockedIconPanel
	{
		[SerializeField] public UnityEngine.UI.Image UnlockedIconTemplate;
		[SerializeField] public RectTransform UnlockedIconRoot;

		public void Clear()
		{
			UnlockedIconTemplate = null;
			UnlockedIconRoot = null;
		}

		public override string ComponentName
		{
			get { return "UnlockedIconPanel";}
		}
	}
}
