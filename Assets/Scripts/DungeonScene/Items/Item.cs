using Assets.Scripts.Datas.Enum;
using Assets.Scripts.Datas.Struct;
using UnityEngine;

public abstract class Item : ScriptableObject //, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private string itemname; //item.name hides object.name, so we use "new" keyword
    [SerializeField] private string description;
    [SerializeField] private int sellPrice;

    protected Sprite sprite;


    // "=>" is a getter. We cannot get name because is private, but we can get it (not set it) through Name.
    public string ItemName { get { return itemname; } }
    public int SellPrice { get { return sellPrice; } }

    // I'm not sure if I want to go this way, commented for now:
    //public _ItemType ItemType{ get { return _ItemType.consumable; } } // Declares a property of TYPE _ItemType , with a name of ItemType

    public abstract string ColoredName { get; }

    public abstract string SpritePath { get; }

    public Sprite GetSprite()
    {
        if (sprite == null)
        {
            Texture2D texture2D = Resources.Load<Texture2D>(SpritePath);
            texture2D.filterMode = FilterMode.Point;
            texture2D.anisoLevel = 0;
            texture2D.wrapMode = TextureWrapMode.Clamp;
            sprite = Sprite.Create(texture2D, new Rect(0, 0, texture2D.width, texture2D.height), Vector2.one * 0.5f, 16);
            sprite.name = texture2D.name;
        }
        return sprite;
    }
    public abstract string GetToolTipInfoText();

    public ItemEnum IDOfItem;

    public ItemType typeOfItem;


}   
