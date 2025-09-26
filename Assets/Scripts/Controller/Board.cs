using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Board : Singleton<Board>
{
    public int _width = 5;
    public int _height = 5;
    [SerializeField] private Tiles _tilePrefab;
    private Tiles[,] _eggTiles;
    private int[,] _eggTilesID;
    private List<(int row, int col, int dist)> _eggSameID;
    private bool _isGreen1;
    private bool _isProcessing = false;
    private int _bestEggLvl = 0;
    [SerializeField] private EggPool _eggPool;
    public EggPool EggPool => _eggPool;
    public override void Awake()
    {
        base.Awake();
        _eggTiles = new Tiles[_height, _width];
        _eggTilesID = new int[_height, _width];
        SetUp();
        // TestBoard();
    }
    private void SetUp()
    {
        SpriteRenderer sr = _tilePrefab.GetComponentInChildren<SpriteRenderer>();
        Sprite sprite = sr.sprite;
        float worldWidth = sprite.rect.width / sprite.pixelsPerUnit;
        float worldHeight = sprite.rect.height / sprite.pixelsPerUnit;
        for (int row = 0; row < _height; row++)
        {
            for (int col = 0; col < _width; col++)
            {
                float x = (col - (_width - 1) / 2f) * worldWidth;
                float y = ((_height - 1 - row) - (_height - 1) / 2f) * worldHeight;
                _isGreen1 = ((row + col) % 2 == 0);
                Tiles tile = Instantiate(_tilePrefab, new Vector2(x, y) + (Vector2)transform.position, Quaternion.identity, this.transform);
                tile.name = "(  " + row + "   " + col + "   )";
                tile.SetTilePosition(row, col);
                _eggTilesID[row, col] = tile.GetCurrentEggID();
                _eggTiles[row, col] = tile;
                
            }
        }
    }
    public void OnTileClicked(Tiles tile)
    {
        if (_isProcessing)
        {
            return;
        }
        if (!tile.GetIsHighlighted())
        {
            SetEggAppear(tile);
        }
        else
        {
            SetEggDisappear(tile);
        }
    }
    private void SetEggAppear(Tiles tile)
    {
        AudioManager.Instance.PlaySFX(AudioManager.Instance.select);
        EventManager.onUpdateEgg?.Invoke(tile.GetEggData(tile.GetCurrentEggID()), tile.GetEggData((tile.GetCurrentEggID() + 1) % GameConfig.TOTAL_EGG_NUMBER));
        if (_eggSameID != null && _eggSameID.Count > 0)
        {
            foreach (var (row, col, dist) in _eggSameID)
            {
                _eggTiles[row, col].PopDown();
            }
            _eggSameID.Clear();
        }
        for (int row = 0; row < _height; row++)
        {
            for (int col = 0; col < _width; col++)
            {
                if (_eggTiles[row, col] == tile && _eggTilesID[row, col] != -1)
                {
                    List<(int, int, int)> sameEgg = BFS.LoangBFS(row, col, _height, _width, _eggTilesID);
                    foreach (var (rr, cc, dist) in sameEgg)
                    {
                        if (!_eggTiles[rr, cc].GetIsHighlighted())
                            _eggTiles[rr, cc].Popup();
                        else
                            _eggTiles[rr, cc].PopDown();
                    }
                    _eggSameID = sameEgg;
                    return;
                }
            }
        }
    }
    private void SetEggDisappear(Tiles tile)
    {
        if (_eggSameID.Count >= 2)
        {
            AudioManager.Instance.PlaySFX(AudioManager.Instance.addScore);
            EventManager.onUpdatePoint?.Invoke(_eggSameID.Count);
            _isProcessing = true;
            var cluster = BFS.LoangBFS(tile.GetTileRow(), tile.GetTileCol(), _height, _width, _eggTilesID);
            StartCoroutine(MergeRelayByDistance(cluster, tile));
            foreach (var (i, j, dist) in _eggSameID)
            {
                _eggTiles[i, j].PopDown();
            }

        }
        else
        {
            tile.PopDown();
        }
    }
    private IEnumerator MergeRelayByDistance(List<(int row, int col, int dist)> cluster, Tiles targetTile)
    {
        var distMap = new Dictionary<(int,int), int>();
        int maxDist = 0;
        foreach (var (r, c, d) in cluster)
        {
            distMap[(r, c)] = d;
            if (d > maxDist) maxDist = d;
        }

        float hopDuration = 0.1f;
        int activeTweens = 0;

        foreach (var (r, c, d) in cluster)
        {
            if (_eggTiles[r, c] == targetTile) continue;

            Egg egg = _eggTiles[r, c].GetEgg();
            if (egg == null) continue;

            activeTweens++;

            int cr = r, cc = c, cd = d;
            var seq = DOTween.Sequence();

            while (cd > 0)
            {
                var parent = BFS.FindParentTowardTarget(cr, cc, cd, distMap, _height, _width);
                Vector3 nextPos = _eggTiles[parent.x, parent.y].transform.position;
                seq.Append(egg.transform.DOMove(nextPos, hopDuration).SetEase(Ease.InOutQuad));

                cr = parent.x; cc = parent.y; cd--;
            }

            seq.OnComplete(() =>
            {
                _eggTiles[r, c].RemoveEgg();
                _eggTilesID[r, c] = -1;

                activeTweens--;
                if (activeTweens == 0)
                {
                    targetTile.SetNewLvlEgg();
                    int tr = targetTile.GetTileRow();
                    int tc = targetTile.GetTileCol();
                    _eggTilesID[tr, tc] = (_eggTilesID[tr, tc] + 1) % GameConfig.TOTAL_EGG_NUMBER;

                    BestEggLvl();

                    foreach (var (rr, cc, _) in cluster) _eggTiles[rr, cc].PopDown();
                    _eggSameID.Clear();

                    CollapseAndRefill();
                    _isProcessing = false;
                }
            });
        }
        if (activeTweens == 0)
        {
            targetTile.SetNewLvlEgg();
            int tr = targetTile.GetTileRow();
            int tc = targetTile.GetTileCol();
            _eggTilesID[tr, tc] = (_eggTilesID[tr, tc] + 1) % GameConfig.TOTAL_EGG_NUMBER;

            BestEggLvl();

            foreach (var (rr, cc, _) in cluster) _eggTiles[rr, cc].PopDown();
            _eggSameID.Clear();

            CollapseAndRefill();
            _isProcessing = false;
        }

        yield return null;
    }
    private void CollapseAndRefill()
    {
        for (int col = 0; col < _width; col++)
        {
            int writeRow = _height - 1;
            for (int row = _height - 1; row >= 0; row--)
            {
                if (_eggTilesID[row, col] != -1)
                {
                    if (row != writeRow)
                    {
                        Egg oldEgg = _eggTiles[row, col].GetEgg();
                        if (oldEgg != null)
                        {
                            oldEgg.transform.SetParent(_eggTiles[writeRow, col].transform, true);
                            oldEgg.MoveTo(_eggTiles[writeRow, col].transform.position, 0.25f);
                        }

                        _eggTilesID[writeRow, col] = _eggTilesID[row, col];
                        _eggTiles[writeRow, col].SetEggInstance(oldEgg, _eggTilesID[writeRow, col]);

                        _eggTilesID[row, col] = -1;
                        _eggTiles[row, col].ClearOnly();
                    }
                    writeRow--;
                }
            }
            for (int r = writeRow; r >= 0; r--)
            {
                int newEggID = UnityEngine.Random.Range(0, GameConfig.EGG_SPAWN_LEVEL);
                _eggTiles[r, col].SpawnEgg(newEggID);
                _eggTilesID[r, col] = _eggTiles[r, col].GetCurrentEggID();

                Egg newEgg = _eggTiles[r, col].GetEgg();
                if (newEgg != null)
                {
                    Vector3 startPos = _eggTiles[r, col].transform.position + Vector3.up * 0.5f;
                    newEgg.transform.position = startPos;
                    float delay = 0.05f * (_height - r);
                    newEgg.MoveTo(_eggTiles[r, col].transform.position, 0.25f + delay);
                }
            }
        }
    }
    private void BestEggLvl()
    {
        int best = -1;
        for (int row = 0; row < _height; row++)
        {
            for (int col = 0; col < _width; col++)
            {
                if (best < _eggTilesID[row, col])
                {
                    best = _eggTilesID[row, col];
                }
            }
        }
        if (best > _bestEggLvl)
        {
            _bestEggLvl = best;
            EventManager.onUpdateEggLvlUI?.Invoke(best + 1);
        }
    }
    public bool GetIsGreen1()
    {
        return _isGreen1;
    }
    private void TestBoard()
    {
        for (int row = 0; row < _height; row++)
        {
            string s = "";
            for (int col = 0; col < _width; col++)
            {
                s = s + _eggTilesID[row, col] + " ";
            }
            Debug.Log(s);
        }
    }
}
