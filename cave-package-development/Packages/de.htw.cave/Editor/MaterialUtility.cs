using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;

namespace Htw.Cave
{
    public static class MaterialUtility
    {
		private static string rootPath = ContentUtility.GetPath("Materials");

		public static Material LoadMaterial(string material)
		{
			return AssetDatabase.LoadAssetAtPath<Material>(Path.Combine(rootPath, material + ".mat"));
		}

		public static Material Tiles2x2()
		{
			return LoadMaterial("tiles_2x2");
		}

		public static Material Tiles3x3()
		{
			return LoadMaterial("tiles_3x3");
		}

		public static Material Tiles5x5()
		{
			return LoadMaterial("tiles_5x5");
		}

		public static Material Tiles10x10()
		{
			return LoadMaterial("tiles_10x10");
		}

		public static Material Tiles20x20()
		{
			return LoadMaterial("tiles_20x20");
		}
    }
}
