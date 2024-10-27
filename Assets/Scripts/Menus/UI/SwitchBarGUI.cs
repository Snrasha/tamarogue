using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SwitchBarGUI : MonoBehaviour
{
    public TextMeshProUGUI numberText;
    public Button topButton;
    public Button bottomButton;
    public int minValue = 0;
    public int maxValue = 3;
    public int currentValue = 0;

    public MyIntEvent OnChangeEvent = new MyIntEvent();




    // Start is called before the first frame update
    void Awake()
    {
        topButton.onClick.AddListener(OnClickTop);
        bottomButton.onClick.AddListener(OnClickBottom);

    }

    private void OnClickTop()
    {
        if (currentValue > minValue)
        {
            currentValue--;
        }
        UpdateText();
        OnChangeEvent.Invoke(1);
    }
    private void OnClickBottom()
    {
        if (currentValue < maxValue)
        {
            currentValue++;
        }
        UpdateText();
        OnChangeEvent.Invoke(-1);
    }
    public void UpdateText()
    {
        numberText.text = "" + currentValue;
       
    }

}
[System.Serializable]
public class MyIntEvent : UnityEvent<int>
{
}
