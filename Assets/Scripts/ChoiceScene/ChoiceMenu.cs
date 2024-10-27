
using Assets.Scripts.Datas.Save;
using Assets.Scripts.Datas.Struct;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static ConfirmationPopupStandard;

public class ChoiceMenu : MonoBehaviour, ConfirmListener
{

    public string tama1 = "Kobou";
    public string tama2 = "Taple";

    public Button confirmButton;

    public SlotManager slotManager;

    public void Awake()
    {
        confirmButton.interactable = true;
        slotManager.Init();
    }

    public void OnClickConfirm()
    {
        if (slotManager.slotTamaFirst.GetTama() != null && slotManager.slotTamaSecond.GetTama() != null)
        {
            tama1 = slotManager.slotTamaFirst.GetTama();
            tama2 = slotManager.slotTamaSecond.GetTama();

            ConfirmationPopupStandard.instance.OpenPopup("Start ?", this);
        }
    }
    public void OnClickBack()
    {
        GameManager.GetInstance().LoadLevel(SceneGame.Choice, SceneGame.Start);
    }


    public void OnClickConfirmPopupYes()
    {
        SaveLoad.currentSave.currentGame.monstersTeam = new List<EntitySaved>();
        SaveLoad.currentSave.currentGame.monstersTeam.Add(new EntitySaved(tama1));
        SaveLoad.currentSave.currentGame.monstersTeam.Add(new EntitySaved(tama2));

        SaveLoad.currentSave.currentGame.floor = 0;
     //   SaveLoad.currentSave.currentGame.hunger = 100;
        SaveLoad.currentSave.currentGame.firstLoad = false;

       GameManager.GetInstance().LoadLevel(SceneGame.Choice, SceneGame.Dungeon);

    }

    public void OnClickConfirmPopupNo()
    {
        ConfirmationPopupStandard.instance.eventSystem.SetSelectedGameObject(confirmButton.gameObject);
    }
}
