using UnityEngine;
using System.Collections;

public class MapLoader : MonoBehaviour 
{
	// Use this for initialization
	void Start () 
	{
		if(G.i != null)G.i.MapL = GetComponent<MapLoader>();
	}



	void OnDestroy()
	{
		if(G.i != null)G.i.MapL = null;
	}



	void OnGUI()
	{
		if(isDebug)
		{
			DebugMenu();
		}
	}



	private void DebugMenu()
	{
		Rect AreaInfo = new Rect(Screen.width - 200.0f, 0, 200.0f, 70.0f);

		GUILayout.BeginArea(AreaInfo);
		{
			InputNumber();

			GUILayout.BeginHorizontal();
			{
				ExcuteButton("SetMap", SetMap);
				ExcuteButton("ResetMap", ResetMap);
			}
			GUILayout.EndHorizontal();
		}
		GUILayout.EndArea();
	}



	private void InputNumber()
	{
		GUILayout.BeginHorizontal();
		{
			GUILayout.Label("MapNumber : ", GUILayout.Width(80.0f));
			MapNumber = GUILayout.TextField(MapNumber, GUILayout.Width(100.0f));
		}
		GUILayout.EndHorizontal();
	}



	private void SetMap()
	{
		string MapName = string.Format("{0:0000}", int.Parse(MapNumber));
		GameObject Map = G.i.ResourceM.Load(MapPath + MapName);

		if(Map != null)
		{
			Map.transform.localScale = Vector3.one;
			Map.transform.localPosition = Vector3.zero;
			Map.transform.localRotation = Quaternion.Euler(Vector3.zero);

			Map.transform.parent = transform;
		}
	}



	private void ResetMap()
	{
		foreach(StageManger manger in GetComponentsInChildren<StageManger>())
		{
			GameObject.Destroy(manger.gameObject);
		}
	}



	private void ExcuteButton(string name, VoidFuntion function)
	{
		if(GUILayout.Button(name))
		{
			if(function != null)function();
		}
	}



	public bool isDebug = false;

	private string MapNumber = "";
	private const string MapPath = "Game/Prefab/Maps/Stage_";

	public delegate void VoidFuntion();
}
