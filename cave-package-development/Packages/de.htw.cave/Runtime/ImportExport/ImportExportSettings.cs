using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Htw.Cave.ImportExport
{
	[CreateAssetMenu(fileName = "New Import Export Settings", menuName = "Htw.Cave/Import Export Settings", order = 40)]
    public class ImportExportSettings : ScriptableObject
    {
		[SerializeField]
		private bool writeLog;
		public bool WriteLog
		{
			get { return this.writeLog; }
			set { this.writeLog = value; }
		}

		[SerializeField]
		private bool customDirectory;
		public bool CustomDirectory
		{
			get { return this.customDirectory; }
			set { this.customDirectory = value; }
		}

		[SerializeField]
		private string directoryPath;
		public string DirectoryPath
		{
			get { return this.directoryPath; }
			set { this.directoryPath = value; }
		}
    }
}
