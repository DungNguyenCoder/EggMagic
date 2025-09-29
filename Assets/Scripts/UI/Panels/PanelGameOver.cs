using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Audio;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class PanelGameOver : Panel
{
    [SerializeField] private GameObject _star1Off;
    [SerializeField] private GameObject _star1On;
    [SerializeField] private GameObject _star2Off;
    [SerializeField] private GameObject _star2On;
    [SerializeField] private GameObject _star3Off;
    [SerializeField] private GameObject _star3On;
    [SerializeField] private TextMeshProUGUI _score;
    [SerializeField] private TextMeshProUGUI _highScore;
    private bool _is1Star = false;
    private bool _is2Star = false;
    private bool _is3Star = false;

    private void Awake()
    {
        this.transform.localScale = new Vector3(0, 0, 0);
        this.transform.DOScale(new Vector3(1, 1, 1), 0.2f).SetUpdate(true);
        _is1Star = false;
        _is2Star = false;
        _is3Star = false;
        _score.text = "" + GameManager.Instance.GetScore();
        _highScore.text = "" + GameManager.Instance._highScore;
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
        _star1Off.SetActive(!_is1Star);
        _star1On.SetActive(_is1Star);
        _star2Off.SetActive(!_is2Star);
        _star2On.SetActive(_is2Star);
        _star3Off.SetActive(!_is3Star);
        _star3On.SetActive(_is3Star);
    }
    public void OnClickHome()
    {
        PanelManager.Instance.ClosePanel(GameConfig.PANEL_GAME_OVER);
        PanelManager.Instance.OpenPanel(GameConfig.PANEL_HOME);
        AudioManager.Instance.PlaySFX(AudioManager.Instance.select);
    }
    public void OnClickShare()
    {
        PanelManager.Instance.OpenPanel(GameConfig.PANEL_INFOMATION);
        AudioManager.Instance.PlaySFX(AudioManager.Instance.select);
    }
    public void OnClickRetart()
    {
        Time.timeScale = 1f;
        PanelManager.Instance.OpenPanel(GameConfig.PANEL_RESTART);
        AudioManager.Instance.PlaySFX(AudioManager.Instance.select);
    }
    public void OnClickLeaderboard()
    {
        PanelManager.Instance.OpenPanel(GameConfig.PANEL_LEADERBOARD);
        AudioManager.Instance.PlaySFX(AudioManager.Instance.select);
        Time.timeScale = 0f;
    }
    public void OnClickHowTo()
    {
        PanelManager.Instance.OpenPanel(GameConfig.PANEL_HOW_TO);
        AudioManager.Instance.PlaySFX(AudioManager.Instance.select);
    }
}