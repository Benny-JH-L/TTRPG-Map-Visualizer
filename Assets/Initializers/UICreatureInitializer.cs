
using System;
using System.Net.NetworkInformation;
using UnityEngine;

/// <summary>
/// Initializes Creature UI Values.
/// </summary>
public class UICreatureInitializer : MonoBehaviour
{
    private static string _debugStart = "UICreatureInitializer | ";
    public GameObject creatureUIDataPanelPrefab;
    public View view;

    public InitGameEventStorage initGameEventStorage;

    private Creature _selectedCreature; //prolly not needed

    public void OnSelectedObject(Component comp, object data)
    {
        if (data is Creature creature)
        {
            Debug.Log($"{_debugStart}Creature selected");
            _selectedCreature = creature;

            // set Creature UI Panel
            view.rightContainer = creatureUIDataPanelPrefab;
            // ...


            // Set initial UI values
            initGameEventStorage.initCreatureName.Raise(this, _selectedCreature.GetSaveData().objectName);
            // ...
            // Set attribtue scores 
            CoreStats coreStats = creature.GetSaveData().coreStats;
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

            Debug.Log($"Stats of {creature.GetSaveData().objectName}:\n{coreStats}");
        }

    }

}
