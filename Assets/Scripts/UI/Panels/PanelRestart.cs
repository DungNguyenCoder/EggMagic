using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PanelRestart : Panel
{
    public void OnClickYesButton()
    {
        AudioManager.Instance.PlaySFX(AudioManager.Instance.select);
        PanelManager.Instance.ClosePanel(GameConfig.PANEL_RESTART);
        Time.timeScale = 1f;
        GameManager.Instance.SetScore(0);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void OnClickNoButton()
    {
        UIManager.Instance.NoOption(GameConfig.PANEL_RESTART);
    }
}
