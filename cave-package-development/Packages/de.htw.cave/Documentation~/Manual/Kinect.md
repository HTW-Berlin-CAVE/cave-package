# Kinect Manual

## Components
* Kinect Actor
* Kinect Brain
* Kinect Head
* Kinect Manager
* Kinect Settings

## Scene Hierarchy
```
	o Kinect Brain <- Kinect Settings
		o Kinect Actor <- Kinect Head
```

## Details
The *Kinect Brain* controls each of the sub components and is responsible
for the initialization of the tracking capabilities. The tracking pipeline itself
is provided by the *Kinect Manager* which will be attached to the *Kinect Brain*
automatically. At runtime the *Kinect Brain* searches for the *Kinect Actor* and
updates the body and face information with the information gathered by the *Kinect Manager*.
If the *Kinect Actor* left the *Tracking Area* a new person inside the area will be tracked.
The *Kinect Head* takes the information from the *Kinect Actor* to position itself inside the
local space.

### Kinect Settings
> **Note**: For more information about the Kinect 2.0 coordinate system visit
[this](https://medium.com/@lisajamhoury/understanding-kinect-v2-joints-and-coordinate-system-4f4b90b9df16) page.

* Sensor Location: Defines the position of the Kinect 2.0 sensor inside the CAVE.
* Tracking Area: Search area for a tracked person (should be covered entirely by the Kinect viewport).
* Actor Priority: Defines the algorithm for the selection of a new person inside the area.
* HD Face: (NOT IMPLEMENTED YET) Use the advanced HD Face capabilities of the Kinect.

## Example Actor Head Implementation
Note that this script needs to be attached to the *GameObject* with the *Kinect Actor*.

```c#
	using System;
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;
	using Htw.Cave.Kinect;

	[RequireComponent(typeof(KinectActor))]
    public class ExampleActorHead : MonoBehaviour
    {
		private KinectActor actor;

		public void Awake()
		{
			this.actor = base.GetComponent<KinectActor>();
		}

		public void Update()
		{
			if(this.actor.HeadVisible())
				Debug.Log(this.actor.GetHeadPosition());
		}
    }
```
