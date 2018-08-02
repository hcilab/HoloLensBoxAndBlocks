using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PickUpManager : MonoBehaviour {

    public GameObject parentShape;
    public GameObject hand;
    public float animState;
    public bool attached = false;
    public bool touchingFinger = false;
    public bool touchingThumb = false;

    private HandAnimatorManager handAnimatorManager;

    // Use this for initialization
    /// <summary>
    /// makes an instance of the HandAnimatorManager script component of ar_hand
    /// </summary>
    void Start()
    {
        hand = GameObject.Find("ar_hand");
        handAnimatorManager = hand.GetComponent<HandAnimatorManager>();
    }

    // Update is called once per frame
    /// <summary>
    /// checks if the pickup is being picked up by the hand, if it is, the pick up is made kinematic and a child of the hand.
    /// when it is no longer being picked up, it falls and stops being a child of the hand.
    /// </summary>
    void Update()
    {
        if (IsPickedUp() && !attached)
        {
            attached = true;
            parentShape.GetComponent<Rigidbody>().isKinematic = true;
            parentShape.transform.parent = hand.transform;
            handAnimatorManager.isAttached = true;
        }

        else if (!IsPickedUp() && attached)
        {
            attached = false;
            parentShape.GetComponent<Rigidbody>().isKinematic = false;
            parentShape.transform.parent = null;
            handAnimatorManager.isAttached = false;
        }
    }

    /// <summary>
    /// checks to see if pick up is in contact with a thumb and a finger
    /// </summary>
    public bool IsPickedUp()
    {
        return (touchingFinger && touchingThumb);
    }
}
