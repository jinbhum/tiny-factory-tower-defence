using UnityEngine;
using System.Collections;

public class CameraManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GameObject _MainMap = GameObject.Find("Main Camera") as GameObject;
        _CameraTrans = _MainMap.transform.FindChild("Map");
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnDrag(Vector2 delta)
    {
        if (enabled)
        {
            _CameraTrans.Rotate(0.0f, -delta.x, 0.0f);
        }
    }

    private Transform _CameraTrans;
    //private bool mDragStarted = false;
}
