using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public GameObject[] circleGroup;
    [Space]
    public GameObject[] spawnPointGroup;
    [Space]
    public GameObject[] tempCircleGropu;
    [Space]
    public GameObject colorChangerPoint;
    [Space]
    public int randomIndex;
    public Vector3 colorChangerPointSpawnOffset = new Vector3(0f, 3.5f, 0f);
    public Vector3 spawnPointChangeOffset = new Vector3(0f, 21f, 0f);

    void Start () {

        SpawnCircle();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void RandomIndex()
    {
        randomIndex = Random.Range(0, 2);
    }

    void SpawnCircle()
    {
        foreach (GameObject point in spawnPointGroup)
        {
            InsObject(point);
        }
        
    }

    void InsObject(GameObject point)
    {
        RandomIndex();
        tempCircleGropu[int.Parse(point.tag)] = Instantiate(circleGroup[randomIndex], point.transform.position, point.transform.rotation);
        Instantiate(colorChangerPoint, point.transform.position + colorChangerPointSpawnOffset, point.transform.rotation);
    }

    public void MoveSpawn(GameObject spawnPoint)
    {
        Debug.Log(spawnPoint.tag);
        spawnPoint.transform.position = new Vector3(0f, spawnPoint.transform.position.y + spawnPointChangeOffset.y, 0f);
        InsObject(spawnPoint);
    }

    public void DestoryCycle(GameObject spawnPoint)
    {
        Destroy(tempCircleGropu[int.Parse(spawnPoint.tag)]);  
    }
}
