using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;
using Htw.Cave.Projector;

namespace Htw.Cave.RigBuilder
{
    public class RigWindow : EditorWindow
    {
		private const string Version = "2.0.3";
		private static RigInfo savedRigInfo;

		private bool rigInScene;
		private int windowMode;
		private RigInfo rigInfo;

		static RigWindow()
		{
			savedRigInfo = DefaultRigInfo();
		}

		private static RigInfo DefaultRigInfo()
		{
			RigInfo info = new RigInfo();
			info.template = RigTemplate.Basic;
			info.selection = (RigSelection)0;
			info.name = "Htw.Cave";
			info.width = 3f;
			info.height = 2.45f;
			info.length = 3f;
			info.position = Vector3.zero;
			info.buildReady = true;
			info.assetsFolder = "Htw.Cave Assets";
			info.sceneTools = true;
			info.menu = true;
			info.kinect = true;
			info.joycon = true;

			return info;
		}

		[MenuItem("GameObject/Htw.Cave/Rig Builder", false, 21)]
		public static void ShowWindow()
		{
			RigWindow window = EditorWindow.GetWindow<RigWindow>(false, "Rig Builder");
			//window.maxSize = new Vector2(330f, 220f);
			window.Show();
		}

		public void OnEnable()
		{
			this.rigInScene = FindObjectOfType<ProjectorBrain>() != null;
			this.windowMode = 0;
			this.rigInfo = savedRigInfo;
		}

		public void OnGUI()
		{
			this.windowMode = GUILayout.Toolbar(this.windowMode, new string[]{"Edit", "Preferences"});//EditorGUILayout.Popup(this.windowMode, new string[]{"Edit", "Preferences"}, EditorStyles.toolbarDropDown);

			EditorGUILayout.Separator();

			if(this.windowMode > 0)
			{
				if(GUILayout.Button("Reset Changes"))
				{
					savedRigInfo = DefaultRigInfo();
					this.rigInfo = savedRigInfo;
				}

				EditorGUILayout.LabelField("Version: " + Version, EditorStyles.miniLabel);
			} else {
				this.rigInfo.name = EditorGUILayout.TextField("Name", this.rigInfo.name);
				this.rigInfo.template = (RigTemplate)EditorGUILayout.EnumPopup("Template", this.rigInfo.template);

				TemplateToSelection();

				using(new EditorGUI.DisabledScope(this.rigInfo.template != RigTemplate.Custom))
					this.rigInfo.selection = (RigSelection)EditorGUILayout.EnumFlagsField(this.rigInfo.selection);

				EditorGUILayout.Separator();

				this.rigInfo.width = EditorGUILayout.FloatField("Width (m)", this.rigInfo.width);
				this.rigInfo.height = EditorGUILayout.FloatField("Height (m)", this.rigInfo.height);
				this.rigInfo.length = EditorGUILayout.FloatField("Length (m)", this.rigInfo.length);
				this.rigInfo.position = EditorGUILayout.Vector3Field("Position", this.rigInfo.position);

				EditorGUILayout.Separator();
				EditorGUILayout.LabelField("Assets", EditorStyles.boldLabel);

				this.rigInfo.assetsFolder = EditorGUILayout.TextField("Assets Folder", this.rigInfo.assetsFolder);
				this.rigInfo.menu = EditorGUILayout.Toggle("Game Menu (Calibration)", this.rigInfo.menu);
				this.rigInfo.buildReady = EditorGUILayout.Toggle("Build Ready", this.rigInfo.buildReady);
				this.rigInfo.sceneTools = EditorGUILayout.Toggle("Scene Tools", this.rigInfo.sceneTools);

				EditorGUILayout.Separator();
				EditorGUILayout.LabelField("Additional Features", EditorStyles.boldLabel);

				this.rigInfo.kinect = EditorGUILayout.Toggle("Microsoft Kinect 2.0 Addin", this.rigInfo.kinect);
				this.rigInfo.joycon = EditorGUILayout.Toggle("Nintendo Joycons", this.rigInfo.joycon);

				EditorGUILayout.Separator();

				EditorGUILayout.BeginHorizontal();
				using(new EditorGUI.DisabledScope(!this.rigInScene))
				{
					if(GUILayout.Button("Replace"))
					{
						RemoveRig();
						CreateRig();
						base.Close();
					}
				}

				if(GUILayout.Button("Create"))
				{
					CreateRig();
					base.Close();
				}
				EditorGUILayout.EndHorizontal();
			}
		}

		private void TemplateToSelection()
		{
			switch(this.rigInfo.template)
			{
				case RigTemplate.Basic:
					this.rigInfo.selection = RigSelection.Front | RigSelection.Bottom | RigSelection.Left | RigSelection.Right;
					break;
				case RigTemplate.Cube:
					this.rigInfo.selection = (RigSelection)~0;
					break;
			}
		}

		private void RemoveRig()
		{
			List<GameObject> list = RigUtility.FindRig();

			foreach(GameObject go in list)
				DestroyImmediate(go);
		}

		private void CreateRig()
		{
			RigBuilder.CreateRig(this.rigInfo);
			savedRigInfo = this.rigInfo;
		}
    }
}
