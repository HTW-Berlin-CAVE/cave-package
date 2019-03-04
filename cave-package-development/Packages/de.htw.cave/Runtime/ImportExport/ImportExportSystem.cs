using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Htw.Cave.ImportExport
{
	[System.Serializable]
	public struct ImportExportEntry
	{
		public GameObject gameObject;
		public ScriptableObject scriptableObject;

		public ImportExportEntry(GameObject gameObject, ScriptableObject scriptableObject)
		{
			this.gameObject = gameObject;
			this.scriptableObject = scriptableObject;
		}
	}

	[AddComponentMenu("Htw.Cave/Misc/Import Export System")]
    public sealed class ImportExportSystem : MonoBehaviour
    {
		private const string logFile = "import_export_log.txt";

		public static ImportExportSystem Instance { get; set; }

		[SerializeField]
		private ImportExportSettings settings;
		public ImportExportSettings Settings
		{
			get { return this.settings; }
			set { this.settings = value; }
		}

		[SerializeField]
		private List<ImportExportEntry> entries;
		public List<ImportExportEntry> Entries
		{
			get { return this.entries; }
			set { this.entries = value; }
		}

		[SerializeField]
		private bool overwriteLocalFiles;
		public bool OverwriteLocalFiles
		{
			get { return this.overwriteLocalFiles; }
			set { this.overwriteLocalFiles = value; }
		}

#if UNITY_EDITOR
		[SerializeField]
		private bool enableInEditor;
		public bool EnableInEditor
		{
			get { return this.enableInEditor; }
			set { this.enableInEditor = value; }
		}
#endif

		private List<ImportExportLog> logs;

		public void Awake()
		{
			if(Instance == null) {
				Instance = this;
			} else {
				Destroy(this);
				return;
			}

			if(this.logs == null)
				this.logs = new List<ImportExportLog>();

#if UNITY_EDITOR
			if(!this.enableInEditor)
			{
				base.enabled = false;
				return;
			}
#endif

			if(this.overwriteLocalFiles)
				OverwriteOnAwake();
			else
				ImportOnAwake();
		}

		public void Start()
		{
			WriteLogAndClear();
		}

		public void OnApplicationQuit()
		{
			WriteLogAndClear();
		}

		public void Import(ScriptableObject scriptableObject)
		{
			if(this.settings == null)
				return;

			string file = ImportExportHelper.PersistentPath(scriptableObject.name + ".json");

			if(this.settings.CustomDirectory)
			{
				string source = ImportExportHelper.AbsolutePath(this.settings.DirectoryPath, scriptableObject.name + ".json");
				ImportExportHelper.CopyFile(source, file);
			}

			if(ImportExportHelper.ExistsFile(file))
			{
				string json = ImportExportHelper.ImportFile(file);
				JsonUtility.FromJsonOverwrite(json, scriptableObject);
				LogNow(ImportExportLogType.Import, scriptableObject);
			} else {
				Export(scriptableObject);
			}
		}

		public void Export(ScriptableObject scriptableObject)
		{
			if(this.settings == null)
				return;

			string json = JsonUtility.ToJson(scriptableObject, true);
			string file = ImportExportHelper.PersistentPath(scriptableObject.name + ".json");
			ImportExportHelper.ExportFile(file, json);

			if(this.settings.CustomDirectory)
			{
				file = ImportExportHelper.AbsolutePath(this.settings.DirectoryPath, scriptableObject.name + ".json");
				ImportExportHelper.BackupFile(file);
				ImportExportHelper.ExportFile(file, json);
			}

			LogNow(ImportExportLogType.Export, scriptableObject);
		}

		public void ImportManually(ScriptableObject scriptableObject, string file)
		{
			string json = ImportExportHelper.ImportFile(file);
			JsonUtility.FromJsonOverwrite(json, scriptableObject);
			LogNow(ImportExportLogType.ImportManually, scriptableObject);
			WriteLogAndClear();
		}

		public List<ImportExportLog> GetLogs()
		{
			return this.logs;
		}

		public void ClearLogFile()
		{
			ImportExportHelper.DeleteFile(ImportExportHelper.PersistentPath(logFile));
		}

		public string ReadLogTail(int n = 1)
		{
			return ImportExportHelper.ReadFileTail(ImportExportHelper.PersistentPath(logFile), n);
		}

		private void ImportOnAwake()
		{
			List<GameObject> gameObjects = null;
			List<ScriptableObject> scriptableObjects = null;

			GetDistinct(ref gameObjects, ref scriptableObjects);

			foreach(ScriptableObject so in scriptableObjects)
				Import(so);

			foreach(GameObject go in gameObjects)
			{
				if(go != gameObject)
					go.SendMessage("Awake");
			}
		}

		private void OverwriteOnAwake()
		{
			List<GameObject> gameObjects = null;
			List<ScriptableObject> scriptableObjects = null;

			GetDistinct(ref gameObjects, ref scriptableObjects);

			foreach(ScriptableObject so in scriptableObjects)
				Export(so);
		}

		private void GetDistinct(ref List<GameObject> gameObjects, ref List<ScriptableObject> scriptableObjects)
		{
			gameObjects = new List<GameObject>();
			scriptableObjects = new List<ScriptableObject>();

			foreach(ImportExportEntry entry in this.entries)
			{
				if(entry.gameObject == null || entry.scriptableObject == null)
					continue;

				gameObjects.Add(entry.gameObject);
				scriptableObjects.Add(entry.scriptableObject);
			}

			gameObjects = gameObjects.Distinct().ToList();
			scriptableObjects = scriptableObjects.Distinct().ToList();
		}

		private void LogNow(ImportExportLogType logType, ScriptableObject scriptableObject)
		{
			if(this.logs == null)
				this.logs = new List<ImportExportLog>();

			this.logs.Add(new ImportExportLog(logType, DateTime.Now, scriptableObject));
		}

		private void WriteLogAndClear()
		{
			WriteLog();
			this.logs.Clear();
		}

		private void WriteLog()
		{
			if(!this.settings.WriteLog)
				return;

			if(this.logs.Count < 1)
				return;

			List<string> lines = new List<string>();

			foreach(ImportExportLog log in this.logs)
				lines.Add(log.ToLog());

			ImportExportHelper.LogLines(ImportExportHelper.PersistentPath(logFile), lines);
		}
    }
}
