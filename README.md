# HoloLensBoxAndBlocks

## Overview
A Unity project that uses an HTC Vive controller, a Thalmic Labs Myo Armband, and a Microsoft HoloLens to emulate the Box and Blocks test for training of upper limb myoelectric prostheses. 

The purpose of this project was to create a virtual training environment for upper limb amputees to train the control of myoelectric postheses. A common training exercise for upper limb amputees is the box and blocks test, in which trainees move as many small cubes as they can in a minute over a partition. 
To create this environment, the game engine Unity was used due to its simple to use interface and robust ability to create experiences for Microsoft HoloLens. Microsoft HoloLens is used to visualize the virtual environment, rendering a 3 dimensional prosthetic hand meant to imitate a real prosthetic limb. The HoloLens has the ability to scan its environment and create a mesh that Unity can access and add to the game environment. Game Objects created in Unity can interact with this mesh through Unity's physics engine, which adds a layer of realism and immersion to the experience. 

The user will be asked to walk around the room that they are in to scan for any table or surfaces, once a sufficient area has been scanned, the user can select which surface to conduct the test on and a holographic Box and Blocks set up will spawn where the user selects. 

