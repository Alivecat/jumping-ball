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
   
    public enum GameState { gameover, playing };
    public GameState currentGameState;
    [Space]
    public int randomIndex;

    public Player player;
    public GameObject startButton;

    public Vector3 colorChangerPointSpawnOffset = new Vector3(0f, 4f, 0f);
    public Vector3 spawnPointChangeOffset = new Vector3(0f, 24f, 0f);

    void Start () {
        SpawnCircle();
       
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void StartButtonClick()
    {
        startButton.SetActive(false);
        currentGameState = GameState.playing;
        player.rb.gravityScale = 2;
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

    public void TriggerSpawnPointEdge(Collider2D col)
    {
        
            DestoryCycle(col.transform.parent.gameObject);
            MoveSpawn(col.transform.parent.gameObject);
            return;
        
    }

    void InsObject(GameObject point)
    {
        
        RandomIndex();
        //将生成的圆环加入一个临时储存的数组，便于删除
        tempCircleGropu[int.Parse(point.tag)] = Instantiate(circleGroup[randomIndex], point.transform.position, point.transform.rotation);
        //在中心点生成颜色切换点
        Instantiate(colorChangerPoint, point.transform.position, point.transform.rotation);
        return;
    }

    public void MoveSpawn(GameObject spawnPoint)
    {
        spawnPoint.transform.position = new Vector3(0f, spawnPoint.transform.position.y + spawnPointChangeOffset.y, 0f);
        Debug.Log(spawnPoint.transform.position);
        Debug.Log(this.gameObject.name);
        InsObject(spawnPoint);
        return;
    }

    public void DestoryCycle(GameObject spawnPoint)
    {
        Destroy(tempCircleGropu[int.Parse(spawnPoint.tag)]);
        return;
    }

    public void RotateDoubleCircle(int index)
    {
        foreach (GameObject circle in tempCircleGropu)
        {
            if(circle.tag == "DoubleCircle")
            {
                switch (index)
                {
                    case 0: //Cyan
                        Quaternion cyanRotation = Quaternion.Euler(0f, 0f, 135f);
                        circle.transform.rotation = Quaternion.Slerp(circle.transform.rotation, cyanRotation, Time.deltaTime* 2f);
                       // Debug.Log(circle.transform.rotation);
                        break;

                    case 1: //yellow
                        Quaternion yellowRotation = Quaternion.Euler(0f, 0f, -135f);
                        circle.transform.rotation = Quaternion.Slerp(circle.transform.rotation, yellowRotation, Time.deltaTime* 2f);
                        //Debug.Log(circle.transform.rotation);
                        break;

                    case 2: //magenta
                        Quaternion magentaRotation = Quaternion.Euler(0f, 0f, -45f);
                        circle.transform.rotation = Quaternion.Slerp(circle.transform.rotation, magentaRotation, Time.deltaTime* 2f);
                       // Debug.Log(circle.transform.rotation);
                        break;

                    case 3: //pink
                        Quaternion pinkRotation = Quaternion.Euler(0f, 0f, 45f);
                        circle.transform.rotation = Quaternion.Slerp(circle.transform.rotation, pinkRotation, Time.deltaTime* 1.5f);
                       // Debug.Log(circle.transform.rotation);
                        break;

                }
            }
        }
    }

}
