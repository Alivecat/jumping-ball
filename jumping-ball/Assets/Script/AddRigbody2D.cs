using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddRigbody2D : MonoBehaviour {

    public GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
	void Update() {
        if (gameObject.GetComponent<Rigidbody2D>() == null && player.GetComponent<Player>().setRigbody2D == true)
        {
            gameObject.AddComponent<Rigidbody2D>();
            gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
        }
        
       if(gameObject.GetComponent<Rigidbody2D>() != null && player.GetComponent<Player>().setRigbody2D == false)
        {
            gameObject.GetComponent<Rigidbody2D>().simulated = false;
        }
	}
}
