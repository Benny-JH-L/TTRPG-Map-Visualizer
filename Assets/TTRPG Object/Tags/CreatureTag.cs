
using System.Collections.Generic;

public class CreatureTag : GeneralTag
{
    //private static List<TeamTag> teamTags;
    private static Dictionary<string, CreatureTag> creatureTags;

    private CreatureTag(string name) : base(name)
    {
        creatureTags[name] = this;
    }

    /// <summary>
    /// Creates a Tag instance with the parameters.
    /// </summary>
    /// <param name="name"></param>
    /// <returns>a TeamTag instance.</returns>
    public static CreatureTag Create(string name)
    {
        if (creatureTags.ContainsKey(name))
            return creatureTags[name];

        return new CreatureTag(name);
    }

    /// <summary>
    /// Must be called to initialize the teamTags Dictionary. Can be an empty or filled Dictionary of the parameter's type.
    /// </summary>
    /// <param name="teamTagsList"></param>
    public static void Initialize(Dictionary<string, CreatureTag> teamTagsDict)
    {
        creatureTags = teamTagsDict;

        // will make it read from a JSON file to do initialization, param may be a file location?
    }

}
