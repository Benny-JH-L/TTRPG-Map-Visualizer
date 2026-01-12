using UnityEngine;

public class TabGrpManagerCreatureInanimateObj : TabGroupManagerBase
{
    public TabGroup creatureGrpTab;
    public TabGroup inanimateObjGrpTab;
    //[SerializeField] private TTRPG_SceneObjectBase _prevSelectedObject;


    protected override void OnStart()
    {
        if (creatureGrpTab == null)
            ErrorOut.Throw(this, "creatureGrpTab null");
        if (inanimateObjGrpTab == null)
            ErrorOut.Throw(this, "inanimateObjGrpTab null");
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

            creatureGrpTab.CheckAnimationTrigger("Reveal (Character)");          // reveal tab
            _selectedGrp = creatureGrpTab;
        }
        else if (data is InanimateObj)
        {
            DebugOut.Log(this, "selected inanimate obj!");
            creatureGrpTab.ExitTabGroup();

            inanimateObjGrpTab.CheckAnimationTrigger("Reveal (Inanimate obj)");  // reveal tab
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
            _selectedGrp.CheckAnimationTrigger("Hide (Character)");    // hide tab Note: the triggers can be uniform in naming/the same now :)

        }
        else if (data is InanimateObj)
        {
            DebugOut.Log(this, "hiding Inanimate obj");

            //animator.SetTrigger("Hide (Inanimate obj)");
            _selectedGrp.CheckAnimationTrigger("Hide (Inanimate obj)");    // hide tab
        }

        _selectedGrp.ExitTabGroup();
        _selectedGrp = null;
    }
}
