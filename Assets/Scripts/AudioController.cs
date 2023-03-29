using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    [SerializeField] private AudioClip launchSound;
    
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private AudioSource _audioSource;

    public void PlayLaunchSound()
    {
        audioSource.PlayOneShot(launchSound);
    }
}
