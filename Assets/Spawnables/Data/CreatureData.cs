
/// <summary>
/// Commonly shared attributes/data of all creatures
/// </summary>
[System.Serializable]
public class CreatureData : GeneralObjectData
{
    public Species species;
    public ClassType className;
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
        species = Species.Humanoid; // by default
        coreStats = new CoreStats();
        
        // set default values -> from a json file prolly
    }

    // constructor to create a save data from a JSON file.

    // outut to json

}
