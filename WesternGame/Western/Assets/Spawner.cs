using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Header("Prefabs")]
    public GameObject cactusPrefab;
    public GameObject enemy1Prefab;
    public GameObject enemy2Prefab;
    public GameObject enemy3Prefab;

    [Header("Spawn X (right side)")]
    public float spawnX = 12f; // 画面右外に出現

    [Header("Fixed Y positions")]
    public float cactusY = -2.4f;
    public float enemy1Y = -1.8f;
    public float enemy2Y = -1.8f;
    public float enemy3Y = -1.7f;

    [Header("Interval (seconds)")]
    public Vector2 intervalRange = new Vector2(1.8f, 2.8f);
    public float startDelay = 1.0f;

   float nextSpawnAt;

    void Start()
    {
        ScheduleNext(startDelay);
    }

    void Update()
    {
        if (Time.time >= nextSpawnAt)
        {
            SpawnRandom();
            ScheduleNext(Random.Range(intervalRange.x, intervalRange.y));
        }
    }

    void ScheduleNext(float delaySec)
    {
        nextSpawnAt = Time.time + delaySec;
    }

    void SpawnRandom()
    {
        int pick = Random.Range(0, 4); // 0〜3のランダム
        GameObject prefab = null;
        float y = 0f;

        switch (pick)
        {
            case 0:
                prefab = cactusPrefab; y = cactusY; break;
            case 1:
                prefab = enemy1Prefab; y = enemy1Y; break;
            case 2:
                prefab = enemy2Prefab; y = enemy2Y; break;
            case 3:
                prefab = enemy3Prefab; y = enemy3Y; break;
        }

        if (prefab != null)
        {
            Instantiate(prefab, new Vector3(spawnX, y, 0f), Quaternion.identity);
        }
    }
}