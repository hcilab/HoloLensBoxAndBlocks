using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideControllerAfterAlign : MonoBehaviour {

    private Renderer mesh;

    // Use this for initialization
	void Start () {
        mesh = GetComponent<Renderer>();
        mesh.enabled = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown("space"))
        {
            //mesh.enabled = false;
        }
	}
}
