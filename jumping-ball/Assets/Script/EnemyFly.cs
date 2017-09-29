using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFly : MonoBehaviour {
	public float speed;
    public bool canDamage;
    public float smoothing;
    public Vector3 NormalScale;

    public GameObject player;
    public Player playerCom;
    public Pool pool;
    public GameObject eye;
	public GameObject eatPoint;
    public GameObject sting;
    public Animator enemyanimator;
    public Animator mouthAnimator;
    public Animator playerAnimator;


    void Start(){
        NormalScale = gameObject.transform.localScale;
        smoothing = 5f;
        canDamage = true;
        mouthAnimator = GameObject.FindGameObjectWithTag("Mouth").GetComponent<Animator>();
        eatPoint = GameObject.Find ("EatPoint");       
        player = GameObject.Find("Player");  
        eye = GameObject.Find("eye");
        sting = GameObject.Find("Sting");
        pool = GameObject.Find("GameManager").GetComponent<Pool>();
        enemyanimator = gameObject.GetComponent<Animator>();
        playerAnimator = GameObject.Find("Player").GetComponent<Animator>();
        playerCom = player.GetComponent<Player>();

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

		if (!canDamage) {
            //怪物飞入嘴中
			gameObject.transform.position = Vector3.Lerp (gameObject.transform.position, player.transform.position, smoothing * Time.deltaTime);
		}
	}

    void OnTriggerEnter2D(Collider2D target)
    {

        if(target.tag == "Player")
        {
            if(playerCom.currentPlayerState == Player.PlayerState.Immortal)
            {
                
                speed = 0;
                enemyanimator.SetTrigger("isDie");
                StartCoroutine(EnemyDie(0.5f));
            }

            if (canDamage && playerCom.currentPlayerState != Player.PlayerState.Immortal)
            {
                playerCom.GameOver(1);
                SetBuff(true, Player.PlayerState.penetration, false, true, true, false, true);
            }
            

        }

        if(target.tag == "EatPoint")
        {

            canDamage = false; //怪物是否可造成伤害
            enemyanimator.SetTrigger("isDie");
			StartCoroutine(EnemyDie(0.5f));
			speed = 1f; // 死亡小怪减速飞入嘴中

            switch (gameObject.tag)
            {
                //bool addBUff, Player.PlayerState state, bool addHp, bool NormalTrigger,bool ignoreEnemy,bool canEat,bool playAnimation
                case "ImmortalEnemy":
                    SetBuff(true, Player.PlayerState.Immortal, false, true, false, false, true);
                    SetAnimation(true, true, true, false);
                    break;
                case "NormalEnemy":
                    SetBuff(false, Player.PlayerState.Normal, true, false, false, true, true);
                    SetAnimation(true, false, false, false);
                    break;
                case "PenetrationEnemy":
                    playerCom.enemyBuffP = true; //切换主角闪烁
                    SetBuff(true, Player.PlayerState.penetration, false, true, true, false, true);
                    SetAnimation(true, false, false, false);
                    break;
                case "SlowMotionEnemy":
                    SetBuff(true, Player.PlayerState.SlowerCircle, false, true, false, true, true);
                    SetAnimation(true, false, false, false);
                    playerCom.slowMotionClock = true;
                    break;
                case "StingEnemy":
                    playerCom.SetSting(true);
                    SetAnimation(true, false, true, true);
                    SetBuff(true, Player.PlayerState.Sting, false, true, false, false, false);
                    break;
            }
            
            
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "EnemyColliderSpace")
        {

            pool.Return(this.transform.gameObject);
        }
        
    }

    IEnumerator EnemyDie(float dieTime = 10f)
    {
        yield return new WaitForSeconds(dieTime);
        //GameObject.Destroy(gameObject);
        canDamage = true;
        speed = 5;
        gameObject.transform.localScale = NormalScale;
        pool.Return(this.transform.gameObject);
    }

    public void SetBuff(bool addBUff, Player.PlayerState state, bool addHp, bool NormalTrigger,bool ignoreEnemy,bool canEat,bool SetNormal)
    {
        //playerCom.ReIgnoreCollisionTrigger = true;

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

        //是否穿透（同时无视敌人）
        if (ignoreEnemy)
        {
            eye.tag = "Untagged";
        }

        //是否可以吃怪 canEat == true 时可以吃
        if (!canEat)
        {
            //eatPoint.gameObject.SetActive(false);
            GameObject.Destroy(eatPoint.GetComponent<Collider2D>());
            //Debug.Log("Destroy eatPoint BoxCollider2D");
        }

        //是否需要恢复默认状态
        if (SetNormal)
        {
            playerCom.setToNormalTrigger = NormalTrigger;
        }

    }

    void SetAnimation(bool eatAnimation,bool ironAnimation,bool setLikeNormalfalse,bool stingAnimation)
    {
        //触发player吃东西动画
        if (eatAnimation)
        {
            mouthAnimator.SetTrigger("isEating");
        }

        if (ironAnimation)
        {
            playerAnimator.SetBool("isIron", true);
        }

        if (setLikeNormalfalse)
        {
            playerAnimator.SetBool("likeNormal", false);
        }

        if (stingAnimation)
        {
            playerAnimator.SetBool("isSting", true);
        }
        
    }
        


}
