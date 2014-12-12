using UnityEngine;
using System.Collections;

public class Monster : MonoBehaviour {

	// Use this for initialization
	void Start () {
        if (G.i.StageM != null)
        {
            G.i.StageM.AddGameOverEvent(MonsterStop);
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider obj)
    {
        if (obj.GetComponent<Missile>() != null)
        {
            MonsterHP = MonsterHP - obj.GetComponent<Missile>().Damege;
            obj.transform.SendMessage("Destroy", SendMessageOptions.DontRequireReceiver);
        }

        if (MonsterHP <= 0)
        {
            obj.transform.parent.SendMessage("ChangeTarget", GetComponent<CapsuleCollider>(), SendMessageOptions.DontRequireReceiver );
            Destroy(this.gameObject);
        }
    }

    public void MonsterEnd()
    {
        G.i.PInfo.SubtractionLife(100);
        G.i.StageM.RemoveGameOverEvent(MonsterStop);
        Destroy(this.gameObject);
    }

    public void MonsterStop()
    {
        this.gameObject.GetComponent<iTweenEvent>().enabled = false;
        this.gameObject.GetComponent<iTween>().enabled = false;
        //G.i.StageM.StopCreatMonster(this.gameObject);
    }


    public int MonsterHP;
    public int MonsterDamage;
}
