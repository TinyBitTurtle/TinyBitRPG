using UnityEngine;
using System;

[CreateAssetMenu]
public class DungeonSettings : ScriptableObject
{
    [Serializable]
    public class TileSet
    {
        public GameObject[] floorTiles;
        public GameObject[] wallTiles;                            // An array of wall tile prefabs.
        public GameObject[] outerWallTiles;                       // An array of outer wall tile prefabs. 
    }

    [Header("Dungeon")]
    public int columns = 100;                                 // The number of columns on the board (how wide it will be).
    public int rows = 100;
    public IntRange numRooms = new IntRange(15, 20);         // The range of the number of rooms there can be.
    public IntRange roomWidth = new IntRange(3, 10);         // The range of widths rooms can have.
    public IntRange roomHeight = new IntRange(3, 10);        // The range of heights rooms can have.
    public IntRange corridorLength = new IntRange(6, 10);// The number of rows on the board (how tall it will be).
    public TileSet[] tileSets;
}
