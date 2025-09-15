
using UnityEngine;
using System;

/// <summary>
/// Initializes Creature UI Values.
/// </summary>
public class UICreatureInitializer : MonoBehaviour
{
    private static string _debugStart = "UICreatureInitializer | ";
    public GameObject creatureUIDataPanelPrefab;
    public View view;

    public InitCreatureGameEventStorage initGameEventStorage;

    private Creature _selectedCreature; //prolly not needed

    public void OnSelectedObject(Component comp, object data)
    {
        if (data is Creature creature)
        {
            Debug.Log($"{_debugStart}Creature selected");
            _selectedCreature = creature;
            CreatureData creatureData = creature.GetSaveData();

            // set Creature UI Panel
            view.rightContainer = creatureUIDataPanelPrefab;
            // ...


            // Set initial UI values
            initGameEventStorage.initDropdownSpecies.Raise(this, EnumToString<Species>());
            PrintStringList(EnumToString<Species>());
            //initGameEventStorage.initDropdownAlignment.Raise(EnumToString<Alignment>);
            initGameEventStorage.initDropdownClass.Raise(this, EnumToString<ClassType>());

            initGameEventStorage.initCreatureName.Raise(this, creatureData.objectName);
            initGameEventStorage.initSpecies.Raise(this, ((int)creatureData.species));
            initGameEventStorage.initAlignment.Raise(this, creatureData.coreStats.alignment);
            initGameEventStorage.initClass.Raise(this, ((int)creatureData.className));

            // Set attribtue scores 
            //CoreStats coreStats = creature.GetSaveData().coreStats;
            CoreStats coreStats = creatureData.coreStats;

            //initScoreSTR.Raise(this, coreStats.strength.GetAbilityScore());
            //initScoreDEX.Raise(this, coreStats.dexterity.GetAbilityScore());
            //initScoreCON.Raise(this, coreStats.constitution.GetAbilityScore());
            //initScoreINT.Raise(this, coreStats.intelligence.GetAbilityScore());
            //initScoreWIS.Raise(this, coreStats.wisdom.GetAbilityScore());
            //initScoreCHA.Raise(this, coreStats.charaisma.GetAbilityScore());

            initGameEventStorage.initAC.Raise(this, coreStats.ac);
            initGameEventStorage.initHP.Raise(this, coreStats.hp);

            initGameEventStorage.initSpeed.Raise(this, coreStats.speedTypes.speed);
            initGameEventStorage.initBurrow.Raise(this, coreStats.speedTypes.burrow);
            initGameEventStorage.initClimb.Raise(this, coreStats.speedTypes.climb);
            initGameEventStorage.initFly.Raise(this, coreStats.speedTypes.fly);
            initGameEventStorage.initSwim.Raise(this, coreStats.speedTypes.swim);

            //Tuple<CoreStats.SpeedType, int> pair = new(CoreStats.SpeedType.speed, coreStats.speedTypes.speed);
            //initSpeedType.Raise(this, pair);
            //pair = new(CoreStats.SpeedType.burrow, coreStats.speedTypes.burrow);
            //initSpeedType.Raise(this, pair);
            //pair = new(CoreStats.SpeedType.climb, coreStats.speedTypes.climb);
            //initSpeedType.Raise(this, pair);
            //pair = new(CoreStats.SpeedType.fly, coreStats.speedTypes.fly);
            //initSpeedType.Raise(this, pair);
            //pair = new(CoreStats.SpeedType.swim, coreStats.speedTypes.swim);
            //initSpeedType.Raise(this, pair);

            initGameEventStorage.initScoreSTR.Raise(this, coreStats.strength);
            initGameEventStorage.initScoreDEX.Raise(this, coreStats.dexterity);
            initGameEventStorage.initScoreCON.Raise(this, coreStats.constitution);
            initGameEventStorage.initScoreINT.Raise(this, coreStats.intelligence);
            initGameEventStorage.initScoreWIS.Raise(this, coreStats.wisdom);
            initGameEventStorage.initScoreCHA.Raise(this, coreStats.charaisma);

            initGameEventStorage.initAdditionalInfo.Raise(this, creature.GetSaveData().additionalInfo);

            Debug.Log($"Stats of {creature.GetSaveData().objectName}:\n{coreStats}");
        }

    }

    private static string[] EnumToString<T>() where T : Enum
    {
        return Enum.GetNames(typeof(T));
    }

    private static void PrintStringList(String[] list)
    {
        string outPut = "[";
        foreach (String s in list)
            outPut += s + ", ";
        Debug.Log(outPut + "]");
    }
}
