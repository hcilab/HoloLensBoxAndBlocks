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
    /// 
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
    /// 
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
