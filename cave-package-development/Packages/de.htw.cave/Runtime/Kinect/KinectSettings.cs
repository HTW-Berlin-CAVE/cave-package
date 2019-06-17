using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Htw.Cave.Kinect
{
	[CreateAssetMenu(fileName = "New Kinect Settings", menuName = "Htw.Cave/Kinect Settings", order = 30)]
    public class KinectSettings : ScriptableObject
    {
		[SerializeField]
		private Vector3 sensorLocation;
		public Vector3 SensorLocation
		{
			get { return sensorLocation; }
			set { this.sensorLocation = value; }
		}

		[SerializeField]
		private Rect trackingArea;
		public Rect TrackingArea
		{
			get { return trackingArea; }
			set { this.trackingArea = value; }
		}

		[SerializeField]
		private KinectActorPriority actorPriority;
		public KinectActorPriority ActorPriority
		{
			get { return actorPriority; }
			set { this.actorPriority = value; }
		}

		[SerializeField]
		private bool hdFace;
		public bool HDFace
		{
			get { return hdFace; }
			set { this.hdFace = value; }
		}

		public Rect TrackingAreaCentered()
		{
			Rect centered = this.trackingArea;
			centered.center -= centered.size * 0.5f;
			return centered;
		}
    }
}
