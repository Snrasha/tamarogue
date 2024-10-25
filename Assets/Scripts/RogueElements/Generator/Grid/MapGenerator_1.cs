// <copyright file="Example3.cs" company="Audino">
// Copyright (c) Audino
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using RogueElements;
using RogueElements.Examples;
using System;
using System.Text;
using UnityEngine;
//https://github.com/audinowho/RogueElements
namespace Assets.Scripts.MapGenerator.Generator.Grid
{
    public class MapGenerator_1
    {

        public static Map RunHall(int seed,int width, int height)
        {

            var layout = new MapGen<MapGenContext>();

            // Initialize a 54x40 floorplan with which to populate with rectangular floor and halls.
            InitFloorPlanStep<MapGenContext> startGen = new InitFloorPlanStep<MapGenContext>(width, height);
            layout.GenSteps.Add(-2, startGen);

            // Create some room types to place
            var genericRooms = new SpawnList<RoomGen<MapGenContext>>
            {
                { new RoomGenSquare<MapGenContext>(new RandRange(4, 8), new RandRange(4, 8)), 10 }, // cross
                { new RoomGenRound<MapGenContext>(new RandRange(5, 9), new RandRange(5, 9)), 10 }, // round
            };

            // Create some hall types to place
            var genericHalls = new SpawnList<PermissiveRoomGen<MapGenContext>>
            {
                { new RoomGenAngledHall<MapGenContext>(0, new RandRange(3, 7), new RandRange(3, 7)), 10 },
                { new RoomGenSquare<MapGenContext>(new RandRange(1), new RandRange(1)), 20 },
            };

            // Feed the room and hall types to a path that is composed of a branching tree
            FloorPathBranch<MapGenContext> path = new FloorPathBranch<MapGenContext>(genericRooms, genericHalls)
            {
                HallPercent = 50,
                FillPercent = new RandRange(45),
                BranchRatio = new RandRange(0, 25),
            };

            layout.GenSteps.Add(-1, path);

            // Draw the rooms onto the tiled map, with 1 TILE padded on each side
            layout.GenSteps.Add(0, new DrawFloorToTileStep<MapGenContext>(1));

            


            MapGenContext context = layout.GenMap(MathUtils.Rand.NextUInt64());
            return context.Map;
        }


       public static Map Run(ulong seed,int width, int height)
        {
           // Console.Clear();
            //const string title = "3: A Map made with Rooms and Halls arranged in a grid.";

            var layout = new MapGen<MapGenContext>();


            //var startGen2 = new InitTilesStep<MapGenContext>(width, height);
            //layout.GenSteps.Add(-3, startGen2);

            //// Initialize a 6x4 grid of 10x10 cells.
            //var startGen = new InitGridPlanStep<MapGenContext>(1)
            //{
            //    CellX = 6,
            //    CellY = 6,
            //    CellWidth = 9,
            //    CellHeight = 9,
            //};

            // Initialize a 54x40 floorplan with which to populate with rectangular floor and halls.
            //InitFloorPlanStep<MapGenContext> startGen = new InitFloorPlanStep<MapGenContext>(width, height);
            //layout.GenSteps.Add(-2, startGen);

            var startGen = new InitGridPlanStep<MapGenContext>(1)
            {
                CellX = 5,
                CellY = 5,
                CellWidth = 12,
                CellHeight = 12,
            };
            layout.GenSteps.Add(-4, startGen);

            // Create a path that is composed of branches in grid lock
            var path = new GridPathBranch<MapGenContext>
            {
                RoomRatio = new RandRange(70),
                BranchRatio = new RandRange(100, 150), //0 to 50 before
            };

            

            var genericRooms = new SpawnList<RoomGen<MapGenContext>>
            {
                { new RoomGenSquare<MapGenContext>(new RandRange(4, 8), new RandRange(4, 8)), 10 }, // cross
                { new RoomGenRound<MapGenContext>(new RandRange(5, 9), new RandRange(5, 9)), 10 }, // round
            };
            path.GenericRooms = genericRooms;

            var genericHalls = new SpawnList<PermissiveRoomGen<MapGenContext>> { { new RoomGenAngledHall<MapGenContext>(50), 10 } };
            path.GenericHalls = genericHalls;

            layout.GenSteps.Add(-4, path);
            var connect = new ConnectRoomStep<MapGenContext>
            {
                ConnectFactor = new RandRange(150,200),
            };
            layout.GenSteps.Add(-3, connect);


            // Output the rooms into a FloorPlan
            layout.GenSteps.Add(-2, new DrawGridToFloorStep<MapGenContext>());

            // Draw the rooms of the FloorPlan onto the tiled map, with 1 TILE padded on each side
            layout.GenSteps.Add(0, new DrawFloorToTileStep<MapGenContext>(2));

            // Run the generator and print
            MapGenContext context = layout.GenMap(seed);
          //  Debug.Log(context.Map.Width + " " + context.Map.Height);
            return context.Map;
           // Print(context.Map, title);
        }

        public static void Print(Map map, string title)
        {
            var topString = new StringBuilder(string.Empty);
            string turnString = title;
            topString.Append($"{turnString,-82}");
            topString.Append('\n');
            for (int i = 0; i < map.Width + 1; i++)
                topString.Append("=");
            topString.Append('\n');

            for (int y = 0; y < map.Height; y++)
            {
                for (int x = 0; x < map.Width; x++)
                {
                    char tileChar;
                    RTile tile = map.Tiles[x][y];
                    switch (tile.ID)
                    {
                        case BaseMap.WALL_TERRAIN_ID:
                            tileChar = '#';
                            break;
                        case BaseMap.ROOM_TERRAIN_ID:
                            tileChar = '.';
                            break;
                        default:
                            tileChar = '?';
                            break;
                    }

                    topString.Append(tileChar);
                }

                topString.Append('\n');
            }

            Debug.Log(topString.ToString());
        }
    }
}
