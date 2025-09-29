using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class PanelHowTo : Panel
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
    public void OnClickBackButton()
    {
        AudioManager.Instance.PlaySFX(AudioManager.Instance.select);
        PanelManager.Instance.ClosePanel(GameConfig.PANEL_HOW_TO);
        Time.timeScale = 1f;
    }
}
