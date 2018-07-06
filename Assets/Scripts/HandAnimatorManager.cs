using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HandAnimatorManager : MonoBehaviour {

    public float animSpeed = 1f;
    public bool isAttached = false;

    Animator handAnimator;

	// Use this for initialization
	void Start () {
        handAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update () {

        if (Input.GetKey("o"))
        {
            HandOpen();
        }

        else if (Input.GetKey("p"))
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
        if (handAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0f)
        {
            handAnimator.speed = 1;
            handAnimator.SetFloat("Speed", -1f * animSpeed);
        }

        else
        {
            HandRest();
        }
    }

    void HandClose()
    {
        if (handAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f && !isAttached)
        {
            handAnimator.speed = 1;
            handAnimator.SetFloat("Speed", 1f * animSpeed);
        }

        else
        {
            HandRest();
        }
    }

    void HandRest()
    {
        handAnimator.speed = 0f;
    }
}
