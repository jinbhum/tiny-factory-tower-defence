using UnityEngine;
using System.Collections;

public class GlobalManager : MonoBehaviour 
{
	public StageManger StageM
	{
		set { _mStageM = value; }
		get { return _mStageM;}
	}


	private StageManger _mStageM = null;
	private InputController _mIController = null;
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
