using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;

public class Egg : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _eggSprite;
    private EggData _data;
    private Tiles _panel;

    public void Setup(EggData data, Tiles panel)
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