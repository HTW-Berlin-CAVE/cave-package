using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;
using Windows.Kinect;
using Microsoft.Kinect.Face;

namespace Htw.Cave.Kinect
{
    public static class KinectActorExtensions
    {
		public static bool InArea(this KinectActor actor, Rect area)
		{
			JointType jointType = JointType.SpineMid;

			if(actor.HeadVisible())
				jointType = JointType.Head;

			Vector3 position = actor.GetJointPosition(jointType);

			return area.Contains(new Vector2(position.x, position.z));
		}

		public static void FindShortest(this KinectActor actor, Body[] bodies)
		{
			float z = float.PositiveInfinity;
			int index = -1;

			for(int i = bodies.Length - 1; i >= 0; --i)
			{
				if(!bodies[i].IsTracked)
					continue;

				Vector3 position = bodies[i].JointPositionRealSpace(JointType.Head, KinectActor.CoordinateOrigin, actor.Floor);

				if(position.z < z)
				{
					z = position.z;
					index = i;
				}
			}

			if(index >= 0)
				actor.Refresh(index);
		}

		public static void FindTallest(this KinectActor actor, Body[] bodies)
		{
			float z = 0f;
			int index = -1;

			for(int i = bodies.Length - 1; i >= 0; --i)
			{
				if(!bodies[i].IsTracked)
					continue;

				Vector3 position = bodies[i].JointPositionRealSpace(JointType.Head, KinectActor.CoordinateOrigin, actor.Floor);

				if(position.z > z)
				{
					z = position.z;
					index = i;
				}
			}

			if(index >= 0)
				actor.Refresh(index);
		}

		public static void ClosestToPoint(this KinectActor actor, Body[] bodies, UnityEngine.Vector3 point, JointType jointType = JointType.Head)
		{
			float distance = float.PositiveInfinity;
			int index = -1;

			for(int i = bodies.Length - 1; i >= 0; --i)
			{
				if(!bodies[i].IsTracked)
					continue;

				Vector3 position = bodies[i].JointPositionRealSpace(jointType, KinectActor.CoordinateOrigin, actor.Floor);
				float sqr = (position - point).sqrMagnitude;

				if(sqr < distance)
				{
					distance = sqr;
					index = i;
				}
			}

			if(index >= 0)
				actor.Refresh(index);
		}
    }
}
