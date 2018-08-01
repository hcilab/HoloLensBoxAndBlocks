# HoloLensBoxAndBlocks
A Unity project that uses an HTC Vive controller, a Thalmic Labs Myo Armband, and a Microsoft HoloLens to emulate the Box and Blocks test for training of upper limb myoelectric prostheses. 

## Overview/Description
The purpose of this project was to create a virtual training environment for upper limb amputees to train the control of myoelectric postheses. A common training exercise for upper limb amputees is the box and blocks test, in which trainees move as many small cubes as they can in a minute over a partition. (Click [here](https://www.youtube.com/watch?v=aow02xRai8U) to see a video of Box And Blocks Test.)

To create this environment, the game engine Unity was used due to its simple to use interface and robust ability to create experiences for Microsoft HoloLens. Microsoft HoloLens is used to visualize the virtual environment, rendering a 3 dimensional prosthetic hand meant to imitate a real prosthetic limb. The HoloLens has the ability to scan its environment and create a mesh that Unity can access and add to the game environment. Game Objects created in Unity can interact with this mesh through Unity's physics engine, which adds a layer of realism and immersion to the experience. A HTC Vive controller is user as a 6DOF tracking device to move the holographic prosthetic limb as closely to a real one as possible.

Upon Starting the application, the user will be asked to select a hand. Then the user will be asked to walk around the room that they are in to scan for any table or surfaces, once a sufficient area has been scanned, the user can select which surface to conduct the test on and a holographic Box and Blocks set up will spawn where the user selects. Once the controller is aligned with the holographic controller, the user is ready to start the test, in which they have 60 seconds to move as many blocks over the partition. The User can retry as many times as they like.

