
using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Updates the Creature UI's values and CreatureData.
/// </summary>
public class UICreatureManager : MonoBehaviour
{
    private static string _debugStart = "UICreatureManager | ";
    public ChangedGameEventStorage changedGameEventStorage;
    public View view;

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
            // when a new creature is selected, initialize the creature actions panel
            if (_selectedCreature != creature)
            {
                _ClearActionsAndSave(_selectedCreature, true);  // save the actions for `_selectedCreature` and clear the actionContainer
                InitCreatureActionPanel(creature);              // initialize the actionContainer to the new creature
                _selectedCreature = creature;
                _saveData = _selectedCreature.saveData;
            }
        }
    }

    private void InitCreatureActionPanel(Creature creature)
    {
        if (creature.GetSaveData().actionNames.Count <= 0)
        {
            Debug.Log($"{_debugStart}no actions detected in save data, adding 1 panel");
            OnAddAction(view.actionPanelPrefab);    // add 1 panel
            return;
        }
        
        Debug.Log($"{_debugStart}Init creature actions");
        for (int i = 0; i < creature.GetSaveData().actionNames.Count; i++)   // Note: `creatureData.actionDescriptions.Count` will have the same count
        {
            GameObject panel = OnAddAction(view.actionPanelPrefab);
            TMP_InputField[] inputs = panel.GetComponentsInChildren<TMP_InputField>();
            inputs[0].text = creature.GetSaveData().actionNames[i];
            inputs[1].text = creature.GetSaveData().actionDescriptions[i];
        }
    }

    //public void InitCreatureActions(Component comp, object data)
    //{
    //    _ClearActionsAndSave(true);    // Clear any action panels

    //    //if (data is CreatureData creatureData)
    //    //{
    //    //    if (creatureData.actionNames.Count <= 0)
    //    //    {
    //    //        OnAddAction(view.actionPanelPrefab);    // add 1 panel
    //    //        return;
    //    //    }

    //    //    for (int i = 0; i < creatureData.actionNames.Count; i++)   // Note: `creatureData.actionDescriptions.Count` will have the same count
    //    //    {
    //    //        GameObject panel = OnAddAction(view.actionPanelPrefab);
    //    //        TMP_InputField[] inputs = panel.GetComponentsInChildren<TMP_InputField>();
    //    //        inputs[0].text = creatureData.actionNames[i];
    //    //        inputs[1].text = creatureData.actionDescriptions[i];
    //    //    }
    //    //}

    //    if (data is Creature creature)
    //    {
    //        if (creature.GetSaveData().actionNames.Count <= 0)
    //        {
    //            Debug.Log($"{_debugStart}no actions detected in save data, adding 1 panel");
    //            OnAddAction(view.actionPanelPrefab);    // add 1 panel
    //            return;
    //        }

    //        Debug.Log($"{_debugStart}Init creature actions");
    //        for (int i = 0; i < creature.GetSaveData().actionNames.Count; i++)   // Note: `creatureData.actionDescriptions.Count` will have the same count
    //        {
    //            GameObject panel = OnAddAction(view.actionPanelPrefab);
    //            TMP_InputField[] inputs = panel.GetComponentsInChildren<TMP_InputField>();
    //            inputs[0].text = creature.GetSaveData().actionNames[i];
    //            inputs[1].text = creature.GetSaveData().actionDescriptions[i];
    //        }
    //    }
    //}

    public void OnDeselectObject(Component comp, object data)
    {
        _ClearActionsAndSave(_selectedCreature, true);
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
        Debug.Log($"CLASS CHANGE: {data}");
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

    //public void InitCreatureActions(Component comp, object data)
    //{
    //    _ClearActionsAndSave(true);    // Clear any action panels

    //    //if (data is CreatureData creatureData)
    //    //{
    //    //    if (creatureData.actionNames.Count <= 0)
    //    //    {
    //    //        OnAddAction(view.actionPanelPrefab);    // add 1 panel
    //    //        return;
    //    //    }

    //    //    for (int i = 0; i < creatureData.actionNames.Count; i++)   // Note: `creatureData.actionDescriptions.Count` will have the same count
    //    //    {
    //    //        GameObject panel = OnAddAction(view.actionPanelPrefab);
    //    //        TMP_InputField[] inputs = panel.GetComponentsInChildren<TMP_InputField>();
    //    //        inputs[0].text = creatureData.actionNames[i];
    //    //        inputs[1].text = creatureData.actionDescriptions[i];
    //    //    }
    //    //}

    //    if (data is Creature creature)
    //    {
    //        if (creature.GetSaveData().actionNames.Count <= 0)
    //        {
    //            Debug.Log($"{_debugStart}no actions detected in save data, adding 1 panel");
    //            OnAddAction(view.actionPanelPrefab);    // add 1 panel
    //            return;
    //        }

    //        Debug.Log($"{_debugStart}Init creature actions");
    //        for (int i = 0; i < creature.GetSaveData().actionNames.Count; i++)   // Note: `creatureData.actionDescriptions.Count` will have the same count
    //        {
    //            GameObject panel = OnAddAction(view.actionPanelPrefab);
    //            TMP_InputField[] inputs = panel.GetComponentsInChildren<TMP_InputField>();
    //            inputs[0].text = creature.GetSaveData().actionNames[i];
    //            inputs[1].text = creature.GetSaveData().actionDescriptions[i];
    //        }
    //    }
    //}

    /// <summary>
    /// Adds another action section in the creature data panel, and returns it
    /// </summary>
    /// <param name="comp"></param>
    /// <param name="data"></param>
    public GameObject OnAddAction(object data)
    {
        if (data is GameObject objToAdd)
        {
            Debug.Log($"{_debugStart}adding action panel");

            // 1. Instantiate the prefab
            //GameObject newActionPanel = Instantiate(actionPanelPrefab);
            GameObject newActionPanel = Instantiate(objToAdd);

            // 2. Set its parent to the container (false keeps UI scaling/anchoring correct)
            newActionPanel.transform.SetParent(view.actionContainer.transform, false);

            return newActionPanel;
            // 3. (Optional) do something with objToAdd (e.g., attach it or pass data)
            // Example: if objToAdd is some UI element you want to place inside the new panel
            //objToAdd.transform.SetParent(newActionPanel.transform, false);

            // 4. (Optional) configure text/images/etc inside the new panel
            // var text = newActionPanel.GetComponentInChildren<Text>();
            // if (text != null) text.text = "Action Added!";
        }
        return null;
    }

    /// <summary>
    /// Adds another action section in the creature data panel
    /// </summary>
    /// <param name="comp"></param>
    /// <param name="data"></param>
    public void OnAddAction(Component comp, object data)
    {
        OnAddAction(data);
    }


    /// <summary>
    /// Clears children of action panel container, and saves the data of the creature depending on param.
    /// </summary>
    private void _ClearActionsAndSave(Creature creature, bool save)
    {
        Debug.Log($"{_debugStart}Clearing all actions | save: {save}");

        foreach (Transform child in view.actionContainer.transform)
        {
            ActionPanelInfo actionPanelinfo;
            if (!child.gameObject.TryGetComponent<ActionPanelInfo>(out actionPanelinfo))
            {
                Debug.Log($"{_debugStart}oh uhhh... couldn't get `ActionPanelInfo` from actionContainer child");
            }
            else if (save)
            {
                // record info and store it
                creature.GetSaveData().AddAction(actionPanelinfo);
            }

            Destroy(child.gameObject);
        }
    }

}
