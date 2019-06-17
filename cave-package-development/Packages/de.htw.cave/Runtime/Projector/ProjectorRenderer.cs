using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Htw.Cave.Util;

namespace Htw.Cave.Projector
{
	[RequireComponent(typeof(ProjectorBrain))]
    public abstract class ProjectorRenderer : MonoBehaviour
    {
		protected ProjectorEyes eyes;
		protected ProjectorEmitter[] emitters;

		public static ProjectorRenderer Attach(ProjectorBrain brain, DeviceOutput output)
		{
			ProjectorRenderer renderer = brain.GetComponent<ProjectorRenderer>();

			if(renderer != null)
				GameObject.Destroy(renderer);

			switch(output)
			{
				case DeviceOutput.Mono:
					renderer = brain.gameObject.AddComponent<ProjectorRendererMono>();
					break;
				case DeviceOutput.Stereo:
					renderer = brain.gameObject.AddComponent<ProjectorRendererStereo>();
					break;
			}

			return renderer;
		}

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
