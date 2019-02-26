using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;

namespace Htw.Cave
{
    public static class PrefabUtility
    {
		public static GameObject LoadPrefab(string prefab)
		{
			return AssetDatabase.LoadAssetAtPath<GameObject>(Path.Combine("Packages/de.htw.cave/Prefabs", prefab + ".prefab"));
		}

		public static GameObject InstantiatePrefab(string prefab)
		{
			return MonoBehaviour.Instantiate<GameObject>(LoadPrefab(prefab));
		}
    }
}
