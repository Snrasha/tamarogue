
using Assets.Scripts.Datas.Save;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using static ConfirmationPopupStandard;

public class ConfirmationPopupStandard : MonoBehaviour, ConfirmListener
{

    public interface ConfirmListener{
        public void OnClickConfirmPopupYes();
        public void OnClickConfirmPopupNo();

    }

    public CanvasGroup uiCanvasGroup;
    public CanvasGroup confirmCanvasGroup;
    public EventSystem eventSystem;

    public GameObject noButton;
    public TextMeshProUGUI title;
    public float hideAlphaGroup = 0f;

    public static ConfirmationPopupStandard instance;

    public ConfirmListener confirmListener1;

    public void Awake()
    {
       // Debug.Log("ConfirmPopup Awake");

        instance = this;
        Application.wantsToQuit += WantsToQuit;

        Hide();
    }
    public void OnDestroy()
    {
      //  Debug.Log("ConfirmPopup Destroyed");
        Application.wantsToQuit -= WantsToQuit;
        instance = null;
    }


    public void OpenPopup(string title,ConfirmListener confirmListener)
    {
        
        this.title.text = title;
        confirmListener1 = confirmListener;
        Display();
        if (eventSystem != null)
        {
            eventSystem.SetSelectedGameObject(noButton);
        }
    }
    public void ClosePopup()
    {
        Hide();
        confirmListener1 = null;
    }

    public void OnClickYes()
    {
        Hide();
        if (confirmListener1 != null)
        {
            confirmListener1.OnClickConfirmPopupYes();
        }
    }

    public void OnClickNo()
    {

        Hide();
        if (confirmListener1 != null)
        {
            confirmListener1.OnClickConfirmPopupNo();
        }
    }
    private void Display()
    {
        GameManager.GetInstance().isPausedPopup = true;
        if (uiCanvasGroup != null)
        {
            uiCanvasGroup.alpha = hideAlphaGroup;
            //reduce the visibility of normal UI, and disable all interraction
            uiCanvasGroup.interactable = false;
            uiCanvasGroup.blocksRaycasts = false;
        }

        //enable interraction with confirmation gui and make visible
        confirmCanvasGroup.alpha = 1;
        confirmCanvasGroup.interactable = true;
        confirmCanvasGroup.blocksRaycasts = true;
    }
    private void Hide()
    {
     //   Debug.Log("Hide " + GameManager.GetInstance());
        GameManager.GetInstance().isPausedPopup = false;
        if (uiCanvasGroup != null)
        {
            //enable the normal ui
            uiCanvasGroup.alpha = 1;
            uiCanvasGroup.interactable = true;
            uiCanvasGroup.blocksRaycasts = true;
        }
        confirmCanvasGroup.alpha = 0f;
        confirmCanvasGroup.interactable = false;
        confirmCanvasGroup.blocksRaycasts = false;
    }

    private bool allowQuitting = false;

    // Used for quit part.
    static bool WantsToQuit()
    {
        if (instance == null)
        {
            return true;
        }
        if (!instance.allowQuitting)
        {
            instance.OpenPopup("Quit ?", instance);
            return false;
        }
        else
        {
            return true;
        }
    }


    public void OnClickConfirmPopupYes()
    {
        allowQuitting = true;
        confirmListener1 = null;


        if (SaveLoad.currentSave !=null&& SaveLoad.currentSave.loaded)
        {
            if (Engine.Instance != null)
            {
                Engine.Instance.SaveState();
            }
        }

        Application.Quit();
    }

    public void OnClickConfirmPopupNo()
    {
        allowQuitting = false;
        confirmListener1 = null;
    }
}
