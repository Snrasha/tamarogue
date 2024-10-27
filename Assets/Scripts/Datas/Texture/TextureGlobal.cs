
using System.Collections.Generic;
using UnityEngine;
namespace Assets.Scripts.Datas.Texture
{
    public static class TextureGlobal
    {
        private static Dictionary<string, TextureTama> tamas = new Dictionary<string, TextureTama>();
        private static Dictionary<string, Sprite> tamasicons = new Dictionary<string, Sprite>();

        public static TextureTama GetTextureTama(string name)
        {
            TextureTama text;
            if (tamas.ContainsKey(name))
            {
                return tamas[name];
            }
            else
            {
                Debug.Log("Tamas/" + name);

                text = new TextureTama(name);

                tamas[name] = text;

            }
            return text;
        }
        public static Sprite GetIconTama(string tama)
        {
            Sprite sprite;
            if (tamasicons.ContainsKey(tama))
            {
                return tamasicons[tama];
            }
            else
            {
              //  Debug.Log("GetIconTama " + tama);
                Texture2D texture = Resources.Load<Texture2D>("Tamas/" + tama+"/Icon");
                sprite=Sprite.Create(texture, new Rect(0,0,texture.width,texture.height), 0.5f * Vector2.one, 16f);

                tamasicons[tama] = sprite;

            }
            return sprite;
        }
    }

}