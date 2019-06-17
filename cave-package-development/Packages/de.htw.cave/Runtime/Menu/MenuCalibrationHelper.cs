using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Htw.Cave.Projector;

namespace Htw.Cave.Menu
{
    public static class MenuCalibrationHelper
    {
		public static List<GameObject> CreateVisualGuide(ProjectorEmitter[] emitters, GameObject prefab)
		{
			List<GameObject> guides = new List<GameObject>();
			List<Vector3> list = new List<Vector3>();

			for(int i = emitters.Length - 1; i >= 0; --i)
			{
				Vector3[] plane = emitters[i].TransformPlane();

				for(int k = plane.Length - 1; k >= 0; --k)
					list.Add(plane[k]);
			}

			foreach(Vector3 vector in list)
			{
				GameObject go = GameObject.Instantiate<GameObject>(prefab);
				go.transform.position = vector;
				guides.Add(go);
			}

			return guides;
		}
    }
}
