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
		private Text enableMirrorText;
		public Text EnableMirrorText
		{
			get { return this.enableMirrorText; }
			set { this.enableMirrorText = value; }
		}

		[SerializeField]
		private Text showAreaText;
		public Text ShowAreaText
		{
			get { return this.showAreaText; }
			set { this.showAreaText = value; }
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
		private List<GameObject> mirrorJoints;
		private GameObject trackingArea;
		private JointType[] types;
		private Rect area;

		public void Awake()
		{
			this.manager = base.GetComponentInParent<MenuManager>();
			this.actor = this.manager.KinectBrain.GetComponentInChildren<KinectActor>();
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
		}

		public void OnDisable()
		{
			HideTrackingArea();
			DestroyMirrorJoints();
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

			if(this.mirrorJoints != null)
				Mirror();
		}

#if UNITY_EDITOR
		public void Reset()
		{
			this.sphereShader = Shader.Find("Standard");
		}
#endif

		public void ToggleMirrorJoints()
		{
			if(this.mirrorJoints == null)
				CreateMirrorJoints();
			else
				DestroyMirrorJoints();
		}

		public void ToggleTrackingArea()
		{
			if(this.trackingArea == null)
				ShowTrackingArea();
			else
				HideTrackingArea();
		}

		private void CreateMirrorJoints()
		{
			this.enableMirrorText.text = "Disable Mirror";

			this.mirrorJoints = MenuKinectHelper.CreateMirrorJoints(this.types, this.sphereShader);
		}

		private void DestroyMirrorJoints()
		{
			this.enableMirrorText.text = "Enable Mirror";

			if(this.mirrorJoints == null)
				return;

			foreach(GameObject sphere in this.mirrorJoints)
				Destroy(sphere);

			this.mirrorJoints = null;
		}

		private void ShowTrackingArea()
		{
			this.showAreaText.text = "Hide Tracking Area";

			this.trackingArea = MenuKinectHelper.CreateTrackingArea(this.manager.KinectBrain.transform, this.area, this.sphereShader);
		}

		private void HideTrackingArea()
		{
			this.showAreaText.text = "Show Tracking Area";

			if(this.trackingArea == null)
				return;

			Destroy(this.trackingArea);
		}

		private void Mirror()
		{
			int jointIndex = 0;

			Vector3 direction = this.manager.transform.position - this.actor.transform.position;
			direction.y = 0f;

			foreach(GameObject sphere in this.mirrorJoints)
			{
				Vector3 worldPosition = this.manager.KinectBrain.transform.TransformPoint(this.actor.GetJointPosition(this.types[jointIndex++]));
				sphere.transform.position = worldPosition + direction;
			}
		}
    }
}
