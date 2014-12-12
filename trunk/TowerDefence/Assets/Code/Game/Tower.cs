using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Tower : MonoBehaviour 
{

	// Use this for initialization
	void Start () 
	{
		if(G.i.StageM != null)
		{
			G.i.StageM.AddGameInitEvent(GameInit);
			G.i.StageM.AddGameClearEvent(GameEnd);
			G.i.StageM.AddGameOverEvent(GameEnd);
		}
	}


	void Update()
	{

	}



	void OnDestroy()
	{
		UsedTile = null;

		if(G.i.StageM != null)
		{
			G.i.StageM.RemoveGameInitEvent(GameInit);
			G.i.StageM.RemoveGameClaerEvent(GameEnd);
			G.i.StageM.RemoveGameOverEvent(GameEnd);
		}
	}



	public void Init()
	{
		transform.position = new Vector3(mUsedTile.position.x, 0.0f, mUsedTile.position.z);
		transform.localScale = Vector3.one;
	}
	
	
	
	public void CallUI()
	{
		Debug.Log("CallUI");
	}



	public void MissingTarget()
	{
		StopAttack();
		SetNextTarget();
		StartAttack();
	}
	
	
	
	public void ChangeTarget(Collider collider)
	{
		StopAttack();
		
		mEnemyList.Remove(collider.transform);
		SetNextTarget();
		
		StartAttack();
	}



	public void TriggerEnter(Collider collider)
	{
		if(!isGameEnd)
		{
			mEnemyList.Add(collider.transform);
			
			if(mEnemyList.Count == 1)
			{
				SetNextTarget();
				StartAttack();
			}
		}
	}
	
	
	
	public void TriggerExit(Collider collider)
	{
		if(!isGameEnd)
		{
			ChangeTarget(collider);
		}
	}



	private void SetNextTarget()
	{
		if(mEnemyList.Count > 0)
		{
			mTargetEnemy = mEnemyList[0];
		}
		else
		{
			mTargetEnemy = null;
		}
	}



	private void StartAttack()
	{
		if(mTargetEnemy != null) StartCoroutine("IEAttacTarget");
	}



	private void StopAttack()
	{
		StopCoroutine("IEAttacTarget");
	}



	private IEnumerator IEAttacTarget()
	{
		while(true)
		{
			yield return new WaitForSeconds(ShootDelay);

			GameObject missile = GetMissile();
			missile.transform.parent = transform;
			missile.transform.localPosition = ShootObject.transform.localPosition;
		}
	}



	private GameObject GetMissile()
	{
		GameObject missile = null;

		if(ShootObject != null)
		{
			missile = GameObject.Instantiate(ShootObject) as GameObject;
			missile.SetActive(true);
			missile.SendMessage("Throwing", mTargetEnemy, SendMessageOptions.DontRequireReceiver);
		}

		return missile;
	}



	public Transform UsedTile
	{
		set{ mUsedTile = value; }
		get{ return mUsedTile; }
	}



	private void GameInit()
	{
		GameObject.Destroy(gameObject);
	}



	private void GameEnd()
	{
		isGameEnd = true;
		StopAttack();
	}



	public GameObject ShootObject = null;

	public float ShootDelay = 0.0f;

	private bool isGameEnd = false;

	private Transform mUsedTile = null;

	private Transform mTargetEnemy = null;
	private List<Transform> mEnemyList = new List<Transform>();
}
