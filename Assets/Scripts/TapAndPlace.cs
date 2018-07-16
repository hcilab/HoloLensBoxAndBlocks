using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity.InputModule;

public class TapAndPlace : MonoBehaviour, IInputClickHandler
{
    public GameObject BoxAndBlocks;

    public void OnInputClicked(InputClickedEventData eventData)
    {
        float yCamRot = Camera.main.transform.eulerAngles.y;
        Quaternion rotation = Quaternion.Euler(0, yCamRot, 0);
        Instantiate(BoxAndBlocks, transform.parent.position, rotation);
        DestroyAllPlaceableMarkers();
    }

    private void DestroyAllPlaceableMarkers()
    {
        GameObject[] placeables = GameObject.FindGameObjectsWithTag("placeable");
        foreach (GameObject placeable in placeables)
        {
            Destroy(placeable);
        }
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
