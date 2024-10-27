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
    public TextMeshProUGUI hpText;
    public TextMeshProUGUI tpText;
    public TextMeshProUGUI currentPlayerLevelText;
    public TextMeshProUGUI currentLevelFloorText;


    public void UpdateUI()
    {

        Entity player = Engine.Instance.GetPlayerEntity();
        if (player != null)
        {
            hpText.text = player.monsterStats.HP.ToString() + "/" + player.monsterStats.MaxHP.ToString();
            tpText.text = player.monsterStats.TP.ToString() + "/" + player.monsterStats.MaxTP.ToString();
            currentPlayerLevelText.text = player.monsterStats.Level.ToString();
        }
        currentLevelFloorText.text = SaveLoad.currentSave.currentGame.floor.ToString();

    }
}