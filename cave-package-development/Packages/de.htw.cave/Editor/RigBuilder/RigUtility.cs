using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;
using JoyconLib;
using Htw.Cave.Projector;
using Htw.Cave.Kinect;
using Htw.Cave.Controller;
using Htw.Cave.Menu;
using Htw.Cave.ImportExport;
using Htw.Cave.Util;

namespace Htw.Cave.RigBuilder
{
    public static class RigUtility
    {
		public static List<GameObject> FindRig()
		{
			ProjectorBrain[] brains = Resources.FindObjectsOfTypeAll<ProjectorBrain>();
			JoyconManager[] joyconManagers = Resources.FindObjectsOfTypeAll<JoyconManager>();

			List<GameObject> list = new List<GameObject>();

			foreach(ProjectorBrain brain in brains)
				list.Add(brain.gameObject);

			foreach(JoyconManager joyconManager in joyconManagers)
				list.Add(joyconManager.gameObject);

			return list;
		}

		public static ProjectorBrain CreateBrain(Vector3 position, string name = "Projector Brain")
		{
			GameObject go = new GameObject(name);
			go.transform.position = position;

			return go.AddComponent<ProjectorBrain>();
		}

		public static ProjectorEyes CreateEyes(ProjectorBrain brain, string name = "Head")
		{
			GameObject go = new GameObject(name);
			go.transform.parent = brain.transform;
			go.transform.localPosition = new Vector3(0f, 1.8f, 0f);

			return go.AddComponent<ProjectorEyes>();
		}

		public static ProjectorMount CreateMount(ProjectorBrain brain, ProjectorEyes eyes, string name = "Projector Mount")
		{
			GameObject go = new GameObject(name);
			go.transform.parent = brain.transform;;
			go.transform.localPosition = new Vector3(0f, 1f, 0f);

			ProjectorMount mount = go.AddComponent<ProjectorMount>();
			mount.Target = eyes.transform;
			return mount;
		}

		public static ProjectorPlane CreatePlane(ProjectorBrain brain, Vector3 position, Vector3 angles, string name = "Projector Plane")
		{
			GameObject go = new GameObject(name);
			go.transform.parent = brain.transform;
			go.transform.localPosition = position;
			go.transform.localEulerAngles = angles;

			return go.AddComponent<ProjectorPlane>();
		}

		public static ProjectorEmitter CreateEmitter(ProjectorMount mount, ProjectorPlane plane, string name = "Projector Emitter")
		{
			GameObject go = new GameObject(name);
			go.transform.parent = mount.transform;
			go.transform.localPosition = Vector3.zero;
			go.transform.LookAt(plane.transform);
			go.AddComponent<Camera>();

			// small hack to archive 90 degree angles
			Vector3 angles = go.transform.localEulerAngles / 90f;
			angles = new Vector3(Mathf.Round(angles.x), Mathf.Round(angles.y), Mathf.Round(angles.z));
			go.transform.localEulerAngles = angles * 90f;

			ProjectorEmitter emitter = go.AddComponent<ProjectorEmitter>();
			emitter.Plane = plane;
			return emitter;
		}

		public static void CreateAssetsFolder(string folder)
		{
			if(!Directory.Exists(Path.Combine(Application.dataPath, folder)))
				Directory.CreateDirectory(Path.Combine(Application.dataPath, folder));
		}

		public static ProjectorConfiguration CreateConfiguration(ProjectorEmitter emitter, string path, string name = "Projector_Configuration")
		{
			ProjectorConfiguration asset = ScriptableObject.CreateInstance<ProjectorConfiguration>();

			AssetDatabase.CreateAsset(asset, "Assets/" + path + "/" + name + ".asset");
			EditorUtility.SetDirty(asset);

			return asset;
		}

		public static ProjectorSettings CreateSettings(string path, string name = "Projector_Settings")
		{
			ProjectorSettings asset = ScriptableObject.CreateInstance<ProjectorSettings>();

			AssetDatabase.CreateAsset(asset, "Assets/" + path + "/" + name + ".asset");
			EditorUtility.SetDirty(asset);

			return asset;
		}

		public static void DefaultConfiguration(ProjectorConfiguration configuration, float width, float height, int order, string displayName)
		{
			configuration.Width = width;
			configuration.Height = height;
			configuration.Order = order;
			configuration.DisplayName = displayName;
			configuration.Near = 0.1f;
			configuration.Far = 1000f;
			configuration.FieldOfView = 90f;
		}

		public static void DefaultSettings(ProjectorSettings settings)
		{
			settings.DeviceOutput = DeviceOutput.Stereo;
			settings.EqualizeImages = true;
			settings.StereoSeparation = 0.065f;
			settings.StereoConvergence = 10f;
		}

		public static void AttachController(ProjectorBrain brain, ProjectorEyes eyes, bool joycon)
		{
			GenericController controller = brain.gameObject.AddComponent<GenericController>();
			controller.Head = eyes.transform;
			controller.Speed = 5f;
			controller.Snappyness = 4f;
			controller.Sensitivity = 2f;

			brain.gameObject.AddComponent<MouseKeyboardUserControl>();

			if(joycon)
				brain.gameObject.AddComponent<JoyconUserControl>();
		}

		public static void AttachKinect(ProjectorBrain brain, ProjectorEyes eyes, Vector3 location, string path)
		{
			KinectBrain kinectBrain = brain.gameObject.AddComponent<KinectBrain>();
			eyes.gameObject.AddComponent<KinectActor>();
			eyes.gameObject.AddComponent<KinectHead>();

			KinectSettings asset = ScriptableObject.CreateInstance<KinectSettings>();
			asset.SensorLocation = location;
			asset.TrackingArea = new Rect(0f, 0f, 2f, 2f);

			AssetDatabase.CreateAsset(asset, "Assets/" + path + "/Kinect_Settings.asset");
			EditorUtility.SetDirty(asset);

			kinectBrain.Settings = asset;
		}

		public static void InstantiateJoycons()
		{
			GameObject go = new GameObject("JoyconLib");
			go.transform.position = Vector3.zero;

			go.AddComponent<JoyconManager>();
		}

		public static void InstantiateMenu(ProjectorBrain projectorBrain, ProjectorMount mount, KinectBrain kinectBrain, string name)
		{
			GameObject eventSystem = PrefabUtility.InstantiatePrefab("EventSystem");
			eventSystem.name = "EventSystem";

			GameObject menu = PrefabUtility.InstantiatePrefab("Menu");
			menu.name = name + " Menu";

			ProjectorEmitter[] emitters = mount.Get();

			Camera menuCamera = null;

			for(int i = emitters.Length - 1; i >= 0; --i)
			{
				if(emitters[i].Configuration.DisplayName == "Front")
				{
					menuCamera = emitters[i].GetComponent<Camera>();
					break;
				}
			}

			if(menuCamera == null)
				menuCamera = emitters[0].Camera.GetComponent<Camera>();

			MenuManager menuManager = menu.GetComponent<MenuManager>();
			menuManager.MenuCamera = menuCamera;
			menuManager.ProjectorBrain = projectorBrain;
			menuManager.KinectBrain = kinectBrain;
		}

		public static void InstantiateImportExport(ProjectorBrain projectorBrain, ProjectorMount mount, KinectBrain kinectBrain, string name, string path)
		{
			GameObject go = new GameObject(name + " ImportExport");
			go.transform.position = Vector3.zero;

			ImportExportSettings asset = ScriptableObject.CreateInstance<ImportExportSettings>();

			AssetDatabase.CreateAsset(asset, "Assets/" + path + "/Import_Export_Settings.asset");
			EditorUtility.SetDirty(asset);

			ImportExportSystem importExport = go.AddComponent<ImportExportSystem>();
			importExport.Settings = asset;
			importExport.EnableInEditor = false;
			importExport.Entries = new List<ImportExportEntry>();

			importExport.Entries.Add(new ImportExportEntry(projectorBrain.gameObject, projectorBrain.Settings));

			ProjectorEmitter[] emitters = mount.Get();

			for(int i = emitters.Length - 1; i >= 0; --i)
				importExport.Entries.Add(new ImportExportEntry(emitters[i].gameObject, emitters[i].Configuration));

			importExport.Entries.Add(new ImportExportEntry(kinectBrain.gameObject, kinectBrain.Settings));

		}
    }
}
