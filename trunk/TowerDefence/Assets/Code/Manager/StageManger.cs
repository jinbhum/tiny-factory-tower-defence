using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StageManger : MonoBehaviour 
{
	// Use this for initialization
	void Start () 
	{
		G.i.StageM = GetComponent<StageManger>();
	}


	void OnDestroy()
	{
		G.i.StageM = null;
	}



	public void GameStart()
	{
        //StartCoroutine(CreateMonster(MonsterObj));
	}



	public void GameEnd()
	{

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
            StartCoroutine(IECreateMonster(monster));
        }
    }

    private IEnumerator IECreateMonster(GameObject monster)
    {
        MonsterCount = 6;
        MonsterPos = monster.transform;
        MonsterstartPos = new Vector3(-60, 0, 35);

        for (int i = 0; i <= MonsterCount; i++)
        {
            GameObject obj;
            obj = Instantiate(monster, MonsterstartPos, Quaternion.identity) as GameObject;
            obj.SetActive(true);
            yield return new WaitForSeconds(2.0f);
        }
    }


    public Transform CurTarget = null;
	public Transform TowerParent = null;

	public BlockState BlockState = null;

    public GameObject MonsterObj = null;
    public Transform MonsterPos;
    public int MonsterCount;

    private Vector3 MonsterstartPos;

	private Dictionary<string, Transform> mUsedTileMap = new Dictionary<string, Transform>();
}

