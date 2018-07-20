using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HandAnimatorManager : MonoBehaviour {

    public float animSpeed = 1f;
    public bool isAttached = false;
    public GameObject textManager;
    TextManager textManagerScript;


    private float myoLeft;
    private float myoRight;

    Animator handAnimator;
    MyoReaderClient myoReaderClient;

	// Use this for initialization
	void Start () {
        handAnimator = GetComponent<Animator>();
        myoReaderClient = GetComponent<MyoReaderClient>();
        textManager = GameObject.Find("TextManager");
        textManagerScript = textManager.GetComponent<TextManager>();
    }

    // Update is called once per frame
    void Update () {

        if (textManagerScript.rightHand)
        {
            myoRight = myoReaderClient.leftReading;
            myoLeft = myoReaderClient.rightReading;
        }
        else
        {
            myoLeft = myoReaderClient.leftReading;
            myoRight = myoReaderClient.rightReading;
        }

        if (myoLeft > myoRight)
        {
            animSpeed = myoLeft;
            HandOpen();
        }

        else if(myoLeft < myoRight)
        {
            animSpeed = myoRight;
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
