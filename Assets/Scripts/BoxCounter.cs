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

    // Use this for initialization
    void Start () {
        textManagerObject = GameObject.Find("TextManager");
        textManagerScript = textManagerObject.GetComponent<TextManager>();

        instantiatorController = instantiator.GetComponent<InstantiatorController>();
        boxCount = 0;
    }
	
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
