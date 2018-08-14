# HoloLensBoxAndBlocks

A Unity project that uses an HTC Vive controller, a Thalmic Labs Myo Armband, and a Microsoft HoloLens to emulate the Box and Blocks test for training of upper limb myoelectric prostheses. 
![photo of set up](https://github.com/hcilab/HoloLensBoxAndBlocks/blob/master/Images/20180807_144659_HoloLens.jpg)
## Overview/Description

The purpose of this project was to create a virtual training environment for upper limb amputees to train the control of myoelectric postheses. A common training exercise for upper limb amputees is the box and blocks test, in which trainees move as many small cubes as they can in a minute over a partition. (Click [here](https://www.youtube.com/watch?v=aow02xRai8U) to see a video of Box And Blocks Test.)

To create this environment, the game engine Unity was used due to its simple to use interface and robust ability to create experiences for Microsoft HoloLens. Microsoft HoloLens is used to visualize the virtual environment, rendering a 3 dimensional prosthetic hand meant to imitate a real prosthetic limb. The HoloLens has the ability to scan its environment and create a mesh that Unity can access and add to the game environment. Game Objects created in Unity can interact with this mesh through Unity's physics engine, which adds a layer of realism and immersion to the experience. A HTC Vive controller is used as a 6DOF tracking device to move the holographic prosthetic limb as closely to a real one as possible.

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

* Windows 10 (cannot be any version with `N`)
* Unity
* [Mixed Reality Toolkit](https://github.com/Microsoft/MixedRealityToolkit-Unity) (included in this repository)
* Visual Studios 2017
* [Processing](https://processing.org/download/) 
* [The Falling Of Momo](https://github.com/hcilab/Momo) (*calibrateAndStream* branch)
* SteamVR with opt in for the Beta (see step 3 below)
* Python 3.6 (see step 3 below)
* pyopenvr (see step 3 below)
* triad_openvr (see step 3 below/included in this repository)

### Setting Up & Installing

This section covers the different steps to install and setup all the required hardware and software components. Many of the required components are well documented. Where thourough documentation is available links will be provided.

1. Ensure your computer as well as HoloLens are in developer mode.
1. download and install Unity and Visual Studio 2017 (can be downloaded and installed via unity installer)
    * make sure when installing Unity to include `Windows Store .NET Scripting Backend`
    * make sure when installing Visual Studio to select both the **Universal Windows Platform development** and **Game Development with Unity** workloads
    * [installation checklist](https://docs.microsoft.com/en-us/windows/mixed-reality/install-the-tools) from microsoft
1. Install Steam and Steam VR. Then install Vive setup [software](https://www.vive.com/eu/) (click `Setup | Download`)
    * go through set up with just the base stations and vive controller or tracker
1. Set up Vive controller (or tracker) without headset in Steam VR. Setup guide [here](https://www.roadtovr.com/how-to-use-the-htc-vive-tracker-without-a-vive-headset/) and [here](http://www.pencilsquaregames.com/getting-steamvr-tracking-data-in-unity-without-a-hmd/) or see `Vive Controller Unity set-up.pdf`.
1. Set up Momo to send Myo armband data via TCP socket:
    * download or clone [this](https://github.com/hcilab/Momo) github repository. Make sure you use the *calibrateAndStream* branch.
    * set up steps are provided in the Momo repository.
    * you will need to install processing libraries to be able to run the project
        * these are the libraries that you need and this is how to install them

### Important Note

For detailed descriptions of all prefabs and scripts in this project, please refer to the `README.md` file in the *Assets* directory

## Running the Project

The project can be played either from the Unity Editor or deployed to the HoloLens as a UWP app. Each require slightly different steps and setup.

### Running the Project in Unity
![running from Unity](https://github.com/hcilab/HoloLensBoxAndBlocks/blob/master/Images/20180803_112557_HoloLens.jpg)
Running the project in Unity is useful because it is fast and great for testing, editing, and debugging. On the downside, rendering quality is greatly reduced and thus is not ideal for implementing or running actual experiments. Here are the following steps to properly run from the Unity editor:

1. ensure that the HoloLens and the computer running Unity are both connected to the same wifi network. (if computer connected via ethernet, create a **mobile hotspot** in windows `settings`)
1. enter holographic remoting:
    * on the HoloLens run *Holographic Remoting Player*
    * in Unity editor select the `Windows` tab and go to `Windows > Holographic Emulation > Emulation Mode > Remote to Device` and enter the HoloLens' IP address then press `connect`
1. make sure both TCP socket server programs are running and streaming data (*Momo* and `ControllerDataUnity.py`).
    * For the controller data, open a command line and make sure you are in the *triad_openvr* directory, type `python ControllerDataUnity.py` and press 'enter'
    * This is what you should see ![command line example](https://github.com/hcilab/HoloLensBoxAndBlocks/blob/master/Images/CMDlineConnected2.PNG)
    * in `Momo.pde` you must change a boolean variable to `false` so program knows will be streaming to Unity editor ![Momo boolean variable for Unity editor](https://github.com/hcilab/HoloLensBoxAndBlocks/blob/master/Images/InkedMomoBoolean.jpg)
1. press play button in Unity editor 

### Deploying and Running as UWP app on HoloLens
![running as UWP](https://github.com/hcilab/HoloLensBoxAndBlocks/blob/master/Images/20180807_144340_HoloLens.jpg)
Deploying the project to the HoloLens is useful because the graphics are much better and is great for running actual experiments. On the downside, deployment takes a few minutes thus making it to slow for debugging or testing. Here are the following steps to properly deploy and run the project as a UWP app on the HoloLens:

1. ensure that the HoloLens and the computer running Unity are both connected to the same wifi network.
1. build the unity project as a UWP app
    * select `File > Build Settings`
    * under **Platform** select **Universal Windows Platform**
    * under **Scenes In Build** click `Add Open Scenens` and make sure *HoloViveController* is the only scene selected
    * select `build` and once the visual studio solution is created run it. Once the app is successfully deployed to the HoloLens the app persists on the HoloLens and can be ran as any other HoloLens app.
    * more detailed build documentation [here](https://docs.microsoft.com/en-us/windows/mixed-reality/exporting-and-building-a-unity-visual-studio-solution) and deployment documentation [here](https://docs.microsoft.com/en-us/windows/mixed-reality/using-visual-studio).
1. ensure both TCP socket server programs are running and streaming data (*Momo* and `ControllerDataUWP.py`).
    * follow same steps for running the python script except this time type `python ControllerDataUWP.py`
    * in `Momo.pde` must change the same boolean variable to `true` so program knows will be streaming to HoloLens
1. select *HoloLensClientTest* app on HoloLens

## Useful Websites/Forums/Pages

here is a list of websites that I found very helpful for development:

* Windows Mixed Reality [documentation](https://docs.microsoft.com/en-us/windows/mixed-reality/)
* Unity [documentation](https://docs.unity3d.com/Manual/wmr_sdk_overview.html) for windows mixed reality
* Mixed Reality Toolkit GitHub [Repo](https://github.com/Microsoft/MixedRealityToolkit-Unity)
* Microsoft [Magazine](https://msdn.microsoft.com/en-us/magazine/), search 'HoloLens' or 'mixed reality' to find helpful articles
* Unity [forum](https://forum.unity.com/forums/windows-mixed-reality.102/) for mixed reality

## Common Problems/Issues

Here are a few things that I struggled with and came up often. see below for potential solutions or work-arounds.

* TCP communication, the namespace used for TCP socket communication in unity doesn't work for a project build for UWP, and vice versa
* if no icons show up in steam VR just go back into the steam settings directory outlined in step 4 of setting up & installing.

## Future Work

In this section I will outline additions that could (and should) be made to the project. I will also outline aspects of this project that could use some improvements and where this project can go if picked up by another developer.

* adding more functionality to the hand, use the third number sent from *Momo* (when a fist is made) to change what the virtual prosthesis does
* improving the pick up functionality to make it more realistic, change the collider physics as well as add haptic feedback to the controller or armband
* add something so that cannot pick up if hand is inside a wall
* improve the UI so that rather than using voice commands, user can select options from a window
* improve the game flow so that user can go back to start menu and change arms in the same game, also keep track of scores etc.
* adding other tests to the game, so that box and blocks is only one of the options

## Authors

* **Wesley Finck** - *overall project* - [WFinck97](https://github.com/WFinck97)

## License

This project is licensed under the MIT License - see the [LICENSE.md](LICENSE.md) file for details

## Acknowledgments

* [Dr. Erik Scheme](http://www.unb.ca/faculty-staff/directory/engineering-electrical-and-computer/scheme-erik.html), for being my co-supervisor
* [Dr. Scott Bateman](https://www.cs.unb.ca/people/scottb), for being my co-supervisor and helping me with many software issues
* [NSERC USRA](http://www.nserc-crsng.gc.ca/Students-Etudiants/UG-PC/USRA-BRPC_eng.asp) for funding a majority of my research
* [HCI Lab](https://hci.cs.unb.ca/) members, for help with software development, unity, and other technical issues
* [IBME](http://www.unb.ca/research/institutes/biomedical/) staff, faculty, and students for help with technical problems
* developers whose code I used / learned from, here are a few:
    * helpful [blog](https://foxypanda.me/tcp-client-in-a-uwp-unity-app-on-hololens/), and his [github](https://github.com/TimboKZ)
    * helpful [blog](https://medium.com/southworks/how-to-use-spatial-understanding-to-query-your-room-with-hololens-4a6192831a6f) and his [github](https://github.com/sGambolati/SpatialDemo)
* and all other unnamed developers who shared much of their work online, helping me get through many hurdles were it not for their open source code and advice