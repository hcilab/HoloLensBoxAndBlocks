using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity;
using HoloToolkit.Unity.SpatialMapping;

public class TextManager : MonoBehaviour {

    public TextMesh InstructionTextMesh;
    public GameState gameState;

    public GameObject canvas;
    public GameObject viveAxes;
    public GameObject controllerVive;
    public GameObject controllerObject;
    public GameObject voiceInput;
    public GameObject spatialUnderstanding;
    public GameObject spatialMapping;
    public GameObject countTrigger;
    public GameObject mappingOrchestrator;
    public GameObject instantiator;

    public bool rightHand = false;
    public bool saidRight = false;
    public bool saidLeft = false;
    public bool saidRestart = false;

    SpatialMappingManager spatialMappingManager;
    BoxCounter boxCounter;
    SpatialUnderstandingCustomMesh customMesh;
    InstantiatorController instantiatorController;

    private bool scanDone = false;
    private float countTime = 20f; //length of test in seconds
    private int numBlocks;


    // Use this for initialization
    /// <summary>
    /// 
    /// </summary>
    void Start () {
        gameState = GameState.StartMenu;
        spatialMappingManager = spatialMapping.GetComponent<SpatialMappingManager>();
    }

    // Update is called once per frame
    /// <summary>
    /// 
    /// </summary>
    void Update () {
        switch (gameState)
        {
            case GameState.StartMenu:
#if !UNITY_EDITOR
                InstructionTextMesh.text = "Welcome to HoloLens Prosthesis Trainer!\nPlease select which arm the prosthesis is on.\n(say 'left' or 'right')";
#else
                InstructionTextMesh.text = "Welcome to HoloLens Prosthesis Trainer!\nPlease select which arm the prosthesis is on.\n(press 'l' or 'r')";
#endif
                if (Input.GetKeyDown("l") || saidLeft)
                {
                    rightHand = false;
                    StartMapping();
                }
                else if (Input.GetKeyDown("r") || saidRight)
                {
                    rightHand = true;
                    StartMapping();
                }
                break;
            case GameState.RoomScan:
                ScanText();
                break;
            case GameState.DoneScan:
                InstructionTextMesh.text = "Scan done.\nSelect a sphere to place box and blocks test.";
                break;
            case GameState.BoxPlaced:
                SpatialMappingManager.Instance.StopObserver();
                spatialUnderstanding.SetActive(false);
                EnableObjectsForTest();
                break;
            case GameState.AlignArm:
#if !UNITY_EDITOR
                InstructionTextMesh.text = "align controller with hologram and say 'align'";
#else
                InstructionTextMesh.text = "align controller with hologram and press 'space'";
#endif
                break;
            case GameState.ArmAligned:
#if !UNITY_EDITOR
                InstructionTextMesh.text = "When ready, say 'start' to start test.\nYou have 60 seconds.";
#else
                InstructionTextMesh.text = "When ready, press 's' to start test.\nYou have 60 seconds.";
#endif
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
#if !UNITY_EDITOR
                InstructionTextMesh.text = "Time's up! You successfully moved " + numBlocks + " blocks.\n Would you like to try again?\n(say 'again' to try again, or do the bloom gesture to end the game.)";
#else
                InstructionTextMesh.text = "Time's up! You successfully moved " + numBlocks + " blocks.\n Would you like to try again?\n(press 'r' to try again, or do the bloom gesture to end the game.)";
#endif
                if (Input.GetKeyDown("r") || saidRestart)
                {
                    saidRestart = false;
                    boxCounter.boxCount = 0;
                    countTime = 20;
                    gameState = GameState.ArmAligned;
                    GameObject pickupInstantiator = GameObject.Find("PickUpInstatiator");
                    instantiatorController = pickupInstantiator.GetComponent<InstantiatorController>();
                    instantiatorController.ResetPrefabs();
                }
                break;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    private void ScanText()
    {
        switch (SpatialUnderstanding.Instance.ScanState)
        {
            case SpatialUnderstanding.ScanStates.Scanning:
                InstructionTextMesh.text = "Scanning in progress.\nWhen ready, tap anywhere to finish scan.";
                break;
            case SpatialUnderstanding.ScanStates.Finishing:
                InstructionTextMesh.text = "Almost complete, finishing scan";
                break;
            case SpatialUnderstanding.ScanStates.Done:
                gameState = GameState.DoneScan;
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// If the box and blocks prefab has been instantiated, the vive axis game object is activated 
    /// as well as the vr_controller_vive_1_5 game object. It also places the controller and hand 
    /// above the box and blocks set up. Depending on which hand is selected the scale of the 
    /// vr_controller_vive_1_5 is adjusted to match the chosen hand. The V
    /// </summary>
    private void EnableObjectsForTest()
    {
        if(GameObject.Find("BoxAndBlocks(Clone)") != null)
        {
            GameObject boxBlocks = GameObject.Find("BoxAndBlocks(Clone)");
            instantiator = GameObject.Find("Instantiators");
            viveAxes.SetActive(true);
            controllerVive.SetActive(true);
            controllerVive.transform.position = boxBlocks.transform.position + new Vector3(0, 0.25f, 0) + -0.15f * boxBlocks.transform.forward;
            if (rightHand)
            {
                controllerVive.transform.localScale = new Vector3(1, 1, 1);
                controllerVive.transform.rotation = Quaternion.Euler(0, boxBlocks.transform.rotation.eulerAngles.y + 180, 0);
            }
            else
            {
                controllerVive.transform.rotation = Quaternion.Euler(0, boxBlocks.transform.rotation.eulerAngles.y, 0);
            }

            gameState = GameState.AlignArm;
        }
    }

    /// <summary>
    /// This method activates the spatial mapping and spatial understanding gameobjects (from
    /// the HoloToolkit asset) as well as the MappingOrchestrator game object. When this method is
    /// called the game state is updated to RoomScan.
    /// </summary>
    private void StartMapping()
    {
        spatialMapping.SetActive(true);
        spatialUnderstanding.SetActive(true);
        mappingOrchestrator.SetActive(true);
        gameState = GameState.RoomScan;
    }
}

/// <summary>
/// This enum is used to keep track of the state of the game, the 9 different game states are 
/// updated on certain conditions being met within this script as well as other scripts that 
/// reference this script.
/// </summary>
public enum GameState {StartMenu, RoomScan, DoneScan, BoxPlaced, AlignArm, ArmAligned, TimerStarted, TimerEnded, Restart}; 
