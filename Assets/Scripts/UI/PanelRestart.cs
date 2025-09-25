using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelRestart : Panel
{
    public void OnClickYesButton()
    {
        UIManager.Instance.Restart();
    }
    public void OnClickNoButton()
    {
        UIManager.Instance.NoOption(GameConfig.PANEL_RESTART);
    }
}
