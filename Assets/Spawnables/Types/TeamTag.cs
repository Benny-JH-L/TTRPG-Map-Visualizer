

using System.Collections.Generic;

public class TeamTag
{
    //private static List<TeamTag> teamTags;
    private static Dictionary<string, TeamTag> teamTags;

    private string tagName;
    // variable to indicate colour

    private TeamTag(string name)
    {
        tagName = name;
        teamTags[name] = this;
    }

    /// <summary>
    /// Creates a TeamTag instance with the parameters.
    /// </summary>
    /// <param name="name"></param>
    /// <returns>a TeamTag instance.</returns>
    public static TeamTag Create(string name)
    {
        if (teamTags.ContainsKey(name))
            return teamTags[name];
           
        return new TeamTag(name);
    }

    /// <summary>
    /// Must be called to initialize the teamTags Dictionary. Can be an empty or filled Dictionary of the parameter's type.
    /// </summary>
    /// <param name="teamTagsList"></param>
    public static void Initialize(Dictionary<string, TeamTag> teamTagsDict)
    {
        teamTags = teamTagsDict;
    }


    public string GetTeamTagName()
    {
        return tagName;
    }

    // function to remove leading and trailing white spaces!

}
