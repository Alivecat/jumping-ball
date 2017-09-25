using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Player : MonoBehaviour
{

    public float jumpForce = 7.5f;      //跳跃的力度

    public enum PlayerState { Normal, Immortal, SlowerCircle, penetration, Sting };  //玩家状态
    public PlayerState currentPlayerState;                                    //玩家当前状态

    public int index;
    [Range(1f, 10f)]
    public float slowness = 10f;        //死亡时慢动作速率
    public string currentColor;
    public bool setToNormalTrigger;
    public bool ReIgnoreCollisionTrigger;
    public float buffTime;
    public int HP;
    public float timeSpentInvincible;
    public bool enemyBuffP;

    public Rigidbody2D rb;
    public SpriteRenderer sr;
    public GameManager GM;
    public GuiControl GuiControl;
    public AudioSource jumpSound;
    public AudioSource colorSwitch;
    public AudioSource die;
    public Animator playerAnimator;
    public Animator eyesAnimator;

    public GameObject eye;
    public GameObject eatPoint;
    public GameObject sting;
    public SpriteRenderer bodySprite;
    public SpriteRenderer eyesSprite;
    public SpriteRenderer mouthSprite;
    public ParticleSystem juice;

    [Space]
    public Color colorCyan;
    public Color colorYellow;
    public Color colorMagenta;
    public Color colorPink;

    private float lastTime;
    private float curTime;
    private float blinkCountI;
    void Start()
    {
        ReIgnoreCollisionTrigger = false;
        lastTime = Time.time;
        HP = 6;
        blinkCountI = 0;
        setToNormalTrigger = false;
        GM.RotateDoubleCircle(index);
        SetRandomColor();
        GM.currentGameState = 0;
    }

    void Update()
    {
        GuiControl.State.text = currentPlayerState.ToString();
        GM.RotateDoubleCircle(index);
        switch (GM.currentGameState)
        {   //游戏状态机
            case GameManager.GameState.gameover:
                rb.gravityScale = 0;
                return;
            case GameManager.GameState.boss:
            case GameManager.GameState.playing:
                //人物状态机
                switch (currentPlayerState)
                {
                    case PlayerState.Sting:
                    case PlayerState.Normal:
                        Jump();
                        break;

                    case PlayerState.Immortal:
                        Jump();
                        SetToNormalFunction();
                        break;

                    case PlayerState.penetration:
                        Jump();
                        SetToNormalFunction();

                        curTime = Time.time;
                        if (curTime - lastTime >= 0.1f)
                        {
                            Blink(enemyBuffP);
                            lastTime = curTime;
                        }

                        break;

                    case PlayerState.SlowerCircle:
                        Jump();
                        SetToNormalFunction();
                        break;
                }
                return;
        }
    }

    void SetToNormalFunction(float buffTime = 6f)
    {
        if (currentPlayerState != PlayerState.Normal && setToNormalTrigger)
        {
            StartCoroutine(SetToNormal(buffTime));
            setToNormalTrigger = false;
        }
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump") || Input.GetMouseButtonDown(0))
        {
            rb.velocity = Vector2.up * jumpForce;
            playerAnimator.SetTrigger("isJump"); //跳跃身体动画
            eyesAnimator.SetTrigger("isJump");   //跳跃眼睛动画
            jumpSound.Play();
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (GM.currentGameState != 0)
        {
            //Normal状态和SlowerCircle
            if (currentPlayerState == PlayerState.Normal || currentPlayerState == PlayerState.SlowerCircle)
            {
                if (col.tag == "ColorChanger")
                {
                    ColorChanger(col);
                    return;
                }

                if (col.tag != currentColor || col.tag == "EdgeTrigger")
                {
                    if (col.tag == "SpawnPointEdge")
                    {
                        SpawnPointEdge(col);
                        return;
                    }

                    //检测触发器物体后4个tag字母是否为nemy，判断是否为Enemy
                    //检测4个字母是为了不引发碰到tag短于5个字母引发的ArgumentOutOfRangeException
                    if (col.tag.Substring(col.tag.Length - 4, 4) == "nemy")
                    {
                        return;
                    }

                    GameOver(2,false);
                }
            }

            //Immortal状态
            if (currentPlayerState == PlayerState.Immortal)
            {
                if (col.tag == "ColorChanger")
                {
                    ColorChanger(col);
                    return;
                }

                if (col.tag == "EdgeTrigger")
                {
                    GameOver(3,true);
                    return;
                }

                if (col.tag == "SpawnPointEdge")
                {
                    SpawnPointEdge(col);
                    return;
                }

                if (gameObject.tag != col.tag)
                {
                    if (col.GetComponent<PartOfColorCircle>())
                    {
                        //当碰撞对象颜色不相等时，将对象触发器属性取消，发生碰撞
                        col.GetComponent<PolygonCollider2D>().isTrigger = false;
                        //0.5秒后恢复对象触发器属性
                        StartCoroutine(SetTriggerTrue(col));
                    }
                }

            }

            //穿越模式
            if (currentPlayerState == PlayerState.penetration)
            {


                if (col.tag != currentColor || col.tag == "EdgeTrigger")
                {
                    if (col.tag == "ColorChanger")
                    {
                        ColorChanger(col);
                        return;
                    }

                    if (col.tag == "EdgeTrigger")
                    {
                        GameOver(4,true);
                        return;
                    }

                    if (col.tag == "SpawnPointEdge")
                    {
                        SpawnPointEdge(col);
                        return;
                    }
                }
            }
            if(currentPlayerState == PlayerState.Sting)
            {
                if (col.tag == "EdgeTrigger")
                {
                    GameOver(5, true);
                }

                

                //检测触发器物体后4个tag字母是否为nemy，判断是否为Enemy
                //检测4个字母是为了不引发碰到tag短于5个字母引发的ArgumentOutOfRangeException
                if (col.tag.Substring(col.tag.Length - 4, 4) == "nemy" || col.tag == "Boss")
                {
                    return;
                }

            }


        }
    }

    public void GameOver(int i,bool immediately = false)
    {
        if (currentPlayerState != PlayerState.penetration || currentPlayerState != PlayerState.Immortal)
        {
            Debug.Log("-HP");
            HP -= 2;
        }

        SetBuff(true, PlayerState.penetration, false, true, true, false, false);
        ReIgnoreCollisionTrigger = true;

        if (HP <= 0)
        {
            Debug.Log("gameover: " + i);
            GM.currentGameState = GameManager.GameState.gameover;
            StartCoroutine(ReloadScene());
        }

        if (immediately)
        {
            Debug.Log("gameover: " + i);
            StartCoroutine(ReloadScene());
        }
    }

    void ColorChanger(Collider2D col)
    {
        Color particleCloor = SetRandomColor();
        particleColorSet(particleCloor);
        juice.Play();
        Destroy(col.gameObject);
        colorSwitch.Play();   
    }

    void particleColorSet(Color particleCloor)
    {
        var colorOL = juice.colorOverLifetime;
        Gradient grad = new Gradient();
        grad.SetKeys(new GradientColorKey[] { new GradientColorKey(particleCloor, 0.0f), new GradientColorKey(new Color(200, 255, 192), 1.0f) },
            new GradientAlphaKey[] { new GradientAlphaKey(1.0f, 0.0f), new GradientAlphaKey(0.0f, 1.0f) });
        colorOL.color = grad;
    }

    void SpawnPointEdge(Collider2D col)
    {
        if(GM.currentGameState == GameManager.GameState.boss)
        {
            GM.DestoryCycle(col.transform.parent.gameObject);
            return;
        }
        GM.DestoryCycle(col.transform.parent.gameObject);
        GM.MoveSpawn(col.transform.parent.gameObject);
    }



    IEnumerator SetTriggerTrue(Collider2D col)
    {
        yield return new WaitForSeconds(0.5f);
        col.GetComponent<PolygonCollider2D>().isTrigger = true;
    }

    IEnumerator ReloadScene()
    {
        particleColorSet(GetCurrentColor());
        juice.Play();
        die.Play();
        Time.timeScale = 1f / slowness;
        Time.fixedDeltaTime = Time.fixedDeltaTime / slowness;
        bodySprite.color = new Color(255f, 255f, 255, 0f);
        eyesSprite.color = new Color(255f, 255f, 255, 0f);
        yield return new WaitForSeconds(4f / slowness);
        Time.timeScale = 1f;
        Time.fixedDeltaTime = Time.fixedDeltaTime * slowness;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    IEnumerator SetToNormal(float buffTime = 6f)
    {
        yield return new WaitForSeconds(buffTime);
        currentPlayerState = PlayerState.Normal;
        blinkCountI = 0;
        enemyBuffP = false;
        if (eye.tag != "Player")
        {
            eye.tag = "Player";
        }
        if (eatPoint.GetComponent<BoxCollider2D>() == false)
        {
            eatPoint.AddComponent<BoxCollider2D>().isTrigger = true;
            
        }
        bodySprite.sortingOrder = 10;
        eyesSprite.sortingOrder = 12;
        mouthSprite.sortingOrder = 13;
    }

    void Blink(bool isBuff)
    {
        blinkCountI++;
        float times = 28;
        if (isBuff)
        {
            times = 58;
        }

         if(blinkCountI <= times)
        {
            if (bodySprite.sortingOrder != -10)
            {
                bodySprite.sortingOrder = -10;
                eyesSprite.sortingOrder = -10;
                mouthSprite.sortingOrder = -10;
            }
            else
            {
                bodySprite.sortingOrder = 10;
                eyesSprite.sortingOrder = 12;
                mouthSprite.sortingOrder = 13;
            }

        }
        
    }

    Color SetRandomColor()
    {
        index = Random.Range(0, 4);
        switch (index)
        {
            case 0:
                currentColor = "Cyan";
                sr.color = colorCyan;
                gameObject.tag = "Cyan";
                return colorCyan;

            case 1:
                currentColor = "Yellow";
                sr.color = colorYellow;
                gameObject.tag = "Yellow";
                return colorYellow;

            case 2:
                currentColor = "Magenta";
                sr.color = colorMagenta;
                gameObject.tag = "Magenta";
                return colorMagenta;

            case 3:
                currentColor = "Pink";
                sr.color = colorPink;
                gameObject.tag = "Pink";
                return colorPink;
        }
        return Color.red;
    }

    Color GetCurrentColor()
    {
        switch (currentColor)
        {
            case "Cyan":
                return colorCyan;
            case "Yellow":
                return colorYellow;
            case "Magenta":
                return colorMagenta;
            case "Pink":
                return colorPink;
        }
        return Color.red;
    }

    public void SetBuff(bool addBUff, PlayerState state, bool addHp, bool NormalTrigger, bool ignoreEnemy, bool canEat, bool playAnimation)
    {
        if (addBUff)
        {   //切换player当前状态
            currentPlayerState = state;
        }

        if (addHp)
        {   //是否加HP
            if (HP < 6)
            {
                HP++;
            }
            Debug.Log("Full HP");

        }

        if (ignoreEnemy)
        {
            eye.tag = "Untagged";
        }

        if (!canEat)
        {
            GameObject.Destroy(eatPoint.GetComponent<BoxCollider2D>());
        }

        //恢复默认状态
        StartCoroutine(SetToNormal(2f));
  
        //触发player吃东西动画
        if (playAnimation)
        {
            playerAnimator.SetTrigger("isEatting");
        }

    }

    public void SetSting(bool set)
    {
        sting.gameObject.SetActive(set);
    }
}
