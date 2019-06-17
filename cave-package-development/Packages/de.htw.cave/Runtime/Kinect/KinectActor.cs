using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Windows.Kinect;
using Microsoft.Kinect.Face;

namespace Htw.Cave.Kinect
{
	[AddComponentMenu("Htw.Cave/Kinect/Kinect Actor")]
    public sealed class KinectActor : MonoBehaviour
    {
		public static UnityEngine.Vector3 CoordinateOrigin { get; set; }

		public UnityEngine.Vector4 Floor { get; private set; }
		public Body Body { get; private set; }
		public FaceFrameResult Face { get; private set; }
		public FaceAlignment FaceAlignment { get; private set; }
		public FaceModel FaceModel { get; private set; }

		private int id;

		public void Awake()
		{
			this.Body = null;
			this.Face = null;
			this.FaceAlignment = null;
			this.FaceModel = null;
			this.id = 0;
		}

		public void Refresh(Windows.Kinect.Vector4 floor, Body[] bodies, FaceFrameResult[] faces)
		{
			this.Floor = floor.ToUnityVector4();
			this.Face = faces[id];
			this.Body = bodies[id];
		}

		public void Refresh(int id)
		{
			this.id = id;
		}

		public bool FullyVisible()
		{
			return this.Body.ClippedEdges.HasFlag(FrameEdges.None);
		}

		public bool HeadVisible()
		{
			return !this.Body.ClippedEdges.HasFlag(FrameEdges.Top);
		}

		public bool FeetVisible()
		{
			return !this.Body.ClippedEdges.HasFlag(FrameEdges.Bottom);
		}

		public UnityEngine.Vector3 GetJointPosition(JointType jointType)
		{
			return this.Body.JointPositionRealSpace(jointType, CoordinateOrigin, this.Floor);
		}

		public UnityEngine.Quaternion GetJointRotation(JointType jointType)
		{
			return this.Body.JointRotation(jointType);
		}

		public UnityEngine.Vector3 GetHeadPosition()
		{
			return GetJointPosition(JointType.Head);
		}

		public UnityEngine.Vector3 GetShoulderPosition()
		{
			return GetJointPosition(JointType.SpineShoulder);
		}

		public UnityEngine.Quaternion GetFaceRotation()
		{
			return this.Face.FaceRotation();
		}

		public UnityEngine.Quaternion GetShoulderRotation()
		{
			return Quaternion.Lerp(GetJointRotation(JointType.ShoulderLeft), GetJointRotation(JointType.ShoulderRight), 0.5f);
		}
    }
}
