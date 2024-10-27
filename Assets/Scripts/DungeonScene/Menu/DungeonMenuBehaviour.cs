using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DungeonMenuBehaviour : MonoBehaviour
{
    [SerializeField]
    private CanvasGroup menuCanvasGroup;

    [SerializeField]
    private CanvasGroup gameCanvasGroup;

    [SerializeField]
    private Button swapTamaButton;

    [SerializeField]
    private GameObject ControlsMenu;
    [SerializeField]
    private DungeonSettingsMenu SettingsMenu;
    [SerializeField]
    private Button ControlsBtn;
    [SerializeField]
    public Button SettingsBtn;
    public EventSystem eventSystem;

    public CanvasGroup menuButtonsLeft;


    

    

    public void Init()
    {

        eventSystem = ConfirmationPopupStandard.instance.eventSystem;
        Close();
        SetControlsBtnListener();
       SetSettingsBtnListener();
    }

    public void Open()
    {
        menuButtonsLeft.interactable = true;

        if (eventSystem != null)
        {
            eventSystem.SetSelectedGameObject(swapTamaButton.gameObject);
        }
        if (gameCanvasGroup != null)
        {
            gameCanvasGroup.alpha = 1f;
            //reduce the visibility of normal UI, and disable all interraction
            gameCanvasGroup.interactable = false;
            gameCanvasGroup.blocksRaycasts = false;
        }

        //enable interraction with confirmation gui and make visible
        menuCanvasGroup.alpha = 1;
        menuCanvasGroup.interactable = true;
        menuCanvasGroup.blocksRaycasts = true;
    }
    public void Close()
    {
        ControlsMenu.SetActive(false);
        SettingsMenu.Toggle(false);
        GameManager.GetInstance().isPausedPopup = false;
        if (gameCanvasGroup != null)
        {
            //enable the normal ui
            gameCanvasGroup.alpha = 1;
            gameCanvasGroup.interactable = true;
            gameCanvasGroup.blocksRaycasts = true;
        }
        menuCanvasGroup.alpha = 0f;
        menuCanvasGroup.interactable = false;
        menuCanvasGroup.blocksRaycasts = false;
    }

    public void OnClickSwapTama()
    {
        Engine.Instance.SwapPlayer();
    }
    public void OnClickQuit()
    {
        ConfirmationPopupStandard.instance.OpenPopup("Save & Quit ?", ConfirmationPopupStandard.instance);
    }
    public void OnClickSettings()
    {
        SettingsMenu.OnClickFocus(this);

       // SettingsMenu.Toggle(!SettingsMenu.gameObject.activeSelf);
    }
    public void OnClickControls()
    {
        ControlsMenu.SetActive(!ControlsMenu.activeSelf);
    }
    public void OnClickTechs()
    {

    }
    public void OnClickBag()
    {

    }
    public void OnSelectControl()
    {
        ControlsMenu.SetActive(true);
    }
    public void OnDeSelectControl()
    {
        ControlsMenu.SetActive(false);
    }
    public void OnSelectSettings()
    {
        SettingsMenu.Toggle(true);
    }
    public void OnDeSelectSettings()
    {
        if (menuButtonsLeft.interactable)
        {
            SettingsMenu.Toggle(false);
        }
    }
    public void SetControlsBtnListener()
    {
        EventTrigger eventTrigger = ControlsBtn.GetComponent<EventTrigger>();

        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerEnter;

        entry.callback.AddListener((eventData) => { OnSelectControl(); });
        eventTrigger.triggers.Add(entry);

        entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.Select;

        entry.callback.AddListener((eventData) => { OnSelectControl(); });
        eventTrigger.triggers.Add(entry);



        entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerExit;

        entry.callback.AddListener((eventData) => { OnDeSelectControl(); });
        eventTrigger.triggers.Add(entry);

        entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.Deselect;

        entry.callback.AddListener((eventData) => { OnDeSelectControl(); });
        eventTrigger.triggers.Add(entry);

    }
    public void SetSettingsBtnListener()
    {
        EventTrigger eventTrigger = SettingsBtn.GetComponent<EventTrigger>();

        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerEnter;

        entry.callback.AddListener((eventData) => { OnSelectSettings(); });
        eventTrigger.triggers.Add(entry);

        entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.Select;

        entry.callback.AddListener((eventData) => { OnSelectSettings(); });
        eventTrigger.triggers.Add(entry);



        entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerExit;

        entry.callback.AddListener((eventData) => { OnDeSelectSettings(); });
        eventTrigger.triggers.Add(entry);

        entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.Deselect;

        entry.callback.AddListener((eventData) => { OnDeSelectSettings(); });
        eventTrigger.triggers.Add(entry);

    }



}
