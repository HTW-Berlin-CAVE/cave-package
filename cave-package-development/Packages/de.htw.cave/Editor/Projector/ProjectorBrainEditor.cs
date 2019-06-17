using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;
using UnityEngine.XR;
using Htw.Cave.Util;

namespace Htw.Cave.Projector
{
	[CustomEditor(typeof(ProjectorBrain))]
    public class ProjectorBrainEditor : Editor
    {
		private ProjectorBrain me;
		private ProjectorMount mount;
		private Editor editor;
		private SerializedProperty settingsProperty;
		private ViewportAxis settingsViewportAxis;

		public void OnEnable()
		{
			this.me = (ProjectorBrain)base.target;
			this.mount = this.me.GetComponentInChildren<ProjectorMount>();
			this.editor = null;
			this.settingsProperty = serializedObject.FindProperty("settings");

			if(this.me.Settings != null)
				RealignViewports();
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

			PreviewChecks();
			serializedObject.ApplyModifiedProperties();
			EditorGUI.EndChangeCheck();
		}

		public void PreviewChecks()
		{
			if(this.me.Settings != null && this.settingsViewportAxis != this.me.Settings.ViewportAxis)
				RealignViewports();

			if(this.me.Settings.DeviceOutput == DeviceOutput.Stereo)
				EditorGUILayout.HelpBox("Stereo is not supported in the editor. Run the build on a stereo capable device.", MessageType.Warning);

			if(QualitySettings.vSyncCount > 0)
				EditorGUILayout.HelpBox("Vertical synchronization can cause low frame rates. Consider turning it off in the quality settings.", MessageType.Warning);

			if(!Application.isPlaying)
			{
				if(PlayerSettings.virtualRealitySupported)
				{
					if(XRSettings.supportedDevices[0] != "stereo")
						EditorGUILayout.HelpBox("Missing Stereo Display (non head-mounted) virtual reality SDK.", MessageType.Error);
				} else {
					EditorGUILayout.HelpBox("Virtual reality support disabled. Enable it inside the XR settings.", MessageType.Error);
				}
			}

			if(PlayerSettings.fullScreenMode != FullScreenMode.ExclusiveFullScreen)
				EditorGUILayout.HelpBox("Consider using the exclusive fullscreen due to problems regarding Nvidia Mosaic.", MessageType.Warning);
		}

		private void RealignViewports()
		{
			if(this.mount != null)
				this.mount.AlignViewports(this.me.Settings.ViewportAxis);
			this.settingsViewportAxis = this.me.Settings.ViewportAxis;
		}
    }
}
