using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;

namespace Htw.Cave
{
    public static class ContentUtility
    {
		private const string contentPath = "Packages/de.htw.cave/Content";

		public static string GetPath(string path)
		{
			return Path.Combine(contentPath, path);
		}
    }
}
