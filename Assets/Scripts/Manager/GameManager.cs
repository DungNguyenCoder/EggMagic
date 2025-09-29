using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    private int _score;
    public int _highScore { get; private set; }
    private float _time = 10f;

    private void Start()
    {
        // PlayerPrefs.DeleteAll();
        _highScore = PlayerPrefs.GetInt(GameConfig.HIGH_SCORE_KEY, 0);
    }
    private void Update()
    {
        if (_time >= 0)
        {
            _time -= Time.deltaTime;
        }
        else if (_time <= 0)
        {
            PanelManager.Instance.OpenPanel(GameConfig.PANEL_GAME_OVER);
        }
    }

    private void OnEnable()
    {
        EventManager.onUpdatePoint += AddScore;
    }
    private void OnDisable()
    {
        EventManager.onUpdatePoint -= AddScore;
    }
    private void AddScore(int point)
    {
        _score += point;
        Debug.Log("" + _score);
        if (_score > _highScore)
        {
            _highScore = _score;
            PlayerPrefs.SetInt(GameConfig.HIGH_SCORE_KEY, _highScore);
            PlayerPrefs.Save();
        }
        EventManager.onUpdatePointUI?.Invoke(_score);
        _time += 4f;
        if (_time > 10f)
        {
            _time = 10f;
        }
    }

    public void SetScore(int score)
    {
        _score = score;
    }
    public int GetScore()
    {
        return _score;
    }
    public float GetTime()
    {
        return _time;
    }
    public void SetTime(float time)
    {
        _time = time;
    }
}
