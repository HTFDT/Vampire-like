using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Random = UnityEngine.Random;
using Vector2 = UnityEngine.Vector2;

public class EnemySpawner : MonoBehaviour
{
    public List<EnemyData> enemyDataList;
    public GameObject enemyPrefab;
    public CameraProps cameraProps;
    public Transform enemyContainer;
    public float generalSpawnDelayInSeconds;
    public float distanceFromCameraBorder;
    public float waveSpawnRadius;
    public float singleEnemySpawnRadius;
    public int numberOfCollisionChecks;
    public int maxAttemptsToSpawn;
    private List<Vector2> _pointsOnCircle;
    private Transform _cameraTransform;

    private void Awake()
    {
        _pointsOnCircle = new List<Vector2>();
        for (var i = 0; i < numberOfCollisionChecks; i++)
        for (var j = 0f; j < 2 * Mathf.PI; j += 2 * Mathf.PI / numberOfCollisionChecks)
            _pointsOnCircle.Add(new Vector2(Mathf.Cos(j), Mathf.Sin(j)));
        _cameraTransform = Camera.main!.transform;
    }

    private void Start()
    {
        StartCoroutine(SpawnGeneralCoroutine());
    }

    public void SpawnWave(WaveController.Wave wave)
    {
        enemyDataList.AddRange(wave.enemies);

        var positions = new List<Vector2>();
        for (var i = 0; i < maxAttemptsToSpawn; i++)
        {
            var pos = GetNextSpawnPosition(waveSpawnRadius);
            if (CanSpawnInRadius(pos, waveSpawnRadius, wave.waveSize, out positions))
                break;
        }
        
        if (positions.Count == 0)
            Debug.LogError("can't spawn wave");

        StartCoroutine(SpawnWaveCoroutine(wave, positions));
    }

    private IEnumerator SpawnWaveCoroutine(WaveController.Wave wave, List<Vector2> positions)
    {
        foreach (var pos in positions)
        {
            SpawnOne(wave.enemies[Random.Range(0, wave.enemies.Count - 1)], pos);
            yield return new WaitForSeconds(1 / wave.rate);
        }
    }

    private bool CanSpawnInRadius(Vector2 position, float radius, int numberToSpawn, out List<Vector2> positions)
    {
        positions = null;
        var positionsToTry = new List<Vector2>();
        for (var i = 0; i < numberToSpawn; i++)
            positionsToTry.Add(position + Random.insideUnitCircle * radius);

        var canSpawn = positionsToTry.All(CanSpawnOn);
        if (canSpawn)
            positions = positionsToTry;
        return canSpawn;
    }

    private IEnumerator SpawnGeneralCoroutine()
    {
        while (true)
        {
            foreach (var data in enemyDataList)
            {
                var pos = Vector2.zero;
                for (var i = 0; i < maxAttemptsToSpawn; i++)
                {
                    var posToTry = GetNextSpawnPosition(distanceFromCameraBorder);
                    if (!CanSpawnOn(posToTry)) continue;
                    pos = posToTry;
                    break;
                }
                
                if (pos == Vector2.zero)
                    Debug.LogError("can't spawn general");
                else
                    SpawnOne(data, pos);
                
            }
            yield return new WaitForSeconds(generalSpawnDelayInSeconds);
        }
    }

    private void SpawnOne(EnemyData data, Vector2 position)
    {
        var obj = Instantiate(enemyPrefab, position, Quaternion.identity, enemyContainer);
        obj.GetComponent<Enemy>().Init(data);
    }

    private Vector2 GetNextSpawnPosition(float distanceFromBorder)
    {
        float x;
        float y;
        if (Random.value >= 0.5)
        {
            x = Random.Range(cameraProps.Left, cameraProps.Right);
            y = Random.value >= 0.5 ? cameraProps.Top : cameraProps.Bottom;
        }
        else
        {
            y = Random.Range(cameraProps.Bottom, cameraProps.Top);
            x = Random.value >= 0.5 ? cameraProps.Left : cameraProps.Right;
        }

        var position = (Vector2)_cameraTransform.position;
        var ray = new Vector2(x, y) - position;

        return position + ray.normalized * (ray.magnitude + distanceFromBorder);
    }

    private bool CanSpawnOn(Vector2 position)
    {
        var filter = new ContactFilter2D();
        filter.SetLayerMask(LayerMask.GetMask("Obstacle", "ProjectileIgnoringObstacle"));
        var results = new List<RaycastHit2D>();
        foreach (var direction in GetRaycastDirections(singleEnemySpawnRadius))
        {
            var hitCount = Physics2D.Raycast(position,
                direction.normalized,
                filter,
                results,
                direction.magnitude
            );
            if (hitCount > 0)
                return false;
        }
        
        return true;
    }

    private IEnumerable<Vector2> GetRaycastDirections(float radius)
    {
        foreach (var point in _pointsOnCircle)
            yield return point * radius;
    }
}