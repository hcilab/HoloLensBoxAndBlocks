using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxCounter : MonoBehaviour {

    public int boxCount;

	// Use this for initialization
	void Start () {
        boxCount = 0;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "pickup")
        {
            boxCount++;
        }
    }
}
