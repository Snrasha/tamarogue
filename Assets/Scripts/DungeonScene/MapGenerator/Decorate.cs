using Assets.Scripts.MapGenerator.Generator.Grid;
using RogueElements.Examples;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Assets.Scripts.MapGenerator
{
    public class Decorate 
    {
        private FiltersWalls filtersWalls;
        public Decorate()
        {
            filtersWalls = new FiltersWalls();
            filtersWalls.Init();
        }

        public void UpdateTilemap(Map map, Tilemap groundMap, Tilemap[] wallMaps, Tile[] goundtiles, Tile[] walltiles)
        {


           // Debug.Log(wallMaps.Length + " " + walltiles.Length);
            for (int y = 0; y < map.Height; y++)
            {
                for (int x = 0; x < map.Width; x++)
                {
                    Vector3Int pos = new Vector3Int(x, y, 0);

                    groundMap.SetTile(pos, goundtiles[0]);
                    if (map.Tiles[x][ y].ID == BaseMap.WALL_TERRAIN_ID)
                    {
                        // none
                        // wallMap.SetTile(pos, GetTile(0));
                        for (int i = 0; i < wallMaps.Length; i++)
                        {
                            Tilemap wallMap = wallMaps[i];

                            int idx = filtersWalls.GetWallTileIndex(x, y,i, map.Width, map.Height, map);

                            if (idx >= 0)
                            {
                                
                                wallMap.SetTile(pos, walltiles[idx]);
                            }
                            else
                            {
                                wallMap.SetTile(pos, walltiles[i]);
                            }
                        }
                    }
                }
            }
        }

    }
}