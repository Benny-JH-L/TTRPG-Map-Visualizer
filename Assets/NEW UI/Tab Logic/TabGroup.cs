using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabGroup : MonoBehaviour
{
    public List<TabButton> tabButtons;
    public Color tabIdle;
    public Color tabHover;
    public Color tabActive;
    public TabButton selectedTab;
    public List<GameObject> objectsToSwap;  
        // `tabButton` element index will swap to GameObject with the same index in `objetsToSwap`
    public void Subscribe(TabButton button)
    {
        if (tabButtons == null)
            tabButtons = new List<TabButton>();

        tabButtons.Add(button);
    }

    public void OnTabEnter(TabButton button)
    {
        ResetTabs();
        if (selectedTab ==  null || button != selectedTab)
            button.background.color = tabHover;
    }

    public void OnTabExit(TabButton button)
    {
        ResetTabs();
    }

    public void OnTabSelected(TabButton button)
    {
        selectedTab = button;
        ResetTabs();
        button.background.color = tabActive;
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
            button.background.color = tabIdle;
        }
    }
}
