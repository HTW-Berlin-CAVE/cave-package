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
using Htw.Cave.Util;

namespace Htw.Cave.RigBuilder
{
    public static class RigBuilder
    {
		public static void CreateRig(RigInfo rigInfo)
		{
			RigUtility.CreateAssetsFolder(rigInfo.assetsFolder);

			ProjectorBrain brain = RigUtility.CreateBrain(rigInfo.position, rigInfo.name);
			ProjectorEyes eyes = RigUtility.CreateEyes(brain);
			ProjectorMount mount = RigUtility.CreateMount(brain, eyes);

			if(rigInfo.selection.HasFlag(RigSelection.Front))
			{
				ProjectorPlane plane = RigUtility.CreatePlane(brain, new Vector3(0, rigInfo.height * 0.5f, rigInfo.length * 0.5f), Vector3.zero, "Projector Plane Front");
				ProjectorEmitter emitter = RigUtility.CreateEmitter(mount, plane, "Projector Emitter Front");

				if(rigInfo.buildReady)
				{
					emitter.Configuration = RigUtility.CreateConfiguration(emitter, rigInfo.assetsFolder, "Projector_Configuration_Front");
					RigUtility.DefaultConfiguration(emitter.Configuration, rigInfo.width, rigInfo.height, 2, "Front");
				}
			}

			if(rigInfo.selection.HasFlag(RigSelection.Back))
			{
				ProjectorPlane plane = RigUtility.CreatePlane(brain, new Vector3(0, rigInfo.height * 0.5f, -rigInfo.length * 0.5f), Vector3.up * 180f, "Projector Plane Back");
				ProjectorEmitter emitter = RigUtility.CreateEmitter(mount, plane, "Projector Emitter Back");

				if(rigInfo.buildReady)
				{
					emitter.Configuration = RigUtility.CreateConfiguration(emitter, rigInfo.assetsFolder, "Projector_Configuration_Back");
					RigUtility.DefaultConfiguration(emitter.Configuration, rigInfo.width, rigInfo.height, 4, "Back");
				}
			}

			if(rigInfo.selection.HasFlag(RigSelection.Top))
			{
				ProjectorPlane plane = RigUtility.CreatePlane(brain, new Vector3(0, rigInfo.height, 0), Vector3.right * -90f, "Projector Plane Top");
				ProjectorEmitter emitter = RigUtility.CreateEmitter(mount, plane, "Projector Emitter Top");

				if(rigInfo.buildReady)
				{
					emitter.Configuration = RigUtility.CreateConfiguration(emitter, rigInfo.assetsFolder, "Projector_Configuration_Top");
					RigUtility.DefaultConfiguration(emitter.Configuration, rigInfo.width, rigInfo.length, 5, "Top");
				}
			}

			if(rigInfo.selection.HasFlag(RigSelection.Bottom))
			{
				ProjectorPlane plane = RigUtility.CreatePlane(brain, Vector3.zero, Vector3.right * 90f, "Projector Plane Bottom");
				ProjectorEmitter emitter = RigUtility.CreateEmitter(mount, plane, "Projector Emitter Bottom");

				if(rigInfo.buildReady)
				{
					emitter.Configuration = RigUtility.CreateConfiguration(emitter, rigInfo.assetsFolder, "Projector_Configuration_Bottom");
					RigUtility.DefaultConfiguration(emitter.Configuration, rigInfo.width, rigInfo.length, 6, "Bottom");
				}
			}

			if(rigInfo.selection.HasFlag(RigSelection.Left))
			{
				ProjectorPlane plane = RigUtility.CreatePlane(brain, new Vector3(-rigInfo.width * 0.5f, rigInfo.height * 0.5f, 0), Vector3.up * -90f, "Projector Plane Left");
				ProjectorEmitter emitter = RigUtility.CreateEmitter(mount, plane, "Projector Emitter Left");

				if(rigInfo.buildReady)
				{
					emitter.Configuration = RigUtility.CreateConfiguration(emitter, rigInfo.assetsFolder, "Projector_Configuration_Left");
					RigUtility.DefaultConfiguration(emitter.Configuration, rigInfo.length, rigInfo.height, 1, "Left");
				}
			}

			if(rigInfo.selection.HasFlag(RigSelection.Right))
			{
				ProjectorPlane plane = RigUtility.CreatePlane(brain, new Vector3(rigInfo.width * 0.5f, rigInfo.height * 0.5f, 0), Vector3.up * 90f, "Projector Plane Right");
				ProjectorEmitter emitter = RigUtility.CreateEmitter(mount, plane, "Projector Emitter Right");

				if(rigInfo.buildReady)
				{
					emitter.Configuration = RigUtility.CreateConfiguration(emitter, rigInfo.assetsFolder, "Projector_Configuration_Right");
					RigUtility.DefaultConfiguration(emitter.Configuration, rigInfo.length, rigInfo.height, 3, "Right");
				}
			}

			if(rigInfo.buildReady)
			{
				brain.Settings = RigUtility.CreateSettings(rigInfo.assetsFolder);
				RigUtility.DefaultSettings(brain.Settings);
				RigUtility.AttachController(brain, eyes, rigInfo.joycon);
			}

			if(rigInfo.sceneTools)
				mount.Gizmos = ProjectorGizmos.Viewport | ProjectorGizmos.Wireframe;

			if(rigInfo.kinect)
				RigUtility.AttachKinect(brain, eyes, new Vector3(0f, rigInfo.height + 0.25f, rigInfo.length * 0.5f), rigInfo.assetsFolder);

			RigUtility.InstantiateImportExport(brain, mount, brain.GetComponent<KinectBrain>(), rigInfo.name, rigInfo.assetsFolder);

			if(rigInfo.menu)
				RigUtility.InstantiateMenu(brain, mount, brain.GetComponent<KinectBrain>(), rigInfo.name);

			if(rigInfo.joycon)
				RigUtility.InstantiateJoycons();

			AssetDatabase.SaveAssets();
			AssetDatabase.Refresh();
		}
    }
}
