using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Htw.Cave.Projector
{
    public sealed class ProjectorRendererStereo : ProjectorRenderer
    {
		protected override void RenderInternal()
		{
			for(int i = base.emitters.Length - 1; i >= 0; --i)
				base.emitters[i].PreRenderStereo(base.eyes);
		}
	}
}
