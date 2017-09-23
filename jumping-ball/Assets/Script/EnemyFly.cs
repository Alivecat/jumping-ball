using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFly : MonoBehaviour {
	public float speed;
    public float dieTime;
    public bool damage;
    public float smoothing;

    public GameObject player;
    public Player playerCom;
    public GameObject eye;
	public GameObject mouse;
    public Animator animator;

	void Start(){
        dieTime = 6f;
        smoothing = 5f;
        damage = true;

        mouse = GameObject.Find ("EatPoint");       
        player = GameObject.Find("Player");  
        eye = GameObject.Find("eye");
        animator = gameObject.GetComponent<Animator>();
        playerCom = player.GetComponent<Player>();
        StartCoroutine (EnemyDie());
	}

	void Update () {
        //怪物左右飞行
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

		if (!damage) {
            //怪物飞入嘴中
			gameObject.transform.position = Vector3.Lerp (gameObject.transform.position, player.transform.position, smoothing * Time.deltaTime);
		}
	}

    void OnTriggerEnter2D(Collider2D target)
    {

        if(target.tag == "Player")
        {
            if(playerCom.currentPlayerState == Player.PlayerState.penetration)
            {
                return;
            }

            if(playerCom.currentPlayerState == Player.PlayerState.Immortal)
            {
                Destroy(gameObject);
            }

            if (damage)
            {
                playerCom.GameOver(1);
            }
            

        }

        if(target.tag == "EatPoint")
        {
            damage = false; //怪物是否可造成伤害
			animator.SetTrigger("isDie");
			StartCoroutine(EnemyDie(0.5f));
			speed = 1f; // 死亡小怪减速飞入嘴中

            switch (gameObject.tag)
            {

			case "ImmortalEnemy":
                    SetBuff(true, Player.PlayerState.Immortal, false, true);
                    break;
                case "NormalEnemy":
                    SetBuff(false, Player.PlayerState.Normal, true, false);
                    break;
                case "PenetrationEnemy":
                    SetBuff(true, Player.PlayerState.penetration, false, true);
                    break;
                case "SlowMotionEnemy":
                    SetBuff(true, Player.PlayerState.SlowerCircle, false, true);
                    break;
            }
            
            
        }

    }

    IEnumerator EnemyDie(float dieTime = 10f)
    {
        yield return new WaitForSeconds(dieTime);
        GameObject.Destroy(gameObject);
    }

    bool isStatePOrI(){
		return playerCom.currentPlayerState == Player.PlayerState.penetration
		|| playerCom.currentPlayerState == Player.PlayerState.Immortal;
	}

    void SetBuff(bool addBUff, Player.PlayerState state, bool addHp, bool NormalTrigger)
    {   
        if (addBUff)
        {   //切换player当前状态
            playerCom.currentPlayerState = state;
        }
        
        if (addHp)
        {   //是否加HP
            if (playerCom.HP < 6)
            {
                playerCom.HP++;
            }
            Debug.Log("Full HP");
            
        }

        //是否需要恢复默认状态
        playerCom.setToNormalTrigger = NormalTrigger;

        //触发player吃东西动画
        playerCom.playerAnimator.SetTrigger("isEatting");
    }
}
