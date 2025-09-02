
/// <summary>
/// Core stats of a creature. Ex. speed, hp, ac, alignment, size, str, con, etc.
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
    /// For ability score < 1, it will be set to 1. MAX modifier value is +5 and LOWEST is -5.
    /// </summary>
    [System.Serializable]
    public class Attribute
    {
        // make private, use public for testing -?
        public int abilityScore;
        public int modifier;

        public Attribute(int abilityScore) 
        {
            SetNewAbilityScore(abilityScore);
        }

        /// <summary>
        /// Set the new ability score value, and updates modifier value.
        /// </summary>
        /// <param name="abilityScore"></param>
        public void SetNewAbilityScore(int abilityScore)
        {
            if (abilityScore <= 1)
                abilityScore = 1;
            this.abilityScore = abilityScore;
            SetModifierValue();
        }

        /// <summary>
        /// Sets the modifier value using the `abilityScore` value
        /// </summary>
        private void SetModifierValue()
        {
            if (abilityScore == 1)
                modifier = -5;
            else if (abilityScore <= 3)
                modifier = -4;
            else if (abilityScore <= 5)
                modifier = -3;
            else if (abilityScore <= 7)
                modifier = -2;
            else if (abilityScore <= 9)
                modifier = -1;
            else if (abilityScore <= 11)
                modifier = 0;
            else if (abilityScore <= 13)
                modifier = 1;
            else if (abilityScore <= 15)
                modifier = 2;
            else if (abilityScore <= 17)
                modifier = 3;
            else if (abilityScore <= 19)
                modifier = 4;
            else // abs >= 20
                modifier = 5;
        }

        public int GetAbilityScore()
        { 
            return abilityScore; 
        }

        public int GetModifier()
        {
            return modifier;
        }
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

    public string name;
    public CreatureSize size;
    public string alignment; // note: will make this into a class to keep track of alignments / or use enum

    public int ac;
    public Speed speedTypes;
    public int hp;

    public Attribute strength;
    public Attribute dexterity;
    public Attribute constitution;
    public Attribute intelligence;
    public Attribute wisdom;
    public Attribute charaisma;

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
        name = null;
        size = CreatureSize.Medium;
        alignment = null;
        ac = 0;

        speedTypes.speed = 0;
        speedTypes.burrow = 0;
        speedTypes.climb = 0;
        speedTypes.fly = 0;
        speedTypes.swim = 0;

        hp = 0;
        strength = new(0);
        dexterity = new(0);
        constitution = new(0);
        intelligence = new(0);
        wisdom = new(0);
        charaisma = new(0);
    }

}

