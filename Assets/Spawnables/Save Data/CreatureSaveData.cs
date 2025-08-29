

/// <summary>
/// Commonly shared attributes/data of all creatures
/// </summary>
[System.Serializable]
public class CreatureSaveData
{
    public string creatureName;
    public int ac;
    public int speed;
    public int hp;

    public CreatureSaveData()
    {
        creatureName = "NULL";
        ac = 0;
        speed = 0;
        hp = 0;
    }

    // constructor to create a save data from a JSON file.

    // outut to json

}
