
/// <summary>
/// Core stats of a creature. Ex. speed, hp, ac, size, str, con, etc.
/// </summary>
[System.Serializable]
public class CoreStats
{
    public enum CreatureSize
    {
        Tiny = 0,
        Small = 1,
        Medium = 2,
        Large = 3,
        Huge = 4,
        Gargantuan = 5,
    }

    public struct Speed
    {
        public int speed;
        public int burrow;
        public int climb;
        public int fly;
        public int swim;
    }

    /// <summary>
    /// to delete prolly
    /// </summary>
    public enum SpeedType
    { 
        speed = 0, 
        burrow = 1, 
        climb = 2, 
        fly = 3, 
        swim = 4 
    }


    //[System.Serializable]
    //public struct CoreStat
    //{
    //public string name;
    //public CreatureSize size;
    //public string alignment; // note: will make this into a class to keep track of alignments / or use enum

    //public int ac;
    //public Speed speedTypes;
    //public int hp;

    //public int strength;
    //public int dexterity;
    //public int constitution;
    //public int intelligence;
    //public int wisdom;
    //public int charaisma;
    //}
    //public CoreStat stats;

    /// <summary>
    /// IF THIS VALUE IS 0, Unity will treat is as a null object for some reason. ENSURE THIS NUMBER IS > 0!
    /// </summary>
    public int defaultInitVal = 5; 
    public CreatureSize size;

    public int ac;
    public Speed speedTypes;
    public int hp;

    public Attribute strength;
    public Attribute dexterity;
    public Attribute constitution;
    public Attribute intelligence;
    public Attribute wisdom;
    public Attribute charaisma;

    // Must be set in initializer first
    public static GameEvent strModChanged;
    public static GameEvent dexModChanged;
    public static GameEvent conModChanged;
    public static GameEvent intModChanged;
    public static GameEvent wisModChanged;
    public static GameEvent chaModChanged;

    public CoreStats()
    {
        //stats.name = null;
        //stats.size = CreatureSize.Medium;
        //stats.alignment = null;
        //stats.ac = 0;

        //stats.speedTypes.speed = 0;
        //stats.speedTypes.burrow = 0;
        //stats.speedTypes.climb = 0;
        //stats.speedTypes.fly = 0;
        //stats.speedTypes.swim = 0;

        //stats.hp = 0;
        //stats.strength = 0;
        //stats.dexterity = 0;
        //stats.constitution = 0;
        //stats.intelligence = 0;
        //stats.wisdom = 0;
        //stats.charaisma = 0;
        size = CreatureSize.Medium;
        ac = defaultInitVal;

        speedTypes.speed = defaultInitVal;
        speedTypes.burrow = defaultInitVal;
        speedTypes.climb = defaultInitVal;
        speedTypes.fly = defaultInitVal;
        speedTypes.swim = defaultInitVal;

        hp = defaultInitVal;
        strength = new(strModChanged, 0);
        dexterity = new(dexModChanged, 0);
        constitution = new(conModChanged, 0);
        intelligence = new(intModChanged, 0);
        wisdom = new(wisModChanged, 0);
        charaisma = new(chaModChanged, 0);
    }

    public override string ToString()
    {
        return $"" +
            $"\nAC:{ac}" +
            $"\nHP:{hp}" +
            $"\n[Speed: {speedTypes.speed} | Burrow: {speedTypes.burrow} | Climb: {speedTypes.climb} | Fly: {speedTypes.fly} | Swim: {speedTypes.swim}]" +
            $"\nSTR:{strength}" +
            $"\nDEX: {dexterity}" +
            $"\nCON:{constitution}" +
            $"\nINT:{intelligence}" +
            $"\nWIS:{wisdom}" +
            $"\nCHA:{charaisma}" +
            $""
            ;
    }

}

