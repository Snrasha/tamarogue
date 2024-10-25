using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ToolTipManager : MonoBehaviour
{

    [SerializeField] private RectTransform toolTipObject; // UI
    [SerializeField] private TextMeshProUGUI infoText;
    [SerializeField] private Vector3 offset;
    [SerializeField] private float padding; // So when the toolTip goes to the borders is still readable and stays within the screen

    public static ToolTipManager Instance;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        this.gameObject.SetActive(false);
    }
    public void OnDestroy()
    {
        Instance = null;
    }
       
    

    private void Update()
    {
        // We want this to follow the cursor
        FollowCursor();
    }

    private void FollowCursor()
    {
        if (!this.gameObject.activeSelf) { return; }

        Vector3 newPos = Input.mousePosition + offset;
        newPos.z = 0f;
        float rightEdgeToScreenEdgeDistance = Screen.width - (newPos.x + toolTipObject.rect.width * 0.5f) - padding;
        if (rightEdgeToScreenEdgeDistance < 0)
        {
            newPos.x += rightEdgeToScreenEdgeDistance;
        }
        float leftEdgeToScreenEdgeDistance = 0 - (newPos.x - toolTipObject.rect.width * 0.5f) + padding;
        if (leftEdgeToScreenEdgeDistance > 0)
        {
            newPos.x += leftEdgeToScreenEdgeDistance;
        }
        float topEdgeToScreenEdgeDistance = Screen.height - (newPos.y + toolTipObject.rect.height * 0.5f) - padding;
        if (topEdgeToScreenEdgeDistance < 0)
        {
            newPos.y += topEdgeToScreenEdgeDistance;
        }

        toolTipObject.transform.position = newPos;
    }

    public void DisplayInfo(Item item) {


        Debug.Log("called successfully");
        StringBuilder builder = new StringBuilder();

        builder.Append("<size=14>").Append(item.ColoredName).Append("</size>").AppendLine();
        builder.Append(item.GetToolTipInfoText());

        infoText.text = builder.ToString(); // We set it in the UI

        this.gameObject.SetActive(true); // So we activate it
        LayoutRebuilder.ForceRebuildLayoutImmediate(toolTipObject); // This resizes correctly the RectTransform we pass through
    }

    public void HideInfo() {

        this.gameObject.SetActive(false); // So we hide it

    }

}
