# Cave Automatic Virtual Environment Package

## About
This package contains several tools to build a CAVE environment with head, body
and basic face tracking capabilities. This package is designed to work with
multiple projectors which need to be configured manually.

The package follows the Unity package guidelines and requires the Unity Package
Manager (UPM) to work. If you need information about the Unity Package Manager
please visit [this](https://docs.unity3d.com/Packages/com.unity.package-manager-ui@1.8/manual/index.html)
site.

## Package Structure
```
de.htw.cave
	Content
	Documentation~ <- we are here
	Editor
	Prefabs
	Runtime
	ThirdParty
	UnityLegacy
```

##### Content
Includes all assets that are shipped by default like icons, images, textures and materials.

##### Documentation
Contains manuals and code documentation.

##### Editor
> **Note**: This code will not be compiled in a build.

The place for all editor scripts (...Editor.cs) or scripts using the build pipeline.

##### Prefabs
Standard location for Prefab assets (more information about Prefabs [here](https://docs.unity3d.com/Manual/Prefabs.html)).

##### Runtime
> **Note**: This is the most important directory. Every script should have a
appropriate namespace.

Includes all scripts available at runtime.

##### ThirdParty
Contains third party scripts and libraries.

##### UnityLegacy
Contains legacy code from Unity.

## Owner and Authors
HTW Berlin - CAVE Project  
Written by s0559090@htw-berlin.de

## Tested
Unity 2018.3f1  
Unity 2018.3f2  
with Kinect SDK 2.0
