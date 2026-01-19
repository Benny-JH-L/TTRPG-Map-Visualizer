
using UnityEngine;

public class TTRPG_View : AbstractUI
{
    public GameObject leftUI;
    public GameObject rightUI;
    
    public AnimatorHelper leftUIAnimatorHelper;
    public AnimatorHelper mainMenuAnimatorHelper;

    [SerializeField] private bool isMainMenuClosed;

    public override void Configure()
    {
        // should have to do nothing...
        isMainMenuClosed = true;
    }


    public override void Setup()
    {
        leftUIAnimatorHelper = leftUI.GetComponent<AnimatorHelper>();
        mainMenuAnimatorHelper = rightUI.GetComponent<AnimatorHelper>();
    }

    /// <summary>
    /// Opens the Main Menu (if it is not already open) and shrinks the left UI.
    /// </summary>
    public void OpenMainMenu()
    {
        DebugOut.Log(this, $"- OpenMainMenu() - ", debugDisabled);

        if (!isMainMenuClosed)   // return when its already opened
        {
            DebugOut.Log(this, $"already opened!", debugDisabled);
            return; 
        }

        leftUIAnimatorHelper.CheckAnimationTrigger("Shrink Left UI");
        mainMenuAnimatorHelper.CheckAnimationTrigger("Open Right UI");
        isMainMenuClosed = false;
    }

    /// <summary>
    /// Closes the Main Menu (if its not closed already) and expands the left UI.
    /// </summary>
    public void CloseMainMenu()
    {
        DebugOut.Log(this, $"- CloseMainMenu() - ", debugDisabled);

        if (isMainMenuClosed)   // return when its already opened
        {
            DebugOut.Log(this, $"already closed!", debugDisabled);
            return;
        }


        leftUIAnimatorHelper.CheckAnimationTrigger("Expand Left UI");
        mainMenuAnimatorHelper.CheckAnimationTrigger("Close Right UI");
        isMainMenuClosed = true;
    }

    public void OnTabSelected(Component component, object data) // not called by game listeners
    {
        if (component != this && data is not TabButton)
        {
            // debug print
            DebugOut.Log(this, "maybe bruh?...", debugDisabled);
            return;
        }
        if (data is TabButton)
        {
            DebugOut.Log(this, "triggered by TabButton...", debugDisabled);

        }

        DebugOut.Log(this, $"OnTabSelected", debugDisabled);
        //leftUIAnimatorHelper.CheckAnimationTrigger("Shrink Left UI");
        //mainMenuAnimatorHelper.CheckAnimationTrigger("Open Right UI");
        OpenMainMenu();
    }

    /// <summary>
    /// Closes the right UI and expands the left UI.
    /// </summary>
    /// <param name="component"></param>
    /// <param name="data"></param>
    public void OnTabDeselected(Component component, object data) // not called by game listeners
    {
        if (component != this && data is not TabButton)
        {
            // debug print
            DebugOut.Log(this, "maybe bruh?...", debugDisabled);
            return;
        }
        if (data is TabButton)
        {
            DebugOut.Log(this, "triggered by TabButton...", debugDisabled);

        }

        DebugOut.Log(this, $"OnTabDeselected", debugDisabled);
        //leftUIAnimatorHelper.CheckAnimationTrigger("Expand Left UI");
        //mainMenuAnimatorHelper.CheckAnimationTrigger("Close Right UI");
        CloseMainMenu();
    }

    public void OnSelectedObjectChanged(Component component, object data)
    {
        DebugOut.Log(this, "- OnSelectedObjectChanged() - ", debugDisabled);
        if (data is ChangedObject changedObject)
        {
            // hide main menu panel
            if (changedObject.newSelectedObj == null)
            {
                OnTabDeselected(this, data);
            }
            // reveal main menu panel when; noththing was selected previously and selected a new non-null TTRPG_SceneObjectBase object  
            else if (changedObject.prevSelectedObj == null) // changedObject.newSelectedObj != null too
            {
                OnTabSelected(this, data);
            }
        }
        else
        {
            WarningOut.Log(this, " - OnSelectedObjectChanged() - data is not ChangedObject... returning");
            return;
        }
    }


    // the bottom two functions work its just that when i press on the
    // right UI tabs it also `deselects` the game object (and then calls the deselectTab)
    // because i have to include the tabs as "UI" components...
    //public void OnSelectedObject(Component component, object data)
    //{
    //    // rn the tabs will appear with no object selected, i need to redo object creation before i implment 
    //    // the group tab appearing for the correct object class.
    //    //return;
    //    DebugOut.Log(this, $"OnSelectedObject");

    //    OnTabSelected(this, data);
    //}

    //public void OnDeselectedObject(Component component, object data)
    //{
    //    // rn the tabs will appear with no object selected, i need to redo object creation before i implment 
    //    // the group tab appearing for the correct object class.
    //    //return;
    //    DebugOut.Log(this, $"OnDeselectedObject");
    //    OnTabDeselected(this, data);
    //}
}
