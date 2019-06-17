using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;

namespace Htw.Cave.Menu
{
	[CustomEditor(typeof(MenuManager))]
    public class MenuManagerEditor : Editor
    {
		private MenuManager me;

		public void OnEnable()
		{
			this.me = (MenuManager)base.target;
		}

		public override void OnInspectorGUI()
		{
			EditorGUI.BeginChangeCheck();
			serializedObject.Update();

			EditorUtils.BrandField();

			DrawDefaultInspector();

			serializedObject.ApplyModifiedProperties();
			EditorGUI.EndChangeCheck();
		}
    }
}
