using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;

namespace Htw.Cave.Projector
{
	[CustomEditor(typeof(ProjectorEmitter))]
    public class ProjectorEmitterEditor : Editor
    {
		private ProjectorEmitter me;
		private ProjectorBrain brain;
		private Editor editor;
		private SerializedProperty configurationProperty;
		private SerializedProperty planeProperty;

		public void OnEnable()
		{
			this.me = (ProjectorEmitter)base.target;
			this.brain = null;
			this.editor = null;
			this.configurationProperty = serializedObject.FindProperty("configuration");
			this.planeProperty = serializedObject.FindProperty("plane");
		}

		public override void OnInspectorGUI()
		{
			EditorGUI.BeginChangeCheck();
			serializedObject.Update();

			EditorUtils.BrandField();

			using(new EditorGUI.DisabledScope(true))
				EditorGUILayout.PropertyField(serializedObject.FindProperty("m_Script"));

			ProjectorEditorUtils.BrainInHierarchy(this.me.transform, ref this.brain);
			ProjectorEditorUtils.BrainField(this.brain);

			EditorGUILayout.PropertyField(this.configurationProperty);

			EditorGUI.indentLevel++;

			if(this.configurationProperty.objectReferenceValue == null)
				this.editor = null;

			if(this.editor == null && this.configurationProperty.objectReferenceValue != null)
				Editor.CreateCachedEditor(this.configurationProperty.objectReferenceValue, null, ref this.editor);

			if(this.editor != null)
			{
				this.editor.DrawDefaultInspector();
				this.editor.serializedObject.ApplyModifiedProperties();
			}

			EditorGUI.indentLevel--;

			EditorGUILayout.PropertyField(this.planeProperty);

			if(GUI.changed && this.me.Configuration != null)
				this.me.ApplyModificationsToCamera();

			serializedObject.ApplyModifiedProperties();
			EditorGUI.EndChangeCheck();
		}
    }
}
