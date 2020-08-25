using UnityEngine;
using UnityEngine.Tilemaps;
namespace TinyBitTurtle
{
    public class DungeonViewTileMap : DungeonView
    {
        public Material dungeonMaterial;
        public ColorGradient colorGradient;
        public Color floorColor;
        public Color wallColor;

        public override void InstantiateTiles(DungeonModel.TileType[][] tiles, GameObject boardHolder, DungeonSettings gameInit)
        {
            // create the Grid object
            GameObject gridObject = new GameObject("Grid");
            gridObject.AddComponent<Grid>();

            // create the tileMap object and attach required scripts
            GameObject tilemapObject = new GameObject("dungeonTilemap");
            tilemapObject.AddComponent<Tilemap>();
            tilemapObject.AddComponent<TilemapRenderer>();
            tilemapObject.AddComponent<TilemapCollider2D>();
            tilemapObject.transform.parent = gridObject.transform;

            // get the tile map component
            Tilemap tilemap = tilemapObject.GetComponent<Tilemap>();
            TilemapRenderer TilemapRenderer = tilemapObject.GetComponent<TilemapRenderer>();
            TilemapRenderer.material = dungeonMaterial;
            tilemapObject.layer = 10;

            int tileSetIdx = Random.Range(0, gameInit.tileSets.Length - 1);

            //colorGradient.Init();

            // Go through all the tiles in the jagged array...
            for (int i = 0; i < tiles.Length; i++)
            {
                for (int j = 0; j < tiles[i].Length; j++)
                {
                    TileDungeonTileMap tileDungeonTileMap = ScriptableObject.CreateInstance<TileDungeonTileMap>();

                    // If the tile type is Wall...
                    if (tiles[i][j] == DungeonModel.TileType.Wall)
                    {
                        // ... instantiate a wall over the top.
                        int randomIndex = Random.Range(0, gameInit.tileSets[tileSetIdx].wallTiles.Length);
                        tileDungeonTileMap.sprite = gameInit.tileSets[tileSetIdx].wallTiles[randomIndex].GetComponent<SpriteRenderer>().sprite;
                        tileDungeonTileMap.colliderType = Tile.ColliderType.Sprite;
                        tileDungeonTileMap.color = wallColor;
                    }
                    else
                    {
                        int randomIndex = Random.Range(0, gameInit.tileSets[tileSetIdx].floorTiles.Length);
                        // ... and instantiate a floor tile for it.
                        tileDungeonTileMap.sprite = gameInit.tileSets[tileSetIdx].floorTiles[randomIndex].GetComponent<SpriteRenderer>().sprite;
                        tileDungeonTileMap.colliderType = Tile.ColliderType.None;
                        tileDungeonTileMap.color = Color.white; //floorColor;
                    }

                    tilemap.SetTile(new Vector3Int(i, j, 0), tileDungeonTileMap);
                }
            }

            //create the minimap
            tilemapObject = new GameObject("MinimapTilemap");
            tilemapObject.AddComponent<Tilemap>();
            tilemapObject.AddComponent<TilemapRenderer>();
            tilemapObject.transform.parent = gridObject.transform;
            tilemapObject.layer = 9;
            //get the tile map component
            tilemap = tilemapObject.GetComponent<Tilemap>();

            for (int i = 0; i < tiles.Length; i++)
            {
                for (int j = 0; j < tiles[i].Length; j++)
                {
                    // If the tile type is Wall...
                    if (tiles[i][j] == DungeonModel.TileType.Floor)
                    {
                        TileDungeonTileMap tileDungeonTileMap = ScriptableObject.CreateInstance<TileDungeonTileMap>();

                        // ... and instantiate a floor tile for it.
                        Transform temp = gameInit.tileSets[tileSetIdx].floorTiles[0].transform.GetChild(0);
                        tileDungeonTileMap.sprite = temp.gameObject.GetComponent<SpriteRenderer>().sprite;
                        tileDungeonTileMap.colliderType = Tile.ColliderType.None;
                        tileDungeonTileMap.color = Color.yellow;

                        tilemap.SetTile(new Vector3Int(i, j, 0), tileDungeonTileMap);
                    }
                }
            }

        }

        //public override void InstantiateOuterWalls(DungeonModel.TileType[][] tiles, GameObject boardHolder, GameSettings gameInit) { }
        //protected override void InstantiateVerticalOuterWall(float xCoord, float startingY, float endingY, GameObject boardHolder, GameSettings gameInit) { }
        //protected override void InstantiateHorizontalOuterWall(float startingX, float endingX, float yCoord, GameObject boardHolder, GameSettings gameInit) { }

        protected override void InstantiateFromArray(GameObject[] prefabs, float xCoord, float yCoord, GameObject boardHolder, DungeonSettings gameInit)
        {
            // Create a random index for the array.
            //int randomIndex = Random.Range(0, prefabs.Length);

            //// The position to be instantiated at is based on the coordinates.
            //Vector3 position = new Vector3(xCoord, yCoord, 0f);

            //// Create an instance of the prefab from the random index of the array.
            //GameObject tileInstance = Instantiate(prefabs[randomIndex], position, Quaternion.identity) as GameObject;

            //// Set the tile's parent to the board holder.
            //tileInstance.transform.parent = boardHolder.transform;
        }
    }
}