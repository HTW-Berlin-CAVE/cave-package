using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;

namespace Htw.Cave.Kinect
{
	[CustomEditor(typeof(KinectActor))]
    public class KinectActorEditor : Editor
    {
		private KinectActor me;
		private KinectBrain brain;

		public void OnEnable()
		{
			this.me = (KinectActor)base.target;
			this.brain = null;
		}

		public override void OnInspectorGUI()
		{
			EditorGUI.BeginChangeCheck();
			serializedObject.Update();

			EditorUtils.BrandField();

			using(new EditorGUI.DisabledScope(true))
				EditorGUILayout.PropertyField(serializedObject.FindProperty("m_Script"));

			KinectEditorUtils.BrainInHierarchy(this.me.transform, ref this.brain);
			KinectEditorUtils.BrainField(this.brain);

			serializedObject.ApplyModifiedProperties();
			EditorGUI.EndChangeCheck();
		}
    }
}
