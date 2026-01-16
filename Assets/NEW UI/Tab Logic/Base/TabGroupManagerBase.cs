using UnityEngine;

/// <summary>
/// Defines how to manage group tabs, be it one or many.
/// </summary>
public abstract class TabGroupManagerBase : MonoBehaviour
{
    [SerializeField] protected TabGroup _selectedTabGrp;
    //[SerializeField] private TTRPG_SceneObjectBase _prevSelectedObject;


    void Start()
    {
        _selectedTabGrp = null;
        OnStart();
    }

    protected abstract void OnStart();


    /// <summary>
    /// for `Hiding` animations that need to call this when they finish
    /// </summary>
    public abstract void OnHideAnimationFinish();

    /// <summary>
    /// for `Reveal` animations that need to call this when they finish
    /// </summary>
    public abstract void OnRevealAnimationFinish();
}
