# Project Assets

In this file I briefly explain the purpose and function of each of the prefabs and scripts in this project

## Prefabs

## Scripts

### BoxCounter.cs

This script is attached to the game object `CountTrigger` which is a child of the `BoxAndBlocks` prefab. It uses `OnTriggerEnter()` to check whether a cube moving through the count trigger above the partition is tagged with *pickup* and if the game is in the `TimerStarted` game state. If Both conditions are met, a counter is increased. This is used to count how many cubes are successfully transfered during the 60 second test.

### CollisionManager.cs

This script is attached to each `cube_trigger` game object which is a child of the `pickup` prefab. It is the script of a cube shaped collider set to a trigger. Using `OnTriggerEnter()` and `OnTriggerExit()` the script determines whether or not the cube is in contact with a thumb and finger. If it is, it informs [PickUpManager.cs](#pickupmanager).

### HandAnimatorManager.cs

This script is attached to the `ar_hand` prefab. It reads values from `MyoReaderClient.cs` and uses these to animate the hand(either open, close, or rest). 

### HideControllerAfterAlign.cs

This script is attached to the `whole_model_group1` game object which is a child of the `vr_controller_vive_1_5` prefab. It disables the mesh renderer of the Vive controller in unity once the controller is aligned with the real controller.

### InstantiatorController.cs

This script is attached to the `PickUpInstantiator` game object which is a child of `Instantiators` game object which is a child of `BoxAndBlock` prefab. This script checks which hand is selected, then instantiates a set number of `pickup` prefabs on the same side as the arm.

### MyoReaderClient.cs

This script is attached to the `ar_hand` prefab. It creates a TCP client socket and reads data from the myo armband sent from the Momo Server. The read string is split up and parsed into `float` variables.

### OffsetFix.cs

This script is attached to the `vr_controller_vive_1_5` prefab. It creates a TCP client socket and reads data from the Vive controller sent from the `pythonScript` server. It aligns the axes of an empty game object, `ViveAxes`, with the axes of the base stations when the user aligns the actual vive controller with the vive controller game object in unity. Then the motion and rotation of the `vr_controller_vive_1_5` is updated relative to the `ViveAxes` axes. The postition and rotation data is read from the TCP client. 

### PartitionCollider.cs


### PickUpManager.cs
### ReadFromPythonServer.cs
### ScanManager.cs
### TapAndPlace.cs
### TextManager.cs
### Timer.cs

not used for anything, should delete.

### UnityClient.cs

### VoiceManager.cs
