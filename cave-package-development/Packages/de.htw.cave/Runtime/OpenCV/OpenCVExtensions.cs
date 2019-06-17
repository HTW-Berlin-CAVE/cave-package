using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Linq;
using UnityEngine;
using OpenCvSharp;

namespace Htw.Cave.OpenCV
{
    public static class OpenCVExtensions
    {
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector2 ToVector2(this Point2d source)
		{
			return new Vector2((float)source.X, (float)source.Y);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<Vector2> ToVector2(this IEnumerable<Point2d> source)
        {
            return source.Select(point => point.ToVector2());
        }

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Point2d ToPoint2d(this Vector2 source)
		{
			return new Point2d((double)source.x, (double)source.y);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Point2d> ToPoint2d(this IEnumerable<Vector2> source)
		{
			return source.Select(vector => vector.ToPoint2d());
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3[] ToVector3s(this Mat mat)
		{
			double[,] data = new double[mat.Rows, mat.Cols];
			mat.GetArray(0, 0, data);

			return new Vector3[] {
				new Vector3((float)data[0, 0], (float)data[0, 1], (float)data[0, 2]),
				new Vector3((float)data[1, 0], (float)data[1, 1], (float)data[1, 2]),
				new Vector3((float)data[2, 0], (float)data[2, 1], (float)data[2, 2])
			};
		}
    }
}
