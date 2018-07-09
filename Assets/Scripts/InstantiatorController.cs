using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiatorController : MonoBehaviour {

    public GameObject pickupPrefab; //for the actual prefab
    GameObject pickupPrefabClone; //for the clone

    int i;

    void Start()
    {
        for(i = 0; i < 10; i++)
        {
            InstantiatePrefab();
        }
        //InstantiatePrefab();
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
        //pickupPrefabClone = Instantiate(pickupPrefab, transform.position, Quaternion.identity) as GameObject;

        var placementPos = new Vector3(Random.Range(-0.12f,0.12f), 0, Random.Range(-0.11f,0.11f)) + transform.position;
        Quaternion placementRot = Quaternion.Euler(0, Random.Range(0f, 360f), 0);

        pickupPrefabClone = Instantiate(pickupPrefab, placementPos, placementRot, transform.parent) as GameObject;
        //pickupPrefabClone.transform.parent = transform.parent;
        //pickupPrefabClone.transform.localScale = new Vector3(0.3967236f, 0.3967236f, 0.3967236f);
    }
}
