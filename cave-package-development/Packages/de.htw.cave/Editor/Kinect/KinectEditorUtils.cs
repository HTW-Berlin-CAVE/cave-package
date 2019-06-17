using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;
#if UNITY_EDITOR_WIN
using Microsoft.Win32;
#endif

namespace Htw.Cave.Kinect
{
	public enum KinectInstall
	{
		Ignore,
		Missing,
		Fine,
	}

    public static class KinectEditorUtils
    {
		public static void BrainInHierarchy(Transform transform, ref KinectBrain brain)
		{
			if(brain == null || brain.transform.hasChanged)
				brain = transform.GetComponentInParent<KinectBrain>();
		}

		public static void BrainField(KinectBrain brain)
		{
			using(new EditorGUI.DisabledScope(true))
				EditorGUILayout.ObjectField("Brain", brain, typeof(KinectBrain), true);

			if(brain == null)
				EditorGUILayout.HelpBox("No brain in parent hierarchy found. Perhaps this component will be ignored.", MessageType.Error);
		}

		[DrawGizmo(GizmoType.InSelectionHierarchy | GizmoType.Active)]
		public static void DrawAreaGizmos(KinectBrain brain, GizmoType type)
		{
			Rect rect = brain.Settings.TrackingAreaCentered();

			Vector3 a = brain.transform.TransformPoint(new Vector3(rect.min.x, 0f, rect.min.y));
			Vector3 b = brain.transform.TransformPoint(new Vector3(rect.max.x, 0f, rect.min.y));
			Vector3 c = brain.transform.TransformPoint(new Vector3(rect.min.x, 0f, rect.max.y));
			Vector3 d = brain.transform.TransformPoint(new Vector3(rect.max.x, 0f, rect.max.y));

			Gizmos.color = Color.green;
			Gizmos.DrawLine(a, b);
			Gizmos.DrawLine(a, c);
			Gizmos.DrawLine(c, d);
			Gizmos.DrawLine(d, b);
		}

#if UNITY_EDITOR_WIN
		public static KinectInstall FindInstallation()
		{
			string sdk = Registry.GetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Kinect\\v2.0", "SDKInstallPath", null) as string;

			if(string.IsNullOrEmpty(sdk))
				return KinectInstall.Missing;

			return KinectInstall.Fine;
		}
#else
		public static KinectInstall FindInstallation()
		{
			return KinectInstall.Ignore;
		}
#endif
    }
}
