using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Htw.Cave.Projector
{
	[AddComponentMenu("Htw.Cave/Projector/Projector Eyes")]
    public sealed class ProjectorEyes : MonoBehaviour
    {
		private Transform left;
		private Transform right;

		public void Awake()
		{
			Initialize();
		}

		public void SetEyeSeparation(float separation)
		{
			Vector3 s = new Vector3(separation * 0.5f, 0f, 0f);

			this.left.localPosition = -s;
			this.right.localPosition = s;
		}

		private void Initialize()
		{
			this.left = (new GameObject("Left Eye")).transform;
			this.left.parent = transform;

			this.right = (new GameObject("Right Eye")).transform;
			this.right.parent = transform;
		}

		public Vector3 Anchor()
		{
			return transform.position;
		}

		public Vector3 Left(bool invert = false)
		{
			if(invert)
				return Right();

			return this.left.position;
		}

		public Vector3 Right(bool invert = false)
		{
			if(invert)
				return Left();

			return this.right.position;
		}
    }
}
