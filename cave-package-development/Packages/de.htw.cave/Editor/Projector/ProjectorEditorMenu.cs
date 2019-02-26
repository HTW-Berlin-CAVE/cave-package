using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;
using Htw.Cave.Joycon;

namespace Htw.Cave.Projector
{
    public static class ProjectorEditorMenu
    {
		[MenuItem("GameObject/Htw.Cave/Projector Brain", false, 41)]
		public static void CreateProjectorBrain()
		{
			GameObject go = new GameObject("Projector Brain");
			go.AddComponent<ProjectorBrain>();
		}

		[MenuItem("GameObject/Htw.Cave/Projector Mount", false, 42)]
		public static void CreateProjectorMount()
		{
			GameObject go = new GameObject("Projector Mount");
			go.AddComponent<ProjectorMount>();
		}

		[MenuItem("GameObject/Htw.Cave/Projector Emitter", false, 43)]
		public static void CreateProjectorEmitter()
		{
			GameObject go = new GameObject("Projector Emitter");
			go.AddComponent<ProjectorEmitter>();
		}

		[MenuItem("GameObject/Htw.Cave/Projector Plane", false, 43)]
		public static void CreateProjectorPlane()
		{
			GameObject go = new GameObject("Projector Plane");
			go.AddComponent<ProjectorPlane>();
		}
    }
}
