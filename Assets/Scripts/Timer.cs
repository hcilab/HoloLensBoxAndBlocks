using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using UnityEngine.UI;


public class Timer : MonoBehaviour {

    public Text countdown;

    private float countTime = 10f;
    private int totalCount;
    private bool started = false;

    BoxCounter boxCounter;

    // Use this for initialization
	void Start () {
        countdown.text = "press 's' to start timer,\nyou have 60 seconds";
        boxCounter = GetComponent<BoxCounter>();
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown("s"))
        {
            started = true;
        }

        if (started)
        {
            countTime -= Time.deltaTime;

            if (countTime > 0)
            {
                countdown.text = countTime.ToString();
            }

            else
            {
                totalCount = boxCounter.boxCount;
                countdown.text = "Times up! you successfully transferred " + totalCount + " blocks.";
            }
        }
    }
}
