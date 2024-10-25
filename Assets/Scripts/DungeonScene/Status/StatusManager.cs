using Assets.Scripts.Datas.Members;
using Assets.Scripts.Datas.Save;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/* Display inventory, player, and game info via UI */
public class StatusManager : MonoBehaviour
{
    public int integrity;
    public int energy;
    public int payload; // current payload
    public int capacity; // max capacity
    public TextMeshProUGUI hpText;
    public TextMeshProUGUI mpText;
    //public Text capacitytext;
    [SerializeField] private RectTransform _newItemUISLotReference; // sub-UI for Inventory

    bool areComponentReferencesLinked;
    public static bool __updateInventoryUI;
    public static bool __removeUsedItems;

    private int currentPlayerLevel;
    private int currentLevelDepth;
    public TextMeshProUGUI currentPlayerLevelText;
    public TextMeshProUGUI currentLevelDepthText;

    public void Init()
    {
        areComponentReferencesLinked = false;
    }

    public void UpdateUI()
    {
        if (!areComponentReferencesLinked)
        {
            GrabComponentReferences();
        }
        Entity player = Engine.Instance.GetPlayerEntity();

        if (player != null)
        {
            hpText.text = player.monsterStats.HP.ToString() + "/" + player.monsterStats.MaxHP.ToString();
            mpText.text = player.monsterStats.TP.ToString() + "/" + player.monsterStats.MaxTP.ToString();
        }
        if (player.level != null)
        {
            currentPlayerLevel = player.monsterStats.Level;
            currentPlayerLevelText.text = currentPlayerLevel.ToString();
        }


        currentLevelDepth = SaveLoad.currentSave.currentGame.floor;
        currentLevelDepthText.text = currentLevelDepth.ToString();
    }


    // Other components shouldn't reference the UI, but the UI just grab the info needed from other components:
    void GrabComponentReferences()
    {
        Entity player = Engine.Instance.GetPlayerEntity();

        // Grab STATUS data
        integrity = player.monsterStats.HP;
        energy = player.monsterStats.TP;
        areComponentReferencesLinked = true;
    }


}