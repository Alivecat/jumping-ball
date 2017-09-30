using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GuiControl : MonoBehaviour
{
    public Text Score;
    public Text State;
    public Text PauseScore;
    public Slider HPbar;
    public Player player;
    public Transform mainCameraTran;
    public Button pauseButton;
    public Image pauseMenu;
    public Image damage_Image;
    public Color flash_Color;

    public Image Clock_Image;
    public Color Clock_Color;

    public float flash_Speed = 5;
    bool playerDamaged;
    bool playerClock;
    public float speed;


    void Start()
    {
        if (PlayerPrefs.GetString("highscore") == "")
        {
            PlayerPrefs.SetString("highscore", "0.00");
        }
        mainCameraTran = GameObject.Find("Main Camera").transform;
        HPbar.value = player.HP;
    }


    void Update()
    {
        HPbar.value = Mathf.Lerp(HPbar.value, player.HP / 6f, speed * Time.deltaTime);
        Score.text = (mainCameraTran.position.y * 0.25f).ToString("F2") + " M";
        PauseScore.text = PlayerPrefs.GetString("highscore") + " M";

        if (Application.platform == RuntimePlatform.Android && (Input.GetKeyDown(KeyCode.Escape)))
        {
            PauseButtonClick();
        }

        if (Application.platform == RuntimePlatform.Android && (Input.GetKeyDown(KeyCode.Home)))
        {
            PauseButtonClick();
        }

        playerDamaged = player.damaged;
        playerClock = player.slowMotionClock;
        PlayImageEffect(false, damage_Image, flash_Color, Color.clear,playerDamaged);
        PlayImageEffect(true, Clock_Image, Clock_Color, new Color(0f,248f,217f,0f), playerClock);

    }

    public void PauseButtonClick()
    {
        Time.timeScale = 0;
        pauseMenu.gameObject.SetActive(true);
        pauseButton.interactable = false;
    }

    public void ContinueButtonClick()
    {
        Time.timeScale = 1;
        pauseMenu.gameObject.SetActive(false);
        pauseButton.interactable = true;
    }

    public void ExitButtonClick()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("LevelSelect");
    }

    public void PlayImageEffect(bool isFluency, Image targetImage, Color targetColor, Color EndColor, bool trigger)
    {
        if (!isFluency && trigger)
        {
            targetImage.color = targetColor;
        }

        if(isFluency && trigger)
        {
            targetImage.color = Color.Lerp(targetImage.color, targetColor, flash_Speed * Time.deltaTime);
        }
        else
        {
            targetImage.color = Color.Lerp(targetImage.color, EndColor, flash_Speed * Time.deltaTime);
        }
        if(player.damaged == true)
        {
            player.damaged = false;
        }
        
    }
    //储存最高分
    public void SetHighSocre()
    {
        bool result = (mainCameraTran.position.y * 0.25f) > float.Parse(PlayerPrefs.GetString("highscore"));

        if (GameManager.isEndless == true && result)
        {
            Debug.Log(result);
            PlayerPrefs.SetString("highscore", (mainCameraTran.position.y * 0.25f).ToString("F2"));
        }
        
    }
}
