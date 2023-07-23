using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [SerializeField] AudioSource WaveStart;

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(this);
        else
            Instance = this;

        EnemyManager.OnWaveStart += PlayWaveStart;
    }

    private void Start()
    {
    }

    void PlayWaveStart(float wave)
    {
        WaveStart.Play();
    }
}
