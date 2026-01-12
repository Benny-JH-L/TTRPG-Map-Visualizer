using UnityEngine;

/// <summary>
/// Defines how to manage group tabs, be it one or many.
/// </summary>
public abstract class TabGroupManagerBase : MonoBehaviour
{
    [SerializeField] protected TabGroup _selectedGrp;
    //[SerializeField] private TTRPG_SceneObjectBase _prevSelectedObject;


    void Start()
    {
        _selectedGrp = null;
        OnStart();
    }

    protected abstract void OnStart();
}
