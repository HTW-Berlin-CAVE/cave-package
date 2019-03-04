# Algorithms

## Holographic Projection
> **Note**: This algorithm is based on the description provided
[here](https://en.wikibooks.org/wiki/Cg_Programming/Unity/Projection_for_Virtual_Reality)
and in [this](https://csc.lsu.edu/~kooima/pdfs/gen-perspective.pdf) paper.

This algorithm manipulates the projection matrix of the cameras. The desired
result is a holographic effect which reflects a off axis perspective projection.

## "Bimber" Matrix
> **Note**: This algorithm is developed by Oliver Bimber and Ramesh Raskar
published in "Spatial Augmented Reality - Merging Real and Virtual Worlds".

Which turns out just to be a normal homography it solves it problem of
the depth buffer by approximation.

## Kalman Filter
One of the famous filters for tracking data.
Solves the problem of noise in continuous data collected from tracking devices.

## One Euro Filter
> **Note**: This algorithm is based on a implementation of the 1€ Filter by
Gery Casiez, Nicolas Roussel and Daniel Vogel which can be found
[here](http://cristal.univ-lille.fr/~casiez/1euro/).

Filters noise more precisely than a Kalman filter.
You can compare both in [this](http://cristal.univ-lille.fr/~casiez/1euro/InteractiveDemo/)
interactive demo.
