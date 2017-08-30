using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFly : MonoBehaviour {
	public float speed;
    public float dieTime;
	public GameObject player;
    public GameObject eye;


	void Start(){
		player = GameObject.Find("Player");
        eye = GameObject.Find("eye");
        dieTime = 5f;
        StartCoroutine (EnemyDie());
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
		yield return new WaitForSeconds (dieTime);
		GameObject.Destroy (gameObject);
	}


    void OnTriggerEnter2D(Collider2D target)
    {

        if(target.tag == "Player")
        {
            if(player.GetComponent<Player>().currentPlayerState == Player.PlayerState.penetration)
            {
                return;
            }

            if(player.GetComponent<Player>().currentPlayerState == Player.PlayerState.Immortal)
            {
                Destroy(gameObject);
            }
            player.GetComponent<Player>().GameOver(1);

        }

        if(target.tag == "EatPoint")
        {
            Destroy(gameObject);

            switch (gameObject.tag)
            {
                case "ImmortalEnemy":
                    player.GetComponent<Player>().currentPlayerState = Player.PlayerState.Immortal;
                    player.GetComponent<Player>().setToNormalTrigger = true;
                    break;
                case "NormalEnemy":
                    player.GetComponent<Player>().scoreText++;
                    player.GetComponent<Player>().SetScore();
                    break;
                case "PenetrationEnemy":
                    player.GetComponent<Player>().currentPlayerState = Player.PlayerState.penetration;
                    player.GetComponent<Player>().setToNormalTrigger = true;
                    break;
                case "SlowMotionEnemy":
                    player.GetComponent<Player>().currentPlayerState = Player.PlayerState.SlowerCircle;
                    player.GetComponent<Player>().setToNormalTrigger = true;
                    break;
            }
            
            
        }

    }

    
}
