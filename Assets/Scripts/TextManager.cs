﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity;

public class TextManager : MonoBehaviour {

    public TextMesh InstructionTextMesh;
    public GameState gameState;

    private bool scanDone = false;

    // Use this for initialization
    void Start () {
        gameState = GameState.RoomScan;
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
                InstructionTextMesh.text = "align controller with hologram and say 'align'";
                break;
        }

        if (scanDone)
        {
            TextAfterScan();
        }
    }

    private void TextAfterScan()
    {
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

    }
}

public enum GameState {StartMenu, RoomScan, DoneScan, BoxPlaced, WaitingForArmAlignment, WaitingForTimerStart, TestStarted, TestEnded}; 
