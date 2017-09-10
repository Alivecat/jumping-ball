using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public GameObject[] circleGroup;
    [Space]
    public GameObject[] spawnPointGroup;
    [Space]
    public GameObject[] tempCircleGropu;
    [Space]
    public GameObject colorChangerPoint;
    [Space]
    [Space]
    public int randomIndex;

    public Player player;
    public GameObject startButton;

    public Vector3 colorChangerPointSpawnOffset = new Vector3(0f, 3.5f, 0f);
    public Vector3 spawnPointChangeOffset = new Vector3(0f, 21f, 0f);

    void Start () {
        SpawnCircle();
       
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void StartButtonClick()
    {
        startButton.SetActive(false);
        player.currentState = Player.GameState.playing;
        player.rb.gravityScale = 3;
    }

     void RandomIndex()
    {
        randomIndex = Random.Range(0, 2);
    }

    void SpawnCircle()
    {
        foreach (GameObject point in spawnPointGroup)
        {
            //调用生成方法
            InsObject(point);
        }
        
    }

    void InsObject(GameObject point)
    {
        RandomIndex();
        //将生成的圆环加入一个临时储存的数组，便于删除
        tempCircleGropu[int.Parse(point.tag)] = Instantiate(circleGroup[randomIndex], point.transform.position, point.transform.rotation);
        //生成颜色切换点
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
