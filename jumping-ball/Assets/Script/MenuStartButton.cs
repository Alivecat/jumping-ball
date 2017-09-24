using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MenuStartButton : MonoBehaviour {
    public Animator button;

    public void ScenceChange()
    {
        SceneManager.LoadScene("LevelSelect");
    }

    public void ShowStartButton()
    {
        button.SetTrigger("ShowStartButton");
    }
}
