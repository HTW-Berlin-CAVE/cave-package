using System;
using System.IO;
using UnityEngine;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;

namespace Htw.Cave.Kinect
{
	public class KinectAddinPreprocessBuild : IPreprocessBuildWithReport
	{
		private const string PackagesDirName = KinectAddinData.PackagesDirName;
		private const string PackageDirName = KinectAddinData.PackageDirName;
		private const string KinectDirName = KinectAddinData.KinectDirName;
		private const string PluginsDirName = KinectAddinData.PluginsDirName;

		public int callbackOrder { get { return 1; } }

		public void OnPreprocessBuild(BuildReport report)
		{
			// moving kinect dlls from package space to assets space
			// because kinect helpers copy all required dlls from assets space to
			// the final plugin location.
			var separator = Path.DirectorySeparatorChar;
			var packageDir = Path.Combine(Application.dataPath, ".." + separator + PackagesDirName + separator + PackageDirName);
			var kinectDir = Path.Combine(packageDir, KinectDirName);
			var targetDir = Application.dataPath + separator + PluginsDirName;

			Debug.Log("Preparing Kinect build. Moving " + kinectDir + " to " + targetDir);
			KinectAddinData.Move(new DirectoryInfo(kinectDir), new DirectoryInfo(targetDir));
			KinectAddinData.Import();
		}
	}
}
