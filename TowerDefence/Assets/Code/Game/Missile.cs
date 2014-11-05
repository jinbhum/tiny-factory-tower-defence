using UnityEngine;
using System.Collections;

public class Missile : MonoBehaviour 
{
//	// Use this for initialization
//	void Start () {
//	
//	}
//	
	// Update is called once per frame
	void Update () 
	{
		if(isThrowing != null)MovePosition();
	}



	public void Throwing(Transform Target)
	{
		mTarget = Target;
		isThrowing = true;
	}



	private void MovePosition()
	{
		transform.position = Vector3.Slerp(transform.position, mTarget.transform.position, ThrowingTime);
	}



	public int Damege = 0;
	public float ThrowingTime = 0.0f;
	public Transform mTarget;

	private bool isThrowing = false;
}
