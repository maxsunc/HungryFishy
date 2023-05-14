using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;


public class Manager : MonoBehaviour
{

    public GameObject pausePanel;

     public void Pause()
    {
        Time.timeScale = 0f;
        pausePanel.SetActive(true);
    }

    public void UnPause()
    {
        Time.timeScale = 1f;
        pausePanel.SetActive(false);
    }

    public void ToMenu()
    {
        Time.timeScale = 1f;
        // transport to new scene
        SceneManager.LoadScene(0);
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        // transport to new scene
        SceneManager.LoadScene(1);
        
    }

}
