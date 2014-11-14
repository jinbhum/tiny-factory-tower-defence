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
		if(isThrowing)MovePosition();
	}



	public void Throwing(Transform Target)
	{
		mTarget = Target;
		isThrowing = true;
		ThrowingStartTime = Time.time;
	}



	private void MovePosition()
	{
		float TargetRatio = (Time.time - ThrowingStartTime) / ThrowingTime;

		transform.LookAt(mTarget);
		transform.position = Vector3.Lerp(transform.position, mTarget.transform.position, TargetRatio);
	}



	public int Damege = 0;
	public float ThrowingTime = 0.0f;
	public Transform mTarget;

	private bool isThrowing = false;

	private float ThrowingStartTime = 0.0f;
}
