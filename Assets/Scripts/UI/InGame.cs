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
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private TextMeshProUGUI _highScoreText;
    [SerializeField] private TextMeshProUGUI _eggLevelText;
    [SerializeField] private Image _currentEggImage;
    [SerializeField] private Image _nextEggImage;
    [SerializeField] private Image _fillBar;
    [SerializeField] private Image _sceneAnimation;
    private void Start()
    {
        StartCoroutine(NextSceneAnimation());
        GameManager.Instance.SetTime(10f);
        _highScoreText.text = "" + PlayerPrefs.GetInt(GameConfig.HIGH_SCORE_KEY, 0);
        _fillBar.fillAmount = 1f;
    }
    private void Update()
    {
        _fillBar.fillAmount = GameManager.Instance.GetTime() / 10f;
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
    private IEnumerator NextSceneAnimation()
    {
        Time.timeScale = 0f;
        _sceneAnimation.fillAmount = 1f;
        float duration = 0.5f;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.unscaledDeltaTime;
            _sceneAnimation.fillAmount = Mathf.Lerp(1f, 0f, elapsed / duration);
            yield return null;
        }
        Time.timeScale = 1f;
        _sceneAnimation.fillAmount = 0f;
    }

    private void UpdatePoint(int _score)
    {
        _scoreText.text = "" + _score;
        _highScoreText.text = "" + PlayerPrefs.GetInt(GameConfig.HIGH_SCORE_KEY, 0);
    }

    private void UpdateEggLvl(int lvl)
    {
        _eggLevelText.text = "" + lvl;
    }

    public void ShowEggPreview(EggData currentData, EggData nextData)
    {
        _currentEggImage.sprite = currentData.eggSprite;
        _nextEggImage.sprite = nextData.eggSprite;
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