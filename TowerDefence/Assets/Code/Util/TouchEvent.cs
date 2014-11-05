using UnityEngine;
using System.Collections;

public class TouchEvent : MonoBehaviour 
{
	// Update is called once per frame
	public virtual void Update () 
	{
#if UNITY_EDITOR
		CheckMouseClick();
		CheckTouch();
#else
		CheckTouch();
#endif
	}
	
	
	
	private void CheckMouseClick()
	{
		if(Input.GetMouseButtonUp(0))
		{
			isMousePressed = false;
			
			TouchInfo tInfo = new TouchInfo();
			
			int id = 0;
			Vector2 pos = Input.mousePosition;
			if(!isMousePressed)Begindelta[id] = pos;
			
			tInfo.Type = TouchType.END;
			tInfo.FingerId = id;
			
			tInfo.Position.x = pos.x;
			tInfo.Position.y = pos.y;
			
			tInfo.BeginDeltaPosition.x = pos.x - Begindelta[id].x;
			tInfo.BeginDeltaPosition.y = pos.y - Begindelta[id].y;
			
			tInfo.PrevDeltaPosition.x = pos.x - Prevdelta[id].x;
			tInfo.PrevDeltaPosition.y = pos.y - Prevdelta[id].y;
			
			switch(id)
			{
				case 0:	if(onTouch0 != null)onTouch0(tInfo); break;
				case 1:	if(onTouch1 != null)onTouch1(tInfo); break;
				case 2:	if(onTouch2 != null)onTouch2(tInfo); break;
				case 3:	if(onTouch3 != null)onTouch3(tInfo); break;
				case 4:	if(onTouch4 != null)onTouch4(tInfo); break;
			}
			
//			Debug.Log("CheckMouseClick :: tInfo :: Type = " + tInfo.Type + ", FingerId = " + tInfo.FingerId + ", Position = " + tInfo.Position + 
//			          ", BeginDeltaPosition = " + tInfo.BeginDeltaPosition + ", PrevDeltaPosition = " + tInfo.PrevDeltaPosition + 
//			          ", Begindelta[" + id + "]" + Begindelta[id] + ", Prevdelta[" + id + "]" + Prevdelta[id]);
		}
		else if(Input.GetMouseButtonDown(0))
		{
			TouchInfo tInfo = new TouchInfo();
			
			int id = 0;
			Vector2 pos = Input.mousePosition;
			Begindelta[id] = pos;
			
			tInfo.Type = TouchType.BEGIN;
			tInfo.FingerId = id;
			
			tInfo.Position.x = pos.x;
			tInfo.Position.y = pos.y;
			
			tInfo.BeginDeltaPosition = Vector2.zero;
			tInfo.PrevDeltaPosition = Vector2.zero;
			
			switch(id)
			{
				case 0:	if(onTouch0 != null)onTouch0(tInfo); break;
				case 1:	if(onTouch1 != null)onTouch1(tInfo); break;
				case 2:	if(onTouch2 != null)onTouch2(tInfo); break;
				case 3:	if(onTouch3 != null)onTouch3(tInfo); break;
				case 4:	if(onTouch4 != null)onTouch4(tInfo); break;
			}
			
			Prevdelta[id] = pos;
			isMousePressed = true;
			
//			Debug.Log("CheckMouseClick :: tInfo :: Type = " + tInfo.Type + ", FingerId = " + tInfo.FingerId + ", Position = " + tInfo.Position + 
//			          ", BeginDeltaPosition = " + tInfo.BeginDeltaPosition + ", PrevDeltaPosition = " + tInfo.PrevDeltaPosition + 
//			          ", Begindelta[" + id + "]" + Begindelta[id] + ", Prevdelta[" + id + "]" + Prevdelta[id]);
		}
		else if(Input.GetMouseButton(0))
		{
			if(isMousePressed)
			{
				TouchInfo tInfo = new TouchInfo();
				
				int id = 0;
				Vector2 pos = Input.mousePosition;
//				Begindelta[id] = pos;
				
				tInfo.Type = TouchType.MOVE;
				tInfo.FingerId = id;
				
				tInfo.Position.x = pos.x;
				tInfo.Position.y = pos.y;
				
				tInfo.BeginDeltaPosition.x = pos.x - Begindelta[id].x;
				tInfo.BeginDeltaPosition.y = pos.y - Begindelta[id].y;

				Begindelta[id] = pos;
				
				tInfo.PrevDeltaPosition.x = pos.x - Prevdelta[id].x;
				tInfo.PrevDeltaPosition.y = pos.y - Prevdelta[id].y;
				
				switch(id)
				{
					case 0:	if(onTouch0 != null)onTouch0(tInfo); break;
					case 1:	if(onTouch1 != null)onTouch1(tInfo); break;
					case 2:	if(onTouch2 != null)onTouch2(tInfo); break;
					case 3:	if(onTouch3 != null)onTouch3(tInfo); break;
					case 4:	if(onTouch4 != null)onTouch4(tInfo); break;
				}
				
				Prevdelta[id] = pos;
				
//				Debug.Log("CheckMouseClick :: tInfo :: Type = " + tInfo.Type + ", FingerId = " + tInfo.FingerId + ", Position = " + tInfo.Position + 
//				          ", BeginDeltaPosition = " + tInfo.BeginDeltaPosition + ", PrevDeltaPosition = " + tInfo.PrevDeltaPosition + 
//				          ", Begindelta[" + id + "]" + Begindelta[id] + ", Prevdelta[" + id + "]" + Prevdelta[id]);
			}
		}

#if UNITY_EDITOR
		if(Input.GetAxis("Mouse ScrollWheel") > 0)
		{
			if(onWheel != null)onWheel(true);
		}

		if(Input.GetAxis("Mouse ScrollWheel") < 0)
		{
			if(onWheel != null)onWheel(false);
		}
#endif
	}
	
	
	
	private void CheckTouch()
	{
		int count = Input.touchCount;
		if(count == 0) return;
		
		for(int i = 0; i < count; i++)
		{
			Touch touch = Input.GetTouch(i);
			int id = touch.fingerId;
			Vector2 pos = touch.position;
			
			TouchInfo tInfo = new TouchInfo();
			
			if(touch.phase == TouchPhase.Began)Begindelta[id] = touch.position;
			
			tInfo.Type = GetTouchType(touch.phase);
			tInfo.FingerId = id;
			
			tInfo.Position.x = pos.x;
			tInfo.Position.y = pos.y;
			
			if(tInfo.Type == TouchType.BEGIN)
			{
				tInfo.BeginDeltaPosition = Vector2.zero;
				tInfo.PrevDeltaPosition = Vector2.zero;
			}
			else
			{
				tInfo.BeginDeltaPosition.x = pos.x - Begindelta[id].x;
				tInfo.BeginDeltaPosition.y = pos.y - Begindelta[id].y;
				
				tInfo.PrevDeltaPosition.x = pos.x - Prevdelta[id].x;
				tInfo.PrevDeltaPosition.y = pos.y - Prevdelta[id].y;
			}
			
			switch(id)
			{
				case 0:	if(onTouch0 != null)onTouch0(tInfo); break;
				case 1:	if(onTouch1 != null)onTouch1(tInfo); break;
				case 2:	if(onTouch2 != null)onTouch2(tInfo); break;
				case 3:	if(onTouch3 != null)onTouch3(tInfo); break;
				case 4:	if(onTouch4 != null)onTouch4(tInfo); break;
			}
			
			
			if(touch.phase == TouchPhase.Began || touch.phase == TouchPhase.Moved)Prevdelta[id] = touch.position;
		}
	}
	
	
	private TouchType GetTouchType(TouchPhase phase)
	{
		TouchType type = TouchType.BEGIN;
		
		switch(phase)
		{
			case TouchPhase.Began:{ type = TouchType.BEGIN; }break;
			case TouchPhase.Moved:{ type = TouchType.MOVE; }break;
			case TouchPhase.Ended:{ type = TouchType.END; }break;
		}
		
		return type;
	}

	
	
	protected delegate void TInfoDelegate(TouchInfo tInfo);
	protected event TInfoDelegate onTouch0, onTouch1, onTouch2, onTouch3, onTouch4;
	
	private Vector2[] Begindelta = new Vector2[5];
	private Vector2[] Prevdelta = new Vector2[5];

	protected delegate void WheelDelegate(bool isForward);
	protected event WheelDelegate onWheel;

	private bool isMousePressed = false;
}



public enum TouchType
{
	BEGIN = 0,
	MOVE = 1,
	END = 2,
}



public class TouchInfo
{
	public TouchType Type;
	public int FingerId;
	public Vector2 Position;
	public Vector2 BeginDeltaPosition;
	public Vector2 PrevDeltaPosition;
	
	public TouchInfo()
	{
		Type = TouchType.BEGIN;
		FingerId = 0;
		Position = new Vector2();
		BeginDeltaPosition = new Vector2();
		PrevDeltaPosition = new Vector2();
	}
}