using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(AnimatorHelper))]
public abstract class TabGroupBase : MonoBehaviour
{
    [SerializeField] protected AnimatorHelper animatorHelper;    // contain animation for this tab (ex. appearing, disapearing, etc.)
    public List<TabButton> tabButtons;          // note: public for now...
    public TabButton selectedTab;               // tab buttons will contain what GameObject to swap to

    void Start()
    {
        selectedTab = null;
        animatorHelper = GetComponent<AnimatorHelper>();

        OnStart();
    }

    protected abstract void OnStart();

    public void Subscribe(TabButton button)
    {
        if (tabButtons == null)
            tabButtons = new List<TabButton>();

        tabButtons.Add(button);
        DebugOut.Log(this, $"{button.name} subscribed to '{this.name}'");
    }

    /// <summary>
    /// What to do while the cursor is hovering over a TabButton.
    /// </summary>
    /// <param name="button"></param>
    public abstract void OnTabEnter(TabButton button);

    /// <summary>
    /// What to do when the cursor exits a TabButton.
    /// </summary>
    /// <param name="button"></param>
    public abstract void OnTabExit(TabButton button);

    /// <summary>
    /// What to do when the cursor clicks a TabButton.
    /// </summary>
    /// <param name="button"></param>
    public abstract void OnTabSelected(TabButton button);

    protected abstract void ResetTabs();

    /// <summary>
    /// `Hide` animations that need to call this when they finish, optional!
    /// </summary>
    public abstract void OnHideAnimationFinish();

    /// <summary>
    /// `Reveal` animations that need to call this when they finish, optional!
    /// </summary>
    public abstract void OnRevealAnimationFinish();

    /// <summary>
    /// Goes through the list of `tabButtons` and sets their and `swapToObject` active states to false.
    /// </summary>
    protected void DeactivateAllTabs()
    {
        foreach (TabButton button in tabButtons)
        {
            button.swapToObject.SetActive(false);
            button.gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// Resets and deactivates all tabs. Selected tab is reset.
    /// </summary>
    public void ExitTabGroup()
    {
        ResetTabs();
        DeactivateAllTabs();
        selectedTab = null;
    }

    /// <summary>
    /// Goes through the list of `tabButtons` and sets their active state to true.
    /// </summary>
    public void ActivateTabGroup()
    {
        foreach (TabButton button in tabButtons)
        {
            button.swapToObject.SetActive(false);   // ensure that none of the tabs' `swapToObject` are active
            button.gameObject.SetActive(true);
        }
    }

    /// <summary>
    /// Sends the `triggerName` to AnimatorHelper reference.
    /// </summary>
    /// <param name="triggerName"></param>
    public void CheckAnimationTrigger(string triggerName)
    {
        DebugOut.Log(this, $"Sending trigger: `{triggerName}` to animator");
        if (animatorHelper == null)
            ErrorOut.Throw(this, "animation helper null");
        animatorHelper.CheckAnimationTrigger(triggerName);
    }
}
