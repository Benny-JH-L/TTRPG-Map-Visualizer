
using UnityEngine;

public class UICreatureManager : MonoBehaviour
{
    private Creature _selectedCreature;
    private CreatureData _saveData;

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
            _selectedCreature.objectName = creatureName;
            Debug.Log("saw d");
        }
    }

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
}
