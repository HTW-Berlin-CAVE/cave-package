# Adjust Profile Settings

## Render Manager Profile

#### Output
Defines the final output mode of the renderer.
**Mono**: This is the mode for normal displays. Unity cameras will not use
the stereo functionality.  
**Stereo**: Choose this mode when your display has a stereo option.
Unity cameras will render in stereo mode with stereoscopic eyes and convergence.

#### Axis
Switch the axis on which the viewport will be split.
**X**: Split the viewport along the x-axis.
**Y**: Split the viewport along the y-axis.

#### Stereo Separation
Distance between both eyes.
> **Note**: This only impacts stereo mode.

#### Stereo Convergence
Distance which defines when the stereo images converge.
> **Note**: This only impacts stereo mode.

#### Use Bimber
Is it allowed to use a bimber matrix to balance the (possibly) distorted output
image.

#### Recompute Bimber
Should the bimber matrix be recomputed at application startup.
> **Note**: This works only when the usage of a bimber matrix is allowed.

# Kinect Manager Profile

#### Mount Location
The location in real space where the Kinect is mounted at.

#### Center Location
The location in real space where the center of the room is defined.

#### Tracking Mode
Switch between tracking modes of the Kinect.
**Body**: Only track the body. This updates the position of the head.
**Face**: Only track the face. This updates the rotation of the head.
**Body and Face**: Track the body and face. This updates both the position and
rotation of the head. Use this to get the intended immersive effect.

#### Filter Freq
Frequency of the noise.

#### Filter Min Cutoff
...

#### Filter Beta
...

#### Filter D Cutoff
...
