# Projector Manual

## Components
* Projector Brain
* Projector Configuration
* Projector Emitter
* Projector Eyes
* Projector Mount
* Projector Plane
* Projector Renderer Mono
* Projector Renderer Stereo
* Projector Settings

## Scene Hierarchy
```
	o Projector Brain <- Projector Settings
		o Projector Eyes
		o Projector Mount
			o Projector Emitter <- Projector Configuration
			o ...
		o Projector Plane
		o ...
```

## Details
The *Projector Brain* controls each of the sub components and is responsible
for the initialization of the render process. There should be
one *Projector Settings* (for the *Projector Brain*) and
multiple *Projector Configuration* (for each *Projector Emitter*) in your assets.
These *ScriptableObjects* are storing the properties for the whole projector hierarchy.
At runtime the *Projector Mount* looks for available *Projector Emitter* components and
stores them locally. Also the *Projector Mount* follows the *Projector Eyes* by default.
Each *Projector Plane* belongs and can be accessed from a *Projector Emitter*.
The *Projector Brain* initializes last and attaches a *Projector Renderer* to itself.
Next it transmits the *Projector Emitter* components found by the *Projector Mount* to the *Projector Renderer*.
The *Projector Renderer* decides if both eyes from the *Projector Eyes* component are used or only
the anchor. After each Update is completed the *Holographic Projection* is calculated from each
*Projector Emitter* with the belonging *Projector Plane* in respect to the transform of the *Projector Eyes*.

### Projector Settings
> **Note**: For more information about stereoscopic rendering visit [this](https://docs.unity3d.com/Manual/StereoscopicRendering.html) page.

* Device Output: Select *Mono* for a monoscopic and *Stereo* for a steroscopic projection.
Note that the stereoscopic projection requires a stereoscopic capable display and will not work in the editor.
* Viewport Axis: Select *X* to distribute the camera viewports horizontally and *Y* to distribute them vertically.
* Stereo Separation: Defines the distance between both eyes.
* Stereo Convergence: Defines the distance at which both eyes will converge.
* Equalize Images: Uses a matrix to compensate for distortions in the rendered image.
* Reload Equalization: Recalculates the matrix and loads the result before startup (can increase loading time).

### Projector Configuration
* Display Name: The readable name of the projector.
* Order: Defines the position of the viewport from left to right.
* Width: The width of the belonging plane.
* Height: The height of the belonging plane.
* Near: Defines the distance of the camera near clip plane.
* Far: Defines the distance of the camera far clip plane.
* Field of View: Defines the field of view of the camera in degrees.
* Invert Stereo: Inverts the stereoscopic projection for this camera.
* Equalization Anchors: Root anchors which are used to calculate the matrix used for equalization.
* Equalization Matrix: The matrix which is used for compensating distortions in the rendered image.
