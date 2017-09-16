using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{

    public float speed;
    public float slowspeed;
    public float angle;
    public int colorIndex;

    public GameObject GM;
    public GameObject player;
    public GameObject insideCircle;
    public GameObject outsideCircle;
    public GameObject eye;
    public GameObject eatPoint;

    void Start()
    {
        player = GameObject.Find("Player");
        colorIndex = player.GetComponent<Player>().index;
        GM = GameObject.Find("GameManager");
        
        if (this.tag == "InsideCircle")
        {
            //内圆与外圆以相等速度反向旋转
            speed = -outsideCircle.GetComponent<Rotator>().speed;
        }
        else
        {
            //外圆随机转速
            RandomSpeed();
        }
    }

    void Update()
    {
        if (player.GetComponent<Player>().currentPlayerState == Player.PlayerState.SlowerCircle)
        {
            //子弹时间内放慢速度
            transform.Rotate(0f, 0f, (speed * 0.5f) * Time.deltaTime);
        }
        else
        {
            transform.Rotate(0f, 0f, speed * Time.deltaTime);
        }

    }




    void RandomSpeed()
    {
        if (this.tag == "OutsideCircle")
        {
            speed = Random.Range(30, 100);

        }
        else
        {
            speed = Random.Range(50, 150);
        }
    }


}
