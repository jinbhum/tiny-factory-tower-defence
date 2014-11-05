using UnityEngine;
using System.Collections;

public class BlockState : MonoBehaviour 
{
	public void CheckState(Transform trans)
	{
		if(trans != null)
		{
			if(trans.gameObject.layer == LayerMask.NameToLayer("AvailableTile"))
			{
				if(!G.i.StageM.UsedTileMap.ContainsKey(trans.GetInstanceID().ToString()))
				{
					SetAvailableState();
				}
				else
				{
					SetImpossibleState();
				}
			}
			else if(trans.gameObject.layer == LayerMask.NameToLayer("ImpossibleTile"))
			{
				SetImpossibleState();
			}
			else if(trans.gameObject.layer == LayerMask.NameToLayer("Tower"))
			{
				SetImpossibleState();
			}
		}
	}

	private void SetAvailableState()
	{
		transform.position = new Vector3(G.i.StageM.CurTarget.position.x, 0.01f, G.i.StageM.CurTarget.position.z);
		renderer.material.color = AvailableColor;
	}



	private void SetImpossibleState()
	{
		transform.position = new Vector3(G.i.StageM.CurTarget.position.x, 0.01f, G.i.StageM.CurTarget.position.z);
		renderer.material.color = ImpossibleColor;
	}



	public Color AvailableColor;
	public Color ImpossibleColor;
}
