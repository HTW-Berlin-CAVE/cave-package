using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Htw.Cave.Math;

namespace Htw.Cave.Projector
{
	[CreateAssetMenu(fileName = "New Projector Configuration", menuName = "Htw.Cave/Projector Configuration", order = 20)]
    public class ProjectorConfiguration : ScriptableObject
    {
		[SerializeField]
		private string displayName;
		public string DisplayName
		{
			get { return this.displayName; }
			set { this.displayName = value; }
		}

		[SerializeField]
		private int order;
		public int Order
		{
			get { return this.order; }
			set { this.order = value; }
		}

		[SerializeField]
		private float width;
		public float Width
		{
			get { return this.width; }
			set { this.width = value; }
		}

		[SerializeField]
		private float height;
		public float Height
		{
			get { return this.height; }
			set { this.height = value; }
		}

		[SerializeField]
		private float near;
		public float Near
		{
			get { return this.near; }
			set { this.near = value; }
		}

		[SerializeField]
		private float far;
		public float Far
		{
			get { return this.far; }
			set { this.far = value; }
		}

		[SerializeField]
		private float fieldOfView;
		public float FieldOfView
		{
			get { return this.fieldOfView; }
			set { this.fieldOfView = value; }
		}

		[SerializeField]
		private bool invertStereo;
		public bool InvertStereo
		{
			get { return this.invertStereo; }
			set { this.invertStereo = value; }
		}

		[SerializeField]
		private Vector2[] equalizationAnchors;
		public Vector2[] EqualizationAnchors
		{
			get { return this.equalizationAnchors; }
			set { this.equalizationAnchors = value; }
		}

		[SerializeField]
		private Vector4[] equalizationMatrix;
		public Matrix4x4 EqualizationMatrix
		{
			get { return new Matrix4x4(this.equalizationMatrix[0], this.equalizationMatrix[1], this.equalizationMatrix[2], this.equalizationMatrix[3]); }
			set { this.equalizationMatrix = new Vector4[]{value.GetColumn(0), value.GetColumn(1), value.GetColumn(2), value.GetColumn(3)}; }
		}

		public void ComputeEqualization()
		{
			Vector2[] destination = ScaledEqualizationAnchors();

			Vector3[] homography = Homography.Find(Homography.Quad, destination);
			this.EqualizationMatrix = Projection.Bimber(homography);
		}

		public Vector2[] ScaledEqualizationAnchors()
		{
			Vector2[] scaled = new Vector2[this.equalizationAnchors.Length];

			for(int i = scaled.Length - 1; i >= 0; --i)
				scaled[i] = this.equalizationAnchors[i] * 2f - Vector2.one;

			return scaled;
		}
    }
}
