using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : Singleton<Board>
{
    private int width = 5;
    private int height = 5;
    [SerializeField] private Tiles tilePrefab;
    private Tiles[,] allTiles;
    private int[,] eggIdTiles;
    void Start()
    {
        allTiles = new Tiles[height, width];
        eggIdTiles = new int[height, width];
        SetUp();
        for (int row = 0; row < height; row++)
        {
            string s = "";
            for (int col = 0; col < width; col++)
            {
                s = s + eggIdTiles[row, col] + " ";
            }
            Debug.Log(s);
        }
    }
    private void SetUp()
    {
        SpriteRenderer sr = tilePrefab.GetComponentInChildren<SpriteRenderer>();
        Sprite sprite = sr.sprite;
        float worldWidth = sprite.rect.width / sprite.pixelsPerUnit;
        float worldHeight = sprite.rect.height / sprite.pixelsPerUnit;
        for (int row = 0; row < height; row++)
        {
            for (int col = 0; col < width; col++)
            {
                float x = (col - (width-1)/2f) * worldWidth;
                float y = ((height - 1 - row) - (height-1)/2f) * worldHeight;
                Tiles tile = Instantiate(tilePrefab, new Vector2(x, y) + (Vector2)transform.position, Quaternion.identity, this.transform);
                bool isGreen1 = (row + col) % 2 == 0;
                tile.SetTile(isGreen1);
                tile.name = "(  " + row + "   " + col + "   )";
                eggIdTiles[row, col] = tile.GetCurrentEggID();
            }
        }
    }
}
