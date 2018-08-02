using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HandAnimatorManager : MonoBehaviour {

    public float animSpeed = 1f;
    public bool isAttached = false;
    public GameObject textManager;

    private float myoLeft;
    private float myoRight;

    TextManager textManagerScript;
    Animator handAnimator;
    MyoReaderClient myoReaderClient;

	// Use this for initialization
	void Start () {
        handAnimator = GetComponent<Animator>();
        myoReaderClient = GetComponent<MyoReaderClient>();
        textManager = GameObject.Find("TextManager");
        textManagerScript = textManager.GetComponent<TextManager>();
    }

    /// <summary>
    /// called at a consistent rate. Checks to see which hand is selected, and updates myoRight
    /// and myoLeft by reaing leftReading and rightReading from MyoReaderClient script.
    /// </summary>
    private void FixedUpdate()
    {
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
    }

    /// <summary>
    /// called once per frame. Compares myoleft and myoRight and sets the animation speed to either open
    /// or closed based on what the values are.
    /// </summary>
    void Update () {

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

    /// <summary>
    /// Opens the hand, and stops the animation time from continuing once fully open.
    /// </summary>
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

    /// <summary>
    /// Closes the hand, and stops the animation time from continuing once fully closed.
    /// </summary>
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

    /// <summary>
    /// pauses the animation of the hand.
    /// </summary>
    void HandRest()
    {
        handAnimator.speed = 0f;
    }
}
