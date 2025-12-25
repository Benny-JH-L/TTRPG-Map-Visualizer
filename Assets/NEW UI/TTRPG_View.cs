using System.Collections.Generic;
using UnityEngine;

public class TTRPG_View : AbstractUI
{
    private static string _debugStart = "[TTRPG_View]";
    public GameObject leftUI;
    public GameObject rightUI;
    
    public Animator leftAnimator;
    public Animator rightAnimator;
    public override void Configure()
    {
        // should have to do nothing...
    }


    public override void Setup()
    {
        leftAnimator = leftUI.GetComponent<Animator>();
        rightAnimator = rightUI.GetComponent<Animator>();
    }

    public void OnTabSelected(Component component, object data)
    {
        if (data is not TabButton && component != this)
        {
            // debug print
            return;
        }
        Debug.Log($"{_debugStart}OnTabSelected");

        leftAnimator.SetTrigger("Shrink Left UI");
        Debug.Log($"{_debugStart}shrinked left UI");

        rightAnimator.SetTrigger("Open Right UI");
        Debug.Log($"{_debugStart}Opened right UI");
    }

    /// <summary>
    /// Closes the right UI and expands the left UI.
    /// </summary>
    /// <param name="component"></param>
    /// <param name="data"></param>
    public void OnTabDeselected(Component component, object data)
    {
        if (data is not TabButton && component != this)
        {
            // debug print
            return;
        }

        Debug.Log($"{_debugStart}OnTabDeselected");
        leftAnimator.SetTrigger("Expand Left UI");
        Debug.Log($"{_debugStart}Expanded Left UI");

        rightAnimator.SetTrigger("Close Right UI");
        Debug.Log($"{_debugStart}Closed Right UI");
    }

    // the bottom two functions work its just that when i press on the
    // right UI tabs it also `deselects` the game object (and then calls the deselectTab)
    // because i have to include the tabs as "UI" components...

    public void OnSelectedObject(Component component, object data)
    {
        // rn the tabs will appear with no object selected, i need to redo object creation before i implment 
        // the group tab appearing for the correct object class.
        return;
        Debug.Log($"{_debugStart}OnSelectedObject");

        OnTabSelected(this, data);
    }

    public void OnDeselectedObject(Component component, object data)
    {
        // rn the tabs will appear with no object selected, i need to redo object creation before i implment 
        // the group tab appearing for the correct object class.
        return;
        Debug.Log($"{_debugStart}OnDeselectedObject");
        OnTabDeselected(this, data);
    }
}
