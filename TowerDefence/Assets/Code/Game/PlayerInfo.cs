using UnityEngine;
using System.Collections;

public class PlayerInfo : MonoBehaviour 
{

	// Use this for initialization
	void Start () 
	{
		G.i.PInfo = GetComponent<PlayerInfo>();
	}



	void Destory()
	{
		G.i.PInfo = null;
	}



	public void SubtractionLife(int SubValue)
	{
		PlayerLife -= SubValue;

		if(PlayerLife < 0) G.i.StageM.GameOver();
	}


	public int PlayerLife = 0;
}
