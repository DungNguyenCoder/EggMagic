using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelShare : Panel
{
    public void OnClickYesButton()
    {
        AudioManager.Instance.PlaySFX(AudioManager.Instance.select);
        PanelManager.Instance.ClosePanel(GameConfig.PANEL_SHARE);
        Time.timeScale = 1f;
    }
    public void OnClickNoButton()
    {
        UIManager.Instance.NoOption(GameConfig.PANEL_SHARE);
    }
}
