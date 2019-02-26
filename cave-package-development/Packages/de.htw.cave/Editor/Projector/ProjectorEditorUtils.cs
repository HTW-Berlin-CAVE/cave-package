using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;

namespace Htw.Cave.Projector
{
    public static class ProjectorEditorUtils
    {
		public static void BrainInHierarchy(Transform transform, ref ProjectorBrain brain)
		{
			if(brain == null || brain.transform.hasChanged)
				brain = transform.GetComponentInParent<ProjectorBrain>();
		}

		public static void BrainField(ProjectorBrain brain)
		{
			using(new EditorGUI.DisabledScope(true))
				EditorGUILayout.ObjectField("Brain", brain, typeof(ProjectorBrain), true);

			if(brain == null)
				EditorGUILayout.HelpBox("No brain in parent hierarchy found. Perhaps this component will be ignored.", MessageType.Error);
		}

		[DrawGizmo(GizmoType.InSelectionHierarchy | GizmoType.NonSelected | GizmoType.Active)]
		public static void DrawAreaGizmos(ProjectorMount mount, GizmoType type)
		{
			ProjectorEmitter[] emitters = mount.Get();

			for(int i = emitters.Length - 1; i >= 0; --i)
			{
				if(emitters[i].Configuration == null)
					continue;

				Vector3[] plane = emitters[i].TransformPlane();

				if(mount.Gizmos.HasFlag(ProjectorGizmos.Viewport))
				{
					Gizmos.color = Color.yellow;

					Gizmos.DrawLine(emitters[i].transform.position, plane[0]);
					Gizmos.DrawLine(emitters[i].transform.position, plane[1]);
					Gizmos.DrawLine(emitters[i].transform.position, plane[2]);
					Gizmos.DrawLine(emitters[i].transform.position, plane[3]);

					Gizmos.DrawLine(plane[0], plane[1]);
					Gizmos.DrawLine(plane[1], plane[2]);
					Gizmos.DrawLine(plane[2], plane[3]);
					Gizmos.DrawLine(plane[3], plane[0]);
				}

				if(mount.Gizmos.HasFlag(ProjectorGizmos.Wireframe))
				{
					Color softYellow = Color.yellow;
					softYellow.a = 0.2f;
					Gizmos.color = softYellow;

					Vector3 a1 = Vector3.Lerp(plane[0], plane[1], 0.25f);
					Vector3 a2 = Vector3.Lerp(plane[0], plane[1], 0.75f);
					Vector3 b1 = Vector3.Lerp(plane[3], plane[2], 0.25f);
					Vector3 b2 = Vector3.Lerp(plane[3], plane[2], 0.75f);

					Vector3 c1 = Vector3.Lerp(plane[1], plane[2], 0.25f);
					Vector3 c2 = Vector3.Lerp(plane[1], plane[2], 0.75f);
					Vector3 d1 = Vector3.Lerp(plane[0], plane[3], 0.25f);
					Vector3 d2 = Vector3.Lerp(plane[0], plane[3], 0.75f);

					Gizmos.DrawLine(a1, b1);
					Gizmos.DrawLine(a2, b2);
					Gizmos.DrawLine(c1, d1);
					Gizmos.DrawLine(c2, d2);
				}

				if(mount.Gizmos.HasFlag(ProjectorGizmos.Anchors))
				{
					Vector3[] equalizationAnchors = emitters[i].Plane.TransformPlane(emitters[i].Configuration.ScaledEqualizationAnchors());

					if(equalizationAnchors.Length > 0)
					{
						Gizmos.color = new Color(0.9f, 1f, 0.5f, 1f);

						Gizmos.DrawLine(equalizationAnchors[0], equalizationAnchors[1]);
						Gizmos.DrawLine(equalizationAnchors[1], equalizationAnchors[2]);
						Gizmos.DrawLine(equalizationAnchors[2], equalizationAnchors[3]);
						Gizmos.DrawLine(equalizationAnchors[3], equalizationAnchors[0]);
					}
				}
			}
		}
    }
}
