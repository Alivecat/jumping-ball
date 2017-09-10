using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour {

	public float speed;
    public float angle;
    public int colorIndex;
    public GameObject ball;
    public GameObject insideCircle;
    public GameObject outsideCircle;

    void Start()
    {
        ball = GameObject.Find("Player");
        colorIndex = ball.GetComponent<Player>().index;
        //Debug.Log(colorIndex);

        SetDoubleCircleRotation(colorIndex);
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
		transform.Rotate(0f, 0f, speed * Time.deltaTime);
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

    public void SetDoubleCircleRotation(int index)
    {
        switch (index)
        {
            case 0: //cyan
                Debug.Log("cyan");
                this.transform.Rotate(0f, 0f, 135f);
                break;
            case 1: //yellow
                Debug.Log("yellow");
                this.transform.Rotate(0f, 0f, -135f);
                break;
            case 2: //magenta
                Debug.Log("magenta");
                this.transform.Rotate(0f, 0f, -45f);
                break;
            case 3: //pink
                Debug.Log("pink");
                this.transform.Rotate(0f, 0f, 45f);
                break;
        }

    }

}
