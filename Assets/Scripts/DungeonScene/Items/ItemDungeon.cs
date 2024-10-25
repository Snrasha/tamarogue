using Assets.Scripts.Datas.Enum;
using Assets.Scripts.Datas.Struct;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/* ToolTip for GameObjects in the scene: Works by click-hold over the object*/
public class ItemDungeon : MonoBehaviour
{
    //[SerializeField] private Item item;
    public Item item; // We make it public now so we can reach it from ItemGenerator.

    public void Init(Item item2)
    {
        item = item2;
        GetComponent<SpriteRenderer>().sprite = item.GetSprite();
    }
    public void Init(ItemStruct itemStruct)
    {
        Consumable consumable=ItemGlobal.GetConsumable(itemStruct.itemEnum);
        transform.localPosition = new Vector3(itemStruct.x, itemStruct.y, 0);
        Init(consumable);
    }

    public ItemStruct GetStruct()
    {
        return new ItemStruct()
        {
            x = (int)transform.localPosition.x,
            y = (int)transform.localPosition.y,
            itemEnum = item.IDOfItem,
            itemType = item.typeOfItem,
        };
        
        //ItemStruct itemStruct = new ItemStruct();
        //itemStruct.x = (int)transform.localPosition.x;
        //itemStruct.y = (int)transform.localPosition.y;
        //itemStruct.itemEnum = item.IDOfItem;
        //itemStruct.itemType = item.typeOfItem;
        //return itemStruct;
    }


    //// Shows tooltip if click-hold mouse
    //public void OnMouseDown ()
    //{
    //  ToolTipManager.Instance.DisplayInfo(item);
    //    Debug.Log("click");
    //}

    //// Hides tooltip if mouse is released
    //public void OnMouseUp()
    //{
    //    ToolTipManager.Instance.HideInfo();
    //    Debug.Log("unclick");
    //}

    //// Shows tooltip if mouse is over the GameObject and "I" (Inspect) is pressed
    //public void OnMouseOver()
    //{
    //    if (Input.GetKeyDown(KeyCode.P))
    //    {
    //        ToolTipManager.Instance.DisplayInfo(item);
    //        Debug.Log("hover + p");
    //    }
    //}
    //// Hides tooltip if mouse exits the GameObject collider area
    //public void OnMouseExit()
    //{
    //    ToolTipManager.Instance.HideInfo();
    //}
}
