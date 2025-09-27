using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Audio;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class InGame : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI highScoreText;
    [SerializeField] private TextMeshProUGUI eggLevelText;
    [SerializeField] private Image currentEggImage;
    [SerializeField] private Image nextEggImage;
    [SerializeField] private Image FillBar;
    private void Start()
    {
        Time.timeScale = 1f;
        GameManager.Instance.SetTime(10f);
        highScoreText.text = "" + PlayerPrefs.GetInt(GameConfig.HIGH_SCORE_KEY, 0);
        FillBar.fillAmount = 1f;
    }
    private void Update()
    {
        FillBar.fillAmount = GameManager.Instance.GetTime() / 10f;
    }

    private void OnEnable()
    {
        EventManager.onUpdatePointUI += UpdatePoint;
        EventManager.onUpdateEggLvlUI += UpdateEggLvl;
        EventManager.onUpdateEgg += ShowEggPreview;
    }
    private void OnDisable()
    {
        EventManager.onUpdatePointUI -= UpdatePoint;
        EventManager.onUpdateEggLvlUI -= UpdateEggLvl;
        EventManager.onUpdateEgg -= ShowEggPreview;
    }

    private void UpdatePoint(int _score)
    {
        scoreText.text = "" + _score;
        highScoreText.text = "" + PlayerPrefs.GetInt(GameConfig.HIGH_SCORE_KEY, 0);
    }

    private void UpdateEggLvl(int lvl)
    {
        eggLevelText.text = "" + lvl;
    }

    public void ShowEggPreview(EggData currentData, EggData nextData)
    {
        currentEggImage.sprite = currentData.eggSprite;
        nextEggImage.sprite = nextData.eggSprite;
    }
    public void OnClickHome()
    {
        PanelManager.Instance.OpenPanel(GameConfig.PANEL_HOME);
        AudioManager.Instance.PlaySFX(AudioManager.Instance.select);
        Time.timeScale = 0f;
    }
    public void OnClickRetart()
    {
        PanelManager.Instance.OpenPanel(GameConfig.PANEL_RESTART);
        AudioManager.Instance.PlaySFX(AudioManager.Instance.select);
        Time.timeScale = 0f;
    }
    public void OnClickPause()
    {
        PanelManager.Instance.OpenPanel(GameConfig.PANEL_PAUSE);
        AudioManager.Instance.PlaySFX(AudioManager.Instance.select);
        Time.timeScale = 0f;
    }
    public void OnClickShare()
    {
        PanelManager.Instance.OpenPanel(GameConfig.PANEL_INFOMATION);
        AudioManager.Instance.PlaySFX(AudioManager.Instance.select);
        Time.timeScale = 0f;
    }
    public void OnClickHowTo()
    {
        PanelManager.Instance.OpenPanel(GameConfig.PANEL_HOW_TO);
        AudioManager.Instance.PlaySFX(AudioManager.Instance.select);
        Time.timeScale = 0f;
    }
}