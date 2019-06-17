using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;

namespace Htw.Cave
{
    public static class IconUtility
    {
		private static string rootPath = ContentUtility.GetPath("Icons");

		public static Texture2D LoadIcon(string icon)
		{
			return AssetDatabase.LoadAssetAtPath<Texture2D>(Path.Combine(rootPath, icon));
		}
    }
}
