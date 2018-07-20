using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiatorController : MonoBehaviour {

    public GameObject textManager;
    public GameObject pickupPrefab; //for the actual prefab
    public int amountBlocks = 30;

    TextManager textManagerScript;

    GameObject pickupPrefabClone; //for the clone
    int i;

    void Start()
    {
        textManager = GameObject.Find("TextManager");
        textManagerScript = textManager.GetComponent<TextManager>();
        // fin text manager and check which hand is selected then change position of the parent first before spawning the blocks
        //transform.parent.transform.position

        if (textManagerScript.rightHand)
        {
            transform.parent.localPosition = new Vector3(0.13425f, 0, 0);
        }

        else
        {
            transform.parent.localPosition = new Vector3(-0.13425f, 0, 0);
        }
        for (i = 0; i < amountBlocks; i++)
        {
            InstantiatePrefab();
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown("r"))
        {
            ResetPrefab();
        }
    }

    void ResetPrefab()
    {
        Destroy(pickupPrefabClone);
        InstantiatePrefab();

    }

    public void InstantiatePrefab()
    {
        int colourCase;
        var placementPos = new Vector3(Random.Range(-0.12f,0.12f), Random.Range(0f, 0.075f), Random.Range(-0.11f,0.11f)) + transform.position;
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
                pickupPrefabClone.GetComponent<MeshRenderer>().material.color = Color.white;
                break;
        }
    }
}
