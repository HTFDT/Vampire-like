using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;


[Serializable]
public class WaveEvent : UnityEvent<WaveController.Wave>
{
}

public class WaveController : MonoBehaviour
{
    [Serializable]
    public class Wave
    {
        public string name;
        public List<EnemyCount> enemies;
    }

    [Serializable]
    public class EnemyCount
    {
        public EnemyData enemyData;
        public int count;
    }
    
    public float delayBetweenWavesInSeconds;
    public List<Wave> waves;
    public WaveEvent spawnWave;

    private IEnumerator LaunchWaveCountdown()
    {
        while (true)
        {
            foreach (var waveToSpawn in waves.Select(t => waves[Random.Range(0, waves.Count)]))
            {
                spawnWave.Invoke(waveToSpawn);
                yield return new WaitForSeconds(delayBetweenWavesInSeconds);
            }
        }
    }

    private void Start()
    {
        StartCoroutine(LaunchWaveCountdown());
    }
}