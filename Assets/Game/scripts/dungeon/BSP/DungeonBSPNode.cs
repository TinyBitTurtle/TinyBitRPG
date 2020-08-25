using System;
using System.Diagnostics;

namespace TinyBitTurtle
{
    public class DungeonBSPNode
    {
        // Orientation - Identifies which axis the Node is split upon.
//        private enum Orientation { Horizontal, Vertical };

//        ///
//        /// Constructor.
//        ///
//        public DungeonBSPNode(double startX, double startY, double endX, double endY)
//        {
//            LeftEdge = startX;
//            RightEdge = endX;
//            TopEdge = startY;
//            BottomEdge = endY;

//            NodeWidth = RightEdge - LeftEdge;
//            NodeHeight = BottomEdge - TopEdge;
//        }

//        ///
//        /// Partitions (splits) this node into two halves, and then creates child nodes for
//        /// each half and recursively partitions both of them (if they are not 'too small').
//        ///
//        public void Partition()
//        {
//            // Choose the axis along which we'll partition (split) this node.  If this is a very
//            // narrow node in one axis, then favor the other axis in order to minimize long corridor-like rooms.
//            if (NodeWidth / NodeHeight & gt; MaxPartitionSizeRatio)
//                SplitOrientation = Orientation.Horizontal;
//            else if (NodeHeight / NodeWidth & gt; MaxPartitionSizeRatio)
//                SplitOrientation = Orientation.Vertical;
//            else
//                SplitOrientation = (r.Next(2) == 1) ? Orientation.Horizontal : Orientation.Vertical;

//            // Split the Node.
//            if (SplitOrientation == Orientation.Horizontal)
//            {
//                // Pick the location along the XAxis (between the LeftEdge and RightEdge) at which we will split.
//                SplitLocation = LeftEdge + HomogenizedRandomValue() * NodeWidth;

//                // Create our two child nodes
//                Left = new DungeonBSPNode(LeftEdge, TopEdge, SplitLocation, BottomEdge);
//                Right = new DungeonBSPNode(SplitLocation, TopEdge, RightEdge, BottomEdge);

//                // Partition child nodes if either is not yet too small
//                if (WeShouldSplit(SplitLocation - LeftEdge))
//                    Left.Partition();
//                if (WeShouldSplit(RightEdge - SplitLocation))
//                    Right.Partition();
//            }
//            else // Vertical split
//            {
//                // Pick the location along the YAxis (between the TopEdge and BottomEdge) at which we will split.
//                SplitLocation = TopEdge + HomogenizedRandomValue() * NodeHeight;

//                // Create our two (Left = upper and Right = lower) child nodes
//                Left = new DungeonBSPNode(LeftEdge, TopEdge, RightEdge, SplitLocation);
//                Right = new DungeonBSPNode(LeftEdge, SplitLocation, RightEdge, BottomEdge);

//                // Partition child nodes if either is not yet too small
//                if (WeShouldSplit(SplitLocation - TopEdge))
//                    Left.Partition();
//                if (WeShouldSplit(BottomEdge - SplitLocation))
//                    Right.Partition();
//            }
//        }

//        private bool WeShouldSplit(double partitionSize)
//        {
//            // For variety, we don't split ~10% of the partitions which are small.  This allows creation of
//            // a few slightly larger rooms in the dungeon (particularly useful later one when we're trying to place 'special' rooms)
//            if (partitionSize & gt; SmallestPartitionSize & amp; &amp; partitionSize & lt; SmallestPartitionSize * 2.0 & amp; &amp; r.NextDouble() & lt;= .1)
//                 return false;
//            // If the partition is bigger than the "Smallest Partition Size" value, then split.
//            return partitionSize & gt; SmallestPartitionSize;
//        }

//        ///
//        /// Returns a random absolute normalized value (between 0.0 and 1.0).  Allows homogenization
//        /// of the partitions to be controlled
//        ///
//        ///
//        private double HomogenizedRandomValue()
//        {
//            return 0.5 - (r.NextDouble() * HomogeneityFactor);
//        }

//        public void EnumerateLeafNodes(AddRoomDelegate addRoomDelegate)
//        {
//            // If this node was partitioned, then recurse into our child nodes; otherwise, call the callback function
//            if (Left != null || Right != null)
//            {
//                if (Left != null)
//                    Left.EnumerateLeafNodes(addRoomDelegate);
//                if (Right != null)
//                    Right.EnumerateLeafNodes(addRoomDelegate);
//            }
//            else
//                addRoomDelegate(LeftEdge, TopEdge, RightEdge, BottomEdge);
//        }

//        ///
//        /// Test code to Render the BSP tree to a graphic context.  Used to validate the BSP tree code.
//        ///
//        ///

//        public void Render(Graphics gr)
//        {
//            float w = Map.MapWidth * 8;
//            float h = Map.MapHeight * 8;
//            gr.FillRectangle(Brushes.Black, (float)LeftEdge * w + 10, (float)TopEdge * h + 10, (float)(RightEdge - LeftEdge) * w, (float)(BottomEdge - TopEdge) * h);
//            gr.DrawRectangle(Pens.Red, (float)LeftEdge * w + 10, (float)TopEdge * h + 10, (float)(RightEdge - LeftEdge) * w, (float)(BottomEdge - TopEdge) * h);

//            if (Left != null)
//                Left.Render(gr);
//            if (Right != null)
//                Right.Render(gr);
//        }

//        // The Edges of this Node in the Map.  Note that we store all Edge values and the SplitLocation
//        // in absolute (as compared to to relative) normalized (0.0-1.0) coordinates.  We don't convert
//        // to actual 'dungeon ints' until we're placing rooms.
//        // TBD: Move to floats?  Move to Rect?
//        // TBD: Do a pass and see if it cleans up any math if I move to relative coordinates &amp; just calculate on the way 'down' the tree...
//        public double LeftEdge;
//        public double RightEdge;
//        public double TopEdge;
//        public double BottomEdge;

//        public double NodeWidth;
//        public double NodeHeight;

//        // Whether we're Split horizontally or Vertically
//        // TBD: Do I need to store this in the node, or can it just be local to the Partition function?
//        private Orientation SplitOrientation;

//        // The absolute normalized location at which this Node will be split.  Will be a value between
//        // LeftEdge &amp; RightEdge, or TopEdge &amp; BottomEdge, depending on SplitOrientation's value.
//        // TBD: Do I need to store this in the node, or can it just be local to the Partition function?
//        public double SplitLocation;

//        // Our Left &amp; Right Child Nodes.  "Left" and "Right" refer to the binary tree node positions, and
//        // apply to both horizontal and vertical orientations (in the latter case, "Left" --&gt; "Up" and
//        // "Right" --&gt; "Down".
//        public DungeonBSPNode Left { get; set; }
//        public DungeonBSPNode Right { get; set; }

//        // TBD: Want our random factor to be completely reproducible.  An entire Dungeon run should be recreatable
//        // just from the initial Random Seed and the list of user inputs.
//        static public Random r;

//        // The smallest size that a partition can get.  In absolute terms, so "0.1" would equate to 1/10th the
//        // size of the map.
//        /// TBD: Make SmallestPartitionSize something that is set by the caller to allow different dungeon generation schemas.
//        static private double SmallestPartitionSize = .15;

//        // When choosing which axis to split a Node on, we want to optimize the number of "squarish"
//        // rooms, and minimize the number of "long corridor rooms."  To do this, when splitting a Node,
//        // we look at the ratio of Width to Height, and if it crosses the MaxPartitionSizeRatio threshold
//        // in either axis, then we forcibly split on the *other* axis.
//        /// TBD: Make MaxPartitionSizeRatio something that is set by the caller to allow different dungeon generation schemas.
//        static private double MaxPartitionSizeRatio = 1.5f;

//        // The homogeneityFactor determines how "common" split partitions are.  The value is between 0.0 and 0.5.  A small value (e.g. .1)
//        // creates a Dungeon with similar size partitions; A large value (e.g. 0.4) creates a Dungeon
//        // with more varied sized partition.  The balance is finding the right number - too small a value == all partitions are the
//        // same; too large a value == higher likelihood of long narrow "corridor rooms"
//        /// TBD: Make HomogeneityFactor something that is set by the caller to allow different dungeon generation schemas.
//        static private double HomogeneityFactor = 0.25;

//        // A pointer back to our Map.  Used to generate rooms
//        static public Map Map;
//    }
    }
}
