using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;
using System;
using System.IO;

namespace Htw.Cave.Kinect
{
	public static class KinectAddinData
	{
		public const string PackagesDirName = "Packages";
		public const string PackageDirName = "de.htw.cave";
		public const string KinectDirName = "ThirdParty/Kinect/Plugins";
		public const string PluginsDirName = "Plugins";

		public static void Move(DirectoryInfo source, DirectoryInfo destination)
		{
			if (!Directory.Exists(source.FullName))
				return;

			Directory.Move(source.FullName, destination.FullName);
			File.Delete(source.FullName + ".meta");
		}

		public static void Copy(DirectoryInfo source, DirectoryInfo destination)
		{
			if (!Directory.Exists(source.FullName))
				return;

			if (!Directory.Exists(destination.FullName))
				destination = Directory.CreateDirectory(destination.FullName);

			foreach(DirectoryInfo dirPath in source.GetDirectories("*", SearchOption.AllDirectories))
				Directory.CreateDirectory(dirPath.FullName.Replace(source.FullName, destination.FullName));

			foreach(FileInfo filePath in source.GetFiles("*", SearchOption.AllDirectories))
				File.Copy(filePath.FullName, filePath.FullName.Replace(source.FullName, destination.FullName));
		}

		public static void Delete(DirectoryInfo destination)
		{
			if (!Directory.Exists(destination.FullName))
				return;

			Directory.Delete(destination.FullName, true);
		}

		public static void Import()
		{
			AssetDatabase.Refresh();
		}
	}
}
