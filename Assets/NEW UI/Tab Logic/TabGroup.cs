using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabGroup : MonoBehaviour
{
    private static string _debugStart = "[Tab Group] ";
    public static int DEFAULT_TAB_SELECT_INDEX = 0;         // for when an object is first selected, open the first menu

    public List<TabButton> tabButtons;
    public GeneralObject selectedObject;

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
            // check if the same tab was pressed
            if (ReferenceEquals(selectedTab, button))
            {
                // same tab was pressed, then UI should close the right UI container and expand the left UI container (in TTRPG_View from Deselect() callback)
                Debug.Log($"{_debugStart}Same Tab selected | index: {button.assignedIndex}");
                selectedTab = null;     // reset
                return;                 
            }
        }
        
        // Base case where there is no tab selected
        if (selectedTab == null)
        {
            selectedTab = button;
            FinishTabSelect(this, true);
        }
        selectedTab = button;
        
        // finish the rest of this code exectuio under FinishTabSelect()
        //selectedTab.Select();
        //ResetTabs();
        //button.background.color = tabActive;

        //Color c = button.originalColor;
        //c.r += tabActiveBrightness;
        //c.g += tabActiveBrightness;
        //c.b += tabActiveBrightness;
        //button.background.color = c;
            // hard to notice that its selected!

        // Set the active tab
        //int index = button.transform.GetSiblingIndex(); // wont work for me since all the tabs are in their own containers
        //int index = button.assignedIndex;
        //for (int i = 0; i < objectsToSwap.Count; i++)
        //{
        //    if (i == index)
        //    {
        //        objectsToSwap[i].SetActive(true);
        //    }
        //    else
        //        objectsToSwap[i].SetActive(false);
        //}
    }

    /// <summary>
    /// Should only be called by this script and a GameEventListener with GameEventSO (`OnRightUIClosedAnimationFinished`) 
    /// that is triggered when the rightUI container is closed.
    /// 
    /// Sets the active tab if `selectedTab` is not null and `selectedGameObject` is not null.
    /// </summary>
    /// <param name="component"></param>
    /// <param name="data"></param>
    public void FinishTabSelect(Component component, object data)
    {
        Debug.Log($"Finishing Tab select");
        //if (selectedTab == null || selectedObject == null)
        //{
        //    Debug.Log($"`Selected tab` or `selected object` is null!");
        //    return;
        //}
        if (selectedTab == null)// || selectedObject == null)
        {
            Debug.Log($"`Selected tab` is null!");
            return;
        }


        // Set the active tab
        //int index = button.transform.GetSiblingIndex(); // wont work for me since all the tabs are in their own containers
        int index = selectedTab.assignedIndex;
        for (int i = 0; i < objectsToSwap.Count; i++)
        {
            if (i == index)
            {
                objectsToSwap[i].SetActive(true);
            }
            else
                objectsToSwap[i].SetActive(false);
        }

        selectedTab.Select();
        ResetTabs();
        selectedTab.background.color = tabActive;
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

    public void OnSelectedObject(Component component, object data)
    {
        // rn the tabs will appear with no object selected, i need to redo object creation before i implment 
        // the group tab appearing for the correct object class.
        return;
        selectedObject = (GeneralObject)data;
        //FinishTabSelect(component, selectedObject);
    }

    public void OnDeselectedObject(Component component, object data)
    {
        // rn the tabs will appear with no object selected, i need to redo object creation before i implment 
        // the group tab appearing for the correct object class.
        return;
        selectedObject = null;
        selectedTab.Deselect();
    }
}
