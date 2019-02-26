
1. Requirements
	1.1 Unity HUB
	1.2 Unity Version
	1.3 Kinect SDK
	1.4 Wiimote
2. Installation
	1.1 From Git
	1.2 From Scratch
		1.2.1 Setup project
		1.2.2 Install Package
		1.2.3 Configure project
	1.3 Validate Installation
3. Start Developing
	3.1
4. Contributing
	4.1 Coding Conventions
	4.2 Package Conventions
	4.3 Architecture
	4.4 Optimization
		#### Preprocessor
		REMOVE GIZMO STUFF FOR REAL
		[More](https://docs.microsoft.com/de-de/dotnet/csharp/language-reference/preprocessor-directives/)
	4.5 Performance
		#### GC
		[More](https://docs.unity3d.com/Manual/UnderstandingAutomaticMemoryManagement.html)
		#### Threading
		[More](https://docs.unity3d.com/Manual/JobSystemOverview.html)
	4.6 Technical Notes
		#### Compiliation
		[More](https://docs.unity3d.com/Manual/PlatformDependentCompilation.html)
		[More](https://docs.unity3d.com/ScriptReference/Debug-isDebugBuild.html)
		### Unity Stereoscopic support
		https://forum.unity.com/threads/virtual-reality-support-for-stereo-displays.489151/
	4.7 Debugging
		#### Debugging
		[More](https://github.com/tsugi/exampleunityangrybots/tree/master/Assets/Scripts)
5. FAQ
	5.1 Package does not work, what should I do?
	5.2 There are warnings inside my project, how can i fix them?
	5.3 The live debug is not working, how should i continue?
	5.4 What does the calibration?
	5.5 ????? Bimbermatrix whaaattt???
	5.6 PLS EXPLAIN PerspectiveOffMatrix
		http://www.songho.ca/opengl/gl_projectionmatrix.html
		http://wiki.unity3d.com/index.php/OffsetVanishingPoint
		http://paulbourke.net/stereographics/stereorender/

ECS WOOOAHHH
https://www.youtube.com/watch?time_continue=1891&v=alZ6wmwvck0

Kinect Gestures
	https://channel9.msdn.com/Blogs/k4wdev/Custom-Gestures-End-to-End-with-Kinect-and-Visual-Gesture-Builder



Third Party:
	Kinect Assets
	Wiimote Assets

Names:
	Htw.Cave.manager
	Htw.Cave.Render
		Htw.Cave.
		Htw.Cave.Frustum
		Htw.Cave.VirtualScreen
	Htw.Cave.LiveDebug
	Htw.Cave.Stereoscopic
		Htw.Cave.Camera
		Htw.Cave.ProjectionPlane
	Htw.Cave.Tracking
		Htw.Cave.Offset
	Htw.Cave.Math
		BimberMatrix
		Quad
	Htw.Cave.Actor
		Collision
			Static
			HitBox
		PointOfView
		Skeletton
		Controller
	Htw.Cave.Kinect
	Htw.Cave.Wiimote
