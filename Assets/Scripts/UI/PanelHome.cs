using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelHome : Panel
{
    public void OnClickYesButton()
    {
        UIManager.Instance.Home();
    }
    public void OnClickNoButton()
    {
        UIManager.Instance.NoOption(GameConfig.PANEL_HOME);
    }
}
