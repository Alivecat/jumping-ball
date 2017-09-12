using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour {

	public float jumpForce = 7.5f;
    public string currentColor;
    public int scoreText;
    public bool havePlayerCollider2D;
    public bool setRigbody2D;

    public enum PlayerState { Normal, Immortal, SlowerCircle, penetration};
    public PlayerState currentPlayerState = 0;
    public int index;
    [Range(1f, 10f)]
    public float slowness = 10f;
    [Space]
    public Rigidbody2D rb;
	public SpriteRenderer sr;
    public Text Score;
    public GameManager GM;
    public GameObject rotator;
    public GameObject doubleCircle;
    public AudioSource jumpSound;
    public AudioSource colorSwitch;
    public AudioSource die;
    public Animator playerAnimator;
    [Space]
	public Color colorCyan;
	public Color colorYellow;
	public Color colorMagenta;
	public Color colorPink;

    void Start ()
	{
        GM.RotateDoubleCircle(index);
        SetRandomColor();
        GM.currentGameState = 0;
        SetScore();
	}

	void Update () {

        GM.RotateDoubleCircle(index);
        switch (GM.currentGameState)
        {
            case GameManager.GameState.gameover:
                rb.gravityScale = 0;
                return;

            case GameManager.GameState.playing:
                Jump();
                switch (currentPlayerState)
                {
                    case PlayerState.Normal:
                        NormalStatesetting();
                        break;

                    case PlayerState.Immortal:
                        setRigbody2D = true;
                        if (!havePlayerCollider2D)
                        {
                            //gameObject.AddComponent<PolygonCollider2D>();
                            havePlayerCollider2D = true;
                        }
                        break;


                    case PlayerState.SlowerCircle:
                        NormalStatesetting();
                        break;

                    case PlayerState.penetration:
                        NormalStatesetting();
                        break;
                }

                return;
        }
    }

    void NormalStatesetting()
    {
        setRigbody2D = false;
        foreach (GameObject circle in GM.tempCircleGropu)
        {
            if (circle.GetComponent<Rigidbody2D>() != null)
            {
                circle.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
                circle.GetComponent<Rigidbody2D>().simulated = false;
            }
            if (havePlayerCollider2D)
            {
                if(circle.tag != currentColor)
                {
                    GameObject.Destroy(gameObject.GetComponent<PolygonCollider2D>());
                    gameObject.AddComponent<PolygonCollider2D>().isTrigger = true;
                    havePlayerCollider2D = false;
                }
               

            }
        }
    }

    void SetScore()
    {
        Score.text = "Score: " + scoreText.ToString();
    }

	void OnTriggerEnter2D (Collider2D col)
	{
        if (GM.currentGameState != 0)
        {
            if (col.tag == "ColorChanger")
            {
                ColorCHangerEnter(col);
                return;
            }

            if (currentPlayerState == PlayerState.Normal || currentPlayerState == PlayerState.SlowerCircle)
            {

                if (col.tag != currentColor || col.tag == "EdgeTrigger")
                {
                    //生成坐标点
                    if (col.tag == "SpawnPointEdge")
                    {
                        GM.colorChangerPointSpawnOffset = new Vector3(0f, 4f, 0f);
                        GM.spawnPointChangeOffset = new Vector3(0f, 24f, 0f);
                        GM.DestoryCycle(col.transform.parent.gameObject);
                        GM.MoveSpawn(col.transform.parent.gameObject);
                        return;
                    }
                    //默认触发死亡慢动作
                    Debug.Log("GAME OVER!");
                    GM.currentGameState = GameManager.GameState.gameover;
                    StartCoroutine(ReloadScene());
                }
            }

            //无敌模式
            if(currentPlayerState == PlayerState.penetration)
            {
                if (col.tag != currentColor || col.tag == "EdgeTrigger")
                {
                    //生成坐标点
                    if (col.tag == "SpawnPointEdge")
                    {
                        GM.colorChangerPointSpawnOffset = new Vector3(0f, 4f, 0f);
                        GM.spawnPointChangeOffset = new Vector3(0f, 24f, 0f);
                        GM.DestoryCycle(col.transform.parent.gameObject);
                        GM.MoveSpawn(col.transform.parent.gameObject);
                        return;
                    }
                }
            }

            if (currentPlayerState == PlayerState.Immortal)
            {
                
                if(col.tag == currentColor)
                {
                    col.GetComponentInParent<Rigidbody2D>().simulated = false;
                    return;
                }

                if(col.tag == "EdgeTrigger")
                {
                    Debug.Log("GAME OVER!");
                    GM.currentGameState = GameManager.GameState.gameover;
                    StartCoroutine(ReloadScene());
                    return;
                }

                if (col.tag == "SpawnPointEdge")
                {
                    GM.colorChangerPointSpawnOffset = new Vector3(0f, 2f, 0f);
                    GM.spawnPointChangeOffset = new Vector3(0f, 12f, 0f);
                    GM.DestoryCycle(col.transform.parent.gameObject);
                    GM.MoveSpawn(col.transform.parent.gameObject);
                    return;
                }

            }
        } 
       
	}

    void Jump()
    {
        if (Input.GetButtonDown("Jump") || Input.GetMouseButtonDown(0))
        {
            rb.velocity = Vector2.up * jumpForce;
            playerAnimator.SetTrigger("isJump");
            jumpSound.Play();
        }
    }

    void ColorCHangerEnter(Collider2D col)
    {
        SetRandomColor();
        Destroy(col.gameObject);
        colorSwitch.Play();
        scoreText++;
        SetScore();
    }

    IEnumerator ReloadScene()
    {
        die.Play();
        Time.timeScale = 1f / slowness;
        Time.fixedDeltaTime = Time.fixedDeltaTime / slowness;
        yield return new WaitForSeconds(1f / slowness);
        Time.timeScale = 1f;
        Time.fixedDeltaTime = Time.fixedDeltaTime * slowness;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

	void SetRandomColor ()
	{
        index = Random.Range(0, 4);
        switch (index)
		{
			case 0:
				currentColor = "Cyan";
				sr.color = colorCyan;
				break;
			case 1:
				currentColor = "Yellow";
				sr.color = colorYellow;
				break;
			case 2:
				currentColor = "Magenta";
				sr.color = colorMagenta;
				break;
			case 3:
				currentColor = "Pink";
				sr.color = colorPink;
				break;
		}
	}

   
}
