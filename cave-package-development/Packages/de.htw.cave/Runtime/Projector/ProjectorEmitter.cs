using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityStandardAssets.ImageEffects;
using Htw.Cave.Math;
using Htw.Cave.Util;

namespace Htw.Cave.Projector
{
	[AddComponentMenu("Htw.Cave/Projector/Projector Emitter")]
	[RequireComponent(typeof(Camera))]
	[RequireComponent(typeof(ScreenOverlay))]
    public sealed class ProjectorEmitter : MonoBehaviour
    {
		[SerializeField]
		private ProjectorConfiguration configuration;
		public ProjectorConfiguration Configuration
		{
			get { return this.configuration; }
			set { this.configuration = value; }
		}

		[SerializeField]
		private ProjectorPlane plane;
		public ProjectorPlane Plane
		{
			get { return this.plane; }
			set { this.plane = value; }
		}

		private Camera cam;
		public Camera Camera
		{
			get { return this.cam; }
			set { this.cam = value; }
		}

		private ScreenOverlay screenOverlay;
		private Vector3[] transformPlane;
		private PixelMask pixelMask;
		private Matrix4x4 equalization;
		private bool invert;
		private Vector3 vr, vu, vn;

		public void Awake()
		{
			this.cam = base.GetComponent<Camera>();
			this.screenOverlay = base.GetComponent<ScreenOverlay>();
			this.transformPlane = this.plane.TransformPlane(this.configuration.Width, this.configuration.Height);
			this.equalization = Matrix4x4.identity;
			this.vr = this.vu = this.vn = Vector3.zero;
			InitializeCamera();
		}

#if UNITY_EDITOR
		public void Reset()
		{
			this.screenOverlay = base.GetComponent<ScreenOverlay>();
			this.screenOverlay.overlayShader = Shader.Find("Hidden/BlendModesOverlay");
			this.screenOverlay.blendMode = ScreenOverlay.OverlayBlendMode.AlphaBlend;
		}

		public void ApplyModificationsToCamera()
		{
			this.cam = base.GetComponent<Camera>();
			InitializeCamera();
		}
#endif

		public void PreRenderMono(ProjectorEyes eyes)
		{
			Vector3[] plane = TransformPlane();

			if(transform.hasChanged)
				Projection.HolographicFastPrecompute(plane[3], plane[2], plane[0], ref vr, ref vu, ref vn);

			Matrix4x4 holographic = Projection.HolographicFast(plane[3], plane[2], plane[0], this.vr, this.vu, this.vn, eyes.Anchor(), this.cam.nearClipPlane, this.cam.farClipPlane);
			this.cam.projectionMatrix = this.equalization * holographic;
		}

		public void PreRenderStereo(ProjectorEyes eyes)
		{
			Vector3[] plane = TransformPlane();

			if(transform.hasChanged)
				Projection.HolographicFastPrecompute(plane[3], plane[2], plane[0], ref vr, ref vu, ref vn);

			Matrix4x4 holographicLeft = Projection.HolographicFast(plane[3], plane[2], plane[0], vr, vu, vn, eyes.Left(this.invert), this.cam.nearClipPlane, this.cam.farClipPlane);
			Matrix4x4 holographicRight = Projection.HolographicFast(plane[3], plane[2], plane[0], vr, vu, vn, eyes.Right(this.invert), this.cam.nearClipPlane, this.cam.farClipPlane);

			this.cam.SetStereoProjectionMatrix(Camera.StereoscopicEye.Left, this.equalization * holographicLeft);
			this.cam.SetStereoProjectionMatrix(Camera.StereoscopicEye.Right, this.equalization * holographicRight);
		}

		public Vector3[] TransformPlane()
		{
			if(this.plane.transform.hasChanged)
				this.transformPlane = this.plane.TransformPlane(this.configuration.Width, this.configuration.Height);

			return this.transformPlane;
		}

		public void ApplyMask()
		{
			this.pixelMask = new PixelMask(this.configuration.EqualizationAnchors);

			Texture2D texture = new Texture2D(this.cam.pixelWidth, this.cam.pixelHeight);

			this.pixelMask.Apply(texture, Color.black);

			this.screenOverlay.texture = texture;
			this.screenOverlay.enabled = true;
		}

		public void DisableMask()
		{
			this.screenOverlay.enabled = false;
		}

		public void Equalize(bool recompute)
		{
			if(recompute)
				this.configuration.ComputeEqualization();

			this.equalization = this.configuration.EqualizationMatrix;
			ApplyMask();
		}

		public void SetStereoEffect(float convergence, float separation)
		{
			this.cam.stereoConvergence = convergence;
			this.cam.stereoSeparation = separation;
			this.invert = this.configuration.InvertStereo;
		}

		private void InitializeCamera()
		{
			this.cam.fieldOfView = this.configuration.FieldOfView;
			this.cam.nearClipPlane = this.configuration.Near;
			this.cam.farClipPlane = this.configuration.Far;
		}
    }
}
