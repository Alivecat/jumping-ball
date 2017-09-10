using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour {

	public float jumpForce = 10f;
    public enum GameState { pause, playing};
    public GameState currentState;

    public Rigidbody2D rb;
	public SpriteRenderer sr;
    public Text Score;
    public GameManager GM;

    public string currentColor;
    public int scoreText;

	public Color colorCyan;
	public Color colorYellow;
	public Color colorMagenta;
	public Color colorPink;
    


    void Start ()
	{
        currentState = 0;
        SetScore();
        SetRandomColor();
	}
	
	// Update is called once per frame
	void Update () {

        switch (currentState)
        {
            case GameState.pause:
                rb.gravityScale = 0;
                return;

            case GameState.playing:
                if (Input.GetButtonDown("Jump") || Input.GetMouseButtonDown(0))
                {
                    rb.velocity = Vector2.up * jumpForce;
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
		if (col.tag == "ColorChanger")
		{
			SetRandomColor();
			Destroy(col.gameObject);
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
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		}

        
	}

	void SetRandomColor ()
	{
		int index = Random.Range(0, 4);

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
