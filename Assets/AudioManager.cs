using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public AudioSource audioSource;
    public AudioClip[] audioClips;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void GetAudio()
    {
        audioSource.clip = audioClips[0];
        audioSource.Play();
    }

    public void DeniedAudio()
    {
        audioSource.clip = audioClips[1];
        audioSource.Play();
    }

    public void WinAudio()
    {
        audioSource.clip = audioClips[2];
        audioSource.Play();
    }

    public void WalkAudio()
    {
        audioSource.clip = audioClips[3];
        audioSource.Play();
    }
}
