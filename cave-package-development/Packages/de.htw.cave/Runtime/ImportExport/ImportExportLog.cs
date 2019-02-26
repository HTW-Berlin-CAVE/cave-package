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
		Export
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
    }
}
