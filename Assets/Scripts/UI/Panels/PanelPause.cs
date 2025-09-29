using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
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
        Debug.Log("Set Music");
        float volume = musicSlider.value;
        myMixer.SetFloat("Music", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat(GameConfig.MUSIC_VOLUME_KEY, volume);
        PlayerPrefs.Save();
    }
    public void SetSFXVolume()
    {
        Debug.Log("Set SFX");
        float volume = sfxSlider.value;
        myMixer.SetFloat("SFX", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat(GameConfig.SFX_VOLUME_KEY, volume);
        PlayerPrefs.Save();
    }
    private void LoadMusicVolume()
    {
        Debug.Log("Load Music");
        float volume = PlayerPrefs.GetFloat(GameConfig.MUSIC_VOLUME_KEY);
        musicSlider.value = volume;
    }

    private void LoadSFXVolume()
    {
        Debug.Log("Load SFX");
        float volume = PlayerPrefs.GetFloat(GameConfig.SFX_VOLUME_KEY);
        sfxSlider.value = volume;
    }
    public void OnClickBackButton()
    {
        AudioManager.Instance.PlaySFX(AudioManager.Instance.select);
        PanelManager.Instance.ClosePanel(GameConfig.PANEL_PAUSE);
        Time.timeScale = 1f;
    }
}
