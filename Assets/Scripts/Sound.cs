using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public AudioClip clip;

    [Range(0f, 1f)]
    public float volume = 0.1f;

    AudioSource source;

    public void AssignSource(AudioSource audioSource)
    {
        source = audioSource;
    }

    public void Play()
    {
        source.volume = volume;
        source.PlayOneShot(clip);
    }
}
