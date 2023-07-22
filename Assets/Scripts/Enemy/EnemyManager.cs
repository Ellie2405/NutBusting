using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance { get; private set; }

    int enemiesAlive = 0;

    float levelTimer;

    [SerializeField] float distanceFromMiddle;
    [SerializeField] EnemyBasic[] enemy;
    byte lastpos;

    [Header("Spawner")]
    [SerializeField] float smallEnemyPercentage;
    [SerializeField] float waveSize;
    [SerializeField] float difficultyScale;
    [SerializeField] int breakBetweenWaves;

    [Header("Time Between Spawns")]
    [SerializeField] float spawnFrequency;
    [SerializeField] float spawnFrequencyIncrease;
    [SerializeField] float spawnFrequencyCap;

    [Header("Debug")]
    [SerializeField] int enemiesSpawnedThisWave;

    public int waveCounter { get; private set; } = 0;
    bool waveSpawning;


    enum enemyTypes
    {
        Normal,
        Heavy
    }

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(this);
        else
            Instance = this;

        EnemyBasic.OnEnemyDeath += EnemyDied;
    }


    private void Start()
    {
        StartWave();
    }

    private void Update()
    {
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

    void StartWave()
    {
        levelTimer = -3;
        waveCounter++;
        Debug.Log($"starting wave {waveCounter}, {waveSize} total, spawn every {spawnFrequency}");
        enemiesSpawnedThisWave = 0;
        waveSpawning = true;
        //add co for short delay between levels
    }

    void Spawner()
    {
        if (enemiesSpawnedThisWave < waveSize)
        {
            if (levelTimer > spawnFrequency * enemiesSpawnedThisWave)
            {
                if (Random.Range(0, 100) < smallEnemyPercentage)
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
        else if (enemiesAlive < 1)
        {
            StartCoroutine(EndWave());
        }
    }

    IEnumerator EndWave()
    {
        waveSpawning = false;
        yield return new WaitForSeconds(breakBetweenWaves);
        //scale difficulty
        // wave size
        waveSize *= MathF.Pow(difficultyScale, waveCounter);
        waveSize = MathF.Round(waveSize);

        // spawn rate
        spawnFrequency -= spawnFrequencyIncrease;
        if (spawnFrequency < spawnFrequencyCap) spawnFrequency = spawnFrequencyCap;

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

    void EnemyDied()
    {
        enemiesAlive--;
    }
}

