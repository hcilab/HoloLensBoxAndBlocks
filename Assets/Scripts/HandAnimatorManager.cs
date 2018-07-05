using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HandAnimatorManager : MonoBehaviour {

    public float animSpeed = 1f;

    Animator handAnimator;

	// Use this for initialization
	void Start () {
        handAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update () {

        if (Input.GetKey("up"))
        {
            HandOpen();
        }

        else if (Input.GetKey("down"))
        {
            HandClose();
        }

        else
        {
            HandRest();
        }
	}

    void HandOpen()
    {
        handAnimator.speed = 1;
        handAnimator.SetFloat("Speed", 1f * animSpeed);
    }

    void HandClose()
    {

    }

    void HandRest()
    {
        handAnimator.speed = 0f;
    }
}
