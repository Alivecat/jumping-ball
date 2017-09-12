using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour {

	public float jumpForce = 7.5f;
    public enum GameState { gameover, playing};
    public GameState currentState;
    public int index;
    [Range(1f, 10f)]
    public float slowness = 10f;

    public Rigidbody2D rb;
	public SpriteRenderer sr;
    public Text Score;
    public GameManager GM;
    public GameObject doubleCircle;
    public AudioSource jumpSound;
    public AudioSource colorSwitch;
    public AudioSource die;
    public Animator playerAnimator;

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
        currentState = 0;
        SetScore();
	}

    void FixedUpdate()
    {
       
    }

	// Update is called once per frame
	void Update () {

        GM.RotateDoubleCircle(index);
        switch (currentState)
        {
            case GameState.gameover:
                rb.gravityScale = 0;
                return;

            case GameState.playing:
                if (Input.GetButtonDown("Jump") || Input.GetMouseButtonDown(0))
                {
                    rb.velocity = Vector2.up * jumpForce;
                    playerAnimator.SetTrigger("isJump");
                    jumpSound.Play();
                }
                return;
        }
    }



    void SetScore()
    {
        Score.text = "Score: " + scoreText.ToString();
    }

	void OnTriggerEnter2D (Collider2D col)
	{
        if (currentState != 0)
        {
            if (col.tag == "ColorChanger")
            {
                SetRandomColor();
                Destroy(col.gameObject);
                colorSwitch.Play();
                scoreText++;
                SetScore();
                return;
            }

            if (col.tag != currentColor || col.tag == "EdgeTrigger")
            {
                if (col.tag == "SpawnPointEdge")
                {
                    GM.DestoryCycle(col.transform.parent.gameObject);
                    GM.MoveSpawn(col.transform.parent.gameObject);
                    return;
                }

                Debug.Log("GAME OVER!");
                currentState = GameState.gameover;
                StartCoroutine(ReloadScene());
            }
        }  
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

    /*IEnumerator ReloadScene()
    {
        
        yield  return new WaitForSeconds(2);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
      
    }*/

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
