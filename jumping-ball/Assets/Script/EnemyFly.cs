using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFly : MonoBehaviour {
	public float speed;
    public float dieTime;
    public bool damage;
    public float smoothing;

    public GameObject player;
    public GameObject eye;
    public Animator animator;

	void Start(){
        smoothing = 5f;
        damage = true;
        animator = gameObject.GetComponent<Animator>();
        player = GameObject.Find("Player");
        eye = GameObject.Find("eye");
        dieTime = 6f;
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
        if(!damage)
            gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, player.transform.position, smoothing * Time.deltaTime);
	}

    IEnumerator EnemyDie(float dieTime = 10f){
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

            if (damage)
            {
                player.GetComponent<Player>().GameOver(1);
            }
            

        }

        if(target.tag == "EatPoint")
        {
            damage = false;
            animator.SetTrigger("isDie");
            StartCoroutine(EnemyDie(0.5f));
            speed = 1f;
            
            switch (gameObject.tag)
            {
                case "ImmortalEnemy":
                    player.GetComponent<Player>().currentPlayerState = Player.PlayerState.Immortal;
                    player.GetComponent<Player>().setToNormalTrigger = true;
                    player.GetComponent<Player>().playerAnimator.SetTrigger("isEatting");
                    break;
                case "NormalEnemy":
                    player.GetComponent<Player>().scoreText++;
                    player.GetComponent<Player>().SetScore();
                    player.GetComponent<Player>().playerAnimator.SetTrigger("isEatting");
                    break;
                case "PenetrationEnemy":
                    player.GetComponent<Player>().currentPlayerState = Player.PlayerState.penetration;
                    player.GetComponent<Player>().setToNormalTrigger = true;
                    player.GetComponent<Player>().playerAnimator.SetTrigger("isEatting");
                    break;
                case "SlowMotionEnemy":
                    player.GetComponent<Player>().currentPlayerState = Player.PlayerState.SlowerCircle;
                    player.GetComponent<Player>().setToNormalTrigger = true;
                    player.GetComponent<Player>().playerAnimator.SetTrigger("isEatting");
                    break;
            }
            
            
        }

    }

    
}
