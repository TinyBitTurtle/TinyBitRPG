using System.Collections;
using UnityEngine;

namespace TinyBitTurtle
{
    public class DungeonModel : SingletonMonoBehaviour<DungeonModel>
    {
        public enum TileShading
        {
            none,
            north,
            east,
            south,
            west,
            north_east
        }

        public enum SpawnCorners
        {
            southWest = 0,
            southEast,
            northEast,
            northWest,

            maxCorner,
        }

        // The type of tile that will be laid in a specific position.
        public enum TileType
        {
            Wall, Floor
        }

        // what is visible in the editor
        public DungeonSettings gameInit;
        public Spawner playerSpawner;
        public Spawner stairsDownSpawner;
        public DungeonView dungeonView;
        public float endOfSpawnDelay;

        public delegate void SetupObjectsCallback(DungeonSettings gameInit, int level, Vector2Int pos, Vector2Int dim, Corridor corridor);
        public SetupObjectsCallback setupObjectsCallback;

        private TileType[][] tiles;                               // A jagged array of tile types representing the board, like a grid.
        private Room[] rooms;                                     // All the rooms that are created for this board.
        private Corridor[] corridors;                             // All the corridors that connect the rooms.
        private GameObject boardHolder;                           // GameObject that acts as a container for all other tiles.
        private int[] startEndRoomsIdx;
        private Spawner.SpawnCallback beginSpawnCallback;
        private Spawner.SpawnCallback endSpawnCallback;

        public void playerSpawn(GameObject newGameObject)
        {
            // attach camera
            CameraCtrl cameraCtrl = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraCtrl>();
            cameraCtrl.Attach(newGameObject);

            // wait before spawning the character
            StartCoroutine(EndSpawn(newGameObject, endOfSpawnDelay));
        }

        public static IEnumerator EndSpawn(GameObject newGameObject, float duration)
        {
            // wait
            yield return new WaitForSecondsRealtime(duration);

            // spawn the object
            Animator animator = newGameObject.GetComponent<Animator>();
            if(animator)
                animator.SetTrigger("spawn");

            // if we are dealing with the player, prepare the object
            GridPlayer2D gridPlayer2D = newGameObject.GetComponent<GridPlayer2D>() as GridPlayer2D;
            if (gridPlayer2D)
            {
                Actor character = newGameObject.GetComponent<Actor>() as Actor;
                if(character)
                    character.control.Setup();
            }
        }

        public void GenerateDungeon(int level = 0)
        {
            // Create the board holder.
            boardHolder = new GameObject("BoardHolder");

            // model
            SetupTilesArray();
            CreateRoomsAndCorridors(level);
            SetTilesValuesForRooms();
            SetTilesValuesForCorridors();

            // view
            if (dungeonView)
            {
                dungeonView.InstantiateTiles(tiles, boardHolder, gameInit);
                dungeonView.InstantiateOuterWalls(tiles, boardHolder, gameInit);
            }
        }

        void SetupTilesArray()
        {
            // Set the tiles jagged array to the correct width.
            tiles = new TileType[gameInit.columns][];

            // Go through all the tile arrays...
            for (int i = 0; i < tiles.Length; i++)
            {
                // ... and set each tile array is the correct height.
                tiles[i] = new TileType[gameInit.rows];
            }
        }

        void CreateRoomsAndCorridors(int level)
        {
            // Create the rooms array with a random size.
            rooms = new Room[gameInit.numRooms.Random];

            // There should be one less corridor than there is rooms.
            corridors = new Corridor[rooms.Length - 1];

            // Create the first room and corridor.
            rooms[0] = new Room();
            corridors[0] = new Corridor();

            // Setup the first room, there is no previous corridor so we do not use one.
            rooms[0].SetupRoom(level, gameInit, gameInit.columns, gameInit.rows);

            // place game specific objects
            if(setupObjectsCallback != null)
                setupObjectsCallback(gameInit, level, rooms[0].pos, rooms[0].dim, null);

            // Setup the first corridor using the first room.
            corridors[0].SetupCorridor(rooms[0], gameInit.corridorLength, gameInit.roomWidth, gameInit.roomHeight, gameInit.columns, gameInit.rows, true);

            for (int i = 1; i < rooms.Length; i++)
            {
                // Create a room.
                rooms[i] = new Room();

                // Setup the room based on the previous corridor.
                rooms[i].SetupRoom(level, gameInit, gameInit.columns, gameInit.rows, corridors[i - 1]);

                // place game specific objects
                if (setupObjectsCallback != null)
                    setupObjectsCallback(gameInit, level, rooms[i].pos, rooms[i].dim, corridors[i - 1]);

                // If we haven't reached the end of the corridors array...
                if (i < corridors.Length)
                {
                    // ... create a corridor.
                    corridors[i] = new Corridor();

                    // Setup the corridor based on the room that was just created.
                    corridors[i].SetupCorridor(rooms[i], gameInit.corridorLength, gameInit.roomWidth, gameInit.roomHeight, gameInit.columns, gameInit.rows, false);
                }
            }

            // extract data from dungeon
            SetupStartRoom();

            // extract data from dungeon
            SetupEndRoom();

            // spawn player in the start room
            SetupPlayer();
        }

        private void SetupPlayer()
        {
            // spawn callback
            beginSpawnCallback = playerSpawn;

            // what is the start pos
            Room startRoom = rooms[startEndRoomsIdx[0]];
            Vector3 playerPos = new Vector3(startRoom.pos.x + (startRoom.dim.x * 0.5f), startRoom.pos.y + (startRoom.dim.y * 0.5f), 0);

            // move the camera where the player will be
            CameraCtrl cameraCtrl = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraCtrl>();
            cameraCtrl.transform.position = playerPos;

            // spawn the player
            playerSpawner.spawn(beginSpawnCallback, playerPos);

            // where is the room to the next level?
            Room endRoom = rooms[startEndRoomsIdx[1]];
            Vector3 nextLevelPos = new Vector3(endRoom.pos.x + (endRoom.dim.x * 0.5f), endRoom.pos.y + (endRoom.dim.y* 0.5f), 0);
            stairsDownSpawner.spawn(null, nextLevelPos);
        }

        private void SetupEndRoom()
        {
            float maxRoomDistance = 0;

            // find start and end rooms
            for (int i = 0; i < rooms.Length; i++)
            {
                // don't take into account start room
                if (i == startEndRoomsIdx[0])
                    continue;

                // current room
                Room room = rooms[i];

                // distance of room center to start room
                Vector2 distanceToStartRoom = new Vector3((room.pos.x + room.dim.x / 2f) - rooms[startEndRoomsIdx[0]].pos.x, (room.pos.y + room.dim.y / 2f) - rooms[startEndRoomsIdx[0]].pos.y);
                float roomSqrDistanceEnd = distanceToStartRoom.SqrMagnitude();

                if (roomSqrDistanceEnd > maxRoomDistance)
                {
                    startEndRoomsIdx[1] = i;
                    maxRoomDistance = roomSqrDistanceEnd;
                }
            }

            //Debug.DrawLine(new Vector3(rooms[startEndRoomsIdx[0]].xPos, rooms[startEndRoomsIdx[0]].yPos), new Vector3(rooms[startEndRoomsIdx[1]].xPos, rooms[startEndRoomsIdx[1]].yPos), Color.white);
        }

        private void SetupStartRoom()
        {
            // init variables with proper values
            startEndRoomsIdx = new int[2];
            float maxRoomDistanceStart = float.MaxValue;

            // randomly choose a starting point in a corner
            SpawnCorners rndstartCorner = (SpawnCorners)Random.Range(0, (int)(SpawnCorners.maxCorner - 1));
            Vector2 startCorner = Vector2.zero;
            Vector2 endCorner = Vector2.zero;
            switch (rndstartCorner)
            {
                case SpawnCorners.southEast:
                    startCorner = new Vector2(0, 100);
                    break;

                case SpawnCorners.northEast:
                    startCorner = new Vector2(100, 100);
                    break;

                case SpawnCorners.northWest:
                    startCorner = new Vector2(100, 0);
                    break;

                case SpawnCorners.southWest:
                    startCorner = Vector2.zero;
                    break;
            }

            // find start and end rooms
            for (int i = 0; i < rooms.Length; i++)
            {
                // current room
                Room room = rooms[i];

                // distance of room to start corner
                Vector2 distanceToCornerStart = new Vector2((room.pos.x + room.dim.x / 2f) - startCorner.x, (room.pos.y + room.dim.y / 2f) - startCorner.y);
                float roomSqrDistanceStart = distanceToCornerStart.SqrMagnitude();

                if (roomSqrDistanceStart < maxRoomDistanceStart)
                {
                    startEndRoomsIdx[0] = i;
                    maxRoomDistanceStart = roomSqrDistanceStart;
                }
            }
        }

        void SetTilesValuesForRooms()
        {
            // Go through all the rooms...
            for (int i = 0; i < rooms.Length; i++)
            {
                Room currentRoom = rooms[i];

                // ... and for each room go through it's width.
                for (int j = 0; j < currentRoom.dim.x; j++)
                {
                    int xCoord = currentRoom.pos.x + j;

                    // For each horizontal tile, go up vertically through the room's height.
                    for (int k = 0; k < currentRoom.dim.y; k++)
                    {
                        int yCoord = currentRoom.pos.y + k;

                        // The coordinates in the jagged array are based on the room's position and it's width and height.
                        tiles[xCoord][yCoord] = TileType.Floor;
                    }
                }
            }
        }


        void SetTilesValuesForCorridors()
        {
            // Go through every corridor...
            for (int i = 0; i < corridors.Length; i++)
            {
                Corridor currentCorridor = corridors[i];

                // and go through it's length.
                for (int j = 0; j < currentCorridor.corridorLength; j++)
                {
                    // Start the coordinates at the start of the corridor.
                    int xCoord = currentCorridor.startXPos;
                    int yCoord = currentCorridor.startYPos;

                    TileShading tileShading = TileShading.none;

                    // Depending on the direction, add or subtract from the appropriate
                    // coordinate based on how far through the length the loop is.
                    switch (currentCorridor.direction)
                    {
                        case CorridorDirection.North:
                            yCoord += j;
                            break;
                        case CorridorDirection.East:
                            xCoord += j;
                            break;
                        case CorridorDirection.South:
                            yCoord -= j;
                            break;
                        case CorridorDirection.West:
                            xCoord -= j;
                            break;
                    }

                    // Set the tile at these coordinates to Floor.
                    tiles[xCoord][yCoord] = TileType.Floor;
                }
            }
        }
    }
}