using System;
using System.IO;
using UnityEngine;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;

namespace Htw.Cave.Kinect
{
	public class KinectAddinPostprocessBuild : IPostprocessBuildWithReport
	{
		private const string PackagesDirName = KinectAddinData.PackagesDirName;
		private const string PackageDirName = KinectAddinData.PackageDirName;
		private const string KinectDirName = KinectAddinData.KinectDirName;
		private const string PluginsDirName = KinectAddinData.PluginsDirName;

		public int callbackOrder { get { return 1000; } }

	    public void OnPostprocessBuild(BuildReport report)
	    {
			// moving kinect dlls from assets space back to package space
			// after they where exported in the building process.
			var separator = Path.DirectorySeparatorChar;
			var packageDir = Path.Combine(Application.dataPath, ".." + separator + PackagesDirName + separator + PackageDirName);
			var kinectDir = Path.Combine(packageDir, KinectDirName);
			var targetDir = Application.dataPath + separator + PluginsDirName;

			KinectAddinData.Move(new DirectoryInfo(targetDir), new DirectoryInfo(kinectDir));
			KinectAddinData.Import();
		}
	}
}
