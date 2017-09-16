using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartOfColorCircle : MonoBehaviour {

    public GameObject eye;
    public GameObject eatPoint;

    void Start () {
        eye = GameObject.Find("eye");
        eatPoint = GameObject.Find("EatPoint");

        //使色彩环忽略player中子物体eye和EatPoint带的collider2D
        Physics2D.IgnoreCollision(gameObject.GetComponent<Collider2D>(), eye.GetComponent<Collider2D>(), true);
        Physics2D.IgnoreCollision(gameObject.GetComponent<Collider2D>(), eatPoint.GetComponent<Collider2D>(), true);

    }

}
