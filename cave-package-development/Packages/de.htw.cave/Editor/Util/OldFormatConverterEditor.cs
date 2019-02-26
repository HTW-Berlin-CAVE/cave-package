using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;

namespace Htw.Cave.Util
{
	[CustomEditor(typeof(OldFormatConverter))]
    public class OldFormatConverterEditor : Editor
    {
		private OldFormatConverter me;
		private double converted;
		private TextAsset textAsset;

		public void OnEnable()
		{
			this.me = (OldFormatConverter)base.target;
			this.converted = 0;
			this.textAsset = this.me.TxtConfiguration;
		}

		public override void OnInspectorGUI()
		{
			EditorGUI.BeginChangeCheck();
			serializedObject.Update();

			EditorUtils.BrandField();

			DrawDefaultInspector();

			if(GUI.changed)
			{
				if(this.me.TxtConfiguration != null && this.me.TxtConfiguration != this.textAsset)
					Convert();

				this.textAsset = this.me.TxtConfiguration;
			}

			if(this.me.TxtConfiguration == textAsset && textAsset != null)
			{
				if(GUILayout.Button("Convert Again"))
					Convert();
			}

			if(EditorApplication.timeSinceStartup < converted)
				EditorGUILayout.HelpBox("Converted successfully!", MessageType.Info);

			EditorGUILayout.HelpBox("This component is only required to convert old configuration files. Please remove it before building.", MessageType.Warning);

			serializedObject.ApplyModifiedProperties();
			EditorGUI.EndChangeCheck();
		}

		private void Convert()
		{
			this.me.Convert();
			AssetDatabase.ForceReserializeAssets(new string[] { AssetDatabase.GetAssetPath(this.me.Configuration) });
			this.converted = EditorApplication.timeSinceStartup + 1;
		}
    }
}
