using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

public class CustomMapTileWindow : EditorWindow 
{
	void OnGUI()
	{
		EditorGUILayout.BeginVertical();
		{
			WidthCount = CustomEditorForms.InputIntegerForm("WidthCount", WidthCount, 100.0f);
			HeightCount = CustomEditorForms.InputIntegerForm("HeightCount", HeightCount, 100.0f);
			TileSize = CustomEditorForms.InputIntegerForm("TileSize", TileSize, 100.0f); 

			isShowTileLayer = CustomEditorForms.CheckBoxForm("ShowTileLayer(Checked : ImpossibleTile)", isShowTileLayer, 400.0f);

			if(isShowTileLayer)
			{
				ShowTileLayer();
			}

			CustomEditorForms.ExcuteButton("Create", 100.0f, ExcuteCreateTiles);
		}
		EditorGUILayout.EndVertical();
	}



	private void ShowTileLayer()
	{
		if(BeforeWCount != WidthCount || BeforeHCount != HeightCount)
		{
			TileLayersData = new bool[WidthCount, HeightCount];

			BeforeWCount = WidthCount;
			BeforeHCount = HeightCount;
		}

		if(WidthCount > 0 && HeightCount > 0)
		{
			for(int hIndex = 0; hIndex < HeightCount; hIndex++)
			{
				EditorGUILayout.BeginHorizontal();

				for(int wIndex = 0; wIndex < WidthCount; wIndex++)
				{
					TileLayersData[wIndex, hIndex] = CustomEditorForms.CheckBoxForm("", TileLayersData[wIndex, hIndex], 15.0f);
				}

				EditorGUILayout.EndHorizontal();
			}
		}
	}



	private void ExcuteCreateTiles()
	{
		List<Transform> SelectList = new List<Transform>(Selection.transforms);

		foreach(Transform Select in SelectList)
		{
			CreateTiles(Select);
		}

		Debug.Log("Tile Create Complite");
	}



	private void CreateTiles(Transform parent)
	{
		if(parent.FindChild(TileParentName) != null) GameObject.DestroyImmediate(parent.FindChild(TileParentName).gameObject);

		GameObject TileParent = new GameObject();
		GameObject AvailableTileParent = new GameObject();
		GameObject ImpossibleTileParent = new GameObject();

		TileParent.name = TileParentName;
		AvailableTileParent.name = AvailableTileParentName;
		ImpossibleTileParent.name = ImpossibleTileParentName;

		TileParent.transform.parent = parent;
		AvailableTileParent.transform.parent = TileParent.transform;
		ImpossibleTileParent.transform.parent = TileParent.transform;

		TileParent.transform.localPosition = Vector3.zero;
		AvailableTileParent.transform.localPosition = Vector3.zero;
		ImpossibleTileParent.transform.localPosition = Vector3.zero;

		TileParent.transform.localScale = Vector3.one;
		AvailableTileParent.transform.localScale = Vector3.one;
		ImpossibleTileParent.transform.localScale = Vector3.one;

		for(int wIndex = 0; wIndex < WidthCount; wIndex++)
		{
			for(int hIndex = 0; hIndex < HeightCount; hIndex++)
			{
				SetTile(wIndex, hIndex, AvailableTileParent.transform, ImpossibleTileParent.transform);
			}
		}

		if(WidthCount%2 == 0)TileParent.transform.localPosition += Vector3.left*TileSize*0.5f;
		if(HeightCount%2 == 0)TileParent.transform.localPosition += Vector3.back*TileSize*0.5f;
	}



	private void SetTile(int wIndex, int hIndex, Transform AvailableParent, Transform ImpossibleParent)
	{
		GameObject Tile = new GameObject();
		BoxCollider TileCollider = Tile.AddComponent<BoxCollider>();

		Tile.name = "MapTile(" + wIndex + "," + hIndex + ")";

		if(TileLayersData[wIndex, hIndex])
		{
			Tile.transform.parent = ImpossibleParent;
			Tile.layer = LayerMask.NameToLayer(ImpossibleTileLayerName);
		}
		else
		{
			Tile.transform.parent = AvailableParent;
			Tile.layer = LayerMask.NameToLayer(AvailableTileLayerName);
		}

		TileCollider.size = new Vector3(TileSize, TileThickness, TileSize);
		Tile.transform.localScale = Vector3.one;

		Tile.transform.localPosition = new Vector3((WidthCount/2 - wIndex)*TileSize, TileThickness, (HeightCount/2 - hIndex)*TileSize);
	}


	private int WidthCount = 0;
	private int HeightCount = 0;

	private int BeforeWCount = 0;
	private int BeforeHCount = 0;

	private int TileSize = 0;
	
	private bool[,] TileLayersData = null;
	private bool isShowTileLayer = false;

	private const float TileThickness = 0.001f;
	private const string TileParentName = "MapTile";

	private const string AvailableTileLayerName = "AvailableTile";
	private const string ImpossibleTileLayerName = "ImpossibleTile";

	private const string TileParent = "TileParent";
	private const string AvailableTileParentName = "AvailableTileParent";
	private const string ImpossibleTileParentName = "ImpossibleTileParent";
}
