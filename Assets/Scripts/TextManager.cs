using System.Collections;
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
    public GameObject spatialMapping;
    public GameObject countTrigger;
    public GameObject mappingOrchestrator;
    public GameObject instantiator;

    public bool rightHand = false;
    public bool saidRight = false;
    public bool saidLeft = false;
    public bool saidRestart = false;

    BoxCounter boxCounter;
    SpatialUnderstandingCustomMesh customMesh;
    InstantiatorController instantiatorController;

    private bool scanDone = false;
    private float countTime = 20f;
    private int numBlocks;


    // Use this for initialization
    void Start () {
        gameState = GameState.StartMenu;
        //customMesh = spatialUnderstanding.GetComponent<SpatialUnderstandingCustomMesh>();
    }

    // Update is called once per frame
    void Update () {

        switch (gameState)
        {
            case GameState.StartMenu:
                InstructionTextMesh.text = "Welcome to HoloLens Prosthesis Trainer!\nPlease select which arm the prosthesis is on.\n(say 'left' or 'right')";
                //multiply hand scale by -1, or whatever I need to do to get it to be a right hand
                //make blocks spawn on other side
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
                //stop the observer here
                break;
            case GameState.BoxPlaced:
                //enable other game objects now
                //stop the observer here

                //disable the mesh here
                spatialUnderstanding.SetActive(false); //get rid of scan mesh
                EnableObjectsForTest();
                break;
            case GameState.AlignArm:
                InstructionTextMesh.text = "align controller with hologram and say 'align'";
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
                    Debug.Log(numBlocks.ToString());
                    gameState = GameState.TimerEnded;
                }
                break;
            case GameState.TimerEnded:
                InstructionTextMesh.text = "Time's up! You successfully moved " + numBlocks + " blocks.\n Would you like to try again?\n(say 'restart' to try again, or do the bloom gesture to end the game.)";
                if (Input.GetKeyDown("r") || saidRestart)
                {
                    saidRestart = false;
                    boxCounter.boxCount = 0;
                    countTime = 20;
                    //reset prefabs
                    gameState = GameState.ArmAligned;
                    GameObject pickupInstantiator = GameObject.Find("PickUpInstatiator");
                    instantiatorController = pickupInstantiator.GetComponent<InstantiatorController>();
                    instantiatorController.ResetPrefabs();
                }
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
            instantiator = GameObject.Find("Instantiators");
            viveAxes.SetActive(true);
            controllerVive.SetActive(true);
            controllerVive.transform.position = boxBlocks.transform.position + new Vector3(0, 0.25f, 0) + -0.5f * boxBlocks.transform.forward;
            if (rightHand)
            {
                controllerVive.transform.localScale = new Vector3(1, 1, 1);
                controllerVive.transform.rotation = Quaternion.Euler(0, boxBlocks.transform.rotation.eulerAngles.y + 180, 0);
            }
            else
            {
                controllerVive.transform.rotation = Quaternion.Euler(0, boxBlocks.transform.rotation.eulerAngles.y, 0);
            }

            voiceInput.SetActive(true);
            gameState = GameState.AlignArm;
        }
    }

    private void StartMapping()
    {
        spatialMapping.SetActive(true);
        spatialUnderstanding.SetActive(true);
        mappingOrchestrator.SetActive(true);
        gameState = GameState.RoomScan;
    }
}

public enum GameState {StartMenu, RoomScan, DoneScan, BoxPlaced, AlignArm, ArmAligned, TimerStarted, TimerEnded, Restart}; 
