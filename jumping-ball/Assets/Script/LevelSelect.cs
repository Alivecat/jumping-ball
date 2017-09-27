using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour {

    public void SelectLevel1()
    {
        GameManager.isEndless = false;
        SceneManager.LoadScene("Level1");
    }

    public void SelectLevel1Endless()
    {
        GameManager.isEndless = true;
        SceneManager.LoadScene("Level1");
    }

    public void LevelSelectExitButtonClick()
    {
        SceneManager.LoadScene("MainMenu");
    }


}
