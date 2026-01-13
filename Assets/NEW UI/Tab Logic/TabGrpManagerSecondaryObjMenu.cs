
using UnityEngine;

public class TabGrpManagerSecondaryObjMenu : TabGroupManagerBase
{
    public TabGroup tabGroup;
    public AnimatorHelper secondaryMenuAnimatorHelper;   // has animations for the secondary menu area
    [SerializeField] private bool doingAnimation;

    protected override void OnStart()
    {
        if (tabGroup == null)
            ErrorOut.Throw(this, "tab group null");
        if (secondaryMenuAnimatorHelper == null)
            ErrorOut.Throw(this, "secondaryMenuAnimatorHelper null");
        doingAnimation = false;
    }

    public void OnSelectedObject(Component component, object data)
    {
        _selectedGrp = tabGroup;
        if (_selectedGrp != null)
        {
            doingAnimation = true;
            DebugOut.Log(this, $"triggering Reveal! at time: {Time.timeAsDouble}");
            tabGroup.ActivateTabGroup();
            tabGroup.OnTabSelected(tabGroup.tabButtons[0]);     // select some arbitrary tab

            tabGroup.CheckAnimationTrigger("Reveal");
            secondaryMenuAnimatorHelper.CheckAnimationTrigger("Reveal");
        }
    }

    public void OnDeselectedObject(Component component, object data)
    {
        if (_selectedGrp != null)
        {
            doingAnimation = true;
            DebugOut.Log(this, $"triggering hide! at time: {Time.timeAsDouble}");
            tabGroup.CheckAnimationTrigger("Hide");
            secondaryMenuAnimatorHelper.CheckAnimationTrigger("Hide");
        }
    }

    /// <summary>
    /// Called by the "reveal" animation when it finishes
    /// </summary>
    public void FinishReveal()
    {
        DebugOut.Log(this, $"finishing Reveal at time: {Time.timeAsDouble}!");
        doingAnimation = false;

    }


    /// <summary>
    /// Called by the "hide" animation when it finishes
    /// </summary>
    public void FinishHide()
    {
        DebugOut.Log(this, $"finishing hide at time: {Time.timeAsDouble}!");

        tabGroup.ExitTabGroup();
        _selectedGrp = null;
        doingAnimation = false;
    }
}
