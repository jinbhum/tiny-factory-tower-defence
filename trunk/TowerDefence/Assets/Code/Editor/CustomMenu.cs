﻿using UnityEngine;
using UnityEditor;
using System.Collections;

public class CustomMenu : MonoBehaviour 
{
	[MenuItem ("Custom/TileWizard")]
	static void TileCreate()
	{
		EditorWindow.GetWindow(typeof(CustomMapTileWindow), true, "TileWizard");
	}
}