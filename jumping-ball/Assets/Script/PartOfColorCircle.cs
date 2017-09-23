using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartOfColorCircle : MonoBehaviour {

    public Player playerCom;

    public Collider2D eye;
    public Collider2D eatPoint;

    private Collider2D thisCollider2D;

    void Start () {
        playerCom = GameObject.Find("Player").GetComponent<Player>();
        eye = GameObject.Find("eye").GetComponent<Collider2D>();
        eatPoint = GameObject.Find("EatPoint").GetComponent<Collider2D>();
        thisCollider2D = gameObject.GetComponent<Collider2D>();

        //使色彩环忽略player中子物体eye和EatPoint带的collider2D
        Physics2D.IgnoreCollision(thisCollider2D, eye, true);
        Physics2D.IgnoreCollision(thisCollider2D, eatPoint, true);

    }

    private void Update()
    {
        if (playerCom.ReIgnoreCollisionTrigger)
        {
            
            Physics2D.IgnoreCollision(thisCollider2D, eye, true);
            Physics2D.IgnoreCollision(thisCollider2D, eatPoint, true);
            playerCom.ReIgnoreCollisionTrigger = false;
            Debug.Log("ReIgnoreCollisionTrigger");
        }
    }
}
