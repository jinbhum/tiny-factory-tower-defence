using UnityEngine;
using UnityEditor;
using System.Collections;

public class TowerMenu : EditorWindow
{
	[MenuItem ("Tower/Create Tower")]
	static void CreateTower()
	{
		TowerCreatePopup _popup = EditorWindow.GetWindow<TowerCreatePopup>("Create Tower");
	}
}
