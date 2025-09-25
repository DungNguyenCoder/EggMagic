using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class UIManager : Singleton<UIManager>
{
    public bool IsPointerOverUI()
    {
        EventSystem eventSystem = EventSystem.current;
        PointerEventData eventData = new PointerEventData(eventSystem);
        eventData.position = Input.mousePosition;
        List<RaycastResult> results = new List<RaycastResult>();
        eventSystem.RaycastAll(eventData, results);
        return results.Count > 0;
    }
    public void NoOption(string panelName)
    {
        AudioManager.Instance.PlaySFX(AudioManager.Instance.select);
        PanelManager.Instance.ClosePanel(panelName);
        Time.timeScale = 1f;
    }
    public void Home()
    {
        AudioManager.Instance.PlaySFX(AudioManager.Instance.select);
        PanelManager.Instance.ClosePanel(GameConfig.PANEL_HOME);
        SceneManager.LoadScene("MainMenu");
    }
    public void Restart()
    {
        AudioManager.Instance.PlaySFX(AudioManager.Instance.select);
        PanelManager.Instance.ClosePanel(GameConfig.PANEL_RESTART);
        Time.timeScale = 1f;
        GameManager.Instance.SetScore(0);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void Pause()
    {
        AudioManager.Instance.PlaySFX(AudioManager.Instance.select);
        PanelManager.Instance.ClosePanel(GameConfig.PANEL_PAUSE);
        Time.timeScale = 1f;
    }
    public void Share()
    {
        AudioManager.Instance.PlaySFX(AudioManager.Instance.select);
        PanelManager.Instance.ClosePanel(GameConfig.PANEL_SHARE);
        Time.timeScale = 1f;
    }
    public void HowTo()
    {
        AudioManager.Instance.PlaySFX(AudioManager.Instance.select);
        PanelManager.Instance.ClosePanel(GameConfig.PANEL_HOW_TO);
        Time.timeScale = 1f;
    }
}