using UnityEngine;
using UnityEditor;
using System.Collections;

public class CustomTowerWindow : EditorWindow 
{
	void OnGUI()
	{
		PrefabSavePath = CustomEditorForms.InputStringForm("PrefabSavePath", PrefabSavePath, 150.0f, 150.0f);

		TowerName = CustomEditorForms.InputStringForm("TowerName", TowerName, 150.0f, 150.0f);

		Damege = CustomEditorForms.InputIntegerForm("Damege", Damege, 150.0f, 150.0f);
		RangeValue = CustomEditorForms.InputFloatForm("RangeValue", RangeValue, 150.0f, 150.0f);
		MissileSize = CustomEditorForms.InputVector3Form("MissileSize", MissileSize, 300.0f);

		ShootDelayTime = CustomEditorForms.InputFloatForm("ShootDelayTime", ShootDelayTime, 150.0f, 150.0f);
		ThrowingTime = CustomEditorForms.InputFloatForm("ThrowingTime", ThrowingTime, 150.0f, 150.0f);

		TowerModel = CustomEditorForms.InputGameObjectForm("TowerModel", TowerModel, 150.0f, 150.0f);
		MissileModel = CustomEditorForms.InputGameObjectForm("MissileModel", MissileModel, 150.0f, 150.0f);

		CustomEditorForms.ExcuteButton("CreateTower", 150.0f, ExcuteCreateTower);
	}



	private void ExcuteCreateTower()
	{
		GameObject Parent = GetTowerParent();
		GameObject Range = GetTowerRange();
		GameObject Missile = GetTowerMissile(Parent);
		GameObject Model = GetTowerModel();

		Range.transform.parent = Parent.transform;
		Missile.transform.parent = Parent.transform;
		Model.transform.parent = Parent.transform;

		SavePrefab(Parent);
		DeleteTower(Parent);
	}



	private GameObject GetTowerParent()
	{
		GameObject TowerParent = new GameObject();

		TowerParent.name = TowerName;

		TowerParent.transform.position = Vector3.zero;
		TowerParent.transform.rotation = Quaternion.Euler(Vector3.zero);
		TowerParent.transform.localScale = Vector3.one;

		Tower TScript = TowerParent.AddComponent<Tower>();
		TScript.ShootDelay = ShootDelayTime;

		return TowerParent;
	}



	private GameObject GetTowerRange()
	{
		GameObject TowerRange = new GameObject();

		TowerRange.name = RangeName;

		TowerRange.transform.localPosition = Vector3.zero;
		TowerRange.transform.localRotation = Quaternion.Euler(Vector3.zero);
		TowerRange.transform.localScale = Vector3.one;

		TowerRange.AddComponent<TowerRange>();
		SphereCollider colliderScript = TowerRange.AddComponent<SphereCollider>();

		colliderScript.radius = RangeValue;

		return TowerRange;
	}



	private GameObject GetTowerMissile(GameObject Parent)
	{
		GameObject TowerMissile = new GameObject();

		Parent.GetComponent<Tower>().ShootObject = TowerMissile;

		TowerMissile.name = MissileName;

		TowerMissile.transform.localPosition = Vector3.zero;
		TowerMissile.transform.localRotation = Quaternion.Euler(Vector3.zero);
		TowerMissile.transform.localScale = Vector3.one;

		TowerMissile.SetActive(false);

		Missile TMScript = TowerMissile.AddComponent<Missile>();
		TMScript.Damege = Damege;
		TMScript.ThrowingTime = ThrowingTime;

		BoxCollider colliderScript = TowerMissile.AddComponent<BoxCollider>();
		colliderScript.size = MissileSize;

		Rigidbody RScript = TowerMissile.AddComponent<Rigidbody>();
		RScript.useGravity = false;
		RScript.isKinematic = false;

		if(MissileModel != null)
		{
			GameObject Model = GameObject.Instantiate(MissileModel) as GameObject;

			Model.transform.parent = TowerMissile.transform;

			Model.transform.localPosition = Vector3.zero;
			Model.transform.localScale = Vector3.one;
			Model.transform.localRotation = Quaternion.Euler(Vector3.zero);

			Model.name = Model.name.Replace("(Clone)", string.Empty);
		}

		return TowerMissile;
	}



	private GameObject GetTowerModel()
	{
		GameObject Tower = null;

		if(TowerModel != null)
		{
			Tower = GameObject.Instantiate(TowerModel) as GameObject;

			Tower.transform.localPosition = Vector3.zero;
			
			Tower.name = Tower.name.Replace("(Clone)", string.Empty);
		}

		return Tower;
	}



	private void SavePrefab(GameObject Tower)
	{
		PrefabUtility.CreatePrefab(PrefabSavePath + TowerName + ".prefab", Tower);
	}


	private void DeleteTower(GameObject Tower)
	{
		GameObject.DestroyImmediate(Tower);
	}
	


	private string TowerName = "";
	private string PrefabSavePath = "Assets/Resources/Game/Prefab/Tower/";

	private int Damege = 0;

	private float ShootDelayTime = 0.0f;
	private float ThrowingTime = 0.0f;

	private float RangeValue = 0.0f;
	private Vector3 MissileSize = Vector3.zero;

	private GameObject TowerModel = null;
	private GameObject MissileModel = null;


	private const string RangeName = "TowerRange";
	private const string MissileName = "ShootObject";
}
