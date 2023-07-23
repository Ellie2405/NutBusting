using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [SerializeField] AudioSource WaveStart;
    [SerializeField] AudioSource RotatingStones;

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

    public void PlayRotatingStone()
    {
        RotatingStones.Play();
    }

    public void StopRotatingStone()
    {
        Debug.Log("stopping sound");
        RotatingStones.Stop();
    }
}
