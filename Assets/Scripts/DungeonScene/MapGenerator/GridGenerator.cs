using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEditor;
using Assets.Scripts.MapGenerator;
using Assets.Scripts.MapGenerator.Generator.Grid;
using RogueElements.Examples;
using Assets.Scripts.Datas.Save;
using Assets.Scripts.Datas.Struct;
using RogueElements;
using System;

public class GridGenerator : MonoBehaviour
{
    public Map map; 

    // FOV variables
    public bool isFOVrecompute;
    public int playerVisibilityRadius;

    public Tilemap floorMap; 
    public Tilemap[] wallMaps;
    private Decorate decorate;

    private int mapWidthX = 106;
    private int mapHeightY = 106;

    [Header("Map")]
    public Texture2D wall_texture;
    public Texture2D ground_texture;
    public Texture2D stair_texture;

    private Tile[] WallTilesToApply;
    private Tile[] GroundTilesToApply;
    private Tile[] StairTilesToApply;
    // ProcGen variables:




    private int typetex = 0;

    //public TileBase tileBase; // Asigned to our floor tilebase in the editor.

    // Start is called before the first frame update
    public void Init()
    {
        decorate = new Decorate();
        LoadWallTiles();
        LoadGroundTiles();
        LoadStairTiles();
        
        playerVisibilityRadius = 4;
        isFOVrecompute = InputHandler.isFOVrecompute; // We set this to the Static bool

        // If true, do not randomize seed for map and recover map.
        bool firstLoad = SaveLoad.currentSave.currentGame.firstLoad;

        if (!firstLoad)
        {
            // Floor setup
            NextFloor();
        }
        else
        {
            LoadFloor();
        }

    }
    public void LoadStairTiles()
    {
        //Sprite sprite = monsterAnimatorInline.m_Sprites[0];



        Vector2 standardPivot = new Vector2(0.5f, 0.5f);
        float groundtiles = stair_texture.width / 17;

        List<Sprite> sprites = new List<Sprite>();



        sprites.Clear();
        for (int i = 0; i < groundtiles; i++)
        {

            UnityEngine.Rect rect2 = new UnityEngine.Rect((int)(i * 17), typetex * 17 + 1, (int)16, (int)16);
            //Color color = ground_texture.GetPixel(i * 17 + 8, 0 + 8);
            //if (color.a != 0)
            //{
            Sprite sprite = Sprite.Create(stair_texture, rect2, standardPivot, 16);
            sprite.name = stair_texture.name + "_" + i;
            sprites.Add(sprite);
            //}
        }

        Tile[] tiles = new Tile[sprites.Count];
        int j = 0;
        foreach (Sprite sp in sprites)
        {
            Tile tile = ScriptableObject.CreateInstance<Tile>();
            tile.name = sp.name;

            tile.sprite = sp;
            tiles[j] = tile;
            j++;
            // break;
        }
        SetStairTiles(tiles);
    }
    public void SetGroundTiles(Tile[] GroundTiles)
    {
        GroundTilesToApply = GroundTiles;

    }
    public void SetStairTiles(Tile[] StairTiles)
    {
        StairTilesToApply = StairTiles;

    }
    public void SetWallTiles(Tile[] WallTiles)
    {
        WallTilesToApply = WallTiles;

    }

    public void LoadWallTiles()
    {
        //Sprite sprite = monsterAnimatorInline.m_Sprites[0];

        //  Debug.Log("Test " + wall_texture.name+" "+ wall_texture.height);


        Vector2 standardPivot = new Vector2(0.5f, 0.5f);
        int w = wall_texture.width / 17;
        int h = wall_texture.height / 17;
        List<Sprite> sprites = new List<Sprite>();

        Tile[] tiles = new Tile[w*h];


        int inc = 0;

        for (int x = 0; x < w; x++)
        {
            for (int y = 0; y < h; y++)
            {
                sprites.Clear();

                int v = h - y - 1;
                UnityEngine.Rect rect2 = new UnityEngine.Rect((int)(x * 17), (int)((v) * 17) + 1, (int)16, (int)16);
                Sprite sprite = Sprite.Create(wall_texture, rect2, standardPivot, 16);
                sprite.name = wall_texture.name + "_" + x + "_" + y;
                
                Tile tile = ScriptableObject.CreateInstance<Tile>();
                tile.name = sprite.name;
                tile.sprite = sprite;
                tiles[inc] = tile;
                inc++;
            }
        }
        SetWallTiles(tiles);

    }


    public void LoadGroundTiles()
    {
        //Sprite sprite = monsterAnimatorInline.m_Sprites[0];



        Vector2 standardPivot = new Vector2(0.5f, 0.5f);
        float groundtiles = ground_texture.width / 17;

        List<Sprite> sprites = new List<Sprite>();



        sprites.Clear();
        for (int i = 0; i < groundtiles; i++)
        {

            UnityEngine.Rect rect2 = new UnityEngine.Rect((int)(i * 17), typetex * 17 + 1, (int)16, (int)16);
            //Color color = ground_texture.GetPixel(i * 17 + 8, 0 + 8);
            //if (color.a != 0)
            //{
            Sprite sprite = Sprite.Create(ground_texture, rect2, standardPivot, 16);
            sprite.name = ground_texture.name + "_" + i;
            sprites.Add(sprite);
            //}
        }

        Tile[] tiles = new Tile[sprites.Count];
        int j = 0;
        foreach (Sprite sp in sprites)
        {
            Tile tile = ScriptableObject.CreateInstance<Tile>();
            tile.name = sp.name;
            tile.sprite = sp;
            tiles[j] = tile;
            j++;
            // break;
        }
        SetGroundTiles(tiles);
    }

    //void Update()
    //{


    //    isFOVrecompute = InputHandler.isFOVrecompute; // We set this to the Static bool
    //    // WIP FOV
    //    if (isFOVrecompute)
    //    {
    //        // Calculates Enemy FOV
    //        CalculateEntityClosestFOV(listOfEnemyEntities);

    //        // TODO: Most likely I can take this outside of Update() for performance. 
    //        // TODO: I think I don't need these and can be deleted. Review.
    //        List<Vector2> listOfDiscovered_vectors = new List<Vector2>();
    //        List<Vector2> listOfClosestFOV_vectors = new List<Vector2>();


    //        // 1.1 Locate the NEW player vector: TODO: Change the +5/-5 to a visibility variable . TODO: Fix that there's the same visibility all around, not 5 in one direction and 4 in the other because counts the playerPos as one tile.
    //        // TODO: Most likely I can take this outside of Update() for performance. 
    //        int _playerPosX = (int)playerReference.localPosition.x;
    //        int _playerPosY = (int)playerReference.localPosition.y;
    //        int _tileDifferenceX = (int)playerReference.localPosition.x + 5;
    //        int _tileDifferenceY = (int)playerReference.localPosition.y + 5;

    //        int _tileOffsetFromPlayerX = (int)playerReference.localPosition.x -5;
    //        int _tileOffsetFromPlayerY = (int)playerReference.localPosition.y - 5;


    //        /* Logic for discovered terrain */
    //        // 1.3 Set a radius around the player. 
 

    //        for (int x = _tileOffsetFromPlayerX; x < _tileDifferenceX; x++)
    //        {
    //            for (int y = _tileOffsetFromPlayerY; y < _tileDifferenceY; y++)
    //            {
    //                listOfDiscovered_vectors.Add(new Vector2(x, y));
    //                listOfClosestFOV_vectors.Add(new Vector2(x, y)); // Will be used to recalculate the color of the nearest tiles.

    //            }
    //        }

    //        // This list will contain all discovered vectors around the player
    //        foreach (var item in listOfDiscovered_vectors)
    //        {
    //            wallMap.SetColor(new Vector3Int((int)item.x, (int)item.y, 0), Color.grey);
    //            floorMap.SetColor(new Vector3Int((int)item.x, (int)item.y, 0), Color.grey);

    //        }

    //        // Calculates Player FOV
    //        CalculatePlayerClosestFOV();
    //        isFOVrecompute = false;


    //    }

    //}

    void ClearReferences() {

        Engine.Instance.itemGenerator.Clear();

        Engine.Instance.enemyGenerator.Clear();

    }




    private void PlacePlayer()
    {
        Engine.Instance.playerGenerator.PlacePlayer();
    }

    // TODO: Most likely we can place this in a different place, for example on GameStateManager to generate enemies as new events or states happen.
    private void PlaceEntities()
    {
        Engine.Instance.enemyGenerator.PlaceEntities(this);
    }
    public Vector2 GetEmptySpace(bool isPlayer=false)
    {
        int _randomX;
        int _randomY;
        bool flag = true;

        Entity player = Engine.Instance.GetPlayerEntity();

        do
        {
            _randomX = UnityEngine.Random.Range(1, mapWidthX - 1);
            _randomY = UnityEngine.Random.Range(1, mapHeightY - 1);

            

            bool ground = Engine.Instance.OnGround(_randomX, _randomY);
            if (ground)
            {

                if(Engine.Instance.HasEnemiesOn(_randomX, _randomY))
                {
                    ground = false;
                }
                if (player != null)
                {
                    if (!isPlayer)
                    {
                        if (GetDistance(player.x, player.y, _randomX, _randomY) < 5)
                        {
                            ground = false;
                        }
                    }
                    else
                    {
                        foreach (ExitZone item in Engine.Instance.itemGenerator.listOfExits)
                        {
                            if ((int)item.transform.localPosition.x == _randomX && (int)item.transform.localPosition.y == _randomY)
                            {
                                ground = false;
                                break;
                            }
                        }
                    }
                }
                if (ground)
                {
                    flag = false;
                }
            }
        } while (flag);
        Vector2 _randomVector = new Vector2(_randomX, _randomY);
        return _randomVector;
    }
    private  int GetDistance(int x,int y,int x2,int y2)
    {
        return (int)Mathf.Sqrt((x2 - x) * (x2 - x) +(y - y2)* (y - y2));
    }


    void PlaceItems() {
        Engine.Instance.itemGenerator.GenerateConsumables(this);
    }

    void PlaceExit()
    {
        Engine.Instance.itemGenerator.PlaceExit(this);
    }

    //// Is inside our GridGenerator because regenerates / re-paints our grid constantly
    //void CalculateEntityClosestFOV(List<Entity> _listOfEntities) {

    //    foreach (var enemy in _listOfEntities)
    //    {
    //        if (enemy != null)
    //        {
    //            Vector3 _entityLocation = new Vector3(enemy.entityLocation.x, enemy.entityLocation.y, 0); // Gets the position vector of each entity
    //            //Debug.Log("Enemy at: " + _entityLocation.ToString());
    //            int _entityVisibilityRadius = 5; // 
    //                                             //int _entityVisibilityDiameter = 6; // _entityVisibilityRadius * 2
    //            int _offsetQuadrant4 = (int)_entityLocation.x - _entityVisibilityRadius; // Top left corner of the enemy quadrant (4). Position -3
    //            int _offsetQuadrant1 = (int)_entityLocation.y - _entityVisibilityRadius; // Top left corner of the enemy quadrant (4). Position -3
    //                                                                                     //int _offsetQuadrant1 = (int)_entityLocation.x + _entityVisibilityRadius; // Top right corner of the enemy quadrant (1) 
    //                                                                                     //int _offsetQuadrant3 = (int)_entityLocation.x - _entityVisibilityRadius;
    //        }
    //    }
    //}

    // Adapted from CalculateEntityClosestFOV , as we can't pass the Player as an argument because is not generated as an Entity, once we change this we can get rid of the function as well.
    //// Is inside our GridGenerator because regenerates / re-pain our grid constantly
    //void CalculatePlayerClosestFOV()
    //{

    //        if (playerReference != null)
    //        {
    //            Vector3 _entityLocation = new Vector3(playerReference.localPosition.x, playerReference.localPosition.y, 0); // Gets the position vector of each entity
    //            //Debug.Log("Enemy at: " + _entityLocation.ToString());
    //            int _entityVisibilityRadius = playerVisibilityRadius; // 
    //            int _entityVisibilityDiameter = _entityVisibilityRadius * 2; // _entityVisibilityRadius * 2
    //            int _offsetQuadrant4 = (int)_entityLocation.x - _entityVisibilityRadius; // Top left corner of the enemy quadrant (4). Position -3
    //            int _offsetQuadrant1 = (int)_entityLocation.y - _entityVisibilityRadius; // Top left corner of the enemy quadrant (4). Position -3
    //                                                                                     //int _offsetQuadrant1 = (int)_entityLocation.x + _entityVisibilityRadius; // Top right corner of the enemy quadrant (1) 
    //                                                                                     //int _offsetQuadrant3 = (int)_entityLocation.x - _entityVisibilityRadius;


    //        // Re-drawns these tiles that are outside CalculatePlayerClosestFOV()
    //        ResetTilesOutsideFOV(_entityVisibilityDiameter, _entityVisibilityRadius, _offsetQuadrant4, _entityLocation);

    //    }

    //}

    void ResetTilesOutsideFOV(int _entityVisibilityDiameter, int _entityVisibilityRadius, int _offsetQuadrant4, Vector3 _entityLocation)
    {
  
    }

    // ORIGINAL
    struct Coord
    {
        public int tileX;
        public int tileY;

        public Coord(int x, int y) {

            tileX = x;
            tileY = y;
        }
    }



    //void ProcessMap() {
    //    // no modifications = o in both threesholds

    //    List<List<Coord>> wallRegions = GetRegions(1); // Get regions of walls (1)


    //    int wallThresholdSize = 0; // 100 map is almost empty, 10 opens up map, 3 no sucede casi nada pero veo bordes desaparecer.
    //    foreach (List<Coord> wallRegion in wallRegions)
    //    {
    //        if (wallRegion.Count < wallThresholdSize)
    //        {
    //            foreach (Coord tile in wallRegion)
    //            {
    //                // This is getting holes in the map and filling them up with walls:
    //                map[tile.tileX, tile.tileY] = 0; // setting it to an empty space.
    //                wallMap.SetTile(new Vector3Int((int)tile.tileX, (int)tile.tileY, 0), null);
    //                //floorMap.SetTile(new Vector3Int((int)tile.tileX, (int)tile.tileY, 0), floorTile);
    //                //floorMap.SetColor(new Vector3Int((int)tile.tileX, (int)tile.tileY, 0), Color.red);
    //                wallMap.SetTile(new Vector3Int((int)tile.tileX, (int)tile.tileY, 0), wallTile);
    //                wallMap.SetColor(new Vector3Int((int)tile.tileX, (int)tile.tileY, 0), Color.red);

    //            }
    //        }
    //    }
    //    Debug.Log("How many wallRegions?" + wallRegions.Count.ToString()); // 932 wrong

    //    // TODO este no funciona para nada, está cogiendo todas las walls del escenario una por una
    //    // We set a threeshold for Floors, if our room size is smaller (<) than our thresshold, 
    //    List<List<Coord>> roomRegions = GetRegions(0); // Get regions of floors (0)

    //    int roomThresholdSize = 0; // 100 nothing happened, 10 nothing, 3 nothing.
    //    List<Room> survivingRooms = new List<Room>(); // Added later on: List of rooms which survive the culling process:

    //    foreach (List<Coord> roomRegion in roomRegions)
    //    {
    //        if (roomRegion.Count < roomThresholdSize) // If our tile count for the rooms is < than our threeshold, then remove athe floor?
    //        {
    //            foreach (Coord tile in roomRegion)
    //            {
    //                map[tile.tileX, tile.tileY] = 1; // setting it to null floor, and then adding a walled space
    //                wallMap.SetTile(new Vector3Int((int)tile.tileX, (int)tile.tileY, 0), null);
    //                //wallMap.SetTile(new Vector3Int((int)tile.tileX, (int)tile.tileY, 0), wallTile);
    //                //wallMap.SetColor(new Vector3Int((int)tile.tileX, (int)tile.tileY, 0), Color.red);
    //                floorMap.SetTile(new Vector3Int((int)tile.tileX, (int)tile.tileY, 0), floorTile);
    //                floorMap.SetColor(new Vector3Int((int)tile.tileX, (int)tile.tileY, 0), Color.red);

    //            }
    //        }
    //        else
    //        {
    //            survivingRooms.Add(new Room(roomRegion, map));
    //        }
    //    }
    //    Debug.Log("How many roomRegions?" + roomRegions.Count.ToString()); // Doesn't even trigger. Code does not reach.

    //    ConnectClosestRooms(survivingRooms); // At the end of the process we'll pass our list of surviving rooms to connect them to eachother
    //}

    // Method that given a certain tile type can return all of the regions of tha type, a list of regions instead of coordinates. A list of a list of coordinates
    //List<List<Coord>> GetRegions(int tileType)
    //{

    //    // TODO this seems to fail and is teturning the initial list.
    //    List<List<Coord>> regions = new List<List<Coord>>();
    //    int[,] mapFlags = new int[mapWidthX, mapHeightY]; // which regions we've already look at

    //    // Then we want to look into all the adjacent tiles:
    //    for (int x = 0; x < mapWidthX; x++)
    //    {
    //        for (int y = 0; y < mapHeightY; y++)
    //        {
    //            // We go through all map and check if these flags are equal to 0 (Not checked) && tile at x and y is the right type (0, floor, 1 wall)
    //            if (mapFlags[x, y] == 0 && map[x, y] == tileType)
    //            {
    //                // then we want to create a new list of coordinates:
    //                List<Coord> newRegion = GetRegionTiles(x, y);// We'll create a new list of coordinates via GetRegionTiles() and passing x and y as starting values
    //                regions.Add(newRegion);

    //                // mark all tiles of the new region as "looked at"
    //                foreach (Coord tile in newRegion)
    //                {
    //                    mapFlags[tile.tileX, tile.tileY] = 1; // 1= looked at
    //                }
    //            }
    //        }
    //    }

    //    Debug.Log("regions" + regions.Count.ToString());
    //    return regions; // return a list of regions // TODO: Problem, regions return 900+when should be 2 or 3.
    //}

    // This gets if the tiles are set to 0: floor or 1: wall
    //List<Coord> GetRegionTiles(int startX, int startY)
    //{

    //    List<Coord> tiles = new List<Coord>(); // To store tiles, either stores the floors or the walls.
    //    int[,] mapFlags = new int[mapWidthX, mapHeightY]; // System.Int32[106,30] which tiles we've already look at, 1 vs 0 
    //    ////TODO this seems to be broken here: in tileType
    //    int tileType = map[startX, startY]; // 0 // Which type of Tile are we looking for

    //    Queue<Coord> _queue = new Queue<Coord>(); // create a new queue of coordinates
    //    _queue.Enqueue(new Coord(startX, startY)); // enqueue our first coordinate

    //    mapFlags[startX, startY] = 1; // Set this to one to mark that we have already looked at this tile (set to 1)
    //    // Last element [105,29] so this was enqueued correctly

    //    while (_queue.Count > 0) // While there's still stuff in the queue...
    //    {

    //        //    //Debug.Log("Queue: " + _queue.Count.ToString());

    //        Coord tile = _queue.Dequeue(); // returns the first item in the queue, AND removed the item from the queue
    //        tiles.Add(tile); // We add this tile to our list
    //                         //    Debug.Log("added" + tile.ToString());


    //        // Then we want to look into all the adjacent tiles:
    //        for (int x = tile.tileX - 1; x < tile.tileX + 1; x++)
    //        {
    //            for (int y = tile.tileY - 1; y < tile.tileY + 1; y++)
    //            {
    //                //            // Check if tile is in range and NOT diagonal
    //                if (H_IsInMapRange(x, y) && (y == tile.tileY || x == tile.tileX))
    //                {
    //                    //                // We make sure we haven't checked this vector yet && we want to make sure is the right tile of tile
    //                    if (mapFlags[x, y] == 0 && map[x, y] == tileType)
    //                    {
    //                        mapFlags[x, y] = 1; // We set the tile as checked
    //                        _queue.Enqueue(new Coord(x, y)); // And finally we add the new coordinate to the queue to be checked in the next iteration

    //                        //                    // Testing visible:
    //                        //                    map.SetTile(new Vector3Int(x, y, 0), floorTile);
    //                        //                    map.SetColor(new Vector3Int(x, y, 0), Color.red);
    //                    }
    //                }
    //            }
    //        }
    //    }


    //    return tiles;
    //    //Count = 3180 seems to sum both walls and floors
    //    //Count = 714 with the floortile = 1 check which is more similar to 932 via floorlist check
    //}

    //void ConnectClosestRooms(List<Room> allRooms)
    //{
    //    int bestDistance = 0;
    //    Coord bestTileA = new Coord(); // We'll store here which tiles resulted as "best distance"
    //    Coord bestTileB = new Coord(); // We'll store here which tiles resulted as "best distance"
    //    Room bestRoomA = new Room(); // We'll want to know from which room the best tiles come from. This is why we make the second empty constructor for room
    //    Room bestRoomB = new Room();
    //    bool possibleConnectionFound = false;

    //    // we'll go through all rooms and compare them to every other room to find the closest one
    //    foreach (Room roomA in allRooms)
    //    {
    //        possibleConnectionFound = false; // Once best connection is found, its going to move on to the next room, so we want to reset this.

    //        foreach (Room roomB in allRooms)
    //        {
    //            // case1: At some point roomA = roomB and we don't want to try to find a connecting to the same room, we'll skip ahead to the next
    //            if (roomA == roomB)
    //            {
    //                continue;
    //            }
    //            // case2: roomA is actually connected to roomB, as already has a connection we'll break, skip all of this and go to next room A
    //            if (roomA.isConnected(roomB))
    //            {
    //                possibleConnectionFound = false; // so it does not create the pagae anyway when exiting the loop and hitting "if (possibleConnectionFound)" check
    //                break;
    //            }
    //            // Look at the distance between all the edge tiles in both rooms:
    //            for (int tileIndexA = 0; tileIndexA < roomA.edgeTiles.Count; tileIndexA++)
    //            {
    //                for (int tileIndexB = 0; tileIndexB < roomB.edgeTiles.Count; tileIndexB++)
    //                {
    //                    // create a coordinate:
    //                    Coord tileA = roomA.edgeTiles[tileIndexA];
    //                    Coord tileB = roomB.edgeTiles[tileIndexB];
    //                    int distanceBetweenRooms = (int)Mathf.Pow(tileA.tileX - tileB.tileX, 2) + (int)Mathf.Pow(tileA.tileY - tileB.tileY, 2);

    //                    if (distanceBetweenRooms < bestDistance || !possibleConnectionFound)
    //                    {
    //                        bestDistance = distanceBetweenRooms;
    //                        possibleConnectionFound = true;
    //                        bestTileA = tileA;
    //                        bestTileB = tileB;
    //                        bestRoomA = roomA;
    //                        bestRoomB = roomB;
    //                    }
    //                }
    //            }
    //        }

    //        // Once we finish the foreach() and found out our connection between A and B
    //        if (possibleConnectionFound)
    //        {
    //            CreatePassage(bestRoomA, bestRoomB, bestTileA, bestTileB);
    //        }
    //    }
    //}

    //void CreatePassage(Room roomA, Room roomB, Coord tileA, Coord tileB) {

    //    // We tell them that they're now connected to eachother
    //    Room.ConnectRooms(roomA,roomB);
    //    Debug.DrawLine(CoordToWorldPoint(tileA), CoordToWorldPoint(tileB), Color.green, 100);
    //}

    // A helper method that transforms a coordinate in an actual world position
    //Vector3 CoordToWorldPoint(Coord tile) {

    //    return new Vector3(-mapWidthX/ 2 + 0.5f + tile.tileX, 2, -mapHeightY / 2 + 0.5f + tile.tileY);
    //}

    //void GetRegions() {

    //    Debug.Log("Getting regions ... ");

    //    // At this point we already can calculate listOfRegion1FloorTiles, so listOfRegion2FloorTiles could be the rest of tiles

    //    foreach (var item in listOfFloorTiles)
    //    {
    //        if (!listOfRegion1FloorTiles.Contains(item))
    //        {
    //            listOfRegion2FloorTiles.Add(item);
    //        }
    //    }


    //    // Get all floor tiles that don't have wall neighbor?
    //    //1) GET ALL FLOOR TILES
    //    //2) SET THEM TO GROUP 0
    //    //3) GO OVER THE GRID TILE, EVERYTIME A GROUP 0 IS MET, MARK IT AS NEXT REGION (1,2,...)
    //    //4)

    //    // TESTING QUEUES:
    //    //Queue<Vector2> _queue = new Queue<Vector2>();

    //    //_queue.Enqueue(new Vector2(0, 0)); // adds element to the END of the queue
    //    //_queue.Enqueue(new Vector2(1, 0));
    //    //_queue.Enqueue(new Vector2(2, 0));
    //    //_queue.Enqueue(new Vector2(3, 0));

    //    //foreach (var item in _queue)
    //    //{
    //    //    Debug.Log(item.ToString());
    //    //}

    //    //_queue.Dequeue(); // removes oldest element from the start of the queue . In this case (0,0)

    //    //foreach (var item in _queue)
    //    //{
    //    //    Debug.Log(item.ToString());
    //    //}


    //}


    public void LoadFloor()
    {
        if (decorate == null)
        {
            return;
        }
        ClearReferences();



        

        ulong seed = SaveLoad.currentSave.currentGame.seed;
        map = MapGenerator_1.Run(seed, SaveLoad.currentSave.currentGame.floor);
        mapWidthX = map.Width;
        mapHeightY = map.Height;

        decorate.UpdateTilemap(map, this.floorMap, wallMaps, GroundTilesToApply, WallTilesToApply);

        UnityEngine.Random.InitState((int)seed);

        //  Debug.Log(map.Width + " " + map.Height);
        PlacePlayer();
        PlaceEntities();
        PlaceItems();
        PlaceExit();

        SaveLoad.currentSave.currentGame.firstLoad = false;
    }
    public void LogOnStep(string message)
    {
        Debug.Log("OnStep " + message);
    }
    public void LogOnStepIn(string message)
    {
        Debug.Log("OnStepIn "+message);
    }
    public void LogOnStepOut()
    {
        Debug.Log("OnStepOut");
    }
    public void LogOnError(Exception exception)
    {
        Debug.Log("OnError "+ exception.StackTrace);
    }
    public void LogOnInit(IGenContext iGenContext)
    {
        Debug.Log("OnInit " + iGenContext.GetType()+" "+ iGenContext.ToString());
    }
    public void NextFloor()
    {
        if (decorate == null)
        {
            return;
        }
        SaveLoad.currentSave.currentGame.floor++;

        ClearReferences();

        if (GameManager._debugNoSave)
        {
            GenContextDebug.OnStep += LogOnStep;
            GenContextDebug.OnStepIn += LogOnStepIn;
            GenContextDebug.OnInit += LogOnInit;
            GenContextDebug.OnStepOut += LogOnStepOut;
            GenContextDebug.OnError += LogOnError;
        }
        ulong seed = RogueElements.MathUtils.Rand.NextUInt64();
        
        SaveLoad.currentSave.currentGame.seed = seed;

        map = MapGenerator_1.Run(seed,SaveLoad.currentSave.currentGame.floor);
        mapWidthX = map.Width;
        mapHeightY = map.Height;

        decorate.UpdateTilemap(map, this.floorMap, wallMaps, GroundTilesToApply, WallTilesToApply);

        UnityEngine.Random.InitState((int)seed);

        //  Debug.Log(map.Width + " " + map.Height);
        PlacePlayer();
        PlaceEntities();
        PlaceItems();
        PlaceExit();

        Engine.Instance.SaveState();
    }

}
