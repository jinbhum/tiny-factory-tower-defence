using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StageManger : MonoBehaviour 
{
	// Use this for initialization
	void Start () 
	{
		G.i.StageM = GetComponent<StageManger>();
		G.i.StageSetting();

        AddGameOverEvent(StopCreateMonster);
		AddGameInitEvent(Init);
	}


	void OnDestroy()
	{
		G.i.StageM = null;

		RemoveGameInitEvent(Init);
	}



	private void Init()
	{
		KillCount = 0;
		ArriveCount = 0;

		mUsedTileMap.Clear();
	}



	public void AddGameClearEvent(VoidFuntion ev){	evGameClear += ev;	}
	public void RemoveGameClaerEvent(VoidFuntion ev){	evGameClear -= ev;	}

	public void AddGameOverEvent(VoidFuntion ev){	evGameOver += ev;	}
	public void RemoveGameOverEvent(VoidFuntion ev){	evGameOver -= ev;	}

	public void AddGameInitEvent(VoidFuntion ev){	evGameInit += ev;	}
	public void RemoveGameInitEvent(VoidFuntion ev){	evGameInit -= ev;	}
	


	public void GameInit()
	{
		Debug.Log("GameInit");
		if(evGameInit != null)evGameInit();
	}



	public void GameClear()
	{
		Debug.Log("GameClear");
		if(evGameClear != null)evGameClear();
	}



	public void GameOver()
	{
		Debug.Log("GameOver");
        if (evGameOver != null) evGameOver();

	}



	public void AddKillCount()
	{
		KillCount++;
		CheckGameOver();
	}



	public void AddArriveCount()
	{
		ArriveCount++;
		CheckGameOver();
	}



	public void CheckGameOver()
	{
		if(G.i.PInfo.GetCurrnetLife() < 0)
		{
			GameOver();
		}
		else
		{
			if(KillCount + ArriveCount > MonsterCount)
			{
				GameClear();
			}
		}
	}



	public void CreateTower(GameObject tower)
	{
		if(tower != null)
		{
			StartCoroutine(IECreateTower(tower));
		}
	}



	private IEnumerator IECreateTower(GameObject tower)
	{
		if(CurTarget.gameObject.layer == LayerMask.NameToLayer("AvailableTile"))
		{
			if(!UsedTileMap.ContainsKey(CurTarget.GetInstanceID().ToString()))
			{
				GameObject tower_copy = GameObject.Instantiate(tower) as GameObject;

				tower_copy.name = tower_copy.name.Replace("(Clone)", string.Empty);
				tower_copy.transform.parent = TowerParent;

				tower_copy.GetComponent<Tower>().UsedTile = CurTarget;
				tower_copy.GetComponent<Tower>().Init();

				UsedTileMap.Add(CurTarget.GetInstanceID().ToString(), CurTarget);

				yield return new WaitForSeconds(0.0f);
			}
		}
	}



	public void DeleteTower(GameObject tower)
	{
		if(tower != null)
		{
			StartCoroutine(IEDeleteTower(tower));
		}
	}



	private IEnumerator IEDeleteTower(GameObject tower)
	{
		UsedTileMap.Remove(tower.GetComponent<Tower>().UsedTile.ToString());

		GameObject.DestroyImmediate(tower);
		yield return new WaitForSeconds(0.0f);
	}



	public Dictionary<string, Transform> UsedTileMap
	{
		get{ return mUsedTileMap;}
	}


    public void CreateMonster(GameObject monster)
    {
        if (monster != null)
        {
            StartCoroutine("IECreateMonster", monster);
        }
    }

    public void StopCreateMonster()
    {
        StopCoroutine("IECreateMonster");
    }


    private IEnumerator IECreateMonster(GameObject monster)
    {
        MonsterPos = monster.transform;
        MonsterstartPos = new Vector3(-60, 0, 35);

        for (int i = 0; i <= MonsterCount; i++)
        {
            GameObject obj;
            obj = Instantiate(monster, MonsterstartPos, Quaternion.identity) as GameObject;
            obj.SetActive(true);
            monsterList.Add(obj);
            yield return new WaitForSeconds(2.0f);
        }
    }


    public Transform CurTarget = null;
	public Transform TowerParent = null;

	public BlockState BlockState = null;

    public GameObject MonsterObj = null;
    public Transform MonsterPos;
    public int MonsterCount;

	private int KillCount = 0;
	private int ArriveCount = 0;

    private Vector3 MonsterstartPos;

	private Dictionary<string, Transform> mUsedTileMap = new Dictionary<string, Transform>();

    public List<GameObject> monsterList = new List<GameObject>();


	// event
	public delegate void VoidFuntion();
	public event VoidFuntion evGameClear;
	public event VoidFuntion evGameOver;
	public event VoidFuntion evGameInit;
}

