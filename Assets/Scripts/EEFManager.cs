using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EEFManager : MonoBehaviour
{
    public static EEFManager instance;
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

    public void GameOverAudio()
    {
        audioSource.clip = audioClips[4];
        audioSource.Play();
    }

    public void AttackAudio()
    {
        audioSource.clip = audioClips[5];
        audioSource.Play();
    }

    public void BrokenAudio()
    {
        audioSource.clip = audioClips[6];
        audioSource.Play();
    }
}
