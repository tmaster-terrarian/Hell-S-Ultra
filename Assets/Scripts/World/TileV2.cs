using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileV2 : Tile
{
    public Sprite tileset;
    public string path;
    public string id;
    public string[] ignores;

    [HideInInspector] public Vector2 gridPos;

    Sprite[] sprites;
    SpriteRenderer spriteRenderer;

    public void CheckNeighbors()
    {
        
    }
}
