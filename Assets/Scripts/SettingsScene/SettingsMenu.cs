using Assets.Scripts.Datas.Save;
using Assets.Scripts.Datas.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static ConfirmationPopupStandard;

public class SettingsMenu : MonoBehaviour, ConfirmListener
{
    public VolumeBehaviour totalVolume;
    public VolumeBehaviour musicVolume;
    public VolumeBehaviour effectsVolume;

    public Button BackButton;
    public Button ApplyButton;
    // Start is called before the first frame update
    void Awake()
    {

        totalVolume.SetVolume(SaveLoad.currentPrefs.totalVolume);
        musicVolume.SetVolume(SaveLoad.currentPrefs.musicVolume);
        effectsVolume.SetVolume(SaveLoad.currentPrefs.effectsVolume);
        ApplyButton.interactable = false;
        // Input.
        //Input.
    }
    public void OnSliderChange()
    {
        
        ApplyButton.interactable = true;
        MusicMenu.MusicInstance.ReloadVolume((musicVolume.GetVolume() / 100f) * (totalVolume.GetVolume() / 100f));
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

    public void OnClickApply()
    {
        SaveLoad.currentPrefs.totalVolume = totalVolume.GetVolume();
        SaveLoad.currentPrefs.musicVolume = musicVolume.GetVolume();
        SaveLoad.currentPrefs.effectsVolume = effectsVolume.GetVolume();

        SaveLoad.SavePrefs();
        MusicMenu.MusicInstance.ReloadVolume();
        ApplyButton.interactable = false;
        ConfirmationPopupStandard.instance.eventSystem.SetSelectedGameObject(BackButton.gameObject);

    }
    public void OnClickConfirmPopupYes()
    {
        GameManager.GetInstance().LoadLevel(SceneGame.Settings, SceneGame.Start);
        MusicMenu.MusicInstance.ReloadVolume();
    }

    public void OnClickConfirmPopupNo()
    {

    }
}
