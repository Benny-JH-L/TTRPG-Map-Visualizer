
/// <summary>
/// Commonly shared attributes/data of all creatures
/// </summary>
[System.Serializable]
public class CreatureData : GeneralObjectData
{
    public Species species;
    public ClassType className;
    public CoreStats coreStats;
    public string additionalInfo;

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
        SetAdditionalInfo();

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

    // constructor to create a save data from a JSON file.

    // outut to json


}
