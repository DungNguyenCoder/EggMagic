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
        System.Array.Sort(eggDatas, (a, b) => a.eggID.CompareTo(b.eggID));
        int _newEggID = Random.Range(0, eggDatas.Length);
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
        egg.Setup(eggDatas[ID], this);
        _currentEggID = ID;
        _currentEgg = egg;
    }
    public void SetNewLvlEgg()
    {
        _currentEggID = (_currentEggID + 1) % 3;
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

    public Egg GetEggInstance()
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
}
