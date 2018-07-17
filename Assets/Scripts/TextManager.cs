﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity;

public class TextManager : MonoBehaviour {

    public TextMesh InstructionTextMesh;
    public GameState gameState;
    public GameObject canvas;
    public GameObject viveAxes;
    public GameObject controllerVive;
    public GameObject voiceInput;
    public GameObject spatialUnderstanding;
    public GameObject countTrigger;

    BoxCounter boxCounter;
    SpatialUnderstandingCustomMesh customMesh;

    private bool scanDone = false;
    private float countTime = 60f;
    private int numBlocks;


    // Use this for initialization
    void Start () {
        gameState = GameState.RoomScan;
        customMesh = spatialUnderstanding.GetComponent<SpatialUnderstandingCustomMesh>();
    }

    // Update is called once per frame
    void Update () {

        switch (gameState)
        {
            case GameState.RoomScan:
                ScanText();
                break;
            case GameState.DoneScan:
                InstructionTextMesh.text = "Scan done.\nSelect a sphere to place box and blocks test.";
                break;
            case GameState.BoxPlaced:
                //enable other game objects now
                EnableObjectsForTest();
                break;
            case GameState.AlignArm:
                InstructionTextMesh.text = "align controller with hologram and say 'align'";
                spatialUnderstanding.SetActive(false); //get rid of scan mesh
                break;
            case GameState.ArmAligned:
                InstructionTextMesh.text = "When ready, say 'start' to start test.\nYou have 60 seconds.";
                if (Input.GetKeyDown("s"))
                {
                    gameState = GameState.TimerStarted;
                }
                break;
            case GameState.TimerStarted:
                if (countTime > 0)
                {
                    countTime -= Time.deltaTime;
                    InstructionTextMesh.text = countTime.ToString();
                }
                else
                {
                    countTrigger = GameObject.Find("CountTrigger");
                    boxCounter = countTrigger.GetComponent<BoxCounter>();
                    numBlocks = boxCounter.boxCount;
                    gameState = GameState.TimerEnded;
                }
                break;
            case GameState.TimerEnded:
                InstructionTextMesh.text = "Time's up! You successfully moved " + numBlocks + " blocks.";
                break;
        }
    }

    private void ScanText()
    {
        switch (SpatialUnderstanding.Instance.ScanState)
        {
            case SpatialUnderstanding.ScanStates.None:
                break;
            case SpatialUnderstanding.ScanStates.ReadyToScan:
                break;
            case SpatialUnderstanding.ScanStates.Scanning:
                InstructionTextMesh.text = "Scanning in progress.\nWhen ready, tap anywhere to finish scan.";
                break;
            case SpatialUnderstanding.ScanStates.Finishing:
                this.InstructionTextMesh.text = "State: Finishing Scan";
                break;
            case SpatialUnderstanding.ScanStates.Done:
                gameState = GameState.DoneScan;
                break;
            default:
                break;
        }
    }

    private void EnableObjectsForTest()
    {
        if(GameObject.Find("BoxAndBlocks(Clone)") != null)
        {
            GameObject boxBlocks = GameObject.Find("BoxAndBlocks(Clone)");
            viveAxes.SetActive(true);
            controllerVive.SetActive(true);
            controllerVive.transform.position = boxBlocks.transform.position + new Vector3(0, 0.25f, 0) + -0.5f * boxBlocks.transform.forward;
            controllerVive.transform.Rotate(0, boxBlocks.transform.rotation.eulerAngles.y, 0);

            voiceInput.SetActive(true);
            gameState = GameState.AlignArm;
        }
    }
}

public enum GameState {StartMenu, RoomScan, DoneScan, BoxPlaced, AlignArm, ArmAligned, TimerStarted, TimerEnded, Restart}; 
