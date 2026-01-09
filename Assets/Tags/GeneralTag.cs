
public abstract class GeneralTag
{
    private string tagName;
    // variable to indicate colour

    protected GeneralTag(string name)
    {
        tagName = name;
    }
    
    /// <summary>
    /// DOES NOTHING RN
    /// </summary>
    public static void InitializeAllTags()
    {
        //CreatureTag.Initialize();
        // and for other tags
    }

    public string GetTagName()
    {
        return tagName;
    }

    // function to remove leading and trailing white spaces!

}
