using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyManager : MonoBehaviour
{
    static public int enemiesAlive = 0;

    bool levelIsPlaying = false;
    float levelTimer;
    [SerializeField] WaveInfo[] currentLevelWaves;
    byte counter = 0;
    [SerializeField] int spawnDelayTimer;

    [SerializeField] float distanceFromMiddle;
    [SerializeField] EnemyBasic[] enemy;
    byte lastpos;

    [SerializeField] float smallEnemyRate;
    [SerializeField] float waveSize;
    [SerializeField] float difficulty;
    [SerializeField] float timeBetweenSpawns;
    int waveCounter = 0;
    [SerializeField] int enemiesSpawnedThisWave;
    bool waveSpawning;


    enum enemyTypes
    {
        Normal,
        Heavy
    }

    private void Start()
    {
        StartLevel(currentLevelWaves);
        StartWave();
    }

    private void Update()
    {
        if (!levelIsPlaying) return;

        levelTimer += Time.deltaTime;

        if (waveSpawning)
        {
            Spawner();
        }
        //if (levelTimer > currentLevelWaves[counter].time)
        //{
        //    StartCoroutine(SpawnDelay(currentLevelWaves[counter].eType, currentLevelWaves[counter].eAmount));

        //    counter++;
        //    if (counter == currentLevelWaves.Length)
        //        levelIsPlaying = false;
        //}
    }

    public void StartLevel(WaveInfo[] waves)
    {
        levelIsPlaying = true;
        levelTimer = 0;
        currentLevelWaves = waves;
    }

    void StartWave()
    {
        levelTimer = -3;
        waveCounter++;
        Debug.Log($"starting wave {waveCounter}, {waveSize} total, spawn every {timeBetweenSpawns}");
        enemiesSpawnedThisWave = 0;
        waveSpawning = true;
        //add co for short delay between levels
    }

    void Spawner()
    {
        if (enemiesSpawnedThisWave < waveSize)
        {
            if (levelTimer > timeBetweenSpawns * enemiesSpawnedThisWave)
            {
                if (Random.Range(0, 100) < smallEnemyRate)
                {
                    SpawnEnemy(0);
                }
                else
                {
                    SpawnEnemy(1);
                }
                enemiesSpawnedThisWave++;
            }
        }
        else
        {
            StartCoroutine(EndWave());
        }
    }

    IEnumerator EndWave()
    {
        waveSpawning = false;
        yield return new WaitForSeconds(10);
        //scale difficulty
        // wave size
        waveSize *= MathF.Pow(difficulty, waveCounter);
        waveSize = MathF.Round(waveSize);

        // spawn rate
        timeBetweenSpawns--;
        if (timeBetweenSpawns < 6) timeBetweenSpawns = 6;

        StartWave();
    }

    [ContextMenu("Spawn")]
    void SpawnEnemy(int type)
    {
        int radialPos = GetRadialPos();
        float radPosition = Constants.MIN_ROTATION * radialPos;
        Instantiate(enemy[type], new Vector3(distanceFromMiddle * Mathf.Sin(radPosition), 1, distanceFromMiddle * Mathf.Cos(radPosition)),
                        quaternion.identity, this.transform);
        enemiesAlive++;
    }

    int GetRadialPos()
    {
        while (true)
        {
            int rp = Random.Range(0, 36);
            if (rp != lastpos)
            {
                lastpos = (byte)rp;
                return rp;
            }
        }
    }

    IEnumerator SpawnDelay(int type, int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            SpawnEnemy(type);
            yield return new WaitForSeconds(spawnDelayTimer);
        }
    }
}

[System.Serializable]
public struct WaveInfo
{
    [SerializeField] public int time;
    [SerializeField] public int eType;
    [SerializeField] public int eAmount;
}

