using UnityEngine;
using System.Collections;

public class TempUIController : MonoBehaviour 
{
	void Awake()
	{
		G.i._mStageSetting += StageSetting;
	}



	void OnDestroy()
	{
		if(G.i.StageM != null)
		{
			G.i.StageM.RemoveGameClaerEvent(SetGameClearUI);
			G.i.StageM.RemoveGameOverEvent(SetGameOverUI);
		}
	}



	public void StageSetting()
	{
		if(G.i.StageM != null)
		{
			G.i.StageM.AddGameClearEvent(SetGameClearUI);
			G.i.StageM.AddGameOverEvent(SetGameOverUI);
		}
	}



	void OnGUI()
	{	
		switch(curState)
		{
			case ButtonState.READY:
				StartUI();
			break;
			case ButtonState.GAME:
				TowerUIMenu();
			break;
			case ButtonState.CLEAR:
				GameClaerUIMenu();
			break;
			case ButtonState.FAIL:
				GameOverUIMenu();
			break;
		}
	}



	private void SetGameClearUI()
	{
		curState = ButtonState.CLEAR;
	}



	private void SetGameOverUI()
	{
		curState = ButtonState.FAIL;
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
		GUILayout.BeginArea(new Rect(0.0f, 0.0f, 300.0f, 200.0f));
		{
			ExcuteButton("Tower_1", Tower_1, CreateTower);
		}
		GUILayout.EndArea();
	}


	private void GameClaerUIMenu()
	{
		float ButtonW = 100.0f;
		float ButtonH = 50.0f;
		float ButtonGap = 10.0f;

		float LabelW = 300.0f;
		float LabelH = 70.0f;
		float LableGap = 15.0f;

		float ScreenWHalf = Screen.width * 0.5f;
		float ScreenHHalf = Screen.height * 0.5f;

		Rect LabelPos = new Rect(ScreenWHalf, ScreenHHalf - LabelH*0.5f - LableGap, LabelW, LabelH);
		Rect ContinuePos = new Rect(ScreenWHalf - ButtonW*0.5f - ButtonGap, ScreenHHalf - ButtonH*0.5f, ButtonW, ButtonH);
		Rect EndsPos = new Rect(ScreenWHalf + ButtonW*0.5f + ButtonGap, ScreenHHalf - ButtonH*0.5f, ButtonW, ButtonH);

		MessageLabel("Mission Clear, Congratulation!!", LabelPos);
		ExcuteButton("Continue", ContinuePos, GameContinue);
		ExcuteButton("End", EndsPos, GameEnd);
	}



	private void GameOverUIMenu()
	{
		float ButtonW = 100.0f;
		float ButtonH = 50.0f;
		float ButtonGap = 10.0f;

		float LabelW = 300.0f;
		float LabelH = 70.0f;
		float LableGap = 15.0f;
		
		float ScreenWHalf = Screen.width * 0.5f;
		float ScreenHHalf = Screen.height * 0.5f;

		Rect LabelPos = new Rect(ScreenWHalf, ScreenHHalf - LabelH*0.5f - LableGap, LabelW, LabelH);
		Rect ContinuePos = new Rect(ScreenWHalf - ButtonW*0.5f - ButtonGap, ScreenHHalf - ButtonH*0.5f, ButtonW, ButtonH);
		Rect EndsPos = new Rect(ScreenWHalf + ButtonW*0.5f + ButtonGap, ScreenHHalf - ButtonH*0.5f, ButtonW, ButtonH);

		MessageLabel("Mission Fail, Retry?", LabelPos);
		ExcuteButton("Continue", ContinuePos, GameContinue);
		ExcuteButton("End", EndsPos, GameEnd);
	}



	private void CreateTower(GameObject Tower)
	{
		G.i.StageM.CreateTower(Tower);
	}



    private void GameStart()
    {
		curState = ButtonState.GAME;
		G.i.StageM.GameInit();
        G.i.StageM.CreateMonster(Monster);
    }



	private void GameContinue()
	{
		GameStart();
	}



	private void GameEnd()
	{
		curState = ButtonState.READY;
	}



	private void MessageLabel(string Msg)
	{
		GUILayout.Label(Msg);
	}



	private void MessageLabel(string Msg, Rect posInfo)
	{
		GUI.Label(posInfo, Msg);
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

			curState = ButtonState.GAME;
        }
    }



	public delegate void CreteFunction(GameObject Target);
    public delegate void VoidFuntion();

    public GameObject Monster = null;
	public GameObject Tower_1 = null;

	private ButtonState curState = ButtonState.READY;

	private enum ButtonState
	{
		READY = 0,
		GAME = 1,
		CLEAR = 2,
		FAIL = 3,
	}
}
