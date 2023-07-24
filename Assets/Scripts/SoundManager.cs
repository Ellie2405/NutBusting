using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [SerializeField] AudioMixer audioMixer;
    [SerializeField] AudioSource WaveStart;
    [SerializeField] AudioSource BGM;
    [SerializeField] AudioSource Ambient1;
    [SerializeField] AudioSource Ambient2;

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(this);
        else
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }

    }

    private void Start()
    {
        BGM.Play();
        Ambient1.Play();
        Ambient2.Play();
        EnemyManager.OnWaveStart += PlayWaveStart;
    }

    void PlayWaveStart(float wave)
    {
        WaveStart.Play();
    }

    public void ToggleSound(bool toggle)
    {
        int volume = toggle ? 0 : -80;
        audioMixer.SetFloat("MasterVolume", volume);
    }

}
