using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public GameObject[] circleGroup;            //色环的预制体组
    [Space]
    public GameObject[] spawnPointGroup;        //刷新点组
    [Space]
    public GameObject[] tempCircleGropu;        //保存目前出现的色环的引用 
    [Space]
    public GameObject colorChangerPoint;        //颜色切换点
    [Space]
    [Space]
    public int randomIndex;                     

    public enum GameState { gameover, playing };    //游戏状态
    public GameState currentGameState;

    public Player player;
    public GameObject startButton;
    public EnemySpawn enemySpawn;

    public Vector3 colorChangerPointSpawnOffset = new Vector3(0f, 3.5f, 0f);    //颜色切换点间距   
    public Vector3 spawnPointChangeOffset = new Vector3(0f, 21f, 0f);           //刷新点切换间距

    void Start () {
        SpawnCircle(); 
    }

    public void StartButtonClick()
    {
        startButton.SetActive(false);
        currentGameState = GameState.playing;
        player.rb.gravityScale = 2;
        enemySpawn.StartCoroutine(enemySpawn.InsEnemy());
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
        //在中心点生成颜色切换点
        Instantiate(colorChangerPoint, point.transform.position, point.transform.rotation);
    }

    public void MoveSpawn(GameObject spawnPoint)
    {
        //将刷新点上移
        spawnPoint.transform.position = new Vector3(0f, spawnPoint.transform.position.y + spawnPointChangeOffset.y, spawnPoint.transform.position.z);
        InsObject(spawnPoint);
    }

    public void DestoryCycle(GameObject spawnPoint)
    {
        //销毁生成点时销毁对应的色环
        Destroy(tempCircleGropu[int.Parse(spawnPoint.tag)]);  
    }

    public void RotateDoubleCircle(int index)
    {
        foreach (GameObject circle in tempCircleGropu)
		{ Transform circlTransform = circle.transform;
            if(circle.tag == "DoubleCircle")
            {
                switch (index)
                {
                    //同步双色环的目标颜色
                    case 0: //Cyan
                        Quaternion cyanRotation = Quaternion.Euler(0f, 0f, -45f);
						circlTransform.rotation = Quaternion.Slerp(circle.transform.rotation, cyanRotation, Time.deltaTime* 2f);
                       
                        break;

                    case 1: //yellow
                        Quaternion yellowRotation = Quaternion.Euler(0f, 0f, 135f);
					    circlTransform.rotation = Quaternion.Slerp(circle.transform.rotation, yellowRotation, Time.deltaTime* 2f);
                        
                        break;

                    case 2: //magenta
                        Quaternion magentaRotation = Quaternion.Euler(0f, 0f, -135f);
					    circlTransform.rotation = Quaternion.Slerp(circle.transform.rotation, magentaRotation, Time.deltaTime* 2f);
                       
                        break;

                    case 3: //pink
                        Quaternion pinkRotation = Quaternion.Euler(0f, 0f, 45f);
					    circlTransform.rotation = Quaternion.Slerp(circle.transform.rotation, pinkRotation, Time.deltaTime* 1.5f); 
                        break;

                }
            }
        }
			
    }

}
