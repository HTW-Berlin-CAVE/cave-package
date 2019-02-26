using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;

namespace Htw.Cave.Kinect
{
	[CustomEditor(typeof(KinectBrain))]
    public class KinectBrainEditor : Editor
    {
		private KinectBrain me;
		private Editor editor;
		private SerializedProperty settingsProperty;

		public void OnEnable()
		{
			this.me = (KinectBrain)base.target;
			this.editor = null;
			this.settingsProperty = serializedObject.FindProperty("settings");
		}

		public override void OnInspectorGUI()
		{
			EditorGUI.BeginChangeCheck();
			serializedObject.Update();

			EditorUtils.BrandField();

			using(new EditorGUI.DisabledScope(true))
				EditorGUILayout.PropertyField(serializedObject.FindProperty("m_Script"));

			EditorGUILayout.PropertyField(this.settingsProperty);

			EditorGUI.indentLevel++;

			if(this.settingsProperty.objectReferenceValue == null)
				this.editor = null;

			if(this.editor == null && this.settingsProperty.objectReferenceValue != null)
				Editor.CreateCachedEditor(this.settingsProperty.objectReferenceValue, null, ref this.editor);

			if(this.editor != null)
			{
				this.editor.DrawDefaultInspector();
				this.editor.serializedObject.ApplyModifiedProperties();
			}

			EditorGUI.indentLevel--;

			serializedObject.ApplyModifiedProperties();
			EditorGUI.EndChangeCheck();
		}
    }
}
