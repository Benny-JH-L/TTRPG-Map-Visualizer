
using System;
using UnityEngine;

/// <summary>
/// Manages the tab groups for the main menu of TTRPG_SceneObjectBase objects
/// </summary>
public class TabGrpManagerMainObjMenu : TabGroupManagerBase
{
    [SerializeField] private TTRPG_View ttrpg_view;
    [SerializeField] private TabGroup creatureGrpTab;
    [SerializeField] private TabGroup inanimateObjGrpTab;
    [SerializeField] private TabGroup _tabGrpToExit;    // tab group to exit out of (helps prevent race condition between `finish animations` functions when they needed to access _selectedTabGrp)
    [SerializeField] private AnimatorHelper revealerAH; // animations to reveal the tab selected
    [SerializeField] private AnimatorHelper hiderAH;    // animations to hide the tab selected
    [SerializeField] private GameObject mainMenuTabStorage; // GameObject that will contain the unused menu tabs (will parent to)
    protected override void OnStart()
    {
        if (ttrpg_view == null)
            ErrorOut.Throw(this, "ttrpg_view null");
        if (creatureGrpTab == null)
            ErrorOut.Throw(this, "creatureGrpTab null");
        if (inanimateObjGrpTab == null)
            ErrorOut.Throw(this, "inanimateObjGrpTab null");
        if (revealerAH == null)
            ErrorOut.Throw(this, "revealer AnimatorHelper null");
        if (hiderAH == null)
            ErrorOut.Throw(this, "hider AnimatorHelper null");

        creatureGrpTab.ExitTabGroup();
        inanimateObjGrpTab.ExitTabGroup();
        _tabGrpToExit = null;
    }

    public void OnSelectedObjectChanged(Component component, object data)
    {
        // check if `data` is type `ChangedObject`
        TTRPG_SceneObjectBase prevSelectedObj = null;
        TTRPG_SceneObjectBase newSelectedObj = null;

        bool areBothChangedObjectsNotNull = true;
        if (data is ChangedObject changedObject)
        {
            prevSelectedObj = changedObject.prevSelectedObj;
            newSelectedObj = changedObject.newSelectedObj;
            areBothChangedObjectsNotNull = prevSelectedObj != null && newSelectedObj != null;

            DebugOut.Log(this, $"prev obj selc type: {prevSelectedObj} | new obj selc type {newSelectedObj}", debugDisabled);
            //ttrpg_view.OpenMainMenu();
        }
        else
        {
            WarningOut.Log(this, " - OnSelectedObjectChanged() - data is not ChangedObject... returning");
            return;
        }

        /*
        * If the newly selected object is;
        * a Creature; activate the Creature's main menu tab, (and if opened already) deactivate the inanimate object main menu tab 
        * a Inanimate Object; activate the Inanimate Object's main menu tab, (and if opened already) deactivate the creature main menu tab 
        * (note if the same TTRPG_SceneObjectBase type is selected then we do not want to hide the tab and open it again!)
        * Then select its default tab for that menu.
        */

        //if (!areBothChangedObjectsNotNull)
        //{
        //    // close the main menu panel --> need another animator | idk if i want this...
        //}

        // don't do anything when the same object type was selected again
        if (areBothChangedObjectsNotNull && changedObject.newSelectedObj.GetType() == changedObject.prevSelectedObj.GetType())
        {
            DebugOut.Log(this, "selected same TTRPG_SceneObjectBase type, returning...", debugDisabled);
            return;
        }

        // deselected object; hide the previously selected object's main menu panel tab (if not null)
        if (prevSelectedObj != null)
        {
            if (prevSelectedObj is Creature)
            {
                DebugOut.Log(this, "hiding Creature tab", debugDisabled);

                //animator.SetTrigger("Hide (Character)");
                _selectedTabGrp.CheckAnimationTrigger("Hide");    // hide tab
            }
            else if (prevSelectedObj is InanimateObj)
            {
                DebugOut.Log(this, "hiding Inanimate obj tab", debugDisabled);

                //animator.SetTrigger("Hide (Inanimate obj)");
                _selectedTabGrp.CheckAnimationTrigger("Hide");    // hide tab
            }

            _tabGrpToExit = _selectedTabGrp;
            // done as a animation event?
            //_selectedGrp.ExitTabGroup();  // instantly stops/inturrupts the `Hide` animations!
            //_selectedGrp = null;
        }

        // newly selected object; reveal the new main menu panel tab (if not null)
        if (newSelectedObj != null)
        {
            if (newSelectedObj is Creature)
            {
                DebugOut.Log(this, "revealing creature tab!", debugDisabled);

                creatureGrpTab.CheckAnimationTrigger("Reveal");          // reveal tab
                _selectedTabGrp = creatureGrpTab;
            }
            else if (newSelectedObj is InanimateObj)
            {
                DebugOut.Log(this, "revealing inanimate obj tab!", debugDisabled);

                inanimateObjGrpTab.CheckAnimationTrigger("Reveal");  // reveal tab
                _selectedTabGrp = inanimateObjGrpTab;
            }
            else
            {
                WarningOut.Log(this, "`selectedObj` is not type InanimateObj or Creature... returning"); // will be the case when there are other classes that inherit (extend) TTRPG_SceneObjectBase!
                return;
            }
            // then reveal the new tab (if the previously selected object type isn't the same type as the new one)
            // hide the previously selected object's tab and reveal the main menu tab of the newly selected TTRPG_SceneObjectBase

            //_prevSelectedObject = (TTRPG_SceneObjectBase)data;

            _selectedTabGrp.ActivateTabGroup(); // the tab group class will call this class's `OnTabSelected()` function
            _selectedTabGrp.OnTabSelected(_selectedTabGrp.tabButtons[0]);   // select some arbitrary tab (note: see yellow text under `Tab Group Manager` for future solution)
        }
        // when the same Scene object is selected, reset the selected tab manually
        else
        {
            _selectedTabGrp.selectedTab = null; 
        }
    }

    public override void OnHideAnimationFinish()
    {
        DebugOut.Log(this, "finishing hide", debugDisabled);
        //_selectedGrp.ExitTabGroup();
        //_selectedGrp = null;

        //if (ReferenceEquals(_selectedTabGrp, _tabGrpToExit))
        //    _selectedTabGrp = null;

        _tabGrpToExit.ExitTabGroup();
        _tabGrpToExit = null;
    }

    public override void OnRevealAnimationFinish()
    {
        // do nothing
    }

    ChangedTabButton changedTabButton;
    public override void OnTabSelected(ChangedTabButton data)
    {
        //DebugOut.Log(this, " - OnTabSelected() - ", debugDisabled);

        // the new `button` is null; ie same tab button was clicked
        if (data.newTabButton == null)
        {
            // Note: If you see the `swapToObject` of the prev button instantly disapearing when the same tab button is selected,
            // its because the button's Deselect() is called on it and sets its active status to false, so that's why.
            ttrpg_view.CloseMainMenu();
            return;
        }
        ttrpg_view.OpenMainMenu();

        return;
        changedTabButton = data;
        // hide the current tab and reveal the newly chosen tab

        // hiding:
        // parent the `swapToObject` (current chosen tab) to the `Hider` GameObject, then trigger animations
        if (data.prevTabButton != null) // base case where the 1st SceneObject is selected
        {
            DebugOut.Log(this, $"hiding {data.prevTabButton.swapToObject.name} tab", debugDisabled);
            data.prevTabButton.swapToObject.transform.SetParent(hiderAH.transform, false);
            data.prevTabButton.swapToObject.SetActive(true);
            hiderAH.CheckAnimationTrigger("Hide");
        }

        DebugOut.Log(this, $"revealing {data.newTabButton.swapToObject.name} tab", debugDisabled);
        // revealing:
        // parent the `swapToObject` (newly chosen tab we want to switch to) to the `Revealer` GameObject, then trigger animations
        data.newTabButton.swapToObject.transform.SetParent(revealerAH.transform, false);
        revealerAH.CheckAnimationTrigger("Reveal");

    }

    public void OnRevealerFinish(Component component, object data)
    {
        return;
        DebugOut.Log(this, "Revealer finished animation", debugDisabled);

        // parent back to the `container` (`mainMenuTabStorage`) under `Right (Main Menu)` GameObject on animiation end
        changedTabButton.newTabButton.swapToObject.transform.SetParent(mainMenuTabStorage.transform, false);
    }


    public void OnHiderFinish(Component component, object data)
    {
        return;
        DebugOut.Log(this, "Hider finished animation", debugDisabled);

        // parent back to the `container` (`mainMenuTabStorage`) under `Right (Main Menu)` GameObject on animiation end
        if (changedTabButton.prevTabButton != null)
        {
            //changedTabButton.prevTabButton.swapToObject.transform.position = Vector3.zero;
            changedTabButton.prevTabButton.swapToObject.SetActive(false);
            changedTabButton.prevTabButton.swapToObject.transform.SetParent(mainMenuTabStorage.transform, false);
        }
    }



    //public void OnSelectedObject(Component component, object data)
    //{
    //    /*
    //     * (For OnSelectObject() & OnDeselectObject())
    //     * On a object select of type TTRPG_SceneObjectBase,
    //     * if it's a Creature; deactivate the inanimate object tab group and activate the Creature's
    //     * if it's a Inanimate Object;  deactivate the creature tab group and activate the Inanimate Object's
    //     * (note if the same TTRPG_SceneObjectBase is selected then we do not want to hide the tab!)  --> this is not possible, unless `data` is sent as null from GameManagerScript!
    //     * Then select its default tab.
    //     */
    //    if (data is Creature)
    //    {
    //        DebugOut.Log(this, "selected creature!", debugDisabled);
    //        inanimateObjGrpTab.ExitTabGroup();

    //        creatureGrpTab.CheckAnimationTrigger("Reveal");          // reveal tab
    //        _selectedTabGrp = creatureGrpTab;
    //    }
    //    else if (data is InanimateObj)
    //    {
    //        DebugOut.Log(this, "selected inanimate obj!", debugDisabled);
    //        creatureGrpTab.ExitTabGroup();

    //        inanimateObjGrpTab.CheckAnimationTrigger("Reveal");  // reveal tab
    //        _selectedTabGrp = inanimateObjGrpTab;
    //    }
    //    else
    //    {
    //        WarningOut.Log(this, "`data` is not an InanimateObj or Creature... returning"); // will be the case when there are other classes that extend TTRPG_SceneObjectBase!
    //        return;
    //    }
    //    //_prevSelectedObject = (TTRPG_SceneObjectBase)data;

    //    _selectedTabGrp.ActivateTabGroup();
    //    _selectedTabGrp.OnTabSelected(_selectedTabGrp.tabButtons[0]);   // select some arbitrary tab (note: see yellow text under `Tab Group Manager` for future solution)
    //}

    //public void OnDeselectedObject(Component component, object data)
    //{
    //    /*
    //     * (For OnSelectObject() & OnDeselectObject())
    //     * On a object select of type TTRPG_SceneObjectBase,
    //     * if it's a Creature; deactivate the inanimate object tab group and activate the Creature's
    //     * if it's a Inanimate Object;  deactivate the creature tab group and activate the Inanimate Object's
    //     * (note if the same TTRPG_SceneObjectBase is selected then we do not want to hide the tab!) --> this is not possible, unless `data` is sent as null from GameManagerScript!
    //     * Then select its default tab.
    //     */
    //    DebugOut.Log(this, "deselecting..", debugDisabled);
    //    // `data` will be of type TTRPG_SceneObjectbase
    //    //if (data.GetType() == _prevSelectedObject.GetType()) --> doesn't work if for ex, we click the map... (tabs percist when no obj is selected)
    //    //{
    //    //    DebugOut.Log(this, "nah");
    //    //    _prevSelectedObject = null;
    //    //    return;
    //    //}

    //    if (data is Creature)
    //    {
    //        DebugOut.Log(this, "hiding Creature", debugDisabled);

    //        //animator.SetTrigger("Hide (Character)");
    //        _selectedTabGrp.CheckAnimationTrigger("Hide");    // hide tab

    //    }
    //    else if (data is InanimateObj)
    //    {
    //        DebugOut.Log(this, "hiding Inanimate obj", debugDisabled);

    //        //animator.SetTrigger("Hide (Inanimate obj)");
    //        _selectedTabGrp.CheckAnimationTrigger("Hide");    // hide tab
    //    }

    //    _selectedTabGrp.ExitTabGroup();
    //    _selectedTabGrp = null;
    //}
}
