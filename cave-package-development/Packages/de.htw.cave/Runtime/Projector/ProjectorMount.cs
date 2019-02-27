using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Htw.Cave.Util;

namespace Htw.Cave.Projector
{
#if UNITY_EDITOR
	[Flags]
	[System.Serializable]
	public enum ProjectorGizmos
	{
		Viewport = 1 << 0,
		Wireframe = 1 << 1,
		Anchors = 1 << 2
	}
#endif

	[AddComponentMenu("Htw.Cave/Projector/Projector Mount")]
    public sealed class ProjectorMount : MonoBehaviour
    {
		[SerializeField]
		private Transform target;
		public Transform Target
		{
			get { return this.target; }
			set { this.target = value; }
		}

#if UNITY_EDITOR
		[SerializeField]
		private ProjectorGizmos gizmos;
		public ProjectorGizmos Gizmos
		{
			get { return this.gizmos; }
			set { this.gizmos = value; }
		}
#endif

		private ProjectorEmitter[] emitters;

		public void Awake()
		{
			this.emitters = Get();
		}

		public void LateUpdate()
		{
			transform.position = this.target.position;
		}

		public ProjectorEmitter[] Get()
		{
			if(this.emitters == null || transform.hasChanged)
				this.emitters = base.GetComponentsInChildren<ProjectorEmitter>(false);

			return this.emitters;
		}

		public ProjectorEmitter[] GetOrdered()
		{
			return Get().OrderBy(emitter => emitter.Configuration.Order).ToArray();
		}

		public void AlignViewports(ViewportAxis axis)
		{
			ProjectorEmitter[] ordered = GetOrdered();
			float steps = 1f / ordered.Length;

			switch(axis)
			{
				case ViewportAxis.X:
					for(int i = 0; i < ordered.Length; ++i)
						ordered[i].GetComponent<Camera>().rect = new Rect(steps * i, 0f, steps, 1f);
					break;
				case ViewportAxis.Y:
					for(int i = 0; i < ordered.Length; ++i)
						ordered[i].GetComponent<Camera>().rect = new Rect(0f, steps * i, 1f, steps);
					break;
			}
		}
    }
}
