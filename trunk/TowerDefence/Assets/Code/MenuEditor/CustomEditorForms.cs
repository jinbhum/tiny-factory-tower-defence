using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

public class CustomEditorForms
{
	public static int InputIntegerForm(string Name, int Value, float NameWidht)
	{
		EditorGUILayout.BeginHorizontal();
		{
			EditorGUILayout.LabelField(Name, GUILayout.Width(NameWidht));
			Value = EditorGUILayout.IntField(Value, GUILayout.Width(50.0f));
		}
		EditorGUILayout.EndHorizontal();

		return Value;
	}




	public static bool CheckBoxForm(string Name, bool Value, float Widht)
	{
		Value = EditorGUILayout.ToggleLeft(Name, Value, GUILayout.Width(Widht));
		return Value;
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