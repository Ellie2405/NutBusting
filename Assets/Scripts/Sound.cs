using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public AudioClip clip;

    [Range(0f, 1f)]
    public float volume = 0.1f;

    public void Play(AudioSource source)
    {
        source.volume = volume;
        source.PlayOneShot(clip);
    }
}
