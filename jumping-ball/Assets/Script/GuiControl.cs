using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GuiControl : MonoBehaviour
{
    public Text Score;
    public Text State;
    public Slider HPbar;
    public Player player;
    public Transform mainCameraTran;
    public Button pauseButton;
    public Image pauseMenu;

    public float speed;

    void Start()
    {
        mainCameraTran = GameObject.Find("Main Camera").transform;
        HPbar.value = player.HP;
    }


    void Update()
    {
        HPbar.value = Mathf.Lerp(HPbar.value, player.HP / 6f, speed * Time.deltaTime);
        Score.text = (mainCameraTran.position.y * 0.25f).ToString("F2") + " M";

        if (Application.platform == RuntimePlatform.Android && (Input.GetKeyDown(KeyCode.Escape)))
        {
            PauseButtonClick();
        }


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

}
