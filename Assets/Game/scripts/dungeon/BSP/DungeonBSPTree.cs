using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonBSPTree : MonoBehaviour
{
//    public void GenerateLevel()
//    {
//        Cells = new Cell[MapWidth, MapHeight];

//        // Fill with Granite
//        PaintCellRectangle(0, 0, MapWidth - 1, MapHeight - 1, new Cell_Granite(), true);

//        // Create BSP tree to partition floor randomly (but w/o overlaps)
//        partitionedTree = PartitionDungeon();

//        // now that we have partitioned the dungeon into non-overlapping regions, create rooms in each "leaf" region
//        // We do this by telling the partitioned dungeon tree to find all "leaf" nodes (e.g. those at the bottom of
//        // the tree which have not been partitioned into smaller regions, and calling our "AddRoom" function with
//        // each leaf node's coordinates.
//        partitionedTree.EnumerateLeafNodes(new AddRoomDelegate(AddRoom));
//    }

//    private DungeonBSPNode PartitionDungeon()
//    {
//        // Initialize a few variables.  These'll eventually be removed
//        DungeonBSPNode.Map = this;

//        // I eventually want to share the random # generator across all objects, so that all that's
//        // necessary to completely recreate a particular run is the initial Seed &amp; the list of user inputs/
//        DungeonBSPNode.rand = rand;

//        // Create the root node;  it covers the entire space (0.0,0.0) - (1.0,1.0)
//        DungeonBSPNode rootNode = new DungeonBSPNode(0.0f, 0.0f, 1.0f, 1.0f);

//        // Do the actual partitioning.
//        rootNode.Partition();

//        return rootNode;
//    }

//    // Create a room in the area specified by the regionNode.
//    public void SquareRoomGenerator(DungeonBSPNode dungeonRegion)
//    {
//        int MinIdealRoomSize = 4;

//        // Convert from absolute normalized coordinates (0.0-1.0) to Map coordinates (0-(MapWidth-1), 0-(MapHeight-1))
//        int xRegionStart = (int)Math.Ceiling((dungeonRegion.LeftEdge * (MapWidth - 1)));
//        int yRegionStart = (int)Math.Ceiling((dungeonRegion.TopEdge * (MapHeight - 1)));
//        int xRegionEnd = (int)((dungeonRegion.RightEdge * (MapWidth - 1)));
//        int yRegionEnd = (int)((dungeonRegion.BottomEdge * (MapHeight - 1)));
//        int regionWidth = xRegionEnd - xRegionStart;
//        int regionHeight = yRegionEnd - yRegionStart;

//        int roomWidth = RandomValueBetween(Math.Min(MinIdealRoomSize, regionWidth), regionWidth);
//        int roomHeight = RandomValueBetween(Math.Min(MinIdealRoomSize, regionHeight), regionHeight);

//        int xRoomStart = xRegionStart + rand.Next(regionWidth - roomWidth);
//        int yRoomStart = yRegionStart + rand.Next(regionHeight - roomHeight);

//        // "Paint" the room into the Map
//        PaintCellRectangle(xRoomStart, yRoomStart, xRoomStart + roomWidth, yRoomStart + roomHeight, new Cell_Wall(), true);
//        PaintCellRectangle(xRoomStart + 1, yRoomStart + 1, xRoomStart + roomWidth - 1, yRoomStart + roomHeight - 1, new Cell_Floor(), true);
//    }

//    public void PaintCellRectangle(int xStart, int yStart, int xEnd, int yEnd, Cell cell, bool forceDraw)
//    {
//        for (int y = yStart; y <= yEnd; y++)
//            for (int x = xStart; x <= xEnd; x++)
//            {
//                if (forceDraw || Cells[x, y] == null || Cells[x, y].GetType() == typeof(Cell_Granite))
//                    Cells[x, y] = cell;
//            }
//    }

//    public void BottomsUpByLevelEnumerate(RoomGeneratorDelegate addRoomCallback, CorridorGeneratorDelegate addCorridorCallback)
//    {
//        Stack stack1 = new Stack();
//        Stack stack2 = new Stack();
//        stack1.Push(this);
//        while (stack1.Count & gt; 0)
//            {
//            DungeonBSPNode currentNode = (DungeonBSPNode)stack1.Pop();
//            stack2.Push(currentNode);
//            if (currentNode.Left != null)
//                stack1.Push(currentNode.Left);
//            if (currentNode.Right != null)
//                stack1.Push(currentNode.Right);
//        }

//        while (stack2.Count & gt; 0)
//            {
//            DungeonBSPNode currentNode = (DungeonBSPNode)stack2.Pop();
//            if (currentNode.Left == null & amp; &amp; currentNode.Right == null)
//                {
//                // Leaf node - create a room
//                if (!currentNode.RoomBuilt)
//                    addRoomCallback(currentNode);
//            }
//                else
//                {
//                // non-leaf node; create corridor
//                if (currentNode.Left.RoomBuilt || currentNode.Right.RoomBuilt)
//                    addCorridorCallback(currentNode);
//            }

//        }


//        public void DefaultCorridorGenerator(DungeonBSPNode dungeonNode)
//        {
//            DungeonBSPNode leftChild = dungeonNode.Left;
//            DungeonBSPNode rightChild = dungeonNode.Right;

//            if (leftChild == null || !leftChild.RoomBuilt)
//                dungeonNode.BoundingRect = rightChild.BoundingRect;
//            else if (rightChild == null || !rightChild.RoomBuilt)
//                dungeonNode.BoundingRect = leftChild.BoundingRect;
//            else
//            {
//                // Draw a corridor between our child nodes.  We have been keeping track of their bounding rectangles
//                // as we've worked our way up the tree, so we can use that ensure we connect corridors to rooms
//                if (dungeonNode.SplitOrientation == DungeonBSPNode.Orientation.Horizontal)
//                {
//                    // child nodes were split horizontally, so draw a horizontal corridor between them.
//                    // If the nodes' regions overlap vertically by at least 3 (leaving room for walls), then we can just dig a
//                    // single horizontal tunnel to connect them; otherwise, we need to dig an 'L' or 'Z' shapped corridor to connect them.
//                    int minOverlappingY = Math.Max(leftChild.BoundingRect.Top, rightChild.BoundingRect.Top);
//                    int maxOverlappingY = Math.Min(leftChild.BoundingRect.Bottom, rightChild.BoundingRect.Bottom);
//                    if (maxOverlappingY - minOverlappingY & gt;= 3)
//                    {
//            // The regions overlap; we can dig a single horizontal corridor to connect them
//            // Determine the range of Y axis values that we can dig at in order to connect the regions
//            int corridorY = minOverlappingY + 1 + rand.Next(maxOverlappingY - minOverlappingY - 2);

//            // Start at the border between the two regions at Y=corridorY and dig towards the outside
//            // edge of each region; we are gauranteed to eventually hit something since the regions' bounding
//            // rects overlapped.
//            DigCorridor(leftChild.BoundingRect.Right, corridorY, Direction.Left);
//            DigCorridor(leftChild.BoundingRect.Right + 1, corridorY, Direction.Right);
//        }
//                    else
//                    {
//            // They don't overlap enough; dig an 'L' or 'Z' shaped corridor to connect them.
//            // For now, just support 'L' corridors
//            int tunnelMeetX, tunnelMeetY;

//            // For variety, the 'L' corridor can come out of the top of the bottom child or the side of the bottom child.
//            bool corridorExitsLeftChildsTopEdge = (rand.Next(2) == 0);

//            // Note that some of the math below (in particular the Mins and Maxs) are because the regions *can* be slightly overlapping in
//            // the appropriate dimension; we don't draw a straight line if they overlap by less than 3 (to minimize the number of odd corridors
//            // that attach to the outside corner of a room)
//            if (leftChild.BoundingRect.Top & gt; rightChild.BoundingRect.Top)
//                        {
//                // Left child region's bounding rect is below the Right child region's bound rect.
//                if (corridorExitsLeftChildsTopEdge)
//                {
//                    //        _____
//                    //   X____|   |
//                    //    |   | R |
//                    //    |   |___|
//                    //  __|__
//                    //  |   |
//                    //  | L |
//                    //  |___|
//                    tunnelMeetX = RandomValueBetween(leftChild.BoundingRect.Left + 1, leftChild.BoundingRect.Right);
//                    tunnelMeetY = RandomValueBetween(rightChild.BoundingRect.Top + 1, Math.Min(rightChild.BoundingRect.Bottom - 1, leftChild.BoundingRect.Top));
//                    DigDownRightCorridor(tunnelMeetX, tunnelMeetY);
//                }
//                else
//                {
//                    //        _____
//                    //        |   |
//                    //        | R |
//                    //        |___|
//                    //  _____   |
//                    //  |   |   |
//                    //  | L |___|X
//                    //  |___|
//                    tunnelMeetX = RandomValueBetween(rightChild.BoundingRect.Left + 1, rightChild.BoundingRect.Right);
//                    tunnelMeetY = RandomValueBetween(Math.Max(leftChild.BoundingRect.Top + 1, rightChild.BoundingRect.Bottom + 1), leftChild.BoundingRect.Bottom);
//                    DigUpLeftCorridor(tunnelMeetX, tunnelMeetY);
//                }
//            }
//                        else
//                        {
//                // Left child region's bounding rect is above the Right child region's bound rect.
//                if (corridorExitsLeftChildsTopEdge)
//                {
//                    //    _____
//                    //    |   |____X
//                    //    | L |   |
//                    //    |___|   |
//                    //          __|__
//                    //          |   |
//                    //          | R |
//                    //          |___|
//                    tunnelMeetX = RandomValueBetween(rightChild.BoundingRect.Left + 1, rightChild.BoundingRect.Right);
//                    tunnelMeetY = RandomValueBetween(leftChild.BoundingRect.Top + 1, Math.Min(leftChild.BoundingRect.Bottom - 1, rightChild.BoundingRect.Top));
//                    DigDownLeftLCorridor(tunnelMeetX, tunnelMeetY);
//                }
//                else
//                {
//                    //    _____
//                    //    |   |
//                    //    | L |
//                    //    |___|
//                    //      |    _____
//                    //      |    |   |
//                    //     X|____| R |
//                    //           |___|
//                    tunnelMeetX = RandomValueBetween(leftChild.BoundingRect.Left + 1, leftChild.BoundingRect.Right);
//                    tunnelMeetY = RandomValueBetween(Math.Min(leftChild.BoundingRect.Bottom, rightChild.BoundingRect.Top + 1), rightChild.BoundingRect.Bottom);
//                    DigUpRightLCorridor(tunnelMeetX, tunnelMeetY);
//                }
//            }
//        }

//        // TBD: Need to set bounding rect for Special Rooms
//    }
//                else // Vertical split
//                {
//                    // child nodes were split vertically, so draw a vertical corridor between them.
//                    // If the nodes' regions overlap horizontally by at least 3 (leaving room for walls), then we can just dig a
//                    // single vertical tunnel to connect them; otherwise, we need to dig an 'L' or 'Z' shapped corridor to connect them.
//                    int minOverlappingX = Math.Max(leftChild.BoundingRect.Left, rightChild.BoundingRect.Left);
//    int maxOverlappingX = Math.Min(leftChild.BoundingRect.Right, rightChild.BoundingRect.Right);
//                    if (maxOverlappingX - minOverlappingX &gt;= 3)
//                    {
//                        // The regions overlap; we can dig a single vertical corridor to connect them
//                        // Determine the range of X axis values that we can dig at in order to connect the regions
//                        int corridorX = minOverlappingX + 1 + rand.Next(maxOverlappingX - minOverlappingX - 2);

//                        // Start at the border between the two regions at X=corridorX and dig towards the outside
//                        // edge of each region; we are gauranteed to eventually hit something since the regions' bounding
//                        // rects overlapped.
//                        DigCorridor(corridorX, leftChild.BoundingRect.Bottom, Direction.Up);
//                        DigCorridor(corridorX, leftChild.BoundingRect.Bottom + 1, Direction.Down);
//}
//                    else
//                    {
//                        // They don't overlap enough; dig an 'L' or 'Z' shaped corridor to connect them.
//                        // For now, just support 'L' corridors
//                        int tunnelMeetX, tunnelMeetY;

//// The 'L' corridor can come out of the top of the bottom child or the side of the bottom child.
//bool corridorExitsRightChildsTopEdge = (rand.Next(2) == 0);

//                        if (leftChild.BoundingRect.Left &gt; rightChild.BoundingRect.Left)
//                        {
//                            // Left (upper) child region's bounding rect is to the right of the Right (lower) child region's bound rect.
//                            if (corridorExitsRightChildsTopEdge)
//                            {
//                                //        _____
//                                //   X____|   |
//                                //    |   | L |
//                                //    |   |___|
//                                //  __|__
//                                //  |   |
//                                //  | R |
//                                //  |___|
//                                tunnelMeetX = RandomValueBetween(rightChild.BoundingRect.Left + 1, Math.Min(rightChild.BoundingRect.Right - 1, leftChild.BoundingRect.Left));
//                                tunnelMeetY = RandomValueBetween(leftChild.BoundingRect.Top + 1, leftChild.BoundingRect.Bottom);
//                                DigDownRightCorridor(tunnelMeetX, tunnelMeetY);
//                            }
//                            else
//                            {
//                                //        _____
//                                //        |   |
//                                //        | L |
//                                //        |___|
//                                //  _____   |
//                                //  |   |   |
//                                //  | R |___|X
//                                //  |___|
//                                tunnelMeetX = RandomValueBetween(Math.Max(leftChild.BoundingRect.Left + 1, rightChild.BoundingRect.Right + 1), leftChild.BoundingRect.Right);
//                                tunnelMeetY = RandomValueBetween(rightChild.BoundingRect.Top + 1, rightChild.BoundingRect.Bottom);
//                                DigUpLeftCorridor(tunnelMeetX, tunnelMeetY);
//                            }
//                        }
//                        else
//                        {
//                            // Left (upper) child region's bounding rect is to the left of the Right (lower) child region's bound rect.
//                            if (corridorExitsRightChildsTopEdge)
//                            {
//                                //    _____
//                                //    |   |____X
//                                //    | L |   |
//                                //    |___|   |
//                                //          __|__
//                                //          |   |
//                                //          | R |
//                                //          |___|
//                                tunnelMeetX = RandomValueBetween(Math.Max(leftChild.BoundingRect.Right, rightChild.BoundingRect.Left + 1), rightChild.BoundingRect.Right);
//                                tunnelMeetY = RandomValueBetween(leftChild.BoundingRect.Top + 1, leftChild.BoundingRect.Bottom);
//                                DigDownLeftLCorridor(tunnelMeetX, tunnelMeetY);
//                            }
//                            else
//                            {
//                                //    _____
//                                //    |   |
//                                //    | L |
//                                //    |___|
//                                //      |    _____
//                                //      |    |   |
//                                //     X|____| R |
//                                //           |___|
//                                tunnelMeetX = RandomValueBetween(leftChild.BoundingRect.Left, Math.Min(rightChild.BoundingRect.Left, leftChild.BoundingRect.Right - 1));
//                                tunnelMeetY = RandomValueBetween(rightChild.BoundingRect.Top + 1, rightChild.BoundingRect.Bottom);
//                                DigUpRightLCorridor(tunnelMeetX, tunnelMeetY);
//                            }
//                        }
//                    }
//                }

//                // Determine our bounding rectangle (as the union of our child nodes' rectangles).
//                dungeonNode.BoundingRect = Rectangle.Union(leftChild.BoundingRect, rightChild.BoundingRect);
//            }
//            // TBD: Not quite right - "RoomOrCorridorBuilt" more accurate
//            dungeonNode.RoomBuilt = true;
//        }

//        private void DigDownRightCorridor(int tunnelMeetX, int tunnelMeetY)
//{
//    // Dig down and right from the specified meeting point until we meet something (note: caller is tasked with gauranteeing that
//    // there is something there to meet!)

//    DigCorridor(tunnelMeetX, tunnelMeetY + 1, Direction.Down);
//    DigCorridor(tunnelMeetX + 1, tunnelMeetY, Direction.Right);

//    // Draw the corner piece
//    Cells[tunnelMeetX, tunnelMeetY] = new Cell_Floor();
//    Cells[tunnelMeetX - 1, tunnelMeetY] = new Cell_Wall();
//    Cells[tunnelMeetX, tunnelMeetY - 1] = new Cell_Wall();
//    Cells[tunnelMeetX - 1, tunnelMeetY - 1] = new Cell_Wall();
//}

//private void DigUpLeftCorridor(int tunnelMeetX, int tunnelMeetY)
//{
//    // Dig up and left from the specified meeting point until we meet something (note: caller is tasked with gauranteeing that
//    // there is something there to meet!)
//    DigCorridor(tunnelMeetX, tunnelMeetY - 1, Direction.Up);
//    DigCorridor(tunnelMeetX - 1, tunnelMeetY, Direction.Left);

//    // Draw the corner piece
//    Cells[tunnelMeetX, tunnelMeetY] = new Cell_Floor();
//    Cells[tunnelMeetX + 1, tunnelMeetY] = new Cell_Wall();
//    Cells[tunnelMeetX, tunnelMeetY + 1] = new Cell_Wall();
//    Cells[tunnelMeetX + 1, tunnelMeetY + 1] = new Cell_Wall();
//}

//private void DigDownLeftLCorridor(int tunnelMeetX, int tunnelMeetY)
//{
//    // Dig down and left from the specified meeting point until we meet something (note: caller is tasked with gauranteeing that
//    // there is something there to meet!)
//    // Dig the two corridors, but don't draw the actual corner piece where they meet (the DigCorridor algorithm doesn't handle it well).
//    DigCorridor(tunnelMeetX, tunnelMeetY + 1, Direction.Down);
//    DigCorridor(tunnelMeetX - 1, tunnelMeetY, Direction.Left);

//    // Draw the corner piece
//    Cells[tunnelMeetX, tunnelMeetY] = new Cell_Floor();
//    Cells[tunnelMeetX, tunnelMeetY - 1] = new Cell_Wall();
//    Cells[tunnelMeetX + 1, tunnelMeetY - 1] = new Cell_Wall();
//    Cells[tunnelMeetX + 1, tunnelMeetY] = new Cell_Wall();

//}

//private void DigUpRightLCorridor(int tunnelMeetX, int tunnelMeetY)
//{
//    // Dig up and right from the specified meeting point until we meet something (note: caller is tasked with gauranteeing that
//    // there is something there to meet!)
//    DigCorridor(tunnelMeetX, tunnelMeetY - 1, Direction.Up);
//    DigCorridor(tunnelMeetX + 1, tunnelMeetY, Direction.Right);

//    // Draw the corner piece
//    Cells[tunnelMeetX, tunnelMeetY] = new Cell_Floor();
//    Cells[tunnelMeetX - 1, tunnelMeetY] = new Cell_Wall();
//    Cells[tunnelMeetX - 1, tunnelMeetY + 1] = new Cell_Wall();
//    Cells[tunnelMeetX, tunnelMeetY + 1] = new Cell_Wall();
//}

//private int RandomValueBetween(int start, int end)
//{
//    return start + rand.Next(end - start);
//}

//private void SetCellToWallIfNotFloor(int curX, int curY)
//{
//    if (Cells[curX, curY].GetType() != typeof(Cell_Floor))
//        Cells[curX, curY] = new Cell_Wall();
//}

//private void DigCorridor(int startX, int startY, Direction directionToDig)
//{
//    int curX = startX;
//    int curY = startY;

//    // Start at (startX, startY) and dig in the specified direction until we hit floor.
//    while (Cells[curX, curY].GetType() != typeof(Cell_Floor))
//    {
//        if (curX == 0 || curX & gt;= 79 || curY == 0 || curY & gt;= 79)
//                    return;

//        Cells[curX, curY] = new Cell_Floor();
//        switch (directionToDig)
//        {
//            case Direction.Left:
//                if (Cells[curX, curY - 1].GetType() == typeof(Cell_Floor))
//                {
//                    // We hit the upper wall of a room; end-cap the corridor and stop building
//                    SetCellToWallIfNotFloor(curX, curY + 1);
//                    SetCellToWallIfNotFloor(curX - 1, curY + 1);
//                    Cells[curX - 1, curY + 1] = new Cell_Wall();
//                    return;
//                }
//                if (Cells[curX, curY + 1].GetType() == typeof(Cell_Floor))
//                {
//                    // We hit the lower wall of a room; end-cap the corridor and stop building
//                    SetCellToWallIfNotFloor(curX, curY - 1);
//                    SetCellToWallIfNotFloor(curX - 1, curY - 1);
//                    return;
//                }
//                SetCellToWallIfNotFloor(curX, curY - 1);
//                SetCellToWallIfNotFloor(curX, curY + 1);
//                curX--;
//                break;

//            case Direction.Right:
//                if (Cells[curX, curY - 1].GetType() == typeof(Cell_Floor))
//                {
//                    // We hit the upper wall of a room; end-cap the corridor and stop building
//                    SetCellToWallIfNotFloor(curX, curY + 1);
//                    SetCellToWallIfNotFloor(curX + 1, curY + 1);
//                    return;
//                }
//                if (Cells[curX, curY + 1].GetType() == typeof(Cell_Floor))
//                {
//                    // We hit the lower wall of a room; end-cap the corridor and stop building
//                    SetCellToWallIfNotFloor(curX, curY - 1);
//                    SetCellToWallIfNotFloor(curX + 1, curY - 1);
//                    return;
//                }
//                SetCellToWallIfNotFloor(curX, curY - 1);
//                SetCellToWallIfNotFloor(curX, curY + 1);
//                curX++;

//                break;
//            case Direction.Up:
//                if (Cells[curX - 1, curY].GetType() == typeof(Cell_Floor))
//                {
//                    // We hit the right wall of a room; end-cap the corridor and stop building
//                    SetCellToWallIfNotFloor(curX + 1, curY);
//                    SetCellToWallIfNotFloor(curX + 1, curY - 1);
//                    return;
//                }
//                if (Cells[curX + 1, curY].GetType() == typeof(Cell_Floor))
//                {
//                    // We hit the left wall of a room; end-cap the corridor and stop building
//                    SetCellToWallIfNotFloor(curX - 1, curY);
//                    SetCellToWallIfNotFloor(curX - 1, curY - 1);
//                    return;
//                }
//                SetCellToWallIfNotFloor(curX - 1, curY);
//                SetCellToWallIfNotFloor(curX + 1, curY);
//                curY--;
//                break;
//            case Direction.Down:
//                if (Cells[curX - 1, curY].GetType() == typeof(Cell_Floor))
//                {
//                    // We hit the right wall of a room; end-cap the corridor and stop building
//                    SetCellToWallIfNotFloor(curX + 1, curY);
//                    SetCellToWallIfNotFloor(curX + 1, curY + 1);
//                    return;
//                }
//                if (Cells[curX + 1, curY].GetType() == typeof(Cell_Floor))
//                {
//                    // We hit the left wall of a room; end-cap the corridor and stop building
//                    SetCellToWallIfNotFloor(curX - 1, curY);
//                    SetCellToWallIfNotFloor(curX - 1, curY + 1);
//                    return;
//                }
//                SetCellToWallIfNotFloor(curX - 1, curY);
//                SetCellToWallIfNotFloor(curX + 1, curY);
//                curY++;
//                break;
//        }
//    }
}