using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour {

	public float jumpForce = 7.5f;      //��Ծ������

    public enum PlayerState { Normal, Immortal, SlowerCircle, penetration };  //���״̬
    public PlayerState currentPlayerState;                                    //��ҵ�ǰ״̬

    public int index;
    [Range(1f, 10f)]
    public float slowness = 10f;        //����ʱ����������

    public Rigidbody2D rb;
	public SpriteRenderer sr;
    public Text Score;
    public GameManager GM;
    public GameObject doubleCircle;
    public GameObject smallCircle;
    public AudioSource jumpSound;
    public AudioSource colorSwitch;
    public AudioSource die;
    public Animator playerAnimator;
    public Animator eyesAnimator;

    public string currentColor;
    public int scoreText;

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
        {   //��Ϸ״̬��
            case GameManager.GameState.gameover:
                rb.gravityScale = 0;
                return;

            case GameManager.GameState.playing:
                //����״̬��
                switch (currentPlayerState)
                {
                    case PlayerState.Normal:
                        jump();
                        break;

                    case PlayerState.Immortal:
                        jump();
                        break;

                    case PlayerState.penetration:
                        jump();
                        break;
                    case PlayerState.SlowerCircle:
                        jump();
                        break;
                }
        return;
        }
    }

    void jump()
    {
        if (Input.GetButtonDown("Jump") || Input.GetMouseButtonDown(0))
        {
            rb.velocity = Vector2.up * jumpForce;
            playerAnimator.SetTrigger("isJump"); //��Ծ���嶯��
            eyesAnimator.SetTrigger("isJump");   //��Ծ�۾�����
            jumpSound.Play();
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
            //Normal״̬
            if(currentPlayerState == PlayerState.Normal || currentPlayerState == PlayerState.SlowerCircle)
            {
                if (col.tag == "ColorChanger")
                {
                    ColorChanger(col);
                    return;
                }

				if (col.tag != currentColor || col.tag == "EdgeTrigger" || col.transform.Find("tag").tag == "L_enemy" || col.transform.Find("tag").tag == "R_enemy")
                {
                    if (col.tag == "SpawnPointEdge")
                    {
                        SpawnPointEdge(col);
                        return;
                    }

                    GameOver();
                }
            }

            //Immortal״̬
            if (currentPlayerState == PlayerState.Immortal)
            {
                if (col.tag == "ColorChanger")
                {
                    ColorChanger(col);
                    return;
                }

                
                if(col.transform.Find("tag").tag == "L_enemy" || col.transform.Find("tag").tag == "R_enemy")
                {
                    GameObject.Destroy(col);
                    return;
                }

                if (col.tag == "EdgeTrigger")
                {
                    GameOver();
                    return;
                }

                if (col.tag == "SpawnPointEdge")
                {
                    SpawnPointEdge(col);
                    return;
                }

                if (gameObject.tag != col.tag)
                {
                    //����ײ������ɫ�����ʱ�������󴥷�������ȡ����������ײ
                    col.GetComponent<PolygonCollider2D>().isTrigger = false;
                    //0.5���ָ����󴥷�������
                    StartCoroutine(SetTriggerTrue(col));
                    return;
                }

            }

            //��Խģʽ
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
                        GameOver();
                        return;
                    }

                    if (col.tag == "SpawnPointEdge")
                    {
                        SpawnPointEdge(col);
                        return;
                    }
                }
            }

        }  
	}

    void GameOver()
    {
        Debug.Log("GAME OVER!");
        GM.currentGameState = GameManager.GameState.gameover;
        StartCoroutine(ReloadScene());
    }

    void ColorChanger(Collider2D col)
    {
        SetRandomColor();
        Destroy(col.gameObject);
        colorSwitch.Play();
        scoreText++;
        SetScore();
    }

    void SpawnPointEdge(Collider2D col)
    {
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
                gameObject.tag = "Cyan";

                break;
			case 1:
				currentColor = "Yellow";
				sr.color = colorYellow;
                gameObject.tag = "Yellow";
                break;
			case 2:
				currentColor = "Magenta";
				sr.color = colorMagenta;
                gameObject.tag = "Magenta";
                break;
			case 3:
				currentColor = "Pink";
				sr.color = colorPink;
                gameObject.tag = "Pink";
                break;
		}
	}
    
     
}
