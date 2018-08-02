using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoxCounter : MonoBehaviour {

    public GameObject textManagerObject;
    public GameObject instantiator;
    public int boxCount;
    public Text countdown;

    private float countTime = 60f;
    public bool started = false;
    private bool ended = false;

    InstantiatorController instantiatorController;
    TextManager textManagerScript;

    /// <summary>
    /// 
    /// </summary>
    void Start () {
        textManagerObject = GameObject.Find("TextManager");
        textManagerScript = textManagerObject.GetComponent<TextManager>();

        instantiatorController = instantiator.GetComponent<InstantiatorController>();
        boxCount = 0;
    }
    /// <summary>
    /// is called when another object enters the trigger. Checks whether the object is a pickup and if game is
    /// in the TimerStarted game state. If both conditions are met, boxCount increments and is used to count
    /// the number of blocks successfully moved over during the 60 second test. A new pickup prefab is instantiated
    /// everytime a blocks is successfully moved over. This is to ensure there are always the same amount of blocks 
    /// on the side to pick them up from.
    /// </summary>
    private void OnTriggerEnter(Collider other)
    {
        if ((other.gameObject.tag == "pickup") && (textManagerScript.gameState == GameState.TimerStarted))
        {
            boxCount++;
            other.gameObject.tag = "counted";
            Debug.Log("the count: " + boxCount.ToString());
            instantiatorController.InstantiatePrefab();
        }
    }
}
