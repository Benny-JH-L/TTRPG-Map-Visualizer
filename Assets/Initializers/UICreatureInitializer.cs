
using UnityEngine;

public class UICreatureInitializer : MonoBehaviour
{
    public GameObject creatureUIDataPanelPrefab;
    public GameObject creatureUIDataPanelContainer;

    public GameEvent initScoreSTR;
    public GameEvent initScoreDEX;
    public GameEvent initScoreCON;
    public GameEvent initScoreINT;
    public GameEvent initScoreWIS;
    public GameEvent initScoreCHA;

    private Creature _selectedCreature; //prolly not needed

    public void OnSelectedObject(Component comp, object data)
    {
        if (data is Creature creature)
        {
            _selectedCreature = creature;
            // set Creature UI Panel
            creatureUIDataPanelContainer = creatureUIDataPanelPrefab;
            // ...


            // Set initial UI values
            // ...
            // Set attribtue scores 
            CoreStats coreStats = creature.GetSaveData().coreStats;
            initScoreSTR.Raise(this, coreStats.strength);
            initScoreDEX.Raise(this, coreStats.dexterity);
            initScoreCON.Raise(this, coreStats.constitution);
            initScoreINT.Raise(this, coreStats.intelligence);
            initScoreWIS.Raise(this, coreStats.wisdom);
            initScoreCHA.Raise(this, coreStats.charaisma);
        }

    }

}
