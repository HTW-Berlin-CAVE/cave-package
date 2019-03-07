# How do I ...

## Enable the Stereo Display?
Go to ```Edit -> Project Settings -> Player -> XR Settings``` and enable the virtual
reality support. Now remove any existing SDK and add the *Stereo Display (non head-mounted)*.

## Disable V-Sync?
Go to ```Edit -> Project Settings -> Quality``` and change for each level the *V Sync Count*
to *Don't Sync*.

## Start the Application in Fullscreen?
Go to ```Edit -> Project Settings -> Player -> Resolution and Presentation``` and change the
*Fullscreen* mode to *Exclusive Fullscreen*.

## Start the Application in fullscreen if the Resolution and Presentation Setting is not working
Open the registry editor (WIN+r and type: regedit.exe), search for the Key ````"HKEY_CURRENT_USER" -> "Software" -> [COMPANY_NAME] -> [PRODUCT_NAME]``` and delete all it's contents.

[Reference](
https://forum.unity.com/threads/playersettings-are-ignored-when-building-windowed-fullscreen.257700/)

## Look for Exceptions and Errors?
Open up the file system and navigate to the persistent data path
(for more information click [here](https://docs.unity3d.com/ScriptReference/Application-persistentDataPath.html)).
There should be a log file which contains all exceptions and errors thrown at runtime.
You can enable and disable the log in the *Project Settings* under the *Player* menu.

## Remove Editor code from the Runtime?
There are two ways: First you can use the [preprocessor directives](https://docs.microsoft.com/de-de/dotnet/csharp/language-reference/preprocessor-directives/)
the second way is to setup a [assembly definition file](https://docs.unity3d.com/Manual/ScriptCompilationAssemblyDefinitionFiles.html).
