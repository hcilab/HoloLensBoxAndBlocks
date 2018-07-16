using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity;

public class TextManager : MonoBehaviour {

    public TextMesh InstructionTextMesh;

    GameState gameState;
    private bool scanDone = false;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
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
                scanDone = true;
                break;
            default:
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
}

public enum GameState {StartMenu, ReadyToScan, Scanning, FinishingScan, DoneScan, WaitingForArmAlignment, WaitingForTimerStart, TestStarted, TestEnded}; 
