using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMManager : MonoBehaviour
{
    public static BGMManager instance;
    public AudioSource audioSource;
    public AudioClip[] audioClips;
    public bool isAttack = false;

    private float fadeDuration = 0.4f;
    private Coroutine fadeCoroutine;

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
        if (fadeCoroutine != null)
        {
            StopCoroutine(fadeCoroutine);
        }

        fadeCoroutine = StartCoroutine(FadeOutAndIn(audioClips[0]));
    }

    public void AttackBGM()
    {
        if (fadeCoroutine != null)
        {
            StopCoroutine(fadeCoroutine);
        }

        fadeCoroutine = StartCoroutine(FadeOutAndIn(audioClips[1]));
    }

    private IEnumerator FadeOutAndIn(AudioClip newClip)
    {
        float fadeOutTime = fadeDuration / 2f;
        float fadeInTime = fadeDuration / 2f;

        float startVolume = audioSource.volume;

        // Fade out
        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.deltaTime / fadeOutTime;
            yield return null;
        }

        audioSource.Stop();
        audioSource.clip = newClip;
        audioSource.Play();

        // Fade in
        while (audioSource.volume < startVolume)
        {
            audioSource.volume += startVolume * Time.deltaTime / fadeInTime;
            yield return null;
        }

        fadeCoroutine = null;
    }
}
