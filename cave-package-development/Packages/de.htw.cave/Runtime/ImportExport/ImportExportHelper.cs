using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Htw.Cave.ImportExport
{
    public static class ImportExportHelper
    {
		public static string PersistentPath(string path)
		{
			return Path.Combine(Application.persistentDataPath, path);
		}

		public static string AbsolutePath(string absolute, string path)
		{
			return Path.Combine(absolute, path);
		}

		public static string ExistingPersistentPath(string path)
		{
			string persistent = PersistentPath(path);
			FileInfo file = new FileInfo(persistent);

			if(file.Exists)
				return persistent;
			else
				return Application.persistentDataPath;
		}

		public static void RenameFile(FileInfo file, string name)
		{
			File.Move(file.FullName, Path.Combine(file.DirectoryName, name));
		}

		public static void DeleteFile(string file)
		{
			DeleteFile(new FileInfo(file));
		}

		public static void DeleteFile(FileInfo file)
		{
			if(file.Exists)
				File.Delete(file.FullName);
		}

		public static bool ExistsFile(string file)
		{
			return ExistsFile(new FileInfo(file));
		}

		public static bool ExistsFile(FileInfo file)
		{
			return file.Exists;
		}

		public static void CopyFile(string source, string destination)
		{
			CopyFile(new FileInfo(source), new FileInfo(destination));
		}

		public static void CopyFile(FileInfo source, FileInfo destination)
		{
			if(source.Exists)
				source.CopyTo(destination.FullName, true);
		}

		public static string ReadFileTail(string file, int n = 1)
		{
			return ReadFileTail(new FileInfo(file), n);
		}

		public static string ReadFileTail(FileInfo file, int n = 1)
		{
			if(file.Exists)
			{
				string[] tail = File.ReadLines(file.FullName).Reverse().Take(n).Reverse().ToArray();
				return String.Join("\n", tail);
			}

			return null;
		}

		public static void ExportFile(string file, string text)
		{
			ExportFile(new FileInfo(file), text);
		}

		public static void ExportFile(FileInfo file, string text)
		{
			if(file.Directory.Exists)
				File.WriteAllText(file.FullName, text);
		}

		public static string ImportFile(string file)
		{
			return ImportFile(new FileInfo(file));
		}

		public static string ImportFile(FileInfo file)
		{
			if(file.Exists)
				return File.ReadAllText(file.FullName);
			return null;
		}

		public static void BackupFile(string file)
		{
			BackupFile(new FileInfo(file));
		}

		public static void BackupFile(FileInfo file)
		{
			if(file.Exists)
			{
				FileInfo backup = new FileInfo(file.Name + ".backup");

				if(backup.Exists)
					File.Delete(backup.FullName);

				RenameFile(file, file.Name + ".backup");
			}
		}

		public static void LogLines(string file, List<string> lines)
		{
			if(File.Exists(file))
			{
				using(StreamWriter sw = File.AppendText(file))
				{
					foreach(string line in lines)
						sw.WriteLine(line);
				}
			} else {
				using(StreamWriter sw = File.CreateText(file))
				{
					foreach(string line in lines)
						sw.WriteLine(line);
				}
			}
		}
    }
}
