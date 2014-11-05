using UnityEngine;
using System.Collections;

public class TempUIController : MonoBehaviour 
{
	void OnGUI()
	{
		CommnoUIMenu();
	}



	private void CommnoUIMenu()
	{
		GUILayout.BeginArea(new Rect(0.0f, 0.0f, 300.0f, 200.0f));
		{
			ExcuteButton("Tower_1", Tower_1, CreateTower);
		}
		GUILayout.EndArea();
	}



	private void TowerUIMenu()
	{

	}



	private void TileUIMenu()
	{

	}



	private void CreateTower(GameObject Tower)
	{
		G.i.StageM.CreateTower(Tower);
	}



	private void ExcuteButton(string name, GameObject tower, CreteFunction function)
	{
		if(GUILayout.Button(name))
		{
			if(function != null)function(tower);
		}
	}



	public delegate void CreteFunction(GameObject Target);

	public GameObject Tower_1 = null;
}
