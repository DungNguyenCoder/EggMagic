using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class PanelInfomation : Panel
{
    private void Awake()
    {
        this.transform.localScale = new Vector3(0, 0, 0);
        this.transform.DOScale(new Vector3(1, 1, 1), 0.2f).SetUpdate(true);
    }
    private void OnDisable()
    {
        DOTween.Kill(this.transform);
    }
    public void OnClickInfoButton()
    {
        AudioManager.Instance.PlaySFX(AudioManager.Instance.select);
        PanelManager.Instance.OpenPanel(GameConfig.PANEL_AUTHOR);
    }
    public void OnClickFacebookButton()
    {
        AudioManager.Instance.PlaySFX(AudioManager.Instance.select);
        Application.OpenURL(GameConfig.FACEBOOK_LINK);
    }
    public void OnClickGithubButton()
    {
        AudioManager.Instance.PlaySFX(AudioManager.Instance.select);
        Application.OpenURL(GameConfig.GITHUB_LINK);
    }
    public void OnClickNoAdsButton()
    {
        AudioManager.Instance.PlaySFX(AudioManager.Instance.select);
    }
    public void OnClickBackButton()
    {
        AudioManager.Instance.PlaySFX(AudioManager.Instance.select);
        PanelManager.Instance.ClosePanel(GameConfig.PANEL_INFOMATION);
        Time.timeScale = 1f;
    }
}
