
using UnityEngine;

public class UICreatureInitializer : MonoBehaviour
{
    private static string _debugStart = "UICreatureInitializer | ";
    public GameObject creatureUIDataPanelPrefab;
    public View view;

    public GameEvent initScoreSTR;
    public GameEvent initScoreDEX;
    public GameEvent initScoreCON;
    public GameEvent initScoreINT;
    public GameEvent initScoreWIS;
    public GameEvent initScoreCHA;

    public GameEvent initCreatureName;

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
            initCreatureName.Raise(this, _selectedCreature.objectName);
            // ...
            // Set attribtue scores 
            CoreStats coreStats = creature.GetSaveData().coreStats;
            //initScoreSTR.Raise(this, coreStats.strength.GetAbilityScore());
            //initScoreDEX.Raise(this, coreStats.dexterity.GetAbilityScore());
            //initScoreCON.Raise(this, coreStats.constitution.GetAbilityScore());
            //initScoreINT.Raise(this, coreStats.intelligence.GetAbilityScore());
            //initScoreWIS.Raise(this, coreStats.wisdom.GetAbilityScore());
            //initScoreCHA.Raise(this, coreStats.charaisma.GetAbilityScore());

            initScoreSTR.Raise(this, coreStats.strength);
            initScoreDEX.Raise(this, coreStats.dexterity);
            initScoreCON.Raise(this, coreStats.constitution);
            initScoreINT.Raise(this, coreStats.intelligence);
            initScoreWIS.Raise(this, coreStats.wisdom);
            initScoreCHA.Raise(this, coreStats.charaisma);
        }

    }

}
