using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using TMPro.Examples;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    public TMP_Text score;
    public Timer timer;
    public GameObject gameOverScreen;

    public void EnableGameOverScreen(float inSeconds)
    {
        Invoke(nameof(Enable), inSeconds);
    }

    private void Enable()
    {
        gameOverScreen.SetActive(true);
        Time.timeScale = 0;
        score.text = (int)timer.currentTime / 60 + " minutes\n" + (int)timer.currentTime % 60 + " seconds";
    }
    
    private void Awake()
    {
        Time.timeScale = 1;
    }

    public void MenuButton()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void RetryButton()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
