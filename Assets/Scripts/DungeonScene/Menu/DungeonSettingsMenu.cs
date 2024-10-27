using Assets.Scripts.Datas.Save;
using Assets.Scripts.Datas.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static ConfirmationPopupStandard;

public class DungeonSettingsMenu : MonoBehaviour,ConfirmListener
{
    public VolumeBehaviour totalVolume;
    public VolumeBehaviour musicVolume;
    public VolumeBehaviour effectsVolume;

    private CanvasGroup canvasGroup;

    public Button BackButton;
    public Button ApplyButton;

    private DungeonMenuBehaviour dungeonMenuBehaviour;


    // Start is called before the first frame update
    void Init()
    {
        canvasGroup = GetComponent<CanvasGroup>();

        canvasGroup.interactable = false;
        totalVolume.SetVolume(SaveLoad.currentPrefs.totalVolume);
        musicVolume.SetVolume(SaveLoad.currentPrefs.musicVolume);
        effectsVolume.SetVolume(SaveLoad.currentPrefs.effectsVolume);
        ApplyButton.interactable = false;
        // Input.
        //Input.
    }
    public void OnClickFocus(DungeonMenuBehaviour dungeonMenuBehaviour)
    {

        this.dungeonMenuBehaviour = dungeonMenuBehaviour;
        dungeonMenuBehaviour.menuButtonsLeft.interactable = false;
        ApplyButton.interactable = false;
        canvasGroup.interactable = true;
        dungeonMenuBehaviour.eventSystem.SetSelectedGameObject(BackButton.gameObject);
    }
    public void OnClickBack()
    {
        if (ApplyButton.interactable)
        {
            ConfirmationPopupStandard.instance.OpenPopup("Cancel changes ?", this);
        }
        else
        {
            OnClickConfirmPopupYes();
        }

    }
    public void Toggle(bool toggle)
    {
        if (toggle)
        {
            Init();
        }
        gameObject.SetActive(toggle);
    }
    public void OnSliderChange()
    {
        ApplyButton.interactable = true;
        MusicMenu.MusicInstance.ReloadVolume((musicVolume.GetVolume() / 100f) * (totalVolume.GetVolume() / 100f));
    }
    public void OnClickApply()
    {
        SaveLoad.currentPrefs.totalVolume = totalVolume.GetVolume();
        SaveLoad.currentPrefs.musicVolume = musicVolume.GetVolume();
        SaveLoad.currentPrefs.effectsVolume = effectsVolume.GetVolume();

        SaveLoad.SavePrefs();
        MusicMenu.MusicInstance.ReloadVolume();
        ApplyButton.interactable = false;
        dungeonMenuBehaviour.eventSystem.SetSelectedGameObject(BackButton.gameObject);

    }

    public void OnClickConfirmPopupYes()
    {
        dungeonMenuBehaviour.menuButtonsLeft.interactable = true;
        dungeonMenuBehaviour.eventSystem.SetSelectedGameObject(dungeonMenuBehaviour.SettingsBtn.gameObject);
        canvasGroup.interactable = false;
    }

    public void OnClickConfirmPopupNo()
    {

    }
}
