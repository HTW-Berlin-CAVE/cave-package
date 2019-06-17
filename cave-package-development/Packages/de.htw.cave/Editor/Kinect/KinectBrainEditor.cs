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
		private static bool searchedInstallation;
		private static KinectInstall kinectInstall;

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

			if(!searchedInstallation)
			{
				kinectInstall = KinectEditorUtils.FindInstallation();
				searchedInstallation = true;
			}

			if(kinectInstall == KinectInstall.Ignore)
				EditorGUILayout.HelpBox("Unable to find Kinect 2.0 SDK installation.", MessageType.Warning);
			else if(kinectInstall == KinectInstall.Missing)
				EditorGUILayout.HelpBox("Kinect 2.0 SDK is not installed. Components that require the SDK can throw errors.", MessageType.Error);

			serializedObject.ApplyModifiedProperties();
			EditorGUI.EndChangeCheck();
		}
    }
}
