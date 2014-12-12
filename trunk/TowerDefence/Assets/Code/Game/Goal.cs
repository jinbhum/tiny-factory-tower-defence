using UnityEngine;
using System.Collections;

public class Goal : MonoBehaviour 
{
	void OnTriggerEnter(Collider collider)
	{
		if(collider == null)return;
		if(collider.transform == null)return;

		if(collider.transform.GetComponent<Monster>() != null)
		{
            collider.GetComponent<Monster>().MonsterEnd();
		}
	}
}
