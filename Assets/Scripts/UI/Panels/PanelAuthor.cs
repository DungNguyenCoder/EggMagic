using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelAuthor : Panel
{
    public void OnClickBackButton()
    {
        AudioManager.Instance.PlaySFX(AudioManager.Instance.select);
        PanelManager.Instance.ClosePanel(GameConfig.PANEL_AUTHOR);
        Time.timeScale = 1f;
    }
}
