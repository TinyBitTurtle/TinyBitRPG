using UnityEngine;

namespace TinyBitTurtle
{
    public class DungeonViewDefault : DungeonView
    {
        public override void InstantiateTiles(DungeonModel.TileType[][] tiles, GameObject boardHolder, DungeonSettings gameInit)
        {
            // Go through all the tiles in the jagged array...
            for (int i = 0; i < tiles.Length; i++)
            {
                for (int j = 0; j < tiles[i].Length; j++)
                {
                    // If the tile type is Wall...
                    if (tiles[i][j] == DungeonModel.TileType.Wall)
                    {
                        // ... instantiate a wall over the top.
                        InstantiateFromArray(gameInit.tileSets[0].wallTiles, i, j, boardHolder, gameInit);
                    }
                    else
                    {
                        // ... and instantiate a floor tile for it.
                        InstantiateFromArray(gameInit.tileSets[0].floorTiles, i, j, boardHolder, gameInit);
                    }
                }
            }
        }

        public override void InstantiateOuterWalls(DungeonModel.TileType[][] tiles, GameObject boardHolder, DungeonSettings gameInit)
        {
            // The outer walls are one unit left, right, up and down from the board.
            float leftEdgeX = -1f;
            float rightEdgeX = gameInit.columns + 0f;
            float bottomEdgeY = -1f;
            float topEdgeY = gameInit.rows + 0f;

            // Instantiate both vertical walls (one on each side).
            InstantiateVerticalOuterWall(leftEdgeX, bottomEdgeY, topEdgeY, boardHolder, gameInit);
            InstantiateVerticalOuterWall(rightEdgeX, bottomEdgeY, topEdgeY, boardHolder, gameInit);

            // Instantiate both horizontal walls, these are one in left and right from the outer walls.
            InstantiateHorizontalOuterWall(leftEdgeX + 1f, rightEdgeX - 1f, bottomEdgeY, boardHolder, gameInit);
            InstantiateHorizontalOuterWall(leftEdgeX + 1f, rightEdgeX - 1f, topEdgeY, boardHolder, gameInit);
        }

        protected override void InstantiateVerticalOuterWall(float xCoord, float startingY, float endingY, GameObject boardHolder, DungeonSettings gameInit)
        {
            // Start the loop at the starting value for Y.
            float currentY = startingY;

            // While the value for Y is less than the end value...
            while (currentY <= endingY)
            {
                // ... instantiate an outer wall tile at the x coordinate and the current y coordinate.
                InstantiateFromArray(gameInit.tileSets[0].outerWallTiles, xCoord, currentY, boardHolder, gameInit);

                currentY++;
            }
        }

        protected override void InstantiateHorizontalOuterWall(float startingX, float endingX, float yCoord, GameObject boardHolder, DungeonSettings gameInit)
        {
            // Start the loop at the starting value for X.
            float currentX = startingX;

            // While the value for X is less than the end value...
            while (currentX <= endingX)
            {
                // ... instantiate an outer wall tile at the y coordinate and the current x coordinate.
                InstantiateFromArray(gameInit.tileSets[0].outerWallTiles, currentX, yCoord, boardHolder, gameInit);

                currentX++;
            }
        }

        protected override void InstantiateFromArray(GameObject[] prefabs, float xCoord, float yCoord, GameObject boardHolder, DungeonSettings gameInit)
        {
            // Create a random index for the array.
            int randomIndex = Random.Range(0, prefabs.Length);

            // The position to be instantiated at is based on the coordinates.
            Vector3 position = new Vector3(xCoord, yCoord, 0f);

            // Create an instance of the prefab from the random index of the array.
            GameObject tileInstance = Instantiate(prefabs[randomIndex], position, Quaternion.identity) as GameObject;

            // Set the tile's parent to the board holder.
            tileInstance.transform.parent = boardHolder.transform;
        }
    }
}