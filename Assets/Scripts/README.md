# HoloLensBoxAndBlocks Unity Script Descriptions

In this .md file I briefly explain the purpose and function of each of the scripts in this project

## BoxCounter

This script is attached to the game object `CountTrigger` which is a child of the `BoxAndBlocks` prefab. It uses `OnTriggerEnter()` to check whether a cube moving through the count trigger above the partition is tagged with *pickup* and if the game is in the `TimerStarted` game state. If Both conditions are met, a counter is increased. This is used to count how many cubes are successfully transfered during the 60 second test.

## CollisionManager

This script is attached to  
## HandAnimatorManager
## HideControllerAfterAlign
## InstantiatorController
## MyoReaderClient
## OffsetFix
## PartitionCollider
## PickUpManager
## ReadFromPythonServer
## ScanManager
## TapAndPlace
## TextManager
## Timer

not used for anything, should delete.

## UnityClient

## VoiceManager
