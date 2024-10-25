
using Assets.Scripts.Datas.Enum;
using System.Collections.Generic;
using UnityEngine;

public static class ItemGlobal
{
    private static Dictionary<ItemEnum, Consumable> consumables = new Dictionary<ItemEnum, Consumable>();

    public static Consumable GetConsumable(ItemEnum name)
    {
        Consumable consumable;
        if (consumables.ContainsKey(name))
        {
            return consumables[name];
        }
        else
        {
            consumable = Resources.Load<Consumable>("Items/Consumables/"+ name.ToString());
            if (consumable != null)
            {
                consumables[name] = consumable;
                consumable.typeOfItem = ItemType.Consumable;
                consumable.IDOfItem = name;
            }

        }
        return consumable;
    }
}
