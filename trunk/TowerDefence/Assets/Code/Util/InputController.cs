using UnityEngine;
using System.Collections;

public class InputController : TouchEvent 
{
	void Awake()
	{
		onTouch0 += OneTouchClick;
		onTouch0 += OneTouchDrag;
	}



	private void OneTouchClick(TouchInfo tInfo)
	{
		if(UICamera.hoveredObject == null)
		{
			if(SensitivityValue < tInfo.BeginDeltaPosition.x && SensitivityValue > tInfo.BeginDeltaPosition.x
			   && SensitivityValue < tInfo.BeginDeltaPosition.y && SensitivityValue > tInfo.BeginDeltaPosition.y
			   && tInfo.Type != TouchType.END)
				return;

			Select(tInfo);
		}
	}


	private void Select(TouchInfo tInfo)
	{
		RaycastHit hit;
		Ray ray = Camera.main.ScreenPointToRay(tInfo.Position);

		bool towerHit = false;

		if(Physics.Raycast(ray, out hit, Mathf.Infinity, TowerMask));
		{
			if(hit.transform != null && !CurTarget.Equals(hit.transform))
			{
				CurTarget = hit.transform;
				G.i.StageM.CurTarget = hit.transform;
				SelectTower(CurTarget);

				towerHit = true;
			}
		}

		if(!towerHit)
		{
			if(Physics.Raycast(ray, out hit, Mathf.Infinity, TileMask));
			{
				if(hit.transform != null && !CurTarget.Equals(hit.transform))
				{
					CurTarget = hit.transform;
					G.i.StageM.CurTarget = hit.transform;
					SelectTile(CurTarget);
				}
			}
		}
	}



	private void SelectTower(Transform Target)
	{
		Target.SendMessage("CallUI", SendMessageOptions.DontRequireReceiver);
		G.i.StageM.BlockState.CheckState(Target);
	}



	private void SelectTile(Transform Target)
	{
		Target.SendMessage("CallUI", SendMessageOptions.DontRequireReceiver);
		G.i.StageM.BlockState.CheckState(Target);
	}


	
	private void OneTouchDrag(TouchInfo tInfo)
	{

	}



	public float SensitivityValue = 5.0f;

	public LayerMask TowerMask;
	public LayerMask TileMask;

	public Transform CurTarget = null;
}
