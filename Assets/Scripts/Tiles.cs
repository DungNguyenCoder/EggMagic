using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tiles : MonoBehaviour
{
    [SerializeField] private GameObject _green1Tile;
    [SerializeField] private GameObject _green2Tile;
    [SerializeField] private Egg _currentEgg;
    private int _currentEggID;
    private EggData[] eggDatas;

    private void Awake()
    {
        Initialize();
    }

    private void Initialize()
    {
        eggDatas = Resources.LoadAll<EggData>(GameConfig.EGG_DATA_PATH);
        int eggToUse = Random.Range(0, eggDatas.Length);
        _currentEggID = eggDatas[eggToUse].eggID;
        Egg egg = Instantiate(_currentEgg, this.transform);
        egg.Setup(eggDatas[eggToUse], this);
    }

    public void SetTile(bool isGreen1)
    {
        _green1Tile.SetActive(isGreen1);
        _green2Tile.SetActive(!isGreen1);
    }
    public int GetCurrentEggID()
    {
        return _currentEggID;
    }
}
