# Package Installation

## Requirements
* [Unity Package Manager](https://docs.unity3d.com/Packages/com.unity.package-manager-ui@1.8/manual/index.html)
* [Kinect SDK 2.0](https://www.microsoft.com/en-us/download/details.aspx?id=44561)
* Stereo Display Support
* [NVIDIA Mosaic](https://www.nvidia.de/object/nvidia-mosaic-technology-de.html) or similar

## Instructions
After the creation of a new Unity project you should have the following directory
structure
```
Assets
Library
Logs
Packages
ProjectSettings
Temp
```
>**Note**: Before you continue make sure that you remove all unnecessary packages
which could collide with third party libraries used in this package.

Now copy the `de.htw.cave` package into the `Packages` folder. Your `Package`
structure should look like this
```
Packages
	de.htw.cave
	manifest.json
```
Finally restart Unity.
