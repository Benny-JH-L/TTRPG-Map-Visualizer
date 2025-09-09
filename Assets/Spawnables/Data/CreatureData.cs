
/// <summary>
/// Commonly shared attributes/data of all creatures
/// </summary>
[System.Serializable]
public class CreatureData : GeneralObjectData
{
    public CreatureType creatureType;
    public CoreStats coreStats;
    // saving throws
    // dmg vulnerabilities
    // dmg resistance
    // dmg immunity
    // sences
    // languages

    // actions; attacks, spells (prob another tab), etc..

    public CreatureData()
    {
        creatureType = CreatureType.Humanoid; // by default
        coreStats = new CoreStats();
        
        // set default values -> from a json file prolly
    }

    // constructor to create a save data from a JSON file.

    // outut to json

}
