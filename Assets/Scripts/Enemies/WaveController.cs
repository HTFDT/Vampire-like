using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WaveController : MonoBehaviour
{
    [Serializable]
    public class Wave
    {
        public List<EnemyData> enemies;
        public int waveSize;
        public float rate;
    }
    
    public float delayBetweenWavesInSeconds;
    public List<Wave> waves;
    public Action<Wave> SpawnWave;

    private IEnumerator LaunchWaveCountdown()
    {
        foreach (var wave in waves)
        {
            SpawnWave.Invoke(wave);
            yield return new WaitForSeconds(delayBetweenWavesInSeconds);
        }
    }

    private void Start()
    {
        StartCoroutine(LaunchWaveCountdown());
    }
}