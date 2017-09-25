using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinMovement : MonoBehaviour {


    public float speedY; //Y轴速度
    public float speedX; //X轴速度
    public float locY;   //Y轴位置
    public float A;      //振幅，最高和最低的距离
    public float W;      //角速度，用于控制周期



    void Update () {
        locY = gameObject.transform.position.y;
        speedX = A * Mathf.Sin(W * -locY);
        transform.Translate(speedX, -speedY * Time.deltaTime, 0f);
        
    }

}
