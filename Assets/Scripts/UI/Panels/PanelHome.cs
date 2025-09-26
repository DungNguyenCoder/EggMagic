using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PanelHome : Panel
{
    public void OnClickYesButton()
    {
        AudioManager.Instance.PlaySFX(AudioManager.Instance.select);
        AudioManager.Instance.PlayMusicFromStart();
        PanelManager.Instance.ClosePanel(GameConfig.PANEL_HOME);
        SceneManager.LoadScene("MainMenu");
    }
    public void OnClickNoButton()
    {
        UIManager.Instance.NoOption(GameConfig.PANEL_HOME);
    }
}
