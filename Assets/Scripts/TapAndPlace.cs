using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity.InputModule;

public class TapAndPlace : MonoBehaviour, IInputClickHandler
{
    public GameObject BoxAndBlocks;
    public GameObject TextManagerObject;

    TextManager textManager;

    // Use this for initialization
    /// <summary>
    /// 
    /// </summary>
    void Start()
    {
        TextManagerObject = GameObject.Find("TextManager");
        textManager = TextManagerObject.GetComponent<TextManager>();
    }

    /// <summary>
    /// is called when user performs the tap gesture on the sphere. Finds the rotation of the gaze about the y axis
    /// to be able to instantiate the box and blocks set up so that it is facing the user. Updates the global game 
    /// state from DoneScan to BoxPlaced.
    /// </summary>
    public void OnInputClicked(InputClickedEventData eventData)
    {
        float yCamRot = Camera.main.transform.eulerAngles.y;
        Quaternion rotation = Quaternion.Euler(0, yCamRot, 0);
        textManager.gameState = GameState.BoxPlaced;
        Instantiate(BoxAndBlocks, transform.parent.position, rotation);
        DestroyAllPlaceableMarkers();
    }

    /// <summary>
    /// destroys all game objects with tag "placeable".
    /// </summary>
    private void DestroyAllPlaceableMarkers()
    {
        GameObject[] placeables = GameObject.FindGameObjectsWithTag("placeable");
        foreach (GameObject placeable in placeables)
        {
            Destroy(placeable);
        }
    }
}
