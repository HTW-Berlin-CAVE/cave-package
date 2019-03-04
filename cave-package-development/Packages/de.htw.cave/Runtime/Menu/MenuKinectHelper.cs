using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Windows.Kinect;

namespace Htw.Cave.Menu
{
    public static class MenuKinectHelper
    {
		public static List<GameObject> CreateMirrorJoints(JointType[] joints, Shader shader)
		{
			List<GameObject> list = new List<GameObject>();

			for(int i = joints.Length - 1; i >= 0; --i)
			{
				GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
				sphere.name = "Kinect Joint " + joints[i].ToString();
				sphere.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
				GameObject.Destroy(sphere.GetComponent<SphereCollider>());

				Renderer rend = sphere.GetComponent<Renderer>();
				rend.material.shader = shader;
				rend.material.SetColor("_Color", new Color(0.973f, 0.475f, 0f));
				rend.material.SetFloat("_Metallic", 0f);
				rend.material.SetFloat("_Glossiness", 0f);

				list.Add(sphere);
			}

			return list;
		}

		public static GameObject CreateTrackingArea(Transform transform, Rect rect, Shader shader)
		{
			GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
			cube.transform.position = transform.position;
			cube.transform.localScale = new Vector3(rect.width, 0.1f, rect.height);
			GameObject.Destroy(cube.GetComponent<BoxCollider>());

			Renderer rend = cube.GetComponent<Renderer>();
			rend.material.shader = shader;
			rend.material.SetColor("_Color", new Color(0.973f, 0.475f, 0f));
			rend.material.SetFloat("_Metallic", 0f);
			rend.material.SetFloat("_Glossiness", 0f);

			return cube;
		}
    }
}
