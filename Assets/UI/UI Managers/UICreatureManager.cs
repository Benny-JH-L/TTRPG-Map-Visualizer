
using System;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Updates the Creature UI's values and CreatureData.
/// </summary>
public class UICreatureManager : MonoBehaviour
{
    public ChangedGameEventStorage changedGameEventStorage;

    private Creature _selectedCreature;
    private CreatureData _saveData;

    // I wish this worked so i don't have to manually add them via inspector (may work if i instantiate during runtime instead of hand creating in the inspector)
    //public void Awake()
    //{
    //    GameEventListener l = gameObject.AddComponent<GameEventListener>();

    //    l.gameEvent = changedGameEventStorage.ACChanged;
    //    UnityAction<Component, object> response = OnACChange;
    //    l.response.AddListener(response);

    //    //l = this.AddComponent<GameEventListener>();
    //    //l.gameEvent = changedGameEventStorage.HPChanged;
    //    //l.response.AddListener(this.OnHPChange);

    //    Debug.Log("adding game event listeners");
    //}

    public void OnSelectedObject(Component comp, object data)
    {
        if (data is Creature creature)
        {
            _selectedCreature = creature;
            _saveData = _selectedCreature.saveData;
        }
    }

    public void OnDeselectObject(Component comp, object data)
    {
        _selectedCreature = null;
        _saveData = null;
    }

    public void OnCreatureNameChange(Component comp, object data)
    {
        if (data is string creatureName)
        {
            _selectedCreature.GetSaveData().objectName = creatureName;
        }
    }

    public void OnSpeciesChange(Component comp, object data)
    {
        if (data is int val)
            _saveData.species = (Species)val;
    }

    public void OnAlignmentChange(Component comp, object data)
    {
        Debug.Log("NOT YET IMPLMENTED ALIGNMENT");
    }

    public void OnClassChange(Component comp, object data)
    {
        if (data is int val)
            _saveData.className = (ClassType)val;
    }

    public void OnACChange(Component comp, object data)
    {
        if (data is int val)
            _saveData.coreStats.ac = val;
    }

    public void OnHPChange(Component comp, object data)
    {
        if (data is int val)
            _saveData.coreStats.hp = val;
    }

    // Speed value changes----
    //public void OnSpeedTypeChange(Component comp, object data)
    //{
    //    if (data is Tuple<CoreStats.SpeedType, int> pair)
    //    {
    //        switch (pair.Item1)
    //        {
    //            case CoreStats.SpeedType.speed:
    //                _saveData.coreStats.speedTypes.speed = pair.Item2;
    //                break;
    //            case CoreStats.SpeedType.burrow:
    //                _saveData.coreStats.speedTypes.burrow = pair.Item2;
    //                break;
    //            case CoreStats.SpeedType.climb:
    //                _saveData.coreStats.speedTypes.climb = pair.Item2;
    //                break;
    //            case CoreStats.SpeedType.fly:
    //                _saveData.coreStats.speedTypes.fly = pair.Item2;
    //                break;
    //            case CoreStats.SpeedType.swim:
    //                _saveData.coreStats.speedTypes.swim = pair.Item2;
    //                break;
    //        }
    //    }
    //}

    public void OnSpeedChange(Component comp, object data)
    {
        if (data is int val)
            _saveData.coreStats.speedTypes.speed = val;
    }

    public void OnBurrowChange(Component comp, object data)
    {
        if (data is int val)
            _saveData.coreStats.speedTypes.burrow = val;
    }

    public void OnClimbChange(Component comp, object data)
    {
        if (data is int val)
            _saveData.coreStats.speedTypes.climb = val;
    }

    public void OnFlyChange(Component comp, object data)
    {
        if (data is int val)
            _saveData.coreStats.speedTypes.fly = val;
    }

    public void OnSwimChange(Component comp, object data)
    {
        if (data is int val)
            _saveData.coreStats.speedTypes.swim = val;
    }
    // Speed value changes end----

    // Attribute changes----
    public void OnSTRChanged(Component comp, object data)
    {
        if (data is int val)
            _saveData.coreStats.strength.SetNewAbilityScore(val);
    }

    public void OnDEXChanged(Component comp, object data)
    {
        if (data is int val)
            _saveData.coreStats.dexterity.SetNewAbilityScore(val);
    }

    public void OnCONChanged(Component comp, object data)
    {
        if (data is int val)
            _saveData.coreStats.constitution.SetNewAbilityScore(val);
    }

    public void OnINTChanged(Component comp, object data)
    {
        if (data is int val)
            _saveData.coreStats.intelligence.SetNewAbilityScore(val);
    }

    public void OnWISChanged(Component comp, object data)
    {
        if (data is int val)
            _saveData.coreStats.wisdom.SetNewAbilityScore(val);
    }

    public void OnCHAChanged(Component comp, object data)
    {
        if (data is int val)
            _selectedCreature.GetSaveData().coreStats.charaisma.SetNewAbilityScore(val);
    }
    // Attribute changes end----

    public void OnAdditionalInfoChanged(Component comp, object data)
    {
        if (data is string val)
            _selectedCreature.GetSaveData().additionalInfo = val;
    }
}
