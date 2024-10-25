
using Newtonsoft.Json;
using System;
using UnityEngine;

public class TextureTama
    {
        public TextureOrb textureOrbIdle;
        public TextureOrb textureOrbRun;
        public TextureOrb textureOrbWalk;
        public TextureOrb textureOrbShadowIdle;
        public TextureOrb textureOrbShadowRun;
        public TextureOrb textureOrbShadowWalk;

        public Floaty floaty;
    public Sprite[][] spritesIdle = new Sprite[4][];
    public Sprite[][] spritesWalk = new Sprite[4][];
    public Sprite[][] spritesRun = new Sprite[4][];
    public Sprite[][] spritesShadowIdle = new Sprite[4][];
    public Sprite[][] spritesShadowWalk = new Sprite[4][];
    public Sprite[][] spritesShadowRun = new Sprite[4][];
    public string tama;
    public int FramesIdle = 4;
    public int FramesWalk = 0;
    public int FramesRun = 4;

    public bool IsSimpleSheet;
    public float Duration = 0.15f;//0.8f

    public float heightToAdd;

    public TextureTama(string tama)
    {
        this.tama = tama;
        LoadJson();
        LoadTexture();

       SetSprites(textureOrbIdle, 0);
        SetSprites(textureOrbWalk, 1);
        SetSprites(textureOrbRun, 2);
        SetSprites(textureOrbShadowIdle, 3);
        SetSprites(textureOrbShadowWalk, 4);
        SetSprites(textureOrbShadowRun, 5);
    }

    public void LoadJson()
    {
        string filePath = "Sprites/Tamas/" + tama + "/Floaty";

        TextAsset targetFile = Resources.Load<TextAsset>(filePath);
        if (targetFile == null)
        {
            return;
        }
        try
        {
            floaty = JsonConvert.DeserializeObject<Floaty>(targetFile.text);
        }
        catch (Exception ex)
        {
            Debug.LogError($"Error parsing Floaty.json: {ex.Message}");
        }
        if (floaty.duration < 0.05)
        {
            floaty.duration = 0.15;
        }
        if (floaty.duration > 5)
        {
            floaty.duration = 0.15;
        }
        if (floaty.durationFlatland < 0.05)
        {
            floaty.durationFlatland = 0.15;
        }
        if (floaty.durationFlatland > 5)
        {
            floaty.durationFlatland = 0.15;
        }


    }

    public void LoadTexture()
    {
        UnityEngine.Object[] textures = Resources.LoadAll("Sprites/Tamas/" + tama, typeof(Texture2D));
        foreach (UnityEngine.Object texture in textures)
        {
            if (texture is Texture2D)
            {
                Texture2D texture2D = (Texture2D)texture;

                string filename = texture.name.Split('.')[0];

                if (filename.StartsWith("Orb"))
                {
                    bool flatland = filename.Contains("Flatland");
                    bool aztland = filename.Contains("Aztland");
                    bool hasDirection = filename.StartsWith("Orb4");
                    bool walk = filename.Contains("Walk");
                    bool run = filename.Contains("Run");
                    bool shadow = filename.Contains("Shadow");
                    bool altcolor = filename.Contains("AltColor");
                    if (altcolor)
                    {
                        continue;
                    }

                    string[] split = filename.Split('_');
                    int frames;
                    try
                    {
                        frames = int.Parse(split[split.Length - 1]);
                    }
                    catch
                    {
                        frames = 4;
                    }

                    TextureOrb textureOrb = new TextureOrb();
                    textureOrb.texture = texture2D;
                    textureOrb.hasDirection = hasDirection;
                    textureOrb.frames = frames;

                    if (shadow)
                    {
                        if (walk)
                        {
                            textureOrbShadowWalk = textureOrb;
                        }
                        else if (run)
                        {
                            textureOrbShadowRun = textureOrb;
                        }
                        else
                        {
                            textureOrbShadowIdle = textureOrb;
                        }
                    }
                    else if (flatland)
                    {

                    }
                    else if (aztland)
                    {
                    }
                    else
                    {
                        if (walk)
                        {
                            textureOrbWalk = textureOrb;
                        }
                        else if (run)
                        {
                            textureOrbRun = textureOrb;
                        }
                        else
                        {
                            textureOrbIdle = textureOrb;
                        }
                    }

                }
            }
        }
        if (textureOrbRun != null && textureOrbWalk == null)
        {
            textureOrbWalk = textureOrbRun;
        }
        if (textureOrbRun == null && textureOrbWalk != null)
        {
            textureOrbRun = textureOrbWalk;
        }
        if (textureOrbShadowRun != null && textureOrbShadowWalk == null)
        {
            textureOrbShadowWalk = textureOrbShadowRun;
        }
        if (textureOrbShadowRun == null && textureOrbShadowWalk != null)
        {
            textureOrbShadowRun = textureOrbShadowWalk;
        }
    }
    public void SetSprites(TextureOrb textureorb, int type)
    {
        if (textureorb == null)
        {
            return;
        }
        SetSprites(textureorb.texture, textureorb.hasDirection, type, textureorb.frames);
    }
    public void SetSprites(Texture2D texture, bool hasDirection, int type, int nbframes)
    {
        if (texture == null)
        {
            return;
        }


        if (type == 0)
        {
            FramesIdle = nbframes;
            SetSprites(texture, hasDirection, spritesIdle, nbframes);
            heightToAdd = (texture.height - 16) / 100f;
        }
        else if (type == 1)
        {
            FramesWalk = nbframes;
            SetSprites(texture, hasDirection, spritesWalk, nbframes);

        }
        else if (type == 2)
        {
            FramesRun = nbframes;
            SetSprites(texture, hasDirection, spritesRun, nbframes);
        }
        else if (type == 3)
        {
            SetSprites(texture, hasDirection, spritesShadowIdle, nbframes);
        }
        else if (type == 4)
        {
            SetSprites(texture, hasDirection, spritesShadowWalk, nbframes);
        }
        else if (type == 5)
        {
            SetSprites(texture, hasDirection, spritesShadowRun, nbframes);
        }
    }

    private void SetSprites(Texture2D texture, bool hasDirection, Sprite[][] sprites, int nbframes)
    {

        int num = texture.width / nbframes;
        int num2;
        IsSimpleSheet = !hasDirection;

        if (IsSimpleSheet)
        {
            num2 = texture.height;
            sprites[0] = new Sprite[nbframes];
            for (int j = 0; j < nbframes; j++)
            {
                Rect rect = new Rect(j * num, 0f, num, num2);
                sprites[0][j] = Sprite.Create(texture, rect, 0.5f * Vector2.one, 16f);
                sprites[0][j].name = texture.name;
            }
        }
        else
        {
            num2 = texture.height / 4;
            for (int k = 0; k < 4; k++)
            {
                sprites[k] = new Sprite[nbframes];
                for (int l = 0; l < nbframes; l++)
                {
                    Rect rect2 = new Rect(l * num, (3 - k) * num2, num, num2);
                    sprites[k][l] = Sprite.Create(texture, rect2, 0.5f * Vector2.one, 16f);
                    sprites[k][l].name = texture.name;

                }
            }
        }
    }
}
