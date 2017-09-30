using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class MenuStartButton : MonoBehaviour {
    public Animator button;
    public Text Show;
    float fadingSpeed = 1;
    bool fading;
    float startFadingTimep;
    Color originalColor;
    Color transparentColor;

    public void ScenceChange()
    {
        SceneManager.LoadScene("LevelSelect");
    }

    private void Start()
    {
        originalColor = Show.color;
        transparentColor = originalColor;
        transparentColor.a = 0;
        Show.text = "Press again to quit";
        Show.color = transparentColor;
    }

#if UNITY_ANDROID
    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (startFadingTimep == 0)
            {
                Show.color = originalColor;
                startFadingTimep = Time.time;
                fading = true;
            }
            else  
            {
                Application.Quit();
            }
        }

        if (fading)
        {
            Show.color = Color.Lerp(originalColor, transparentColor, (Time.time - startFadingTimep) * fadingSpeed);
            if (Show.color.a < 2.0 / 255)
            {
                Show.color = transparentColor;
                startFadingTimep = 0;
                fading = false;
            }
        }

    }
#endif
}





