using Assets.Scripts.MapGenerator.Generator.Grid;
using RogueElements.Examples;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class Minimap : MonoBehaviour
{

    private Texture2D minimapTex;
    private Texture2D minimapTexAll;

    private Image image;
    private Color[] colors;
    private int paddingX;
    private int paddingY;

    public void Init()
    {
        colors = new Color[6];

        ColorUtility.TryParseHtmlString("#181425", out colors[0]);
        ColorUtility.TryParseHtmlString("#8b9bb4", out colors[1]);
        ColorUtility.TryParseHtmlString("#04ff00", out colors[2]);
        ColorUtility.TryParseHtmlString("#ff0044", out colors[3]);
        ColorUtility.TryParseHtmlString("#feae34", out colors[4]); 
        ColorUtility.TryParseHtmlString("#2ce8f5", out colors[5]);
    }
    public void UpdateMap(Map map, int playerx, int playery)
    {
        int padding = 4;

        for (int j = -padding; j <= padding; j++)
        {
            for (int i = -padding; i <= padding; i++)
            {
                int x = playerx + i;
                int y = playery + j;
                if (x >= 0 && y >= 0 && x < map.Width && y < map.Height)
                {
                    Vector3Int dest = new Vector3Int(x, y, 0);

                    if (Engine.Instance.enemyGenerator.HasEnemy(x, y))
                    {
                        WriteTile(dest, 3);
                        continue;
                    }
                    if (Engine.Instance.itemGenerator.HasItem(x, y))
                    {
                        WriteTile(dest, 2);
                        continue;
                    }
                    if (Engine.Instance.itemGenerator.HasExit(x, y))
                    {
                        WriteTile(dest, 4);
                        continue;
                    }

                    WriteTile(dest, (map.Tiles[x][y].ID == BaseMap.ROOM_TERRAIN_ID) ? 1 : 0);
                   // Debug.Log(dest+" "+ ((map.Tiles[x][y].ID == BaseMap.ROOM_TERRAIN_ID) ? 1 : 0));
                }

            }
        }
        WriteTile(new Vector3Int(playerx, playery, 0), 5);
     //   Debug.Log(playerx + "hhhh " + playery + " " + paddingX + " " + paddingY);

        RedrawMiniMap(playerx, playery);
    }

    public void ResetTiles(Map map)
    {
        RectTransform rectTransform = GetComponent<RectTransform>();
        image = GetComponent<Image>();
        paddingX = (int)(rectTransform.rect.width / 4);
        paddingY = (int)(rectTransform.rect.height / 4);
        //Debug.Log(paddingX + " " + paddingY);
         minimapTexAll = new Texture2D(map.Width+ paddingX * 2, map.Height + paddingY*2);
        minimapTexAll.filterMode = FilterMode.Point;
        minimapTexAll.anisoLevel = 0;
        minimapTexAll.wrapMode = TextureWrapMode.Clamp;
        var fillColorArray = new Color[minimapTexAll.width * minimapTexAll.height];
        
        for (var i = 0; i < fillColorArray.Length; ++i)
        {
            fillColorArray[i] = colors[0]; 
        }
        minimapTexAll.SetPixels(fillColorArray);
        

        minimapTex = new Texture2D(paddingX, paddingY);
        minimapTex.filterMode = FilterMode.Point;
        minimapTex.anisoLevel = 0;
        minimapTex.wrapMode = TextureWrapMode.Clamp;
     //   Debug.Log(minimapTex.width + " " + minimapTex.height);

        Sprite sprite = Sprite.Create(minimapTex, new Rect(0,0,minimapTex.width,minimapTex.height), new Vector2(0.5f,0.5f), 8);
        sprite.name =  "minimapTex";
        image.sprite = sprite;
       // minimapTex.set
        for (int y = 0; y < map.Height; y++)
        {
            for (int x = 0; x < map.Width; x++)
            {
                Vector3Int pos = new Vector3Int(x ,y, 0);
                WriteTile(pos, 0);
            }
        }
        RedrawMiniMap(map.Width / 2, map.Height / 2);
    }
    public void RedrawMiniMap(int playerx,int playery)
    {
        //   Debug.Log(minimapTexAll.GetPixels(playerx, playery,  paddingX, paddingY).Length);

     //    Debug.Log(minimapTexAll.GetPixels(playerx+ paddingX/2, playery + paddingY / 2, paddingX , paddingY).Length);
     //    Debug.Log(paddingX* paddingY);

        minimapTex.SetPixels(minimapTexAll.GetPixels(playerx + paddingX / 2, playery + paddingY / 2, paddingX, paddingY));
        minimapTex.Apply();
    }


    public void WriteTile(Vector3Int pos, int v)
    {
      //  Debug.Log(pos.x + " " + pos.y+" "+v);

        minimapTexAll.SetPixel(pos.x+ paddingX, pos.y+ paddingY, colors[v]);
    }
}
