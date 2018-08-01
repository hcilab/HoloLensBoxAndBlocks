# HoloLensBoxAndBlocks

A Unity project that uses an HTC Vive controller, a Thalmic Labs Myo Armband, and a Microsoft HoloLens to emulate the Box and Blocks test for training of upper limb myoelectric prostheses. 

## Overview/Description

The purpose of this project was to create a virtual training environment for upper limb amputees to train the control of myoelectric postheses. A common training exercise for upper limb amputees is the box and blocks test, in which trainees move as many small cubes as they can in a minute over a partition. (Click [here](https://www.youtube.com/watch?v=aow02xRai8U) to see a video of Box And Blocks Test.)

To create this environment, the game engine Unity was used due to its simple to use interface and robust ability to create experiences for Microsoft HoloLens. Microsoft HoloLens is used to visualize the virtual environment, rendering a 3 dimensional prosthetic hand meant to imitate a real prosthetic limb. The HoloLens has the ability to scan its environment and create a mesh that Unity can access and add to the game environment. Game Objects created in Unity can interact with this mesh through Unity's physics engine, which adds a layer of realism and immersion to the experience. A HTC Vive controller is user as a 6DOF tracking device to move the holographic prosthetic limb as closely to a real one as possible.

Upon Starting the application, the user will be asked to select a hand. Then the user will be asked to walk around the room that they are in to scan for any table or surfaces, once a sufficient area has been scanned, the user can select which surface to conduct the test on and a holographic Box and Blocks set up will spawn where the user selects. Once the controller is aligned with the holographic controller, the user is ready to start the test, in which they have 60 seconds to move as many blocks over the partition. The User can retry as many times as they like.

## Getting Started

These instructions outline all the required components and steps to get a working copy of the project up and running on your local machine for development and testing purposes. See deployment for notes on how to deploy the project on a live system.

### Required Hardware

This project uses the following hardware:

* [Microsoft HoloLens](https://www.microsoft.com/en-us/hololens)
* Thalmic Labs [Myo armband](https://www.myo.com/)
* HTC Vive Controller/Tracker (must be gen. 1, otherwise interferes with HoloLens)
* HTC Vive Tracker Dongle (required for tracker, and to make controller wireless without the headset)
* HTC Vive BaseStations

### Required Software

This project uses the following software:

* Unity
* [Mixed Reality Toolkit](https://github.com/Microsoft/MixedRealityToolkit-Unity)
* Visual Studios 2017
* Processing 
* [The Falling Of Momo](https://github.com/hcilab/Momo) (*calibrateAndStream* branch)
* SteamVR (with opt in for the Beta) (see step 2 below)
* Python 3.6 (see step 2 below)
* pyopenvr (see step 2 below)
* triad_openvr (see step 2 below)

### Setting Up & Installing

This section covers the different steps to install and setup all the required hardware and software components. Many of the required components are well documented. Where thourough documentation is available links will be provided.

1. Install Mixed Reality Toolkit to Unity. Documentation [here](https://github.com/Microsoft/MixedRealityToolkit-Unity).
2. Install Steam and Steam VR. Then install Vive setup [software](https://www.vive.com/eu/) (click `Setup | Download`)
    * go through set up with just the base stations and vive
2. Set up Vive controller (or tracker) without headset in Steam VR. Setup guide [here](https://www.roadtovr.com/how-to-use-the-htc-vive-tracker-without-a-vive-headset/) and [here](http://www.pencilsquaregames.com/getting-steamvr-tracking-data-in-unity-without-a-hmd/).
3. Set up python TCP socket server script to send tracking data from Vive controller:
    * make sure `ScriptName` is in the same directory as `triad_openvr`
    * to run from command line, make sure python 3.6 is enabled, `cd` to directory with `ScriptName` and type:
```
python ScriptName
```
*   * this is required to use the controller when running the project in either the Unity editor or deployed to the HoloLens as UWP app.
4. Set up Momo to send Myo armband data via TCP socket:
    * 

Say what the step will be

```
Give the example
```

And repeat

```
until finished
```

End with an example of getting some data out of the system or using it for a little demo

## Running the tests

Explain how to run the automated tests for this system

### Break down into end to end tests

Explain what these tests test and why

```
Give an example
```

### And coding style tests

Explain what these tests test and why

```
Give an example
```

## Deployment

Add additional notes about how to deploy this on a live system

## Built With

* [Dropwizard](http://www.dropwizard.io/1.0.2/docs/) - The web framework used
* [Maven](https://maven.apache.org/) - Dependency Management
* [ROME](https://rometools.github.io/rome/) - Used to generate RSS Feeds

## Contributing

Please read [CONTRIBUTING.md](https://gist.github.com/PurpleBooth/b24679402957c63ec426) for details on our code of conduct, and the process for submitting pull requests to us.

## Versioning

We use [SemVer](http://semver.org/) for versioning. For the versions available, see the [tags on this repository](https://github.com/your/project/tags). 

## Authors

* **Billie Thompson** - *Initial work* - [PurpleBooth](https://github.com/PurpleBooth)

See also the list of [contributors](https://github.com/your/project/contributors) who participated in this project.

## License

This project is licensed under the MIT License - see the [LICENSE.md](LICENSE.md) file for details

## Acknowledgments

* Hat tip to anyone whose code was used
* Inspiration
* etc