using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ArmyPlace : MonoBehaviour
{
    public Transform target;
    public Tilemap TilemapArmy;
    public Tilemap TilemapWater;
    public Tilemap TilemapDetail;

    public TileBase tilewater;
    public TileBase tilemapdetail;
    public TileBase[] Water;
    public TileBase[] mapdetail;
    private Vector2 first;
    private Vector2 second;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            first = Input.mousePosition;
        }
        if (Input.GetMouseButtonUp(0))
        {
            second = Input.mousePosition;
            if ((first.x - second.x) > 0.1
                || (first.x - second.x) < -0.1)
            {;
                return;
            }
            Vector3 mousePosition = Input.mousePosition;
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
            Vector3Int CellPosition = TilemapArmy.WorldToCell(worldPosition);
            Vector3 PlacePosition = CellPosition;
            tilewater = TilemapWater.GetTile(CellPosition);
            tilemapdetail = TilemapDetail.GetTile(CellPosition);
            if(!IsPlacable(tilewater,tilemapdetail))
            {
                return;
            }
            PlacePosition.x += 0.5f;
            PlacePosition.z = -0.5f;
            target.position = PlacePosition;
            Instantiate(target, target.position, target.rotation);
        }
    }

    bool IsPlacable(TileBase ClickTileWater, TileBase ClickTileDetail)
    {
        bool isPlacable = true;
        for(int i = 0;i<Water.Length;i++)
        {
            if (ClickTileWater == Water[i])
            {
                isPlacable = false;
                break;
            }
        }
        for(int i = 0;i<mapdetail.Length;i++)
        {
            if (ClickTileDetail == mapdetail[i])
            {

                isPlacable = false;
                break;
            }
        }

        return isPlacable;
    }
}
