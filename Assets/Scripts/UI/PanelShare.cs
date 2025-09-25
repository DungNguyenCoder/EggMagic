using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelShare : Panel
{
    public void OnClickYesButton()
    {
        UIManager.Instance.Share();
    }
    public void OnClickNoButton()
    {
        UIManager.Instance.NoOption(GameConfig.PANEL_SHARE);
    }
}
