using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class AudioManager : Singleton<AudioManager>
{
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource SFXSource;
    [SerializeField] private AudioMixer audioMixer;
    public AudioClip addScore;
    public AudioClip gameOver;
    public AudioClip eggBreak;
    public AudioClip select;

    public override void Awake()
    {
        base.Awake();
    }
    private void Start()
    {
        if (PlayerPrefs.HasKey(GameConfig.MUSIC_VOLUME_KEY))
        {
            float vol = PlayerPrefs.GetFloat(GameConfig.MUSIC_VOLUME_KEY);
            audioMixer.SetFloat("Music", Mathf.Log10(vol) * 20);
        }
        if (PlayerPrefs.HasKey(GameConfig.SFX_VOLUME_KEY))
        {
            float vol = PlayerPrefs.GetFloat(GameConfig.SFX_VOLUME_KEY);
            audioMixer.SetFloat("SFX", Mathf.Log10(vol) * 20);
        }
    }
    public void PlaySFX(AudioClip audioClip)
    {
        SFXSource.PlayOneShot(audioClip);
    }
    public void PlayMusicFromStart()
    {
        musicSource.Stop();
        musicSource.time = 0f;
        musicSource.Play();
    }
}
