using UnityEngine;
using System.Collections;

public class GlobalManager : MonoBehaviour 
{
	public StageManger StageM
	{
		set { _mStageM = value; }
		get { return _mStageM;}
	}



	public ResourcesManager ResourceM
	{
		set { _mResourceM = value; }
		get { return _mResourceM; }
	}



	public PlayerInfo PInfo
	{
		set { _mPlayerInfo = value; }
		get { return _mPlayerInfo; }
	}



	public MapLoader MapL
	{
		set { _mMapLoader = value; }
		get { return _mMapLoader; }
	}


	private StageManger _mStageM = null;
	private ResourcesManager _mResourceM = null;

	private PlayerInfo _mPlayerInfo = null;
	private MapLoader _mMapLoader = null;
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
