using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Htw.Cave.Util
{
    public sealed class PixelMask
    {
		public Vector2[] Points { get; set; }

		public PixelMask()
		{
			this.Points = null;
		}

		public PixelMask(Vector2[] points)
		{
			this.Points = points;
		}

		public void Apply(Texture2D texture, Color color)
		{
			Color[] pixels = texture.GetPixels();

			for(int x = 0; x < texture.width; ++x)
			{
				for(int y = 0; y < texture.height; ++y)
				{
					int offset = y * texture.width + x;
					pixels[offset] = color;
					Vector2 point = new Vector2((float)x / (float)texture.width, (float)y / (float)texture.height);

					if(Contains(point))
						pixels[offset].a = 0f;
				}
			}

			texture.SetPixels(pixels);
			texture.Apply();
		}

		private bool Contains(Vector2 point)
		{
			for(int i = 1; i <= this.Points.Length; ++i)
			{
				Vector2[] line = { this.Points[i - 1], this.Points[i % this.Points.Length] };

				if(!IsOnRightSide(line, point))
					return false;
			}

			return true;
		}

		private bool IsOnRightSide(Vector2[] line, Vector2 point)
		{
			return (point.y - line[0].y) * (line[1].x - line[0].x) - (point.x - line[0].x) * (line[1].y - line[0].y) <= 0;
		}
    }
}
