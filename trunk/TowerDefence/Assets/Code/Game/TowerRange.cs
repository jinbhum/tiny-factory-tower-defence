using UnityEngine;
using System.Collections;

public class TowerRange : MonoBehaviour
{
	void OnTriggerEnter(Collider collider)
	{
		if(collider.transform.gameObject.layer != LayerMask.NameToLayer("Tower") &&
		   collider.transform.gameObject.layer != LayerMask.NameToLayer("ShootRange") &&
		   collider.transform.gameObject.layer != LayerMask.NameToLayer("ShootObject") &&
		   collider.transform.gameObject.layer != LayerMask.NameToLayer("AvailableTile") &&
		   collider.transform.gameObject.layer != LayerMask.NameToLayer("ImpossibleTile")
		   )
		{
			Debug.Log("EnterTarget = " + collider.transform);

			transform.parent.SendMessage("TriggerEnter", collider, SendMessageOptions.DontRequireReceiver);
		}
	}
	
	
	
	void OnTriggerExit(Collider collider)
	{
		if(collider.transform.gameObject.layer != LayerMask.NameToLayer("Tower") ||
		   collider.transform.gameObject.layer != LayerMask.NameToLayer("ShootRange") ||
		   collider.transform.gameObject.layer != LayerMask.NameToLayer("ShootObject") ||
		   collider.transform.gameObject.layer != LayerMask.NameToLayer("AvailableTile") ||
		   collider.transform.gameObject.layer != LayerMask.NameToLayer("ImpossibleTile")
		   )
		{
			Debug.Log("ExitTarget = " + collider.transform);

			transform.parent.SendMessage("TriggerExit", collider, SendMessageOptions.DontRequireReceiver);
		}
	} 
}
