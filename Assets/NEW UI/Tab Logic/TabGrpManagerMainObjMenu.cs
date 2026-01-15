using System;
using UnityEngine;

public class TabGrpManagerMainObjMenu : TabGroupManagerBase
{
    public TabGroup creatureGrpTab;
    public TabGroup inanimateObjGrpTab;
    [SerializeField] private TTRPG_SceneObjectBase _selectedObject;

    protected override void OnStart()
    {
        if (creatureGrpTab == null)
            ErrorOut.Throw(this, "creatureGrpTab null");
        if (inanimateObjGrpTab == null)
            ErrorOut.Throw(this, "inanimateObjGrpTab null");
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
        }
        else
        {
            WarningOut.Log(this, " - OnSelectedObjectChanged() - data is not ChangedObject... returning");
            return;
        }

        /*
        * If the newly selected object is;
        * a Creature;  activate the Creature's main menu tab, (and if opened already) deactivate the inanimate object main menu tab 
        * a Inanimate Object; activate the Inanimate Object's main menu tab, (and if opened already) deactivate the creature main menu tab 
        * (note if the same TTRPG_SceneObjectBase type is selected then we do not want to hide the tab and open it again!)
        * Then select its default tab for that menu.
        */

        if (!areBothChangedObjectsNotNull)
        {
            // close the main menu panel --> need another animator
        }
        // don't do anything when the same object type was selected again
        else if (areBothChangedObjectsNotNull && changedObject.newSelectedObj.GetType() == changedObject.prevSelectedObj.GetType())
        {
            DebugOut.Log(this, "selected same TTRPG_SceneObjectBase type, returning...");
            return;
        }

        // deselected object; hide the previously selected object's main menu panel tab (if not null)
        if (prevSelectedObj != null)
        {
            if (prevSelectedObj is Creature)
            {
                DebugOut.Log(this, "hiding Creature tab");

                //animator.SetTrigger("Hide (Character)");
                _selectedGrp.CheckAnimationTrigger("Hide");    // hide tab

            }
            else if (prevSelectedObj is InanimateObj)
            {
                DebugOut.Log(this, "hiding Inanimate obj tab");

                //animator.SetTrigger("Hide (Inanimate obj)");
                _selectedGrp.CheckAnimationTrigger("Hide");    // hide tab
            }

            _selectedGrp.ExitTabGroup();
            _selectedGrp = null;
        }

        // newly selected object; reveal the new main menu panel tab (if not null)
        if (newSelectedObj != null)
        {
            if (newSelectedObj is Creature)
            {
                DebugOut.Log(this, "revealing creature tab!");

                creatureGrpTab.CheckAnimationTrigger("Reveal");          // reveal tab
                _selectedGrp = creatureGrpTab;
            }
            else if (newSelectedObj is InanimateObj)
            {
                DebugOut.Log(this, "revealing inanimate obj tab!");

                inanimateObjGrpTab.CheckAnimationTrigger("Reveal");  // reveal tab
                _selectedGrp = inanimateObjGrpTab;
            }
            else
            {
                WarningOut.Log(this, "`selectedObj` is not type InanimateObj or Creature... returning"); // will be the case when there are other classes that inherit (extend) TTRPG_SceneObjectBase!
                return;
            }
            // then reveal the new tab (if the previously selected object type isn't the same type as the new one)
            // hide the previously selected object's tab and reveal the main menu tab of the newly selected TTRPG_SceneObjectBase

            //_prevSelectedObject = (TTRPG_SceneObjectBase)data;
            _selectedGrp.ActivateTabGroup();
            _selectedGrp.OnTabSelected(_selectedGrp.tabButtons[0]);   // select some arbitrary tab (note: see yellow text under `Tab Group Manager` for future solution)
        }
    }

    public void OnSelectedObject(Component component, object data)
    {
        /*
         * (For OnSelectObject() & OnDeselectObject())
         * On a object select of type TTRPG_SceneObjectBase,
         * if it's a Creature; deactivate the inanimate object tab group and activate the Creature's
         * if it's a Inanimate Object;  deactivate the creature tab group and activate the Inanimate Object's
         * (note if the same TTRPG_SceneObjectBase is selected then we do not want to hide the tab!)  --> this is not possible, unless `data` is sent as null from GameManagerScript!
         * Then select its default tab.
         */
        if (data is Creature)
        {
            DebugOut.Log(this, "selected creature!");
            inanimateObjGrpTab.ExitTabGroup();

            creatureGrpTab.CheckAnimationTrigger("Reveal");          // reveal tab
            _selectedGrp = creatureGrpTab;
        }
        else if (data is InanimateObj)
        {
            DebugOut.Log(this, "selected inanimate obj!");
            creatureGrpTab.ExitTabGroup();

            inanimateObjGrpTab.CheckAnimationTrigger("Reveal");  // reveal tab
            _selectedGrp = inanimateObjGrpTab;
        }
        else
        {
            WarningOut.Log(this, "`data` is not an InanimateObj or Creature... returning"); // will be the case when there are other classes that extend TTRPG_SceneObjectBase!
            return;
        }
        //_prevSelectedObject = (TTRPG_SceneObjectBase)data;

        _selectedGrp.ActivateTabGroup();
        _selectedGrp.OnTabSelected(_selectedGrp.tabButtons[0]);   // select some arbitrary tab (note: see yellow text under `Tab Group Manager` for future solution)
    }

    public void OnDeselectedObject(Component component, object data)
    {
        /*
         * (For OnSelectObject() & OnDeselectObject())
         * On a object select of type TTRPG_SceneObjectBase,
         * if it's a Creature; deactivate the inanimate object tab group and activate the Creature's
         * if it's a Inanimate Object;  deactivate the creature tab group and activate the Inanimate Object's
         * (note if the same TTRPG_SceneObjectBase is selected then we do not want to hide the tab!) --> this is not possible, unless `data` is sent as null from GameManagerScript!
         * Then select its default tab.
         */
        DebugOut.Log(this, "deselecting..");
        // `data` will be of type TTRPG_SceneObjectbase
        //if (data.GetType() == _prevSelectedObject.GetType()) --> doesn't work if for ex, we click the map... (tabs percist when no obj is selected)
        //{
        //    DebugOut.Log(this, "nah");
        //    _prevSelectedObject = null;
        //    return;
        //}

        if (data is Creature)
        {
            DebugOut.Log(this, "hiding Creature");

            //animator.SetTrigger("Hide (Character)");
            _selectedGrp.CheckAnimationTrigger("Hide");    // hide tab

        }
        else if (data is InanimateObj)
        {
            DebugOut.Log(this, "hiding Inanimate obj");

            //animator.SetTrigger("Hide (Inanimate obj)");
            _selectedGrp.CheckAnimationTrigger("Hide");    // hide tab
        }

        _selectedGrp.ExitTabGroup();
        _selectedGrp = null;
    }
}
