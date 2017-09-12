using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour {

	public float speed;
    public float slowspeed;
    public float angle;
    public int colorIndex;
    
    public GameObject GM;
    public GameObject player;
    public GameObject insideCircle;
    public GameObject outsideCircle;

    void Start()
    {
        slowspeed = 80;
        player = GameObject.Find("Player");
        colorIndex = player.GetComponent<Player>().index;
        GM = GameObject.Find("GameManager");
        //GM.GetComponent<GameManager>().RotateDoubleCircle(colorIndex);
        if (this.tag == "InsideCircle")
        {
            //Debug.Log("the speed of outside circle is: " + outsideCircle.GetComponent<Rotator>().speed);
            speed = -outsideCircle.GetComponent<Rotator>().speed;
        }
        else
        {
            RandomSpeed();
        }
    }

	void Update () {
        if(player.GetComponent<Player>().currentPlayerState == Player.PlayerState.SlowerCircle)
        {
            transform.Rotate(0f, 0f, (speed * 0.8f) * Time.deltaTime);
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
            speed = Random.Range(80, 150);
            
        }
        else
        {
            speed = Random.Range(80, 220);
        }
    }


}
