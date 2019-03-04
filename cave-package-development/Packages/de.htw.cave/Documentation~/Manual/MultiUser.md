# Multi User Manual

## Components Affected
* Kinect Brain
* Projector Brain
* Projector Eyes

## Scene Hierarchy
### Single Rendering
```
	o Projector Brain, Kinect Brain
		o Projector Eyes, Kinect Actor (1)
		o Kinect Actor (2)
		o ...
```

### Multi Rendering
```
	o Projector Brain, Kinect Brain
		o Projector Eyes, Kinect Actor (1)
		o Projector Eyes, Kinect Actor (2)
		o ...
```

## Details
To enable multi user support you need to change a few things.
First of all you need to fit the process of finding the available actors to your needs.
At the moment the *Kinect Brain* looks only for one *Kinect Actor*. After the *Kinect Actor*
has been found the *Kinect Brain* updates the component regularly (every frame).
Also the *Projector Brain* need a small change because it also only looks for one *Projector Eyes* component.
The *Projector Eyes* component is used as the origin of the render process so its up to you if you want
to render multiple *Projector Eyes* with multiple *Kinect Actor* components or render a single *Projector Eyes*
component with also multiple *Kinect Actor* components.
To update multiple *Kinect Actor* components you need to find the index of every person currently tracked in the
*Bodies* array. Now you can call *Refresh* with the index on every *Kinect Actor* and *Refresh* again with the data
that the *Kinect Manager* collected.
