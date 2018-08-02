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
    /// <summary>
    /// Checks to see if the other collider that comes in contact with this trigger is either tagged with 
    /// "finger" or "thumb" and set a bool from PickUpManager accordingly. It also checks to see if the 
    /// pickup is in contact with a wall while it is picked up, if these conditions are met the cube is 
    /// dropped.
    /// </summary>
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
        else if ((other.gameObject.tag == "wall" || other.gameObject.transform.parent.tag == "wall") && pickUpManager.IsPickedUp())
        {
            pickUpManager.touchingFinger = false;
            pickUpManager.touchingThumb = false;
        }
    }
    /// <summary>
    /// Checks to see if the other collider is tagged with "finger" or "thumb" when no longer in contact,
    /// if so, the cube is dropped.
    /// </summary>
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
