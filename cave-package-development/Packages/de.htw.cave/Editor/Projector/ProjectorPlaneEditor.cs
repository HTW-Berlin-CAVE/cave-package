using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;

namespace Htw.Cave.Projector
{
	[CustomEditor(typeof(ProjectorPlane))]
	[CanEditMultipleObjects]
    public class ProjectorPlaneEditor : Editor
    {
		private ProjectorPlane me;
		private ProjectorBrain brain;

		public void OnEnable()
		{
			this.me = (ProjectorPlane)base.target;
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
