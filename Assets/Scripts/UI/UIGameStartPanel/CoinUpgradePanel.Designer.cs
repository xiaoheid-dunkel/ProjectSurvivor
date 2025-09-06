/****************************************************************************
 * 2023.9 LIANGXIEWIN
 ****************************************************************************/

using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace ProjectSurvivor
{
	public partial class CoinUpgradePanel
	{
		[SerializeField] public UnityEngine.UI.Button BtnClose;
		[SerializeField] public UnityEngine.UI.Text CoinText;
		[SerializeField] public RectTransform CoinUpgradeItemRoot;
		[SerializeField] public UnityEngine.UI.Button CoinUpgradeItemTemplate;

		public void Clear()
		{
			BtnClose = null;
			CoinText = null;
			CoinUpgradeItemRoot = null;
			CoinUpgradeItemTemplate = null;
		}

		public override string ComponentName
		{
			get { return "CoinUpgradePanel";}
		}
	}
}
