using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public GameObject[] circleGroup;
    [Space]
    public GameObject[] spawnPointGroup;
    [Space]
    public int randomIndex;

    // Use this for initialization
	void Start () {

        RandomIndex();

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void RandomIndex()
    {
        randomIndex = Random.Range(0, 2);
    }

    void spawnCircle()
    {
        foreach (GameObject point in spawnPointGroup)
        {
            Instantiate(circleGroup[randomIndex], point.transform.position, point.transform.rotation);
        }
        
    }

}
