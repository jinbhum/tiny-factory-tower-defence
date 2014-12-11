using UnityEngine;
using System.Collections;

public class GlobalManager : MonoBehaviour 
{
	public PlayerInfo PInfo
	{
		set { _mPlayerInfo = value; }
		get { return _mPlayerInfo; }
	}

	
	public StageManger StageM
	{
		set { _mStageM = value; }
		get { return _mStageM;}
	}


	private PlayerInfo _mPlayerInfo = null;
	private StageManger _mStageM = null;
	private InputController _mIController = null;



	// event

	public void StageSetting()
	{
		if(_mStageSetting != null)_mStageSetting();
	}


	public delegate void VoidFuntion();
	public event VoidFuntion _mStageSetting;
}




public class G
{
	public static GlobalManager i
	{
		get
		{
			if(instance == null) 
			{
				GameObject global = GameObject.Find("Global");
				if(global != null)instance = global.GetComponent<GlobalManager>();
			}
			return instance;
		}
	}
	
	private static GlobalManager instance;
}
