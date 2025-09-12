
using UnityEngine;

public class AttributeManager : MonoBehaviour
{
    private Creature _selectedCreature;

    public void OnSelectedObject(Component comp, object data)
    {
        if (data is Creature creature)
            _selectedCreature = creature;
    }

    public void OnDeselectObject(Component comp, object data)
    {
        _selectedCreature = null;
    }

    public void OnSTRChanged(Component comp, object data)
    {
        if (data is int val)
            _selectedCreature.GetSaveData().coreStats.strength.SetNewAbilityScore(val);
    }

    public void OnDEXChanged(Component comp, object data)
    {
        if (data is int val)
            _selectedCreature.GetSaveData().coreStats.dexterity.SetNewAbilityScore(val);
    }

    public void OnCONChanged(Component comp, object data)
    {
        if (data is int val)
            _selectedCreature.GetSaveData().coreStats.constitution.SetNewAbilityScore(val);
    }

    public void OnINTChanged(Component comp, object data)
    {
        if (data is int val)
            _selectedCreature.GetSaveData().coreStats.intelligence.SetNewAbilityScore(val);
    }

    public void OnWISChanged(Component comp, object data)
    {
        if (data is int val)
            _selectedCreature.GetSaveData().coreStats.wisdom.SetNewAbilityScore(val);
    }

    public void OnCHAChanged(Component comp, object data)
    {
        if (data is int val)
            _selectedCreature.GetSaveData().coreStats.charaisma.SetNewAbilityScore(val);
    }
}
