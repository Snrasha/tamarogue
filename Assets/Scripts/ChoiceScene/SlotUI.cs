using Assets.Scripts.Datas.Texture;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SlotUI : MonoBehaviour
{
    [SerializeField]
    private Image icon;

    [SerializeField]
    private Button button;

    private string tama;

    private bool enabledbtn = false;

    public string GetTama()
    {
        return tama;
    }
    public void ToggleButton(bool flag)
    {
        ToggleButton(flag, false);
    }
    public void ToggleButton(bool flag,bool notrequiredTama)
    {
        button.interactable = flag && (notrequiredTama || tama!=null);
        enabledbtn = flag;
    }

    public void SetTama(string tama)
    {
        this.tama = tama;
        if (tama == null)
        {
            button.interactable = false;
            icon.enabled = false;
        }
        else
        {
            button.interactable = enabledbtn;
            icon.enabled = true;
            icon.sprite = TextureGlobal.GetIconTama(tama);
        }
    }

    public void SetListener(SlotManager slotManager, int index)
    {
        EventTrigger eventTrigger = GetComponent<EventTrigger>();
        int temp = index;

        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerEnter;

        entry.callback.AddListener((eventData) => { slotManager.OnSelectedItemSlot(temp); });
        eventTrigger.triggers.Add(entry);

        entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.Select;

        entry.callback.AddListener((eventData) => { slotManager.OnSelectedItemSlot(temp); });
        eventTrigger.triggers.Add(entry);

        entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerClick;

        entry.callback.AddListener((eventData) => { slotManager.OnClickItemSlot(temp); });
        eventTrigger.triggers.Add(entry);

        entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.Submit;

        entry.callback.AddListener((eventData) => { slotManager.OnClickItemSlot(temp); });
        eventTrigger.triggers.Add(entry);
    }

}
