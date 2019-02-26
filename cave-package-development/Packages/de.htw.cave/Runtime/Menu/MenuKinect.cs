using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Windows.Kinect;
using Htw.Cave.Kinect;

namespace Htw.Cave.Menu
{
    public sealed class MenuKinect : MonoBehaviour
    {
		[SerializeField]
		private Shader sphereShader;
		public Shader SphereShader
		{
			get { return this.sphereShader; }
			set { this.sphereShader = value; }
		}

		private MenuManager manager;
		private KinectActor actor;
		private List<GameObject> spheres;
		private JointType[] types;

		public void Awake()
		{
			this.manager = base.GetComponentInParent<MenuManager>();
			this.actor = this.manager.KinectBrain.GetComponentInChildren<KinectActor>();
			this.spheres = new List<GameObject>();
			this.types = new JointType[]{
				JointType.Head,
				JointType.HandLeft,
				JointType.HandRight,
				JointType.ElbowLeft,
				JointType.ElbowRight,
				JointType.ShoulderLeft,
				JointType.ShoulderRight,
				JointType.SpineShoulder,
				JointType.SpineMid,
				JointType.KneeLeft,
				JointType.KneeRight,
				JointType.AnkleLeft,
				JointType.AnkleRight,
				JointType.FootLeft,
				JointType.FootRight
			};
		}

		public void OnEnable()
		{
			InstantiateJointSpheres();
		}

		public void OnDisable()
		{
			DestroyJointSpheres();
		}

		public void Update()
		{
			Mirror();
		}

		private void InstantiateJointSpheres()
		{
			for(int i = this.types.Length - 1; i >= 0; --i)
			{
				GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
				sphere.name = "Kinect Joint " + this.types[i].ToString();
				Destroy(sphere.GetComponent<SphereCollider>());

				Renderer rend = sphere.GetComponent<Renderer>();
				rend.material.shader = this.sphereShader;
				rend.material.SetColor("_Color", new Color(0.973f, 0.475f, 0f));
				rend.material.SetFloat("_Metallic", 0f);
				rend.material.SetFloat("_Glossiness", 0f);

				this.spheres.Add(sphere);
			}
		}

		private void DestroyJointSpheres()
		{
			foreach(GameObject sphere in this.spheres)
				Destroy(sphere);

			this.spheres.Clear();
		}

		private void Mirror()
		{
			int jointIndex = 0;

			foreach(GameObject sphere in this.spheres)
			{
				sphere.transform.position = manager.transform.TransformPoint(this.actor.GetJointPosition(this.types[jointIndex++]));
				sphere.transform.position = sphere.transform.position * 0.5f - transform.forward * 0.5f;
			}
		}
    }
}
