using Assets.Scripts.MapGenerator.Generator.Grid;
using RogueElements.Examples;

namespace Assets.Scripts.MapGenerator
{
    public class FiltersWalls
    {
        private int[,] filcheck;
        private int[][] filcheckwalls;
        public int[][] filters;

        public void Init()
        {
            filcheck = new int[8, 2];
            filcheck[0, 0] = -1;
            filcheck[0, 1] = 1;

            filcheck[1, 0] = 0;
            filcheck[1, 1] = 1;

            filcheck[2, 0] = 1;
            filcheck[2, 1] = 1;

            filcheck[3, 0] = -1;
            filcheck[3, 1] = 0;
            filcheck[4, 0] = 1;
            filcheck[4, 1] = 0;
            filcheck[5, 0] = -1;
            filcheck[5, 1] = -1;
            filcheck[6, 0] = 0;
            filcheck[6, 1] = -1;
            filcheck[7, 0] = 1;
            filcheck[7, 1] = -1;
            int inc = 0;

            filcheckwalls = new int[4][];
            filcheckwalls[inc++] = new int[] {0, 1, 3 };
            filcheckwalls[inc++] = new int[] { 1, 2, 4 };
            filcheckwalls[inc++] = new int[] { 3, 5, 6 };
            filcheckwalls[inc++] = new int[] { 4, 6, 7 };


            filters = new int[20][];


            inc = 0;
            filters[inc++] = new int[] { 1, 1, 1 };
            filters[inc++] = new int[] { 1, 1, 1 };
            filters[inc++] = new int[] { 1, 1, 1 };
            filters[inc++] = new int[] { 1, 1, 1 };

            filters[inc++] = new int[] { 0, 1, 1 };
            filters[inc++] = new int[] { 1, 0, 1 };
            filters[inc++] = new int[] { 1, 0, 1 };
            filters[inc++] = new int[] { 1, 1, 0 };

            filters[inc++] = new int[] {2, 0, 0 };
            filters[inc++] = new int[] { 0, 2,0 };
            filters[inc++] = new int[] { 0, 2,0 };
            filters[inc++] = new int[] { 0, 0, 2 };

            filters[inc++] = new int[] { 2, 0, 1 };
            filters[inc++] = new int[] { 0, 2, 1 };
            filters[inc++] = new int[] { 1,2, 0 };
            filters[inc++] = new int[] { 1, 0, 2 };

            filters[inc++] = new int[] { 2, 1, 0 };
            filters[inc++] = new int[] { 1, 2, 0 };
            filters[inc++] = new int[] { 0, 2,1 };
            filters[inc++] = new int[] { 0, 1, 2};
            //filters[inc++] = new int[] { 1, 1, 1, 1, 1, 1, 1, 1 };//nothing
            //filters[inc++] = new int[] { 1, 1, 1, 1, 1, 1, 1, 1 };//nothing

            //filters[inc++] = new int[] { 2, 1, 2, 0, 0, 0, 0, 0 };//bottom_join
            //filters[inc++] = new int[] { 2, 0, 2, 0, 0, 2, 1, 2 };//top_join


            //filters[inc++] = new int[] { 2, 0,2, 1, 1, 1, 1, 1 };
            //filters[inc++] = new int[] { 1, 1, 1, 1, 1, 2, 0, 2 };
            //filters[inc++] = new int[] { 1, 1, 2, 1, 0, 1, 1, 2 };
            //filters[inc++] = new int[] { 2, 1, 1, 0, 1, 2,1, 1 };

            //filters[inc++] = new int[] { 2, 0, 2, 1, 0, 1, 1, 2 };//topright_angle
            //filters[inc++] = new int[] { 2, 0, 2, 0, 1, 2, 1, 1 };//topleft_angle
            //filters[inc++] = new int[] { 1, 1, 2, 1, 0, 2, 0, 2 };//bottomright_angle
            //filters[inc++] = new int[] { 2, 1, 1, 0, 1, 2, 0, 2 };//bottomleft_angle

            //filters[inc++] = new int[] { 0, 1, 1, 1, 1, 1, 1, 1 };//topleft_corner
            //filters[inc++] = new int[] { 1, 1, 0, 1, 1, 1, 1, 1 };//topright_corner

            //filters[inc++] = new int[] { 1, 1, 1, 1, 1, 1, 1, 0 };//bottomright_corner
            //filters[inc++] = new int[] { 1, 1, 1, 1, 1, 0, 1, 1 };//bottomleft_corner



        }


        public int GetWallTileIndex(int x, int y,int index, int mapWidth, int mapHeight, Map map)
        {

            if (x > 0 && x < mapWidth - 1 && y > 0 && y < mapHeight - 1)
            {
                int check;
                int curmax = 0;
                int idxsaved = 0;
                for (int c = index; c < filters.Length; c+=4)
                {
                    check = 0;
                    for (int d = 0; d < filters[0].Length; d++)
                    {

                        int v = filters[c][d];
                        if (v == 2)
                        {
                            check++;
                        }
                        else
                        {
                            int idx = filcheckwalls[index][d];
                            check += v == (map.Tiles[x + filcheck[idx, 0]][y + filcheck[idx, 1]].ID == BaseMap.WALL_TERRAIN_ID ? 1 : 0) ? 1 : 0;
                        }
                    }
                    if (check > curmax)
                    {
                        curmax = check;
                        idxsaved = c;
                    }
                }
                return idxsaved;
            }
            return -1;

        }



    }
}