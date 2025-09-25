using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tiles : MonoBehaviour
{
    [SerializeField] private GameObject _green1Tile;
    [SerializeField] private GameObject _green2Tile;
    [SerializeField] private Egg _eggPrefabs;
    private int _row;
    private int _col;
    private Egg _currentEgg;
    private int _currentEggID;
    private EggData[] _eggDatas;
    private Animator _animator;
    private bool _isHighlighted = false;
    private void Awake()
    {
        _green1Tile.SetActive(false);
        _green2Tile.SetActive(false);
        _animator = GetComponent<Animator>();
        Initialize();
    }

    private void OnMouseDown()
    {
        if (UIManager.Instance.IsPointerOverUI())
            return;
        Board.Instance.OnTileClicked(this);
    }

    private void Initialize()
    {
        _eggDatas = Resources.LoadAll<EggData>(GameConfig.EGG_DATA_PATH);
        GameConfig.TOTAL_EGG_NUMBER = _eggDatas.Length;
        System.Array.Sort(_eggDatas, (a, b) => a.eggID.CompareTo(b.eggID));
        int _newEggID = Random.Range(0, GameConfig.EGG_SPAWN_LEVEL);
        SpawnEgg(_newEggID);
        SetTile(Board.Instance.GetIsGreen1());
    }
    public void SpawnEgg(int ID)
    {
        if (_currentEgg != null)
        {
            Board.Instance.EggPool.ReturnEgg(_currentEgg);
            _currentEgg = null;
        }
        Egg egg = Board.Instance.EggPool.GetEgg();
        egg.transform.SetParent(this.transform);
        egg.transform.localPosition = Vector3.zero;
        // Egg egg = Instantiate(_eggPrefabs, this.transform);
        egg.Setup(_eggDatas[ID], this);
        _currentEggID = ID;
        _currentEgg = egg;
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
    public void SetNewLvlEgg()
    {
        _currentEggID = (_currentEggID + 1) % GameConfig.TOTAL_EGG_NUMBER;
        SpawnEgg(_currentEggID);
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
    public bool GetIsHighlighted()
    {
        return _isHighlighted;
    }
    public void RemoveEgg()
    {
        if (_currentEgg != null)
        {
            Board.Instance.EggPool.ReturnEgg(_currentEgg);
            _currentEgg = null;
        }
    }
    public Egg GetEgg()
    {
        return _currentEgg;
    }
    public void SetEggInstance(Egg egg, int eggId)
    {
        _currentEgg = egg;
        _currentEggID = eggId;
    }
    public void ClearOnly()
    {
        _currentEgg = null;
        _currentEggID = -1;
    }
    public int GetTileRow()
    {
        return this._row;
    }
    public int GetTileCol()
    {
        return this._col;
    }
    public void SetTilePosition(int row, int col)
    {
        _row = row;
        _col = col;
    }
    public EggData GetEggData(int idx)
    {
        return _eggDatas[idx];
    }
}
