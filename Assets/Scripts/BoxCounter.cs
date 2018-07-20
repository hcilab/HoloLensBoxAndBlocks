using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoxCounter : MonoBehaviour {

    public GameObject instantiator;
    public int boxCount;
    public Text countdown;

    private float countTime = 60f;
    public bool started = false;
    private bool ended = false;

    InstantiatorController instantiatorController;

    // Use this for initialization
    void Start () {
        boxCount = 0;
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
            }

            else
            {
                ended = true;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "pickup" && started && !ended)
        {
            boxCount++;
            other.gameObject.tag = "counted";
            Debug.Log("the count: " + boxCount.ToString());
        }

        
    }
}
