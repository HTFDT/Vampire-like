using System;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    public float currentTime;
    public TMP_Text textElement;

    private void Update()
    {
        currentTime += Time.deltaTime;
        textElement.text = (int)currentTime / 60 + ":" + (int)currentTime % 60;
    }
}
