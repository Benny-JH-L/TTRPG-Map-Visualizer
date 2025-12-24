using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabGroup : MonoBehaviour
{
    private static string _debugStart = "[Tab Group] ";
    public List<TabButton> tabButtons;

    // not used--
    public Color tabIdle;
    public Color tabHover;
    //--

    public Color tabActive;
    [Range(0.1f, 0.5f)]
    public float tabHoverBrightness = 0.2f;
    //[Range(-0.01f, -0.3f)]
    //public float tabActiveBrightness = -0.01f;

    public TabButton selectedTab;
    public List<GameObject> objectsToSwap;  
        // `tabButton` element `assignedIndex` will swap to GameObject with the same index in `objetsToSwap`
    public void Subscribe(TabButton button)
    {
        if (tabButtons == null)
            tabButtons = new List<TabButton>();

        tabButtons.Add(button);
    }

    /// <summary>
    /// While the cursor is hovering over a TabButton it will be highlighted.
    /// </summary>
    /// <param name="button"></param>
    public void OnTabEnter(TabButton button)
    {
        ResetTabs();
        if (selectedTab == null || button != selectedTab)
        {
            //button.background.color = tabHover;

            // make the color brighter
            Color c = button.background.color;
            c.r += tabHoverBrightness;
            c.g += tabHoverBrightness;
            c.b += tabHoverBrightness;
            button.background.color = c;
        }
    }

    public void OnTabExit(TabButton button)
    {
        ResetTabs();
    }

    public void OnTabSelected(TabButton button)
    {
        if (selectedTab != null)
        {
            selectedTab.Deselect();
            if (ReferenceEquals(selectedTab, button))
            {
                Debug.Log($"{_debugStart}Same Tab selected | index: {button.assignedIndex}");
                selectedTab = null; // reset
                return;
            }
        }

        Debug.Log($"{_debugStart}Tab selected | index: {button.assignedIndex}");

        selectedTab = button;
        selectedTab.Select();

        ResetTabs();

        button.background.color = tabActive;
        //Color c = button.originalColor;
        //c.r += tabActiveBrightness;
        //c.g += tabActiveBrightness;
        //c.b += tabActiveBrightness;
        //button.background.color = c;

        // Set the active tab
        //int index = button.transform.GetSiblingIndex(); // wont work for me since all the tabs are in their own containers
        int index = button.assignedIndex;
        for (int i = 0; i < objectsToSwap.Count; i++)
        {
            if (i == index)
                objectsToSwap[i].SetActive(true);
            else
                objectsToSwap[i].SetActive(false);
        }
    }

    public void ResetTabs()
    {
        foreach (TabButton button in tabButtons)
        {
            if (selectedTab != null && button == selectedTab)
                continue;
            //button.background.color = tabIdle;    
            // or keep color same color
            button.background.color = button.originalColor;
        }
    }
}
