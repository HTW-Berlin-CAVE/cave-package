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
			this.emitters = base.GetComponentsInChildren<ProjectorEmitter>(false);
		}

		public void LateUpdate()
		{
			transform.position = this.target.position;
		}

		public ProjectorEmitter[] Get()
		{
			if(transform.hasChanged)
				this.emitters = base.GetComponentsInChildren<ProjectorEmitter>(false);

			return this.emitters;
		}

		public ProjectorEmitter[] GetOrdered()
		{
			return Get().OrderBy(emitter => emitter.Configuration.Order).ToArray();
		}
    }
}
