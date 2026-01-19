
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

    public void OnSelectedObjectChanged(Component component, object data)
    {
        if (data is ChangedObject changedObject)
        {
            TTRPG_SceneObjectBase selectedObj = changedObject.newSelectedObj;
            TTRPG_SceneObjectBase prevSelectedObj = changedObject.prevSelectedObj;

            // show the secondary menu tab & panel only when there was no selected object previously
            if (selectedObj != null && prevSelectedObj == null)
            {
                _selectedTabGrp = tabGroup;
                doingAnimation = true;

                DebugOut.Log(this, $"triggering Reveal! at time: {Time.timeAsDouble}", debugDisabled);
                tabGroup.ActivateTabGroup();
                tabGroup.OnTabSelected(tabGroup.tabButtons[0]);     // select some arbitrary tab

                tabGroup.CheckAnimationTrigger("Reveal");
                secondaryMenuAnimatorHelper.CheckAnimationTrigger("Reveal");
            }
            // hide the secondary menu tab & panel when there is no selected object
            else if (selectedObj == null)
            {
                doingAnimation = true;
                DebugOut.Log(this, $"triggering hide! at time: {Time.timeAsDouble}", debugDisabled);
                tabGroup.CheckAnimationTrigger("Hide");
                secondaryMenuAnimatorHelper.CheckAnimationTrigger("Hide");
            }
        }
        else
        {
            WarningOut.Log(this, " - OnSelectedObjectChanged() - data is not ChangedObject"); // will be the case when there are other classes that inherit (extend) TTRPG_SceneObjectBase!
        }
    }

    //public void OnSelectedObject(Component component, object data)
    //{
    //    _selectedGrp = tabGroup;
    //    if (_selectedGrp != null)
    //    {
    //        doingAnimation = true;
    //        DebugOut.Log(this, $"triggering Reveal! at time: {Time.timeAsDouble}");
    //        tabGroup.ActivateTabGroup();
    //        tabGroup.OnTabSelected(tabGroup.tabButtons[0]);     // select some arbitrary tab

    //        tabGroup.CheckAnimationTrigger("Reveal");
    //        secondaryMenuAnimatorHelper.CheckAnimationTrigger("Reveal");
    //    }
    //}

    //public void OnDeselectedObject(Component component, object data)
    //{
    //    if (_selectedGrp != null)
    //    {
    //        doingAnimation = true;
    //        DebugOut.Log(this, $"triggering hide! at time: {Time.timeAsDouble}");
    //        tabGroup.CheckAnimationTrigger("Hide");
    //        secondaryMenuAnimatorHelper.CheckAnimationTrigger("Hide");
    //    }
    //}

    ///// <summary>
    ///// Called by the "reveal" animation when it finishes
    ///// </summary>
    //public void FinishReveal()
    //{
    //    DebugOut.Log(this, $"finishing Reveal at time: {Time.timeAsDouble}!");
        
    //    doingAnimation = false;
    //}

    /// <summary>
    /// Called by the "hide" animation when it finishes
    /// </summary>
    public override void OnHideAnimationFinish()
    {
        DebugOut.Log(this, $"finishing hide at time: {Time.timeAsDouble}!", debugDisabled);

        tabGroup.ExitTabGroup();
        _selectedTabGrp = null;
        doingAnimation = false;
    }

    public override void OnRevealAnimationFinish()
    {
        // do nothing
        DebugOut.Log(this, $"finishing reveal at time: {Time.timeAsDouble}!", debugDisabled);

    }
}
