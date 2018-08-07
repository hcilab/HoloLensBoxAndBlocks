# Project Assets

In this file I briefly explain the purpose and function of each of the prefabs and scripts in this project

## Prefabs

![List of Prefabs in editor](Images/PrefabList_2.png)

### ar_hand.prefab

Is the model of the hand that is tracked my the vive controller and used to pick-up blocks

### BoxAndBlocks.prefab

Is the Box and Blocks set up measured to the official dimensions. The side that spawns the blocks is raised 5 cm to closer emulate the high at which most blocks would be picked up at(since the actual test has 150 blocks whereas this set up only has 30 due to rendering issues).

### pickup.prefab

Is the cube that is instantiated on the box and blocks set up. Made to be picked up by the ar_hand prefab.

### PickUpInstantiator.prefab

has the `InstantiatorController.cs` script as a component. Is positioned in the middle of either side the unfolder box and blocks so that blocks can randomly spawn anywhere within that side.

### PlaceableMarker.prefab

Is the game object that is placed in each "placeable" location after the room scan is complete.

### SteamVR_male_hand_left.prefab

is a duplicate of ar_hand. is not used in the game.

### vr_controller_vive_1_5.prefab

Is the prefab of the vive controller model and is used to align the real vive controller with this game object.

### vr_male_hand_left.prefab

Is a duplicate of ar_hand. is not used in the game.
## Scripts

Please see individual scripts for more detailed description of the function and subsequent methods.

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

Not used for anything, should delete.

### PickUpManager.cs

This script is attached to each `cube_trigger` game object which is a child of the `pickup` prefab. It checks to see if a block is picked up and not already attached to the hand, if this condition is met, the pickup becomes kinematic and a child of the hand to make it seem as though it is picked up. 

### ScanManager.cs

This script is attached to the `MappingOrchestrator` game object. It scans the room using the HoloLens spatial mapping and understanding capabilities. Once the room is scanned it checks any surfaces that meet the constrains of a "placeable" location. A sphere is instantiated above each location that meets the criteria.

### TapAndPlace.cs

This script is attached to the `Sphere` game object which is a child of the `PlaceableMarker` prefab. If the gaze icon in on the sphere and the user does the 'click' gesture, all placeable markers will be deleted and the `BoxAndBlocks` prefab will spawn at the selected marker.

### TextManager.cs

This script is attached to the `TextManager` game object. It controls the flow of the game and breaks it up into different states. Depending on which state the game is in, different lines of code will execute. The game state can change from events occuring in this script or other scripts that reference `TextManager.cs`. It also prompts the user on what to do next with text depending on the current game state.

### Timer.cs

not used for anything, should delete.

### VoiceManager.cs

This script is attached to the `VoiceInput` game object. It takes voice input and checks to see if there are any matches with pre-defined keywords. If there is a match and it occurs during the proper game state, certain actions will take place.
