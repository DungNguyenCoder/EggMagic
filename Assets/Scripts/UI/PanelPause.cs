using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class PanelPause : Panel
{
    [SerializeField] private AudioMixer myMixer;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;
    private void Start()
    {
        if (PlayerPrefs.HasKey(GameConfig.MUSIC_VOLUME_KEY))
            LoadMusicVolume();
        else
            SetMusicVolume();

        if (PlayerPrefs.HasKey(GameConfig.SFX_VOLUME_KEY))
            LoadSFXVolume();
        else
            SetSFXVolume();
    }
    public void SetMusicVolume()
    {
        float volume = musicSlider.value;
        myMixer.SetFloat("Music", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat(GameConfig.MUSIC_VOLUME_KEY, volume);
        PlayerPrefs.Save();
    }
    public void SetSFXVolume()
    {
        float volume = sfxSlider.value;
        myMixer.SetFloat("SFX", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat(GameConfig.SFX_VOLUME_KEY, volume);
        PlayerPrefs.Save();
    }
    private void LoadMusicVolume()
    {
        musicSlider.value = PlayerPrefs.GetFloat(GameConfig.MUSIC_VOLUME_KEY);
    }
    private void LoadSFXVolume()
    {
        sfxSlider.value = PlayerPrefs.GetFloat(GameConfig.SFX_VOLUME_KEY);
    }
    public void OnClickBackButton()
    {
        UIManager.Instance.Pause();
    }
}
