using UnityEngine;
using System;
using System.Collections.Generic;
using Assets.Scripts.Datas.Enum;
using Assets.Scripts.Datas.Save;
using Assets.Scripts.Datas.Struct;

public class ItemGenerator :MonoBehaviour
{
    private int numberOfItemsPerLevel;
    public List<ItemDungeon> listOfItems = new List<ItemDungeon>();
    public List<ExitZone> listOfExits = new List<ExitZone>();

    public GameObject itemPrefab;
    public GameObject exitPrefab;

    public Transform items;

    public GameObject HasExit(int x, int y)
    {
        foreach (ExitZone item in listOfExits)
        {
            if (item.gameObject.activeSelf && item.transform.localPosition.x == x && item.transform.localPosition.y == y)
            {
                return item.gameObject;
            }
        }
        return null;
    }
    public GameObject HasItem(int x, int y)
    {
        foreach (ItemDungeon item in listOfItems)
        {
            if (item.gameObject.activeSelf && item.transform.localPosition.x == x && item.transform.localPosition.y == y)
            {
                return item.gameObject;
            }
        }
        return null;
    }
    public void Clear()
    {
        foreach (ExitZone item in listOfExits)
        {
            Destroy(item.gameObject);
        }
        foreach (ItemDungeon item in listOfItems)
        {
            Destroy(item.gameObject);
        }
        listOfExits.Clear();
        listOfItems.Clear();

    }
    public void DestroyItem(ItemDungeon item)
    {
        listOfItems.Remove(item);
        item.gameObject.SetActive(false);
        Destroy(item.gameObject);
    }

    public void SaveItems(GameSave currentGame)
    {
        currentGame.itemArray = new ItemStruct[this.listOfItems.Count];
        int c = 0;
        foreach (ItemDungeon item in listOfItems)
        {
            currentGame.itemArray[c] = item.GetStruct();
            c++;
        }
    }
    public void SaveExits(GameSave currentGame)
    {
        currentGame.exitArray = new ExitStruct[this.listOfExits.Count];
        int c = 0;
        foreach (ExitZone item in listOfExits)
        {
            currentGame.exitArray[c] = item.GetStruct();
            c++;
        }
    }

    public void GenerateConsumables(GridGenerator gridGenerator) {



        GameObject itemObject;
        ItemDungeon itemD;
        numberOfItemsPerLevel = 5;

        if (SaveLoad.currentSave.currentGame.firstLoad)
        {
            ItemStruct[] itemsArray = SaveLoad.currentSave.currentGame.itemArray;

            for (int i = 0; i < itemsArray.Length; i++)
            {
                ItemStruct item = itemsArray[i];
                itemObject = Instantiate(itemPrefab,Vector3.zero, Quaternion.identity);
                itemD = itemObject.GetComponent<ItemDungeon>();
                itemD.Init(item);
                itemObject.transform.parent = items;
                listOfItems.Add(itemD);
            }
            return;
        }

        for (int i = 0; i < numberOfItemsPerLevel; i++)
        {
            // Random vector where we'll instantiate this

            // Random Item effect our instance will have
            int _rand = UnityEngine.Random.Range(1, 4);
            Consumable consumable = ItemGlobal.GetConsumable((ItemEnum)_rand);
            Vector2 space=gridGenerator.GetEmptySpace();
            itemObject=Instantiate(itemPrefab, new Vector3(space.x , space.y, 0), Quaternion.identity);
            itemD= itemObject.GetComponent<ItemDungeon>();
            itemD.Init(consumable);

            itemObject.transform.parent = items;
            listOfItems.Add(itemD);
        }
    }
   public  void PlaceExit(GridGenerator gridGenerator)
    {
        GameObject exit;
        ExitZone exitZone;
        if (SaveLoad.currentSave.currentGame.firstLoad)
        {
            ExitStruct[] exitArray = SaveLoad.currentSave.currentGame.exitArray;

            for (int i = 0; i < exitArray.Length; i++)
            {
                ExitStruct item = exitArray[i];
                exit = Instantiate(exitPrefab, new Vector3(item.x, item.y, 0), Quaternion.identity);
                exit.transform.parent = items;
                 exitZone = exit.GetComponent<ExitZone>();
                exitZone.type = item.type;

                listOfExits.Add(exitZone);
            }
            return;
        }

        Vector2 _randomVector = gridGenerator.GetEmptySpace();
        //   Instantiate(_exit, new Vector3(_randomVector.x + 0.5f, _randomVector.y + 0.5f, 0), Quaternion.identity);
        exit = Instantiate(exitPrefab, new Vector3(_randomVector.x, _randomVector.y, 0), Quaternion.identity);
        exit.transform.parent = items;
         exitZone = exit.GetComponent<ExitZone>();
        exitZone.type = ExitType.Exit;
        // Add to list to keep track
        listOfExits.Add(exitZone);
    }

    public void ResolveItem(GameObject _item)
    {

        ItemDungeon itemDungeon = _item.GetComponent<ItemDungeon>();
        if (itemDungeon.item.IDOfItem == ItemEnum.RecoveryDisc)
        {
         //   MessageLogManager.Instance.AddToQueue(gameObject.name + " Heal HP (+10%)"); // Removing the consume-on-pickup funct, as now goes to the inventory
            MessageLogManager.Instance.AddToQueue(gameObject.name + " not implemented"); 
        }
        else if (itemDungeon.item.IDOfItem == ItemEnum.Meat)
        {
         //   MessageLogManager.Instance.AddToQueue(gameObject.name + " Food found (+1)");
            MessageLogManager.Instance.AddToQueue(gameObject.name + " not implemented"); 
        }
        else if (itemDungeon.item.IDOfItem == ItemEnum.Honey)
        {
           // MessageLogManager.Instance.AddToQueue(gameObject.name + " Food found (+1)");
            MessageLogManager.Instance.AddToQueue(gameObject.name + " not implemented");
        }
        else
        {
            MessageLogManager.Instance.AddToQueue(gameObject.name + " not implemented");
        }
        DestroyItem(itemDungeon);
    }
}
