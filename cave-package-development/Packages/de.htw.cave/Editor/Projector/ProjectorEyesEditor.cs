using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;

namespace Htw.Cave.Projector
{
	[CustomEditor(typeof(ProjectorEyes))]
	[CanEditMultipleObjects]
    public class ProjectorEyesEditor : Editor
    {
		private ProjectorEyes me;
		private ProjectorBrain brain;

		public void OnEnable()
		{
			this.me = (ProjectorEyes)base.target;
			this.brain = null;
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

			serializedObject.ApplyModifiedProperties();
			EditorGUI.EndChangeCheck();
		}
    }
}
