
using System.Text;
using UnityEngine;

[CreateAssetMenu(fileName = "New Consumable", menuName ="Items/Consumable")]
public class Consumable : Item
{
    [SerializeField] private Rarity rarity;
    [SerializeField] private string useText = "Use: Something";

    

    public Rarity Rarity { get { return rarity; } }

    //public override string ColoredName => throw new System.NotImplementedException();
    public override string ColoredName {
        get {
            string hexcolor = ColorUtility.ToHtmlStringRGB(rarity.TextColor);
            return $"<color=#{hexcolor}> { ItemName } </color>";
        }
    }
    public override string SpritePath
    {
        get
        {
            return "Sprites/Items/Consumables/" + name;
        }
    }

    
    public override string GetToolTipInfoText()
    {
        // String builder to avoid lots of garbage and garbage collection
        StringBuilder builder = new StringBuilder();

        // Now we build the string
        builder.Append(Rarity.name).AppendLine();
        builder.Append("<color=green>Use: ").Append(useText).Append("</color>").AppendLine();
        builder.Append("Sell Price: ").Append(SellPrice).Append(" credits");

        // Finally we return an unique string, not dozens:
        return builder.ToString();

    }
}
