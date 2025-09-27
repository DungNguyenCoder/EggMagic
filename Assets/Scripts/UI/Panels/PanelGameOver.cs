using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Audio;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class PanelGameOver : Panel
{
    [SerializeField] private GameObject Star1Off;
    [SerializeField] private GameObject Star1On;
    [SerializeField] private GameObject Star2Off;
    [SerializeField] private GameObject Star2On;
    [SerializeField] private GameObject Star3Off;
    [SerializeField] private GameObject Star3On;
    private bool _is1Star = false;
    private bool _is2Star = false;
    private bool _is3Star = false;

    private void Awake()
    {
        _is1Star = false;
        _is2Star = false;
        _is3Star = false;
        SetStar(_is1Star, _is2Star, _is3Star);
    }
    private void Start()
    {
        if (GameManager.Instance.GetScore() >= GameManager.Instance._highScore / 3)
        {
            _is1Star = true;
        }
        if (GameManager.Instance.GetScore() >= GameManager.Instance._highScore * 2 / 3)
        {
            _is2Star = true;
        }
        if (GameManager.Instance.GetScore() >= GameManager.Instance._highScore)
        {
            _is3Star = true;
        }
        SetStar(_is1Star, _is2Star, _is3Star);
    }
    private void SetStar(bool _is1Star, bool _is2Star, bool _is3Star)
    {
        Star1Off.SetActive(!_is1Star);
        Star1On.SetActive(_is1Star);
        Star2Off.SetActive(!_is2Star);
        Star2On.SetActive(_is2Star);
        Star3Off.SetActive(!_is3Star);
        Star3On.SetActive(_is3Star);
    }
    public void OnClickHome()
    {
        PanelManager.Instance.OpenPanel(GameConfig.PANEL_HOME);
        AudioManager.Instance.PlaySFX(AudioManager.Instance.select);
        Time.timeScale = 0f;
    }
    public void OnClickShare()
    {
        PanelManager.Instance.OpenPanel(GameConfig.PANEL_INFOMATION);
        AudioManager.Instance.PlaySFX(AudioManager.Instance.select);
        Time.timeScale = 0f;
    }
    public void OnClickRetart()
    {
        PanelManager.Instance.OpenPanel(GameConfig.PANEL_RESTART);
        AudioManager.Instance.PlaySFX(AudioManager.Instance.select);
        Time.timeScale = 0f;
    }
    public void OnClickRankings()
    {
        
    }
    public void OnClickHowTo()
    {
        PanelManager.Instance.OpenPanel(GameConfig.PANEL_HOW_TO);
        AudioManager.Instance.PlaySFX(AudioManager.Instance.select);
        Time.timeScale = 0f;
    }
}