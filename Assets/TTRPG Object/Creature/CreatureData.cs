
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using static UnityEditor.Searcher.SearcherWindow;

/// <summary>
/// Commonly shared attributes/data of all creatures
/// </summary>
[System.Serializable]
public class CreatureData : GeneralObjectData
{
    private static string _debugStart = "CreatureData | ";
    public TTRPG_Species species;
    public TTRPG_Class className;
    public TTRPG_Alignments alignment;
    public TTRPG_CoreStats coreStats;
    public string additionalInfo;

    public List<string> actionNames;  // will prolly use these...
    public List<string> actionDescriptions;

    // saving throws
    // dmg vulnerabilities
    // dmg resistance
    // dmg immunity
    // sences
    // languages

    // actions; attacks, spells (prob another tab), etc..

    public CreatureData()
    {
        // default values
        species = TTRPG_Species.Humanoid; 
        className = TTRPG_Class.NONE;
        alignment = TTRPG_Alignments.NONE;    
        coreStats = new TTRPG_CoreStats();
        SetAdditionalInfo();

        actionNames = new List<string>();
        actionDescriptions = new List<string>();
        // set default values -> from a json file prolly
    }

    private void SetAdditionalInfo()
    {
        additionalInfo =
            "Saving Throws: " +
            "\nSkills: " +
            "\nDamage Vulnerabilities: " +
            "\nDamage Resistances: " +
            "\nDamage Immunities: " +
            "\nSenses: " +
            "\nLanguages: "
            ;
    }

    public void AddAction(ActionPanelInfo actionInfo)
    {
        if (actionInfo.actionDescription == string.Empty && actionInfo.actionName == string.Empty)
            return;

        actionNames.Add(actionInfo.actionName);
        actionDescriptions.Add(actionInfo.actionDescription);

        UnityEngine.Debug.Log($"{_debugStart}added Action: [{actionInfo.actionName} : {actionInfo.actionDescription}]");
    }

    // constructor to create a save data from a JSON file.

    // outut to json


}
