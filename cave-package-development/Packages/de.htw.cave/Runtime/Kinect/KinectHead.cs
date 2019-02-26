using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Windows.Kinect;
using Microsoft.Kinect.Face;

namespace Htw.Cave.Kinect
{
	[AddComponentMenu("Htw.Cave/Kinect/Kinect Head")]
	[RequireComponent(typeof(KinectActor))]
    public sealed class KinectHead : MonoBehaviour
    {
		private KinectActor actor;

		public void Awake()
		{
			this.actor = base.GetComponent<KinectActor>();
		}

		public void Update()
		{
			if(this.actor.HeadVisible())
			{
				transform.localRotation = Quaternion.Slerp(transform.localRotation, this.actor.GetFaceRotation(), Time.deltaTime * 0.8f);
				transform.localPosition = this.actor.GetHeadPosition();
			} else {
				transform.localRotation = Quaternion.Slerp(transform.localRotation, this.actor.GetShoulderRotation(), Time.deltaTime * 0.8f);
				transform.localPosition = this.actor.GetShoulderPosition() + Vector3.up * 0.3f;
			}
		}
    }
}
