using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorCircle : MonoBehaviour {

    public GameObject player;

    void Start()
    {
        
        player = GameObject.FindGameObjectWithTag("Player");

        if (gameObject.GetComponent<PolygonCollider2D>().isTrigger == false || 
            player.GetComponentInParent<Player>().currentPlayerState == Player.PlayerState.Immortal)
        {
            SetTriggerTrue();
        }
    }

    public void SetTriggerTrue()
    {
        gameObject.GetComponent<PolygonCollider2D>().isTrigger = true;
        return;
    }
}
