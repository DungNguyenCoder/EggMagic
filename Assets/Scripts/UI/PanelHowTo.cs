using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelHowTo : Panel
{
    public void OnClickBackButton()
    {
        UIManager.Instance.HowTo();
    }
}
