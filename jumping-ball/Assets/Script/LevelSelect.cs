using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour {

    public Camera mainCamera;
    public string sideTrigger = "front";
    public Quaternion frontside = Quaternion.Euler(0f, 0f, 0f);
    public Quaternion backside = Quaternion.Euler(0f, 180f, 0f);

    private void Update()
    {
        
       
    }

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
        GameManager.isEndless = false;
        SceneManager.LoadScene("MainMenu");
    }

    public void ChangeSide()
    {
        
    }

    private void RotateCamera()
    {
        if (sideTrigger == "front")
        {
            mainCamera.transform.rotation = Quaternion.Lerp(mainCamera.transform.rotation, frontside, Time.deltaTime * 10f);
            sideTrigger = "back";
        }

        if (sideTrigger == "back")
        {
            mainCamera.transform.rotation = Quaternion.Lerp(mainCamera.transform.rotation, backside, Time.deltaTime * 10f);
            sideTrigger = "front";
        }
    }
}
