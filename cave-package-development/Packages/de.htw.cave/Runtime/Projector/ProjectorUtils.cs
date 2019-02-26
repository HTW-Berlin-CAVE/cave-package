using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Htw.Cave.Util;

namespace Htw.Cave.Projector
{
    public static class ProjectorUtils
    {
		public static void AttachRenderer(ProjectorBrain brain, DeviceOutput output)
		{
			switch(output)
			{
				case DeviceOutput.Mono:
					brain.gameObject.AddComponent<ProjectorRendererMono>();
					break;
				case DeviceOutput.Stereo:
					brain.gameObject.AddComponent<ProjectorRendererStereo>();
					break;
			}
		}

		public static void AlignViewports(ProjectorEmitter[] emitters, ViewportAxis axis)
		{
			float steps = 1f / emitters.Length;
			float offset = 0f;

			switch(axis)
			{
				case ViewportAxis.X:
					for(int i = 0; i < emitters.Length; ++i)
					{
						emitters[i].GetComponent<Camera>().rect = new Rect(offset, 0f, steps, 1f);
						offset += steps;
					}
					break;
				case ViewportAxis.Y:
					for(int i = 0; i < emitters.Length; ++i)
					{
						emitters[i].GetComponent<Camera>().rect = new Rect(0f, offset, 1f, steps);
						offset += steps;
					}
					break;
			}
		}
    }
}
