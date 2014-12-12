using UnityEngine;
using System.Collections;

public class PlayerInfo : MonoBehaviour 
{
	void Awake()
	{
		G.i._mStageSetting += StageSetting;
	}


	// Use this for initialization
	void Start () 
	{
		G.i.PInfo = GetComponent<PlayerInfo>();
	}



	void Destory()
	{
		G.i.PInfo = null;

		if(G.i.StageM != null)
		{
			G.i.StageM.RemoveGameInitEvent(GameInit);
		}
	}



	public void StageSetting()
	{
		if(G.i.StageM != null)
		{
			G.i.StageM.AddGameInitEvent(GameInit);
		}
	}



	public void GameInit()
	{
		_mLife = PlayerLife;
	}



	public void SubtractionLife(int SubValue)
	{
		_mLife -= SubValue;

		if(_mLife < 0) G.i.StageM.GameOver();
	}


	public int PlayerLife = 0;
	private int _mLife = 0; 
}
