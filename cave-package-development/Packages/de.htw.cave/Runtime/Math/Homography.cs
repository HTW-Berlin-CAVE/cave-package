using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Linq;
using UnityEngine;
using OpenCvSharp;
using Htw.Cave.OpenCV;

namespace Htw.Cave.Math
{
    public static class Homography
    {
		public static Vector2[] Quad =>	new Vector2[] {
			new Vector2(-1f, 1f),
			new Vector2(1f, 1f),
			new Vector2(1f, -1f),
			new Vector2(-1f, -1f)
		};

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3[] Find(IEnumerable<Vector2> source, IEnumerable<Vector2> destination)
		{
			return Cv2.FindHomography(source.ToPoint2d(), destination.ToPoint2d()).ToVector3s();
		}
    }
}
