using UnityEngine;

/// <summary>
/// Note: uses color for now
/// </summary>
public class TabGroup : TabGroupBase
{
    // not used--
    public Color tabIdle;
    public Color tabHover;
    //--
    public Color tabActive;

    [Range(0.1f, 0.5f)]
    public float tabHoverBrightness = 0.2f;
    //[Range(-0.01f, -0.3f)]
    //public float tabActiveBrightness = -0.01f;

    [SerializeField] private TabGroupManagerBase tabGroupManager;   // the manager for this tab group (if it exists as its parent)
    protected override void OnStart()
    {
        tabGroupManager = GetComponentInParent<TabGroupManagerBase>();

        if (tabGroupManager == null )
        {
            WarningOut.Log(this, $"`{this.name}` does not have a tab group manager as it's parent!");
        }
    }

    /// <summary>
    /// While the cursor is hovering over a TabButton it will be highlighted.
    /// </summary>
    /// <param name="button"></param>
    public override void OnTabEnter(TabButton button)
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

    public override void OnTabExit(TabButton button)
    {
        ResetTabs();
    }

    public override void OnTabSelected(TabButton button)
    {
        DebugOut.Log(this, "selected button", debugDisabled);
        if (selectedTab != null)
        {
            //selectedTab.swapToObject.SetActive(false);  // done in Deselect() now
            selectedTab.Deselect();                 // deactivate the old tab GameObject

            // check if the same tab was pressed
            if (ReferenceEquals(selectedTab, button))
            {
                // same tab was pressed, then UI should close the right UI container and expand the left UI container (in TTRPG_View from Deselect() callback)
                DebugOut.Log(this, $"Same Tab selected", debugDisabled);
                //SameTabSelected(button);
                tabGroupManager.OnTabSelected(new ChangedTabButton {prevTabButton = selectedTab, newTabButton = null});
                selectedTab = null;     // reset
                return;
            }
        }

        // Set the new active tab GameObject
        tabGroupManager.OnTabSelected(new ChangedTabButton { prevTabButton = selectedTab, newTabButton = button });
        selectedTab = button;
        ResetTabs();
        selectedTab.Select();
        selectedTab.background.color = tabActive;

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
    /// resets the colors to their original color
    /// </summary>
    protected override void ResetTabs()
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

    /// <summary>
    /// Call this function when a AnimationEvent (set in animation clip) for `Hide` is finished
    /// </summary>
    public override void OnHideAnimationFinish()
    {
        if (tabGroupManager != null)
        {
            tabGroupManager.OnHideAnimationFinish();
        }
        else
            WarningOut.Log(this, "cannot do `tabGroupManager.OnHideAnimationFinish()` bc tabGroupManager is null");
    }

    /// <summary>
    /// Call this function when a AnimationEvent (set in animation clip) for `Reveal` is finished
    /// </summary>
    public override void OnRevealAnimationFinish()
    {
        if (tabGroupManager != null)
        {
            tabGroupManager.OnRevealAnimationFinish();
        }
        else
            WarningOut.Log(this, "cannot do `tabGroupManager.OnRevealAnimationFinish()` bc tabGroupManager is null");
    }
}



//using System.Collections.Generic;
//using UnityEngine;

//public class TabGroup : MonoBehaviour
//{
//    public static int DEFAULT_TAB_SELECT_INDEX = 0;         // for when an object is first selected, open the first menu

//    public TTRPG_SceneObjectBase selectedObject;
//    public List<AnimatorHelper> animatorList;             // add animators via editor

//    // not used--
//    public Color tabIdle;
//    public Color tabHover;
//    //--
//    public Color tabActive;

//    [Range(0.1f, 0.5f)]
//    public float tabHoverBrightness = 0.2f;
//    //[Range(-0.01f, -0.3f)]
//    //public float tabActiveBrightness = -0.01f;

//    public List<TabButton> tabButtons;
//    public TabButton selectedTab;
//    //public List<GameObject> objectsToSwap;  // tab button will contain this info
//        // `tabButton` element `assignedIndex` will swap to GameObject with the same index in `objetsToSwap`

//    void Start()
//    {
//        tabButtons = new List<TabButton>();
//        selectedTab = null;
//    }

//    public void Subscribe(TabButton button)
//    {
//        if (tabButtons == null)
//            tabButtons = new List<TabButton>();

//        tabButtons.Add(button);
//    }

//    /// <summary>
//    /// While the cursor is hovering over a TabButton it will be highlighted.
//    /// </summary>
//    /// <param name="button"></param>
//    public void OnTabEnter(TabButton button)
//    {
//        ResetTabs();
//        if (selectedTab == null || button != selectedTab)
//        {
//            //button.background.color = tabHover;

//            // make the color brighter
//            Color c = button.background.color;
//            c.r += tabHoverBrightness;
//            c.g += tabHoverBrightness;
//            c.b += tabHoverBrightness;
//            button.background.color = c;
//        }
//    }

//    public void OnTabExit(TabButton button)
//    {
//        ResetTabs();
//    }

//    public void OnTabSelected(TabButton button)
//    {
//        if (selectedTab != null)
//        {
//            selectedTab.swapToObject.SetActive(false);  // deactivate the old tab GameObject
//            selectedTab.Deselect();

//            // check if the same tab was pressed
//            if (ReferenceEquals(selectedTab, button))
//            {
//                // same tab was pressed, then UI should close the right UI container and expand the left UI container (in TTRPG_View from Deselect() callback)
//                DebugOut.Log(this, $"Same Tab selected | index: {button.assignedIndex}");
//                selectedTab = null;     // reset
//                return;
//            }
//        }

//        // Set the new active tab GameObject
//        button.swapToObject.SetActive(true);
//        selectedTab = button;

//        //// Set the active tab
//        ////int index = button.transform.GetSiblingIndex(); // wont work for me since all the tabs are in their own containers
//        //int index = selectedTab.assignedIndex;
//        //for (int i = 0; i < objectsToSwap.Count; i++)
//        //{
//        //    if (i == index)
//        //    {
//        //        objectsToSwap[i].SetActive(true);
//        //    }
//        //    else
//        //        objectsToSwap[i].SetActive(false);
//        //}

//        selectedTab.Select();
//        ResetTabs();
//        selectedTab.background.color = tabActive;

//        // finish the rest of this code exectuio under FinishTabSelect()
//        //selectedTab.Select();
//        //ResetTabs();
//        //button.background.color = tabActive;

//        //Color c = button.originalColor;
//        //c.r += tabActiveBrightness;
//        //c.g += tabActiveBrightness;
//        //c.b += tabActiveBrightness;
//        //button.background.color = c;
//        // hard to notice that its selected!

//        // Set the active tab
//        //int index = button.transform.GetSiblingIndex(); // wont work for me since all the tabs are in their own containers
//        //int index = button.assignedIndex;
//        //for (int i = 0; i < objectsToSwap.Count; i++)
//        //{
//        //    if (i == index)
//        //    {
//        //        objectsToSwap[i].SetActive(true);
//        //    }
//        //    else
//        //        objectsToSwap[i].SetActive(false);
//        //}
//    }

//    /// <summary>
//    /// Should only be called by this script and a GameEventListener with GameEventSO (`OnRightUIClosedAnimationFinished`) 
//    /// that is triggered when the rightUI container is closed.
//    /// 
//    /// Sets the active tab if `selectedTab` is not null and `selectedGameObject` is not null.
//    /// </summary>
//    /// <param name="component"></param>
//    /// <param name="data"></param>
//    //public void FinishTabSelect(Component component, object data)
//    //{
//    //    Debug.Log($"Finishing Tab select");
//    //    //if (selectedTab == null || selectedObject == null)
//    //    //{
//    //    //    Debug.Log($"`Selected tab` or `selected object` is null!");
//    //    //    return;
//    //    //}
//    //    if (selectedTab == null)// || selectedObject == null)
//    //    {
//    //        Debug.Log($"`Selected tab` is null!");
//    //        return;
//    //    }


//    //    // Set the active tab
//    //    //int index = button.transform.GetSiblingIndex(); // wont work for me since all the tabs are in their own containers
//    //    int index = selectedTab.assignedIndex;
//    //    for (int i = 0; i < objectsToSwap.Count; i++)
//    //    {
//    //        if (i == index)
//    //        {
//    //            objectsToSwap[i].SetActive(true);
//    //        }
//    //        else
//    //            objectsToSwap[i].SetActive(false);
//    //    }

//    //    selectedTab.Select();
//    //    ResetTabs();
//    //    selectedTab.background.color = tabActive;
//    //}

//    // resets the colors to their original
//    public void ResetTabs()
//    {
//        foreach (TabButton button in tabButtons)
//        {
//            if (selectedTab != null && button == selectedTab)
//                continue;
//            //button.background.color = tabIdle;    
//            // or keep color same color
//            button.background.color = button.originalColor;
//        }
//    }

//    /// <summary>
//    /// Goes through the list of `tabButtons` and set's `swapToObject` activate status to false.
//    /// </summary>
//    public void DeactivateAllTabs()
//    {
//        foreach (TabButton button in tabButtons)
//        {
//            button.swapToObject.SetActive(false);
//        }
//    }

//    public void OnSelectedObject(Component component, object data)
//    {
//        if (data is Creature creature)
//        {
//            DebugOut.Log(this, "selected creature!");
//            //animator.SetTrigger("Reveal (Character)");
//            CheckAnimatorsTrigger("Reveal (Character)");    // reveal tab
//        }
//        else if (data is InanimateObj obj)
//        {
//            DebugOut.Log(this, "selected inanimate obj");
//            //animator.SetTrigger("Reveal (Inanimate obj)");
//            CheckAnimatorsTrigger("Reveal (Inanimate obj)");    // reveal tab
//        }
//        OnTabSelected(tabButtons[DEFAULT_TAB_SELECT_INDEX]);    // open the default tab --> need to figure out how to open the proper tab for creature and inanimate obj

//        // rn the tabs will appear with no object selected, i need to redo object creation before i implment 
//        // the group tab appearing for the correct object class.
//        return;
//        selectedObject = (TTRPG_SceneObjectBase)data;
//        //FinishTabSelect(component, selectedObject);
//    }
//    // have 1 tab group class that over loops all the tabs, but then separate the creature and inanimage obj with empty game objects or smth
//    // then the triggers can have uniform names (reavel, hide, etc.)
//    // and fixes the issues of 2 tabgroups wanting to close and open the other tab components they do not have

//    public void OnDeselectedObject(Component component, object data)
//    {
//        if (data is Creature) // `data` will be the same as `selectedObject`
//        {
//            //animator.SetTrigger("Hide (Character)");
//            CheckAnimatorsTrigger("Hide (Character)");    // hide tab

//        }
//        else if (data is InanimateObj)
//        {
//            //animator.SetTrigger("Hide (Inanimate obj)");
//            CheckAnimatorsTrigger("Hide (Inanimate obj)");    // hide tab
//        }

//        DeactivateAllTabs();
//        // unselect the tab
//        ResetTabs();
//        selectedTab.Deselect();
//        selectedTab = null;

//        //else if (data is InanimateObj)
//        //    animator.Set

//        //selectedObject = null;
//        // rn the tabs will appear with no object selected, i need to redo object creation before i implment 
//        // the group tab appearing for the correct object class.
//        return;
//        selectedObject = null;
//        selectedTab.Deselect();
//    }

//    /// <summary>
//    /// Sends the `triggerName` to AnimatorHelper instances.
//    /// </summary>
//    /// <param name="triggerName"></param>
//    private void CheckAnimatorsTrigger(string triggerName)
//    {
//        DebugOut.Log(this, $"Sending trigger: `{triggerName} to animators`");
//        foreach (AnimatorHelper helper in animatorList)
//        {
//            helper.CheckAnimationTrigger(triggerName);
//        }
//    }
//}
