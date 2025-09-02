
/// <summary>
/// Commonly shared attributes/data of all creatures
/// </summary>
[System.Serializable]
public class CreatureData
{
    public CreatureType creatureType;
    public CoreStats coreStats;

    public CreatureData()
    {
        creatureType = CreatureType.Humanoid; // by default
        coreStats = new CoreStats();
        
        // set default values -> from a json file prolly
    }

    // constructor to create a save data from a JSON file.

    // outut to json

}
