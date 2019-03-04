using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Htw.Cave.ImportExport
{
	public enum ImportExportLogType
	{
		Import,
		Export,
		ImportManually
	}

    public struct ImportExportLog
    {
		public ImportExportLogType logType;
		public DateTime dateTime;
		public ScriptableObject scriptableObject;

		public ImportExportLog(ImportExportLogType logType, DateTime dateTime, ScriptableObject scriptableObject)
		{
			this.logType = logType;
			this.dateTime = dateTime;
			this.scriptableObject = scriptableObject;
		}

		public string ToLog()
		{
			string line = this.dateTime.ToString("yyyy-MM-dd HH:mm:ss.fff");

			switch(this.logType)
			{
				case ImportExportLogType.Import:
					line = line + " - Import";
					break;
				case ImportExportLogType.Export:
					line = line + " - Export";
					break;
				case ImportExportLogType.ImportManually:
					line = line + " - Import Manually";
					break;
			}

			return line + " - " + this.scriptableObject.name;
		}
    }
}
