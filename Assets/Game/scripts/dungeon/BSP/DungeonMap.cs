using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonMap : MonoBehaviour
{

    // Variety is the spice of life; mix things up a bit.
//    RoomGeneratorDelegate roomGenerator;

//            // Choose square or round rooms (or mix).  In future, have more complex room generators.
//            switch (Rand.Next(3))
//            {
//                case 0:
//                    roomGenerator = new RoomGeneratorDelegate(SquareRoomGenerator);
//                    break;
//                case 1:
//                    roomGenerator = new RoomGeneratorDelegate(RoundRoomGenerator);
//                    break;
//                default:
//                    roomGenerator = new RoomGeneratorDelegate(RandomShapeRoomGenerator);
//                    break;
//            }

//// Create a room in the area specified by the regionNode.
//public void RoundRoomGenerator(DungeonBSPNode dungeonRegion)
//{
//    public void RandomShapeRoomGenerator(DungeonBSPNode dungeonRegion)
//    {
//        if (Rand.Next(2) == 0)
//            RoundRoomGenerator(dungeonRegion);
//        else
//            SquareRoomGenerator(dungeonRegion);
//    }

//    int MinIdealRoomSize = 7;

//    // Convert from absolute normalized coordinates (0.0-1.0) to Map coordinates (0-(MapWidth-1), 0-(MapHeight-1))
//    int xRegionStart = (int)Math.Ceiling((dungeonRegion.RegionEdges.Left * (TargetMap.MapWidth - 1)));
//    int yRegionStart = (int)Math.Ceiling((dungeonRegion.RegionEdges.Top * (TargetMap.MapHeight - 1)));
//    int xRegionEnd = (int)((dungeonRegion.RegionEdges.Right * (TargetMap.MapWidth - 1)));
//    int yRegionEnd = (int)((dungeonRegion.RegionEdges.Bottom * (TargetMap.MapHeight - 1)));
//    int regionWidth = xRegionEnd - xRegionStart;
//    int regionHeight = yRegionEnd - yRegionStart;

//    int roomWidth = RandomValueBetween(Math.Min(MinIdealRoomSize, regionWidth), regionWidth);
//    int roomHeight = RandomValueBetween(Math.Min(MinIdealRoomSize, regionHeight), regionHeight);

//    int xRoomStart = xRegionStart + Rand.Next(regionWidth - roomWidth);
//    int yRoomStart = yRegionStart + Rand.Next(regionHeight - roomHeight);

//    // Store the room coordinates in the Dungeon Region Node (we'll want them again later for corridor creation)
//    dungeonRegion.BoundingRect = new Rectangle(xRoomStart, yRoomStart, roomWidth, roomHeight);
//    dungeonRegion.RoomBuilt = true;

//    // "Paint" the room into the Map
//    TargetMap.PaintCellEllipse(xRoomStart, yRoomStart, xRoomStart + roomWidth, yRoomStart + roomHeight, new Cell_Wall(), true);
//    TargetMap.PaintCellEllipse(xRoomStart + 1, yRoomStart + 1, xRoomStart + roomWidth - 1, yRoomStart + roomHeight - 1, new Cell_Floor(), true);
//}
}
