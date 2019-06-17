# Import Export System Manual

## Components
* Import Export Settings
* Import Export System

## Details
On startup the system will create a unique list of *GameObject*'s' and *ScriptableObject*'s
from the given list. Each *ScriptableObject* will be imported using the persistent data path
or the custom directory. If a file does not exist a new one will be created by exporting the *ScriptableObject*.
After the import has completed *Awake* will be called on each *GameObject*
to initialize the *ScriptableObject*'s again.
If you export files the system will overwrite the files on the persistent data path. If you have
a custom directory the system will create *.backup* files before writing the changes to disk.

### Import Export Settings
* Write Log: Write a log file or not (small impact to loading time).
* Custom Directory: Should a custom directory be used to load files. Note that
this should be checked if you want to load files from a single location.
* Directory Path: The path of the custom directory. Note that a path should be
given if you want to load files from a single location.

### System Tools
* Overwrite Local Files: Overwrites local files on import (persistent data path) with the
information stored currently in the settings and configurations. Note that this feature
is used mainly in the editor.
* Enable In Editor: Import and export files in the editor play mode.

### Importing and Locate Existing Files
Select the *ScriptableObject* in the list and select the existing file in your assets.
Hit import and the current properties will be overwritten or hit the open button if you want to
know where these files are exported to.

### Importing old CAVE Text Format
Go to the *Projector Mount* and select every *Projector Emitter* which configuration should be converted.
Now open up the add component context menu and add the ```Htw.Cave -> Misc -> Old Format Converter```.
Click on every *Projector Emitter* separately and select the *.txt* file you want to convert.


## Example Export Implementation
Note that a existing *Import Export System* is required.

```c#
	using System;
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;
	using Htw.Cave.ImportExport;

	public class ExampleExport : MonoBehaviour
	{
		public ScriptableObject scriptable;

		public void OnDisable()
		{
			ImportExportSystem.Instance.Export(scriptable);
		}
	}
```
