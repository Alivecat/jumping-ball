using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFly : MonoBehaviour {
	public float speed;
	public GameObject player;


	void Start(){
		player = GameObject.Find("Player");
		StartCoroutine (EnemyDie ());
	}

	void Update () {

		if (player.GetComponent<Player> ().currentPlayerState == Player.PlayerState.SlowerCircle) {
			if (gameObject.transform.Find("tag").tag == "L_enemy") {
				transform.Translate ((speed * 0.5f) * Time.deltaTime, 0f, 0f);
			}
			if (gameObject.transform.Find("tag").tag == "R_enemy") {
				transform.Translate ((-speed * 0.5f) * Time.deltaTime, 0f, 0f);
			}

		} else {
			if (gameObject.transform.Find("tag").tag == "L_enemy") {
				transform.Translate (speed * Time.deltaTime, 0f, 0f);
			}
			if (gameObject.transform.Find("tag").tag == "R_enemy") {
				transform.Translate (-speed * Time.deltaTime, 0f, 0f);
			}

		}


	}

	IEnumerator EnemyDie(){
		yield return new WaitForSeconds (5f);
		GameObject.Destroy (gameObject);
	}


    void OnTriggerEnter2D(Collider2D target)
    {
        if(target.tag == "Player")
        {
            if(player.GetComponent<Player>().currentPlayerState == Player.PlayerState.Immortal 
                || player.GetComponent<Player>().currentPlayerState == Player.PlayerState.penetration)
            {
                return;
            }
            Debug.Log("EnemyFly's onTriggerEnter hit you");
            player.GetComponent<Player>().GameOver();
        }

        if(target.tag == "EatPoint")
        {

            switch (gameObject.tag)
            {
                case "ImmortalEnemy":
                    player.GetComponent<Player>().currentPlayerState = Player.PlayerState.Immortal;
                    break;
                case "NormalEnemy":
                    player.GetComponent<Player>().currentPlayerState = Player.PlayerState.Normal;
                    break;
                case "PenetrationEnemy":
                    player.GetComponent<Player>().currentPlayerState = Player.PlayerState.penetration;
                    break;
                case "SlowMotionEnemy":
                    player.GetComponent<Player>().currentPlayerState = Player.PlayerState.SlowerCircle;
                    break;
            }
            Destroy(gameObject);
        }

        

    }
}
