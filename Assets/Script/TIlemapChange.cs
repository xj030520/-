using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TIlemapChange : MonoBehaviour
{

    public Tilemap Tilemap;
    public TileBase[] baseTile;
    public TileBase[] ClickTile;
    Tile[] attTiles;
    TileData tileData;

    Vector3Int preCellPosition;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePosition = Input.mousePosition;
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        Vector3Int cellPosition = Tilemap.WorldToCell(worldPosition);
        TileBase tb = Tilemap.GetTile(preCellPosition);
        for (int i = 0; i < baseTile.Length; i++)
        {
            if (ClickTile[i] == tb)
            {
                Tilemap.SetTile(preCellPosition, baseTile[i]);
                break;
            }
        }

        tb = Tilemap.GetTile(cellPosition);
        if (tb == null)
        {
            return;
        }

        else
        {
            for (int i = 0; i < baseTile.Length; i++)
            {
                if (baseTile[i] == tb)
                {
                    Tilemap.SetTile(cellPosition, ClickTile[i]);
                    preCellPosition = cellPosition;
                    break;
                }
            }
        }
    }
}
