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
		private Text inTrackingAreaText;
		public Text InTrackingAreaText
		{
			get { return this.inTrackingAreaText; }
			set { this.inTrackingAreaText = value; }
		}

		[SerializeField]
		private Text visibilityText;
		public Text VisibilityText
		{
			get { return this.visibilityText; }
			set { this.visibilityText = value; }
		}

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
		private Rect area;

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
			this.area = this.manager.KinectBrain.Settings.TrackingAreaCentered();
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
			if(this.actor.InArea(this.area))
			{
				this.inTrackingAreaText.text = "Yes";

				if(this.actor.FullyVisible())
					this.visibilityText.text = "Full";
				else if(this.actor.HeadVisible())
					this.visibilityText.text = "Head";
				else if(this.actor.FeetVisible())
					this.visibilityText.text = "Feet";
				else
					this.visibilityText.text = "Bad";
			} else {
				this.inTrackingAreaText.text = "No";
				this.visibilityText.text = "Missing";
			}


			Mirror();
		}

#if UNITY_EDITOR
		public void Reset()
		{
			this.sphereShader = Shader.Find("Standard");
		}
#endif

		private void InstantiateJointSpheres()
		{
			for(int i = this.types.Length - 1; i >= 0; --i)
			{
				GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
				sphere.name = "Kinect Joint " + this.types[i].ToString();
				sphere.transform.localScale = new Vector3(0.15f, 0.15f, 0.15f);
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

			Vector3 direction = this.manager.transform.position - this.actor.transform.position;
			direction.y = 0f;

			foreach(GameObject sphere in this.spheres)
			{
				Vector3 worldPosition = this.manager.KinectBrain.transform.TransformPoint(this.actor.GetJointPosition(this.types[jointIndex++]));
				sphere.transform.position = worldPosition + direction;
			}
		}
    }
}
