using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Htw.Cave.Projector
{
	[AddComponentMenu("Htw.Cave/Projector/Projector Plane")]
    public sealed class ProjectorPlane : MonoBehaviour
    {
		[SerializeField]
		private ProjectorEmitter emitter;
		public ProjectorEmitter Emitter
		{
			get { return this.emitter; }
			set { this.emitter = value; }
		}

		public Vector3[] TransformPlane(float width, float height)
		{
			return new Vector3[]{
				transform.TransformPoint(new Vector3(-width * 0.5f, height * 0.5f, 0f)),
				transform.TransformPoint(new Vector3(width * 0.5f, height * 0.5f, 0f)),
				transform.TransformPoint(new Vector3(width * 0.5f, -height * 0.5f, 0f)),
				transform.TransformPoint(new Vector3(-width * 0.5f, -height * 0.5f, 0f))
			};
		}

		public Vector3[] TransformPlane(Vector2[] points)
		{
			return new Vector3[]{
				transform.TransformPoint(new Vector3(points[0].x, points[0].y, 0f)),
				transform.TransformPoint(new Vector3(points[1].x, points[1].y, 0f)),
				transform.TransformPoint(new Vector3(points[2].x, points[2].y, 0f)),
				transform.TransformPoint(new Vector3(points[3].x, points[3].y, 0f))
			};
		}
    }
}
