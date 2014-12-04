using UnityEngine;
using System.Collections;

public class TempUIController : MonoBehaviour 
{
	void OnGUI()
	{
		CommnoUIMenu();
        StartUI();
	}


	private void CommnoUIMenu()
	{
		GUILayout.BeginArea(new Rect(0.0f, 0.0f, 300.0f, 200.0f));
		{
			ExcuteButton("Tower_1", Tower_1, CreateTower);
		}
		GUILayout.EndArea();
	}

    private void StartUI()
    {
        float ButtonW = 200.0f;
        float ButtonH = 200.0f;

        Rect pos = new Rect(Screen.width * 0.5f - ButtonW * 0.5f, Screen.height * 0.5f - ButtonH * 0.5f, ButtonW, ButtonH);

        ExcuteButton("StartButton", pos, GameStart);

 //       GUILayout.BeginArea(new Rect(Screen.width * 0.5f - ButtonW * 0.5f, Screen.height * 0.5f - ButtonH * 0.5f, ButtonW, ButtonH));

 ////       GUILayout.BeginArea(new Rect(Screen.width / 2, Screen.height / 2, 200.0f, 200.0f));

 //       //GUILayout.BeginArea(new Rect(0.0f, 30.0f, 300.0f, 200.0f));
 //       {
 //           StartButton("Start");
 //       }
 //       GUILayout.EndArea();
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


    private void GameStart()
    {
        G.i.StageM.CreateMonster(Monster);
    }



	private void ExcuteButton(string name, GameObject tower, CreteFunction function)
	{
		if(GUILayout.Button(name))
		{
			if(function != null)function(tower);
		}
	}


    private void ExcuteButton(string name, Rect posInfo, VoidFuntion function)
    {
        if (GUI.Button(posInfo, name))
        {
            if (function != null) function();
        }
    }



    private void StartButton(string name)
    {
        if (GUILayout.Button(name))
        {
            Debug.Log("Hello ~ !");
            G.i.StageM.CreateMonster(Monster);
        }
    }


	public delegate void CreteFunction(GameObject Target);
    public delegate void VoidFuntion();

    public GameObject Monster = null;
	public GameObject Tower_1 = null;
}
