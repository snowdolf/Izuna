using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    private AudioSource sourceBGM;
    private AudioSource sourceSFX;

    public AudioClip clipBGM;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        sourceBGM = gameObject.AddComponent<AudioSource>();
        sourceBGM.loop = true;
        sourceBGM.volume = 0.18f;

        sourceSFX = gameObject.AddComponent<AudioSource>();
        sourceSFX.loop = false;

        PlayBGM();
    }

    public void PlayBGM()
    {
        if (clipBGM != null)
        {
            sourceBGM.Stop();
            sourceBGM.clip = clipBGM;
            sourceBGM.Play();
        }
    }

    public void StopBGM()
    {
        sourceBGM.Stop();
    }

    public void PlaySound(AudioClip clip, float soundEffectVolume, float soundEffectPitchVariance)
    {
        sourceSFX.Stop();
        sourceSFX.volume = soundEffectVolume;
        sourceSFX.pitch = 1f + Random.Range(-soundEffectPitchVariance, soundEffectPitchVariance);
        sourceSFX.PlayOneShot(clip);
    }
}
