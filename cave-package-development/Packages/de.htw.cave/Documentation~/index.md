# Cave Automatic Virtual Environment Package

## About
This package contains several tools to build a cave environment with head, body
and basic face tracking capabilities. This package is designed to work with
multiple projectors which need to be configured manually.

The package follows the Unity package guidelines and requires the Unity Package
Manager (UPM) to work. If you need information about the Unity Package Manager
please visit [this](https://docs.unity3d.com/Packages/com.unity.package-manager-ui@1.8/manual/index.html)
site.

## Package Structure
```
de.htw.cave
	Documentation <- we are here
	Editor
	Menus
	PropertyDrawers
	Runtime
	Tests
	ThirdParty
	Unity
```
##### Documentation
Contains package and code documentation.
It should have almost the same structure as the `Runtime` directory.

##### Editor
Contains all editor scripts (...Editor.cs) or scripts using the build pipeline.
> **Note**: This code will not be compiled in a build.

##### Menus
This folder contains special editor scripts which provide menus inside the Unity Editor.
> **Note**: This code will not be compiled in a build.

##### PropertyDrawers
Contains property drawers (inherit PropertyDrawer) of serialized objects.
> **Note**: This code will not be compiled in a build.

##### Runtime
Contains all scripts available at runtime.
> **Note**: This is the most important directory. Every script should have a
appropriate namespace.

##### Tests
Contains script tests.

##### ThirdParty
Contains third party scripts and libraries.

##### UnityLegacy
Contains legacy code from Unity.

## Owner and Authors
HTW Berlin

## Tested
Unity 2018.3f1
Unity 2018.3f2
with Kinect SDK 2.0
