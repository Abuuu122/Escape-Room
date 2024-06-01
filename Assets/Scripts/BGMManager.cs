using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMManager : MonoBehaviour
{
    public static BGMManager instance;
    public AudioSource audioSource;
    public AudioClip[] audioClips;
    public bool isAttack = false;

    public void Start()
    {
        NormalBGM();
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void NormalBGM()
    {
        audioSource.clip = audioClips[0];
        audioSource.Play();
    }

    public void AttackBGM()
    {
        audioSource.clip = audioClips[1];
        audioSource.Play();
    }
}
