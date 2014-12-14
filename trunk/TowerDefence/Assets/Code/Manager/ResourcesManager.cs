using UnityEngine;
using System.Collections;

public class ResourcesManager : MonoBehaviour 
{
	// Use this for initialization
	void Start () 
	{
		if(G.i != null) G.i.ResourceM = GetComponent<ResourcesManager>();
	}



	void OnDestroy()
	{
		if(G.i != null) G.i.ResourceM = null;
	}


	public GameObject Load(string path)
	{
		GameObject Resource = Resources.Load(path) as GameObject;
		GameObject ResourceCopy = null;

		if(Resource != null)
		{
			ResourceCopy = GameObject.Instantiate(Resource) as GameObject;
			ResourceCopy.name = Resource.name;
		}

		return ResourceCopy;
	}
}
