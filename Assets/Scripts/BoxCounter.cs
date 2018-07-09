using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoxCounter : MonoBehaviour {

    public int boxCount;
    public Text countdown;

    private float countTime = 20f;
    private bool started = false;
    private bool ended = false;

    // Use this for initialization
    void Start () {
        boxCount = 0;
        countdown.text = "press 's' to start timer,\nyou have 60 seconds";
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown("s"))
        {
            started = true;
        }

        if (started && !ended)
        {
            countTime -= Time.deltaTime;

            if (countTime > 0)
            {
                countdown.text = countTime.ToString();
            }

            else
            {
                countdown.text = "Times up! you successfully transferred " + boxCount + " blocks.";
                ended = true;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "pickup" && started && !ended)
        {
            boxCount++;
        }
    }
}
