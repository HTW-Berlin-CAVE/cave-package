using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Windows.Kinect;

namespace Htw.Cave.Kinect
{
	[AddComponentMenu("Htw.Cave/Kinect/Kinect Brain")]
    public sealed class KinectBrain : MonoBehaviour
    {
		[SerializeField]
		private KinectSettings settings;
		public KinectSettings Settings
		{
			get { return this.settings; }
			set { this.settings = value; }
		}

		private KinectManager manager;
		private KinectActor actor;
		private Rect area;

		public void Awake()
		{
			this.actor = base.GetComponentInChildren<KinectActor>();
			Initialize();
		}

		public void Update()
		{
			this.manager.AcquireFrames();
			this.actor.Refresh(this.manager.Floor, this.manager.Bodies, this.manager.FaceFrameResults);

			if(!this.actor.InArea(this.area))
			{
				SearchActor(this.manager.Bodies);
				this.actor.Refresh(this.manager.Floor, this.manager.Bodies, this.manager.FaceFrameResults);
			}
		}

		private void Initialize()
		{
			KinectActor.CoordinateOrigin = this.settings.SensorLocation;

			this.manager = base.GetComponent<KinectManager>();

			if(this.manager == null)
				this.manager = base.gameObject.AddComponent<KinectManager>();

			this.area = this.settings.TrackingAreaCentered();
		}

		private void SearchActor(Body[] bodies)
		{
			switch(this.settings.ActorPriority)
			{
				case KinectActorPriority.ClosestToCenter:
					this.actor.ClosestToPoint(bodies, this.area.center);
					break;
				case KinectActorPriority.Shortest:
					this.actor.FindShortest(bodies);
					break;
				case KinectActorPriority.Tallest:
					this.actor.FindTallest(bodies);
					break;
			}
		}
    }
}
