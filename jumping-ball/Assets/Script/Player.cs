using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Player : MonoBehaviour {

	public float jumpForce = 7.5f;      //��Ծ������

    public enum PlayerState { Normal, Immortal, SlowerCircle, penetration };  //���״̬
    public PlayerState currentPlayerState;                                    //��ҵ�ǰ״̬

    public int index;
    [Range(1f, 10f)]
    public float slowness = 10f;        //����ʱ����������
    public string currentColor;
    public bool setToNormalTrigger;
	public float buffTime;
	public int HP;
	public float timeSpentInvincible;

    public Rigidbody2D rb;
	public SpriteRenderer sr;
    public GameManager GM;
    public GuiControl GuiControl;
    public AudioSource jumpSound;
    public AudioSource colorSwitch;
    public AudioSource die;
    public Animator playerAnimator;
    public Animator eyesAnimator;
	public GameObject mouse;

	public Color colorCyan;
	public Color colorYellow;
	public Color colorMagenta;
	public Color colorPink;

    void Start ()
	{
		HP = 6;
        setToNormalTrigger = false;
        GM.RotateDoubleCircle(index);
        SetRandomColor();
        GM.currentGameState = 0;
	}

	void Update () {
        GuiControl.State.text = currentPlayerState.ToString();
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
                        SetToNormalFunction();
                        break;

                    case PlayerState.penetration:
                        jump();
                        SetToNormalFunction();
						
                       /* timeSpentInvincible += Time.deltaTime; 

                        if (timeSpentInvincible < 3f) { 
	                        float remainder = timeSpentInvincible % 0.3f; 
							//gameObject.GetComponents<SpriteRenderer>().
						if(remainder <= 0.15f){
						gameObject.transform.Translate (gameObject.transform.position.x, -100f, gameObject.transform.position.z);
					}
					//gameObject.SetActive(remainder > 0.15f); 
						} 
                        else { 
					gameObject.transform.Translate (gameObject.transform.position.x, -35f, gameObject.transform.position.z);
                        }*/
                        
                        break;
                    case PlayerState.SlowerCircle:
                        jump();
                        SetToNormalFunction();
                        break;
                }
        return;
        }
    }

    void SetToNormalFunction()
    {
        if (currentPlayerState != PlayerState.Normal && setToNormalTrigger)
        {
            StartCoroutine(SetToNormal());
            setToNormalTrigger = false;
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

				if (col.tag != currentColor || col.tag == "EdgeTrigger")
                {
                    if (col.tag == "SpawnPointEdge")
                    {
                        SpawnPointEdge(col);
                        return;
                    }

                    //��ⴥ���������4��tag��ĸ�Ƿ�Ϊnemy���ж��Ƿ�ΪEnemy
                    //���4����ĸ��Ϊ�˲���������tag����5����ĸ������ArgumentOutOfRangeException
                    if (col.tag.Substring(col.tag.Length - 4,4) == "nemy")
                    {
                        return;
                    }
                    GameOver(2);
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

                if (col.tag == "EdgeTrigger")
                {
                    GameOver(3);
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
                        //����ײ������ɫ�����ʱ�������󴥷�������ȡ����������ײ
                        col.GetComponent<PolygonCollider2D>().isTrigger = false;
                        //0.5���ָ����󴥷�������
                        StartCoroutine(SetTriggerTrue(col));
                    }
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
                        GameOver(4);
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

    public void GameOver(int i)
    {
		HP -= 2;
		currentPlayerState = PlayerState.penetration;
		StartCoroutine(SetToNormal(2f));

		if (HP <= 0) {
			Debug.Log ("gameover: " + i);
			GM.currentGameState = GameManager.GameState.gameover;
			StartCoroutine (ReloadScene ());
		}
    }

    void ColorChanger(Collider2D col)
    {
        SetRandomColor();
        Destroy(col.gameObject);
        colorSwitch.Play();
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

	IEnumerator SetToNormal(float buffTime = 6f)
    {
		yield return new WaitForSeconds(buffTime);
        currentPlayerState = PlayerState.Normal;
		mouse.SetActive (true);
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
