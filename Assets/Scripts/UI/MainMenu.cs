using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : Panel
{
    [SerializeField] private GameObject gameTitle;
    [SerializeField] private GameObject playButton;
    private void Start()
    {
        gameTitle.transform.DOScale(0.9f, 1f).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo);
        playButton.transform.DOScale(0.9f, 1f).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo);
    }
    public void OnClickPlayButton()
    {
        AudioManager.Instance.PlaySFX(AudioManager.Instance.select);
        AudioManager.Instance.PlayMusicFromStart();
        SceneManager.LoadScene("GamePlay");
    }
    public void OnClickRankings()
    {
        
    }
    public void OnClickShare()
    {
        PanelManager.Instance.OpenPanel(GameConfig.PANEL_SHARE);
        AudioManager.Instance.PlaySFX(AudioManager.Instance.select);
        Time.timeScale = 0f;
    }
    public void OnClickSetting()
    {
        PanelManager.Instance.OpenPanel(GameConfig.PANEL_PAUSE);
        AudioManager.Instance.PlaySFX(AudioManager.Instance.select);
        Time.timeScale = 0f;
    }
    public void OnClickGift()
    {

    }
    public void OnClickHowTo()
    {
        PanelManager.Instance.OpenPanel(GameConfig.PANEL_HOW_TO);
        AudioManager.Instance.PlaySFX(AudioManager.Instance.select);
        Time.timeScale = 0f;
    }
}
