using UnityEngine;
namespace TinyBitTurtle
{
    public abstract class DungeonView : MonoBehaviour
    {
        public virtual void InstantiateTiles(DungeonModel.TileType[][] tiles, GameObject boardHolder, DungeonSettings gameInit) { }
        public virtual void InstantiateOuterWalls(DungeonModel.TileType[][] tiles, GameObject boardHolder, DungeonSettings gameInit) { }
        protected virtual void InstantiateVerticalOuterWall(float xCoord, float startingY, float endingY, GameObject boardHolder, DungeonSettings gameInit) { }
        protected virtual void InstantiateHorizontalOuterWall(float startingX, float endingX, float yCoord, GameObject boardHolder, DungeonSettings gameInit) { }
        protected virtual void InstantiateFromArray(GameObject[] prefabs, float xCoord, float yCoord, GameObject boardHolder, DungeonSettings gameInit) { }
    }
}
