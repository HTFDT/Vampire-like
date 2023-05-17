using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Random = UnityEngine.Random;
using Vector2 = UnityEngine.Vector2;

public class EnemySpawner : MonoBehaviour
{
    private HashSet<EnemyData> _enemyDataList;
    public GameObject enemyPrefab;
    public CameraProps cameraProps;
    public Transform enemyContainer;
    public float generalSpawnDelayInSeconds;
    public float distanceFromCameraBorder;
    public float singleEnemySpawnRadius;
    public int numberOfCollisionChecks;
    public int maxAttemptsToSpawn;
    private List<Vector2> _pointsOnCircle;
    private Transform _cameraTransform;

    private void Awake()
    {
        _enemyDataList = new HashSet<EnemyData>();
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
        _enemyDataList.AddRange(wave.enemies.Select(p => p.enemyData));
        var positions = GetSpawnPositions(wave.enemies.Sum(e => e.count));
        SpawnWaveEnemies(wave, positions);
    }

    private List<Vector2> GetSpawnPositions(int n)
    {
        var positions = new List<Vector2>();
        for (var i = 0; i < n; i++)
        {
            var pos = Vector2.zero;
            for (var j = 0; j < maxAttemptsToSpawn; j++)
            {
                var posToTry = GetNextSpawnPosition(singleEnemySpawnRadius);
                if (!CanSpawnOn(posToTry)) continue;
                pos = posToTry;
                break;
            }

            if (pos == Vector2.zero) continue;
            positions.Add(pos);
        }

        return positions;
    }

    private void SpawnWaveEnemies(WaveController.Wave wave, List<Vector2> positions)
    {
        var enemiesToSpawn = wave.enemies.ToDictionary(c => c.enemyData, c => c.count);
        foreach (var pos in positions)
        {
            var e = enemiesToSpawn.Keys.ElementAt(Random.Range(0, enemiesToSpawn.Count));
            if (--enemiesToSpawn[e] <= 0)
                enemiesToSpawn.Remove(e);
            SpawnOne(e, pos);
        }
    }

    private IEnumerator SpawnGeneralCoroutine()
    {
        while (true)
        {
            if (_enemyDataList.Count != 0)
            {
                var data = _enemyDataList.ElementAt(Random.Range(0, _enemyDataList.Count));
                var pos = Vector2.zero;
                for (var i = 0; i < maxAttemptsToSpawn; i++)
                {
                    var posToTry = GetNextSpawnPosition(distanceFromCameraBorder);
                    if (!CanSpawnOn(posToTry)) continue;
                    pos = posToTry;
                    break;
                }

                if (pos != Vector2.zero)
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