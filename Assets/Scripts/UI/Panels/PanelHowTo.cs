using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelHowTo : Panel
{
    public void OnClickBackButton()
    {
        AudioManager.Instance.PlaySFX(AudioManager.Instance.select);
        PanelManager.Instance.ClosePanel(GameConfig.PANEL_HOW_TO);
        Time.timeScale = 1f;
    }
}
