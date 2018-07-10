using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartitionCollider : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "cube_trigger")
        {
            //if is picked up, drop it.
            //drop();
        }
    }
}
