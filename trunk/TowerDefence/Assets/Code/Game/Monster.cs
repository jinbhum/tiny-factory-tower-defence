using UnityEngine;
using System.Collections;

public class Monster : MonoBehaviour {

	// Use this for initialization
	void Start () {

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
        else
        {
        }

        //if (obj.GetComponent<Goal>() != null)
        //{
        //    MonsterEnd();
        //}


        if (MonsterHP <= 0)
        {
            obj.transform.parent.SendMessage("ChangeTarget", GetComponent<CapsuleCollider>(), SendMessageOptions.DontRequireReceiver );
            Destroy(this.gameObject);
        }
    }

    public void MonsterEnd()
    {
        G.i.PInfo.SubtractionLife(100);
        Destroy(this.gameObject);
    }


    public int MonsterHP;
    public int MonsterDamage;
}
