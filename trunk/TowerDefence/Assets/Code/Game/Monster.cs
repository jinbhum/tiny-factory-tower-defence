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
            obj.transform.parent.SendMessage("Destroy", GetComponent<BoxCollider>(), SendMessageOptions.DontRequireReceiver);
        }
        else
        {
        }

        if (MonsterHP <= 0)
        {
            obj.transform.parent.SendMessage("ChangeTarget", GetComponent<CapsuleCollider>(), SendMessageOptions.DontRequireReceiver );
            Destroy(this.gameObject);
        }
    }

    public int MonsterHP;
}
