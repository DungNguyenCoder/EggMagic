using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    private int width = 5;
    private int height = 5;
    [SerializeField] private BackgroundTile tilePrefab;
    private BackgroundTile[,] allTiles;

    void Start()
    {
        allTiles = new BackgroundTile[width, height];
        SetUp();
    }
    private void SetUp()
    {
        SpriteRenderer sr = tilePrefab.GetComponentInChildren<SpriteRenderer>();
        Sprite sprite = sr.sprite;
        float worldWidth  = sprite.rect.width  / sprite.pixelsPerUnit;
        float worldHeight = sprite.rect.height / sprite.pixelsPerUnit;
        for (int i = -width/2; i <= width/2; i++)
        {
            for (int j = -height/2; j <= height/2; j++)
            {
                Vector2 tempPosition = new Vector2(worldWidth * i, worldHeight * j);
                BackgroundTile tile = Instantiate(tilePrefab, tempPosition + (Vector2)transform.position, Quaternion.identity, this.transform);
                bool isGreen1 = (i + j) % 2 == 0;
                tile.SetTile(isGreen1);
                tile.name = "(i, j)";
            }
        }
    }
}
