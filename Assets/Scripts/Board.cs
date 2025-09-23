using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Board : Singleton<Board>
{
    public int _width = 5;
    public int _height = 5;
    [SerializeField] private Tiles _tilePrefab;
    private Tiles[,] _eggTiles;
    private int[,] _eggTilesID;
    private List<(int, int)> _eggSameID;
    private bool _isGreen1;
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
                _eggTilesID[row, col] = tile.GetCurrentEggID();
                _eggTiles[row, col] = tile;
            }
        }
    }
    public List<(int, int)> LoangBFS(int sx, int sy)
    {
        List<(int, int)> result = new List<(int, int)>();
        bool[,] _visited;

        int[] dx = { 0, -1, 0, 1 };
        int[] dy = { -1, 0, 1, 0 };

        Queue<(int, int)> q = new Queue<(int, int)>();
        _visited = new bool[_height, _width];

        q.Enqueue((sx, sy));
        _visited[sx, sy] = true;

        while (q.Count > 0)
        {
            var (x, y) = q.Dequeue();
            result.Add((x, y));

            for (int k = 0; k < 4; k++)
            {
                int nx = x + dx[k];
                int ny = y + dy[k];

                if (nx >= 0 && nx < _height && ny >= 0 && ny < _width)
                {
                    if (!_visited[nx, ny] && _eggTilesID[nx, ny] == _eggTilesID[x, y])
                    {
                        _visited[nx, ny] = true;
                        q.Enqueue((nx, ny));
                    }
                }
            }
        }
        result.Reverse();
        return result;
    }

    public void OnTileClicked(Tiles tile)
    {
        if (!tile.GetIsHighlighted())
        {
            if (_eggSameID != null && _eggSameID.Count > 0)
            {
                foreach (var (row, col) in _eggSameID)
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
                        List<(int, int)> sameEgg = LoangBFS(row, col);
                        // Debug.Log($"({r},{c}), egg count = {list.Count}");
                        foreach (var (rr, cc) in sameEgg)
                        {
                            if (!_eggTiles[rr, cc].GetIsHighlighted())
                                _eggTiles[rr, cc].Popup();
                            else
                                _eggTiles[rr, cc].PopDown();
                            // Debug.Log($"({rr}, {cc})");
                        }
                        _eggSameID = sameEgg;
                        return;
                    }
                }
            }
        }
        else
        {
            if (_eggSameID.Count >= 2)
            {
                foreach (var (i, j) in _eggSameID)
                {
                    if (_eggTiles[i, j] != tile)
                    {
                        _eggTiles[i, j].RemoveEgg();
                        _eggTilesID[i, j] = -1;
                    }
                    else
                    {
                        _eggTiles[i, j].SetNewLvlEgg();
                        _eggTilesID[i, j] = (_eggTilesID[i, j] + 1) % 3;
                    }
                    _eggTiles[i, j].PopDown();
                }
                _eggSameID.Clear();

                CollapseAndRefill();
            }
            else
            {
                tile.PopDown();
            }
        }
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
                        Egg oldEgg = _eggTiles[row, col].GetEggInstance();
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
                int newIdx = UnityEngine.Random.Range(0, 3);
                _eggTiles[r, col].SpawnEgg(newIdx);
                _eggTilesID[r, col] = _eggTiles[r, col].GetCurrentEggID();

                Egg newEgg = _eggTiles[r, col].GetEggInstance();
                if (newEgg != null)
                {
                    Vector3 startPos = _eggTiles[r, col].transform.position + Vector3.up * 3f;
                    newEgg.transform.position = startPos;
                    float delay = 0.05f * (_height - r);
                    newEgg.MoveTo(_eggTiles[r, col].transform.position, 0.25f + delay);
                }
            }
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
