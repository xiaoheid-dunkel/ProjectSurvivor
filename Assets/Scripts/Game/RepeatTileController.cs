using UnityEngine;
using QFramework;
using UnityEngine.Tilemaps;

namespace ProjectSurvivor
{
	public partial class RepeatTileController : ViewController
	{
		//Editor code (commented out) : Provides a button to recalculate bounds
		// #if UNITY_EDITOR
		// [UnityEditor.CustomEditor(typeof(RepeatTileController))]
		// public class RepeatTileControllerEditor : UnityEditor.Editor
		// {
		// 	public override void OnInspectorGUI()
		// 	{
		// 		base.OnInspectorGUI();
		//
		// 		if (GUILayout.Button("重新计算 Bounds"))
		// 		{
		// 			var controller = target as RepeatTileController;
		// 			controller.Tilemap.CompressBounds();
		// 		}
		// 	}
		// }
		// #endif
		//Tilemaps for eight directions
		private Tilemap mUp;
		private Tilemap mDown;
		private Tilemap mLeft;
		private Tilemap mRight;
		private Tilemap mUpLeft;
		private Tilemap mUpRight;
		private Tilemap mDownLeft;
		private Tilemap mDownRight;
		private Tilemap mCenter;
		//Current area coordinates
		private int AreaX = 0;
		private int AreaY = 0;
		//Create tilemaps for each direction and set parent
		void CreateTileMaps()
		{
			//Instantiate tilemas for each direction and set parent
			mUp = Tilemap.InstantiateWithParent(transform);
			mDown = Tilemap.InstantiateWithParent(transform);
			mLeft = Tilemap.InstantiateWithParent(transform);
			mRight = Tilemap.InstantiateWithParent(transform);
			mUpLeft = Tilemap.InstantiateWithParent(transform);
			mUpRight = Tilemap.InstantiateWithParent(transform);
			mDownLeft = Tilemap.InstantiateWithParent(transform);
			mDownRight = Tilemap.InstantiateWithParent(transform);
			mCenter = Tilemap;
		}
		//Updae positions of all tilemaps
		void UpdatePositions()
		{
			//Set positions for tilemaps in each direction
			mUp.Position(new Vector3(AreaX * Tilemap.size.x, (AreaY + 1) * Tilemap.size.y));
			mDown.Position(new Vector3(AreaX * Tilemap.size.x,(AreaY - 1) * Tilemap.size.y));
			mLeft.Position(new Vector3((AreaX - 1) * Tilemap.size.x, (AreaY + 0) * Tilemap.size.y));
			mRight.Position(new Vector3((AreaX + 1) * Tilemap.size.x, (AreaY + 0) * Tilemap.size.y));
			mUpLeft.Position(new Vector3((AreaX - 1) * Tilemap.size.x, (AreaY + 1) * Tilemap.size.y));
			mUpRight.Position(new Vector3((AreaX + 1) * Tilemap.size.x, (AreaY + 1) * Tilemap.size.y));
			mDownLeft.Position(new Vector3((AreaX - 1) * Tilemap.size.x, (AreaY - 1) * Tilemap.size.y));
			mDownRight.Position(new Vector3((AreaX + 1) * Tilemap.size.x, (AreaY - 1) * Tilemap.size.y));
			mCenter.Position(new Vector3((AreaX + 0) * Tilemap.size.x, (AreaY + 0) * Tilemap.size.y));

		}
		
		void Start()
		{
			CreateTileMaps();
			UpdatePositions();
		}
		
		private void Update()
		{
			if (Player.Default && Time.frameCount % 60 == 0)
			{
				//Convert polayer position to cell coordinates
				var cellPos = Tilemap.layoutGrid.WorldToCell(Player.Default.transform.Position());
				//Calculate area coordinates
				AreaX = cellPos.x / Tilemap.size.x;
				AreaY = cellPos.y / Tilemap.size.y;
				//Update tilemap positions
				UpdatePositions();
			}
		}
	}
}
