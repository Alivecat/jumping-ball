using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudSpawn : MonoBehaviour {

    public Camera MainCamera;
    public GameObject[] CloudGroup;
    public Vector3 SpawnLoc;

     void Start()
    {
        //Instantiate(CloudGroup[RandomIndex()], MainCamera.transform, true);
    }

    void Update () {
		
	}

    int RandomIndex()
    {
        return Random.Range(0, 4);
    }
}
