using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

public class CustomEditorForms
{
	public static string InputStringForm(string Name, string Value, float NameWidht, float ValueWidht = 50.0f)
	{
		EditorGUILayout.BeginHorizontal();
		{
			EditorGUILayout.LabelField(Name, GUILayout.Width(NameWidht));
			Value = EditorGUILayout.TextField(Value, GUILayout.Width(ValueWidht));
		}
		EditorGUILayout.EndHorizontal();
		
		return Value;
	}



	public static int InputIntegerForm(string Name, int Value, float NameWidht, float ValueWidht = 50.0f)
	{
		EditorGUILayout.BeginHorizontal();
		{
			EditorGUILayout.LabelField(Name, GUILayout.Width(NameWidht));
			Value = EditorGUILayout.IntField(Value, GUILayout.Width(ValueWidht));
		}
		EditorGUILayout.EndHorizontal();

		return Value;
	}



	public static float InputFloatForm(string Name, float Value, float NameWidht, float ValueWidht = 50.0f)
	{
		EditorGUILayout.BeginHorizontal();
		{
			EditorGUILayout.LabelField(Name, GUILayout.Width(NameWidht));
			Value = EditorGUILayout.FloatField(Value, GUILayout.Width(ValueWidht));
		}
		EditorGUILayout.EndHorizontal();
		
		return Value;
	}



	public static Vector3 InputVector3Form(string Name, Vector3 Value, float Widht)
	{
		Value = EditorGUILayout.Vector3Field(Name, Value, GUILayout.Width(Widht));
		return Value;
	}



	public static bool CheckBoxForm(string Name, bool Value, float Widht)
	{
		Value = EditorGUILayout.ToggleLeft(Name, Value, GUILayout.Width(Widht));
		return Value;
	}



	public static GameObject InputGameObjectForm(string Name, Object Value, float NameWidht, float ValueWidht = 50.0f)
	{	
		EditorGUILayout.BeginHorizontal();
		{
			EditorGUILayout.LabelField(Name, GUILayout.Width(NameWidht));
			Value = EditorGUILayout.ObjectField(Value, typeof(GameObject), false, GUILayout.Width(ValueWidht));
		}
		EditorGUILayout.EndHorizontal();

		return (GameObject)Value;
	}



	public static void ExcuteButton(string Name, float ButtonWidht, VoidFuntion funtion)
	{
		if(GUILayout.Button(Name, GUILayout.Width(ButtonWidht)))
		{
			if(funtion != null)funtion();
		}
	}



	public static void BlankLine()
	{
		EditorGUILayout.Space();
	}



	public delegate void VoidFuntion();
}