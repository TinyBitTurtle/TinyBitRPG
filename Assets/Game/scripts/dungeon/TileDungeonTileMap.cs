using UnityEngine;
using UnityEngine.Tilemaps;

public class TileDungeonTileMap : TileBase
{
    public Sprite sprite;
    public Tile.ColliderType colliderType;
    public Color color;

    public override void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
    {
        tileData.sprite = sprite;
        tileData.colliderType = colliderType;
        tileData.color = color;
    }
}
