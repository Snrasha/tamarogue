
using System.Collections.Generic;
using UnityEngine;

public static class TextureGlobal
{
    private static Dictionary<string, TextureTama> tamas = new Dictionary<string, TextureTama>();

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
}
