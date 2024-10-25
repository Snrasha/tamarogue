using Assets.Scripts.Datas.Save;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{

    public TextMeshProUGUI versionText;

    public Button continueButton;

    public void Awake()
    {
        versionText.text = "V" + Application.version;

       
        continueButton.interactable = SaveLoad.currentSave.gameInProgress;
        if (continueButton.interactable)
        {
            ConfirmationPopupStandard.instance.eventSystem.SetSelectedGameObject(continueButton.gameObject);
        }
    }
    public void OnClickSettings()
    {
        GameManager.GetInstance().LoadLevel(SceneGame.Start, SceneGame.Settings);
    }

    public void OnClickOnContinue()
    {
        GameManager.GetInstance().LoadLevel(SceneGame.Start, SceneGame.Dungeon);

    }
    public void OnClickOnNewGame()
    {
        GameManager.GetInstance().LoadLevel(SceneGame.Start, SceneGame.Choice);

    }
    public void OnClickQuit()
    {
        ConfirmationPopupStandard.instance.OpenPopup("Quit ?", ConfirmationPopupStandard.instance);
    }


    public void OnClickCredits()
    {
        GameManager.GetInstance().LoadLevel(SceneGame.Start, SceneGame.Credits);
    }

}
