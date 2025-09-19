using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;

public class Egg : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _eggSprite;
    private EggData _data;
    private BackgroundTile _panel;

    public void Setup(EggData data, BackgroundTile panel)
    {
        _data = data;
        _panel = panel;
        _eggSprite.sprite = data.eggSprite;
    }

    // public void OnClick()
    // {
    //     if (_panel != null)
    //     {
    //         _panel.SelectCharacter(_data);
    //     }
    // }
}