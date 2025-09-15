/// <summary>
/// For ability score < 1, it will be set to 1. MAX modifier value is +5 and LOWEST is -5.
/// </summary>
[System.Serializable]
public class Attribute
{
    // make private, use public for testing -?
    public int abilityScore;
    public int modifier;
    public GameEvent attributeModifierChanged;

    public Attribute(GameEvent attributeModifierChanged, int abilityScore)
    {
        this.attributeModifierChanged = attributeModifierChanged;
        SetNewAbilityScore(abilityScore);
    }

    /// <summary>
    /// Set the new ability score value, and updates modifier value.
    /// Raises `attributeModifierChanged` event.
    /// </summary>
    /// <param name="abilityScore"></param>
    public void SetNewAbilityScore(int abilityScore)
    {
        if (abilityScore <= 1)
            abilityScore = 1;
        this.abilityScore = abilityScore;
        SetModifierValue();
        attributeModifierChanged.Raise(null, modifier);
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

    public override string ToString()
    {
        string modStr = modifier >= 0 ? "+" + modifier : modifier.ToString();
        return $"[{abilityScore}] ({modStr})";
    }
}