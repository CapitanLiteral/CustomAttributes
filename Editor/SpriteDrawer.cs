using System;
using System.Collections;
using System.Collections.Generic;
using Unity.UNetWeaver;
using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(ShowSpriteAttribute))]
public class SpriteDrawer : PropertyDrawer {
 
	private static GUIStyle s_TempStyle = new GUIStyle();
 
	public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
	{                                    
		var ident = EditorGUI.indentLevel;
		EditorGUI.indentLevel = 0;
 
		Rect spriteRect;
		
		GUIStyle style = new GUIStyle();
     
		//create object field for the sprite
		spriteRect = new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight);
		property.objectReferenceValue = EditorGUI.ObjectField(position, property.name, property.objectReferenceValue, typeof(Sprite), false);

		//if this is not a repain or the property is null exit now
		if (Event.current.type != EventType.Repaint || property.objectReferenceValue == null)
			return;

		//draw a sprite
		Sprite sp = property.objectReferenceValue as Sprite;
		
		spriteRect.y += EditorGUIUtility.singleLineHeight + 4;
		float asp = sp.rect.size.x / sp.rect.size.y;
		spriteRect.width = 64*asp;
		spriteRect.height = 64;
		
		s_TempStyle.normal.background = sp.texture;
		s_TempStyle.Draw(spriteRect, GUIContent.none, false, false, false, false);

		EditorGUI.indentLevel = ident;
	}
 
	public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
	{
		return base.GetPropertyHeight(property, label) + 70f;
	}
}