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

			CustomEditorForms.ExcuteButton("Create", 100.0f, ExcuteCreateTiles);
		}
		EditorGUILayout.EndVertical();
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

		TileParent.name = TileParentName;
		TileParent.transform.parent = parent;

		TileParent.transform.localPosition = Vector3.zero;
		TileParent.transform.localScale = Vector3.one;

		for(int wIndex = 0; wIndex < WidthCount; wIndex++)
		{
			for(int hIndex = 0; hIndex < HeightCount; hIndex++)
			{
				SetTile(wIndex, hIndex, TileParent.transform);
			}
		}

		if(WidthCount%2 == 0)TileParent.transform.localPosition += Vector3.left*TileSize*0.5f;
		if(HeightCount%2 == 0)TileParent.transform.localPosition += Vector3.back*TileSize*0.5f;
	}



	private void SetTile(int wIndex, int hIndex, Transform parent)
	{
		GameObject Tile = new GameObject();
		BoxCollider TileCollider = Tile.AddComponent<BoxCollider>();

		Tile.name = "MapTile(" + wIndex + "," + hIndex + ")";
		Tile.layer = LayerMask.NameToLayer(TileLayerName);
		TileCollider.size = new Vector3(TileSize, TileThickness, TileSize);

		Tile.transform.parent = parent;
		Tile.transform.localScale = Vector3.one;

		Tile.transform.localPosition = new Vector3((WidthCount/2 - wIndex)*TileSize, TileThickness, (HeightCount/2 - hIndex)*TileSize);
	}


	private int WidthCount = 0;
	private int HeightCount = 0;

	private int TileSize = 0;


	private const float TileThickness = 0.001f;
	private const string TileParentName = "MapTile";
	private const string TileLayerName = "MapTile";
}
