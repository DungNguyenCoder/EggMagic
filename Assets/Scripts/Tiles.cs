using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tiles : MonoBehaviour
{
    [SerializeField] private GameObject _green1Tile;
    [SerializeField] private GameObject _green2Tile;
    [SerializeField] private Egg _eggPrefabs;
    private Egg _currentEgg;
    private int _currentEggID;
    private EggData[] eggDatas;
    private Animator _animator;
    private bool _isHighlighted = false;
    private int _currentEggIndex;
    private void Awake()
    {
        _green1Tile.SetActive(false);
        _green2Tile.SetActive(false);
        _animator = GetComponent<Animator>();
        Initialize();
    }

    private void OnMouseDown()
    {
        Board.Instance.OnTileClicked(this);
    }

    private void Initialize()
    {
        eggDatas = Resources.LoadAll<EggData>(GameConfig.EGG_DATA_PATH);
        _currentEggIndex = Random.Range(0, eggDatas.Length);
        SpawnEgg(_currentEggIndex);
        SetTile(Board.Instance.GetIsGreen1());
    }
    public void SpawnEgg(int idx)
    {
        _currentEggID = eggDatas[idx].eggID;
        Egg egg = Board.Instance.EggPool.GetEgg();
        egg.transform.SetParent(this.transform);
        egg.transform.localPosition = Vector3.zero;
        // Egg egg = Instantiate(_eggPrefabs, this.transform);
        egg.Setup(eggDatas[idx], this);
        if (_currentEgg != null)
        {
            Board.Instance.EggPool.ReturnEgg(egg);
        }
        _currentEgg = egg;
    }
    public void SetNewLvlEgg()
    {
        _currentEggIndex++;
        if (_currentEggIndex >= eggDatas.Length)
        {
            _currentEggIndex = 0;
        }
        RemoveEgg();
        SpawnEgg(_currentEggIndex);
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
    public void Popup()
    {
        _animator.SetBool("Popup", true);

        SpriteRenderer sr = null;
        if (_green1Tile.activeSelf)
        {
            sr = _green1Tile.GetComponent<SpriteRenderer>();
        }
        else
        {
            sr = _green2Tile.GetComponent<SpriteRenderer>();
        }
        sr.color = new Color(0f, 1f, 0f);

        _isHighlighted = true;
    }
    public void PopDown()
    {
        _animator.SetBool("Popup", false);

        SpriteRenderer sr = null;
        if (_green1Tile.activeSelf)
        {
            sr = _green1Tile.GetComponent<SpriteRenderer>();
        }
        else
        {
            sr = _green2Tile.GetComponent<SpriteRenderer>();
        }
        sr.color = new Color(1f, 1f, 1f);

        _isHighlighted = false;
    }

    public bool GetIsHighlighted()
    {
        return _isHighlighted;
    }

    public void RemoveEgg()
    {
        if (_currentEgg != null)
        {
            // _eggPrefabs.gameObject.SetActive(false);
            // Destroy(_currentEgg.gameObject);
            Board.Instance.EggPool.ReturnEgg(_currentEgg);
            _currentEgg = null;
        }
    }
}
