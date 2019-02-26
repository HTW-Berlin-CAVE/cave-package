using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;

namespace Htw.Cave.Projector
{
	[CustomEditor(typeof(ProjectorMount))]
    public class ProjectorMountEditor : Editor
    {
		private ProjectorMount me;
		private ProjectorBrain brain;
		private SerializedProperty targetProperty;
		private SerializedProperty gizmosProperty;

		public void OnEnable()
		{
			this.me = (ProjectorMount)base.target;
			this.brain = null;
			this.targetProperty = serializedObject.FindProperty("target");
			this.gizmosProperty = serializedObject.FindProperty("gizmos");
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

			EditorGUILayout.PropertyField(this.targetProperty);
			this.gizmosProperty.intValue = (int)(ProjectorGizmos)EditorGUILayout.EnumFlagsField("Editor Gizmos", (ProjectorGizmos)this.gizmosProperty.intValue);

			serializedObject.ApplyModifiedProperties();
			EditorGUI.EndChangeCheck();
		}
    }
}
