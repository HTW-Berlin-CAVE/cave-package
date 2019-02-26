using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Htw.Cave.Util;

namespace Htw.Cave.Projector
{
	[CreateAssetMenu(fileName = "New Projector Settings", menuName = "Htw.Cave/Projector Settings", order = 10)]
    public class ProjectorSettings : ScriptableObject
    {
		[SerializeField]
		private DeviceOutput deviceOutput;
		public DeviceOutput DeviceOutput
		{
			get { return this.deviceOutput; }
			set { this.deviceOutput = value; }
		}

		[SerializeField]
		private ViewportAxis viewportAxis;
		public ViewportAxis ViewportAxis
		{
			get { return this.viewportAxis; }
			set { this.viewportAxis = value; }
		}

		[SerializeField]
		private float stereoSeparation;
		public float StereoSeparation
		{
			get { return this.stereoSeparation; }
			set { this.stereoSeparation = value; }
		}

		[SerializeField]
		private float stereoConvergence;
		public float StereoConvergence
		{
			get { return this.stereoConvergence; }
			set { this.stereoConvergence = value; }
		}

		[SerializeField]
		private bool equalizeImages;
		public bool EqualizeImages
		{
			get { return this.equalizeImages; }
			set { this.equalizeImages = value; }
		}

		[SerializeField]
		private bool reloadEqualization;
		public bool ReloadEqualization
		{
			get { return this.reloadEqualization; }
			set { this.reloadEqualization = value; }
		}
    }
}
