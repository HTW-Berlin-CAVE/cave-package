using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Htw.Cave.Projector
{
	[RequireComponent(typeof(ProjectorBrain))]
    public abstract class ProjectorRenderer : MonoBehaviour
    {
		protected ProjectorEyes eyes;
		protected ProjectorEmitter[] emitters;

		public void SetRenderTargets(ProjectorEyes eyes, ProjectorEmitter[] emitters)
		{
			this.eyes = eyes;
			this.emitters = emitters;
		}

		public void Render()
		{
			RenderInternal();
		}

		protected abstract void RenderInternal();
	}
}
