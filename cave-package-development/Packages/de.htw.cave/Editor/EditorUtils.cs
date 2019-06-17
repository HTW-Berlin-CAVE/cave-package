using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;

namespace Htw.Cave
{
    public static class EditorUtils
    {
		private static Texture2D brand = IconUtility.LoadIcon("htw_cave_brand.png");

		public static void BrandField()
		{
			EditorGUILayout.BeginVertical();
			EditorGUILayout.LabelField(new GUIContent(brand), EditorStyles.centeredGreyMiniLabel, GUILayout.ExpandWidth(true), GUILayout.MinHeight(75));
			EditorGUILayout.EndVertical();
		}

		public static void BrandHelpField()
		{
			EditorGUILayout.BeginVertical();
			GUIStyle style = EditorStyles.helpBox;
			style.alignment = TextAnchor.MiddleCenter;
			EditorGUILayout.LabelField(new GUIContent(brand), style, GUILayout.ExpandWidth(true), GUILayout.MinHeight(75));
			EditorGUILayout.EndVertical();
		}

		public static void BrandFooterField()
		{
			EditorGUILayout.BeginVertical();
			GUIStyle style = EditorStyles.helpBox;
			style.alignment = TextAnchor.MiddleCenter;
			EditorGUILayout.LabelField("2018 HTW Berlin", style);
			EditorGUILayout.EndVertical();
		}
    }
}
