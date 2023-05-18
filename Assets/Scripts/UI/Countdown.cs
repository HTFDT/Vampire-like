using System.Collections;
using TMPro;
using UnityEngine;

public class Countdown : MonoBehaviour
{
    public TMP_Text countdown;
    private float _currentTime;

    public void SetCountdown(float time)
    {
        gameObject.SetActive(true);
        _currentTime = time;
        StartCoroutine(CountdownCoro());
    }

    private IEnumerator CountdownCoro()
    {
        while (_currentTime > 0)
        {
            countdown.text = ((int)_currentTime).ToString();
            yield return new WaitForSeconds(1);
            _currentTime -= 1;
        }
        gameObject.SetActive(false);
    }
}
