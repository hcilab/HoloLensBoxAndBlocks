using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiatorController : MonoBehaviour {

    public GameObject textManager;
    public GameObject pickupPrefab; //for the actual prefab
    public int numCubes = 0;

    private int maxNumCubes = 30;

    TextManager textManagerScript;

    GameObject pickupPrefabClone; //for the clone

    /// <summary>
    /// creates an instance of the TextManager gameobject to access the text manager script component attached to it.
    /// checks which hand has been selected and moves the parent object the same side as the arm.
    /// </summary>
    void Start()
    {
        textManager = GameObject.Find("TextManager");
        textManagerScript = textManager.GetComponent<TextManager>();

        if (textManagerScript.rightHand)
        {
            transform.parent.localPosition = new Vector3(0.13425f, 0, 0);
        }

        else
        {
            transform.parent.localPosition = new Vector3(-0.13425f, 0, 0);
        }
    }

    /// <summary>
    /// spawns 1 block every frame until there are maxNumCubes amount.
    /// </summary>
    private void Update()
    {
        if(numCubes < maxNumCubes)
        {
            InstantiatePrefab();
            numCubes++;
        }
    }

    /// <summary>
    /// Finds all game objects with tag "counted" and destroys them. Also finds all game objects with tag "pickup"
    /// and destroys them but also instantiates a new pick up for everyone destroyed. This is so that all cubes are
    /// reset to the original side and all extra ones are destroyed.
    /// </summary>
    public void ResetPrefabs()
    {
        GameObject[] countedPickups = GameObject.FindGameObjectsWithTag("counted");
        foreach (GameObject counted in countedPickups)
        {
            Destroy(counted.transform.parent.gameObject);
        }

        GameObject[] pickups = GameObject.FindGameObjectsWithTag("pickup");
        foreach (GameObject pickup in pickups)
        {
            Destroy(pickup.transform.parent.gameObject);
            InstantiatePrefab();
        }
    }

    /// <summary>
    /// Instantiates a pickup prefab at some random location on the chosen side of the box and blocks
    /// set up. The prefab is assigned randomly one of four colours.
    /// </summary>
    public void InstantiatePrefab()
    {
        int colourCase;
        var placementPos = new Vector3(Random.Range(-0.12f,0.12f), 0.0675f, Random.Range(-0.11f,0.11f)) + transform.position;
        Quaternion placementRot = Quaternion.Euler(0, Random.Range(0f, 360f), 0);

        pickupPrefabClone = Instantiate(pickupPrefab, placementPos, placementRot, transform.parent) as GameObject;

        colourCase = Random.Range(1, 5);

        switch (colourCase)
        {
            case 1:
                pickupPrefabClone.GetComponent<MeshRenderer>().material.color = Color.red;
                break;
            case 2:
                pickupPrefabClone.GetComponent<MeshRenderer>().material.color = Color.yellow;
                break;
            case 3:
                pickupPrefabClone.GetComponent<MeshRenderer>().material.color = Color.blue;
                break;
            case 4:
                pickupPrefabClone.GetComponent<MeshRenderer>().material.color = Color.green;
                break;
            default:
                break;
        }
    }
}
