using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundTile : MonoBehaviour
{
    [SerializeField] private GameObject green1Tile;
    [SerializeField] private GameObject green2Tile;
    [SerializeField] private Egg eggPrefab;

    private EggData[] eggDatas;

    private void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        eggDatas = Resources.LoadAll<EggData>(GameConfig.EGG_DATA_PATH);
        int eggToUse = Random.Range(0, eggDatas.Length);
        Egg egg = Instantiate(eggPrefab, this.transform);
        egg.Setup(eggDatas[eggToUse], this);
    }

    public void SetTile(bool isGreen1)
    {
        green1Tile.SetActive(isGreen1);
        green2Tile.SetActive(!isGreen1);
    }
}
