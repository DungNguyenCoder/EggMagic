using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[CreateAssetMenu(fileName = "Egg", menuName = "EggData", order = 0)]
public class EggData : ScriptableObject
{
    public int eggID;
    public Sprite eggSprite;
    public GameObject eggAnimation;
}