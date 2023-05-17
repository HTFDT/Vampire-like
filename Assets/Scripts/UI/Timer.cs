using System;
using System.Globalization;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public int currentTime;
    public TMP_Text textElement;

    private void Update()
    {
        currentTime = (int)Time.time;
        textElement.text = currentTime / 60 + ":" + currentTime % 60;
    }
}
