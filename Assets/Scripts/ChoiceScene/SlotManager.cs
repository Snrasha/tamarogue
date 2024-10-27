using Assets.Scripts.Datas.Enum.Stats;
using Assets.Scripts.Datas.Members;
using Assets.Scripts.Datas.Save;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SlotManager : MonoBehaviour
{

    public SlotUI slotTamaFirst;
    public SlotUI slotTamaSecond;

    public EventSystem eventSystem;

    private SlotUI selectedSlotTeam;


    [SerializeField]
    private GameObject grid;

    [SerializeField]
    private SwitchBarGUI switchBar;

    [SerializeField]
    private GameObject prefabTamaSlot;

    private const int maxcolumn = 4;
    private const int maxrow = 4;
    private SlotUI[,] tamaSlots;
    List<Species> species;

    private int currentRow;
    public void Init()
    {
        switchBar.OnChangeEvent.AddListener(OnChangeRow);

        species = SaveLoad.currentSave.unlockedMonsters;

        //for (int i = 1; i < 40; i++)
        //{
        //    species.Add((Species)i);
        //}

        foreach (Transform child in grid.transform)
        {
            Destroy(child.gameObject);
        }
        tamaSlots = new SlotUI[maxcolumn, maxrow];

        for (int j = 0; j < maxrow; j++)
        {
            for (int i = 0; i < maxcolumn; i++)
            {
                GameObject slot = Instantiate(prefabTamaSlot, grid.transform);
                SlotUI slotscript = slot.GetComponent<SlotUI>();
                slotscript.SetListener(this, i + j * maxcolumn);
                slotscript.SetTama(null);
                tamaSlots[i, j] = slotscript;
            }
        }

        slotTamaFirst.SetListener(this, -1);
        slotTamaSecond.SetListener(this, -2);


        ResetMaxValueScrollView();
        SetItems(0);
        ToggleAllButtons(false);
        eventSystem.SetSelectedGameObject(slotTamaFirst.gameObject);
    }
    private void ToggleAllButtons(bool toggle)
    {
        for (int j = 0; j < maxrow; j++)
        {
            for (int i = 0; i < maxcolumn; i++)
            {
                tamaSlots[i, j].ToggleButton(toggle);
            }
        }
        slotTamaFirst.ToggleButton(!toggle,true);
        slotTamaSecond.ToggleButton(!toggle,true);
        if (toggle)
        {
            eventSystem.SetSelectedGameObject(tamaSlots[0, 0].gameObject);
        }

    }
    private void ResetMaxValueScrollView()
    {
        switchBar.maxValue = species.Count / (maxrow * maxcolumn) + 1;
    }
    public void OnChangeRow(int order)
    {
        SetItems(switchBar.currentValue);
    }
    public void OnClickItemSlot(int index)
    {
        if (index == -1)
        {
            selectedSlotTeam = slotTamaFirst;
            ToggleAllButtons(true);
            return;
        }
        else if (index == -2)
        {
            selectedSlotTeam = slotTamaSecond;
            ToggleAllButtons(true);
            return;
        }


        // Set Tama
        SlotUI item = tamaSlots[index % maxcolumn, index / maxcolumn];


        selectedSlotTeam.SetTama(item.GetTama());
        eventSystem.SetSelectedGameObject(selectedSlotTeam.gameObject);
        ToggleAllButtons(false);
        //item.SetItem(itemInventory);
    }

    public void OnSelectedItemSlot(int index)
    {
        if (index == -1)
        {
            return;
        }
        else if (index == -2)
        {
            return;
        }
        SlotUI item = tamaSlots[index % maxcolumn, index / maxcolumn];

        // Display stats
    }

    public void SetItems(int row)
    {

        species.Sort();

        switchBar.currentValue = row;
        switchBar.UpdateText();

        currentRow = row;

        int index = 0;
        int nb = 0;
        int nbmin = row * (maxcolumn * maxrow);
        int max = (maxcolumn * maxrow);

        foreach (Species item in species)
        {
         //   Debug.Log("Item " + item);
            if (nb < nbmin)
            {
                nb++;
                continue;
            }
            if (index >= max)
            {
                break;
            }
            //   Debug.Log((index % maxcolumn) + " " + (index / maxcolumn));
            tamaSlots[index % maxcolumn, index / maxcolumn].SetTama(item.ToString());
            index++;
        }
        for (; index < max; index++)
        {
            tamaSlots[index % maxcolumn, index / maxcolumn].SetTama(null);
        }

    }
}
