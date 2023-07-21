using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXDestroyOnEnd : MonoBehaviour
{
    [SerializeField] ParticleSystem _particleSystem;


    
    private void OnParticleSystemStopped()
    {
        Destroy(gameObject);
    }
}
