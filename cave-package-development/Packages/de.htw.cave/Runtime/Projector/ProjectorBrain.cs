using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Htw.Cave.ImportExport;

namespace Htw.Cave.Projector
{
	[AddComponentMenu("Htw.Cave/Projector/Projector Brain")]
    public sealed class ProjectorBrain : MonoBehaviour
    {
		[SerializeField]
		private ProjectorSettings settings;
		public ProjectorSettings Settings
		{
			get { return this.settings; }
			set { this.settings = value; }
		}

		private ProjectorEyes eyes;
		private ProjectorMount mount;
		private new ProjectorRenderer renderer;
		private ProjectorEmitter[] emitters;

		public void Awake()
		{
			this.eyes = base.GetComponentInChildren<ProjectorEyes>();
			this.mount = base.GetComponentInChildren<ProjectorMount>();
			this.emitters = this.mount.GetOrdered();
		}

		public void Start()
		{
			LateInitialize();
		}

		public void LateUpdate()
		{
			this.renderer.Render();
		}

		private void LateInitialize()
		{
			this.eyes.SetEyeSeparation(this.settings.StereoSeparation);
			this.mount.AlignViewports(this.settings.ViewportAxis);

			for(int i = this.emitters.Length - 1; i >= 0; --i)
			{
				this.emitters[i].SetStereoEffect(this.settings.StereoConvergence, this.settings.StereoSeparation);

				if(this.settings.EqualizeImages)
					this.emitters[i].Equalize(this.settings.ReloadEqualization);
				else
					this.emitters[i].DisableMask();
			}

			this.renderer = ProjectorRenderer.Attach(this, this.settings.DeviceOutput);
			this.renderer.SetRenderTargets(this.eyes, this.emitters);
		}
    }
}
