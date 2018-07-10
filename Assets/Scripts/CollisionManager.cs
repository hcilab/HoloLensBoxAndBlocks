using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionManager : MonoBehaviour {

    public GameObject hand;
    PickUpManager pickUpManager;

    // Use this for initialization
    void Start()
    {
        hand = GameObject.Find("ar_hand");
        pickUpManager = GetComponent<PickUpManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "finger")
        {
            pickUpManager.touchingFinger = true;
        }
        else if (other.gameObject.tag == "thumb")
        {
            pickUpManager.touchingThumb = true;
        }

        else if (other.gameObject.tag == "wall")
        {
            pickUpManager.touchingFinger = false;
            pickUpManager.touchingThumb = false;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "finger")
        {
            pickUpManager.touchingFinger = false;
        }
        else if (other.gameObject.tag == "thumb")
        {
            pickUpManager.touchingThumb = false;
        }
    }
}
