using UnityEngine;

namespace TinyBitTurtle
{
    public class Room
    {
        public Vector2Int pos;
        public Vector2Int dim;
        public CorridorDirection enteringCorridor;    // The direction of the corridor that is entering this room.

        // This is used for the first room.  It does not have a Corridor parameter since there are no corridors yet.
        public void SetupRoom(int level, DungeonSettings gameInit, int columns, int rows)
        {
            // Set a random width and height.
            dim.x = gameInit.roomWidth.Random;
            dim.y = gameInit.roomHeight.Random;

            // Set the x and y coordinates so the room is roughly in the middle of the board.
            pos.x = Mathf.RoundToInt(columns / 2f - dim.x / 2f);
            pos.y = Mathf.RoundToInt(rows / 2f - dim.y / 2f);
        }


        // This is an overload of the SetupRoom function and has a corridor parameter that represents the corridor entering the room.
        public void SetupRoom(int level, DungeonSettings gameInit, int columns, int rows, Corridor corridor)
        {
            // Set the entering corridor direction.
            enteringCorridor = corridor.direction;

            // Set random values for width and height.
            dim.x = gameInit.roomWidth.Random;
            dim.y = gameInit.roomHeight.Random;

            switch (corridor.direction)
            {
                // If the corridor entering this room is going north...
                case CorridorDirection.North:
                    // ... the height of the room mustn't go beyond the board so it must be clamped based
                    // on the height of the board (rows) and the end of corridor that leads to the room.
                    dim.y = Mathf.Clamp(dim.y, 1, rows - corridor.EndPositionY);

                    // The y coordinate of the room must be at the end of the corridor (since the corridor leads to the bottom of the room).
                    pos.y = corridor.EndPositionY;

                    // The x coordinate can be random but the left-most possibility is no further than the width
                    // and the right-most possibility is that the end of the corridor is at the position of the room.
                    pos.x = Random.Range(corridor.EndPositionX - dim.x + 1, corridor.EndPositionX);

                    // This must be clamped to ensure that the room doesn't go off the board.
                    pos.x = Mathf.Clamp(pos.x, 0, columns - dim.x);
                    break;
                case CorridorDirection.East:
                    dim.x = Mathf.Clamp(dim.x, 1, columns - corridor.EndPositionX);
                    pos.x = corridor.EndPositionX;

                    pos.y = Random.Range(corridor.EndPositionY - dim.y + 1, corridor.EndPositionY);
                    pos.y = Mathf.Clamp(pos.y, 0, rows - dim.y);
                    break;
                case CorridorDirection.South:
                    dim.y = Mathf.Clamp(dim.y, 1, corridor.EndPositionY);
                    pos.y = corridor.EndPositionY - dim.y + 1;

                    pos.x = Random.Range(corridor.EndPositionX - dim.x + 1, corridor.EndPositionX);
                    pos.x = Mathf.Clamp(pos.x, 0, columns - dim.x);
                    break;
                case CorridorDirection.West:
                    dim.x = Mathf.Clamp(dim.x, 1, corridor.EndPositionX);
                    pos.x = corridor.EndPositionX - dim.x + 1;

                    pos.y = Random.Range(corridor.EndPositionY - dim.y + 1, corridor.EndPositionY);
                    pos.y = Mathf.Clamp(pos.y, 0, rows - dim.y);
                    break;
            }
        }
    }
}