using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiatorController : MonoBehaviour {

    public GameObject pickupPrefab; //for the actual prefab
    GameObject pickupPrefabClone; //for the clone

    void Start()
    {
        InstantiatePrefab();
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

    void InstantiatePrefab()
    {
        pickupPrefabClone = Instantiate(pickupPrefab, transform.position, Quaternion.identity) as GameObject;
        pickupPrefabClone.transform.parent = transform.parent;
        //pickupPrefabClone.transform.localScale = new Vector3(0.3967236f, 0.3967236f, 0.3967236f);
    }
}
