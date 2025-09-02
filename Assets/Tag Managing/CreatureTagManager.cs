

using System.Collections.Generic;

[System.Serializable]
public class CreatureTagManager
{
    //private string _noTag = "No Tag";
    //private TeamTag _noTag = new TeamTag("No Tag");
    private CreatureTag _noTag = CreatureTag.Create("No Tag");
    private CreatureTag _NeutralTag = CreatureTag.Create("Neutral");

    // listens to spawned creature event -> yes will need to update all other spawned events that work 
    // on character spawning.

    /*
    idea is game event listener triggers this, when a creature is spawned.
    then it puts it automatically tags it as `Neutral`.
    
    Then when the user chooses a new tag for it, game event listener, triggers and 
    updates the tag for that creature, and put's it in its new team, (also removing itself
    from the old team)

    thinking of using a dictionary with {key: tag name | value: list of creatures/characters}
    (could make it faster with a list instead, where a dicionary is used to track tags with a number, think of OS class)
    (but it would see gains with VERY VERY LARGE INPUTS)
    */

    //private Dictionary<string, List<Creature>> teamsAndMembersDictionary;
    private Dictionary<CreatureTag, List<Creature>> teamsAndMembersDictionary;

    //public TeamTracker(List<string> tagList) : this()
    //{
    //    foreach (string tag in tagList)
    //        teamsAndMembersDictionary.Add(tag, new List<Creature>());
    //}

    public CreatureTagManager(List<CreatureTag> tagList) : this()
    {
        foreach (CreatureTag tag in tagList)
            teamsAndMembersDictionary.Add(tag, new List<Creature>());
    }

    public CreatureTagManager()
    {
        teamsAndMembersDictionary = new Dictionary<CreatureTag, List<Creature>>();
        teamsAndMembersDictionary.Add(_noTag, new List<Creature>());
    }

    public void add(CreatureTag tag, Creature creature)  // associates the tag and creature 
    {
        if (!tagExists(tag))
            teamsAndMembersDictionary[tag] = new List<Creature>();
        teamsAndMembersDictionary[tag].Add(creature);
    }

    /// <summary>
    /// Changes the team tag of a creature with the desired one.
    /// </summary>
    /// <param name="newTag"></param>
    /// <param name="creature"></param>
    /// <returns>True: if change was made, False otherwise.</returns>
    public bool changeTag(CreatureTag newTag, Creature creature)  // attempts to change the tag for this creature if it exists
    {
        // don't change tag
        if (creature.GetCreatureTag() == newTag)
            return true;

        // change the tag
        add(newTag, creature);
        teamsAndMembersDictionary[creature.GetCreatureTag()].Remove(creature);
        return true;
        ////bool creatureExists = teamsAndMembersDictionary.Values.Any(creatureList => creatureList.Contains(creature));
        
        //// find the creature in the dictionary
        //bool creatureExistsInDict = false;
        //foreach (KeyValuePair<TeamTag, List<Creature>> valuePair in teamsAndMembersDictionary)
        //{
        //    if (valuePair.Value.Contains(creature))
        //    {
        //        teamsAndMembersDictionary[valuePair.Key].Remove(creature);
        //        creatureExistsInDict = true;
        //        break;
        //    }
        //}

        //if (creatureExistsInDict)
        //    add(newTag, creature);

        //return creatureExistsInDict;    // true, change was made, false otherwise
    }

    //public bool removeCreature(TeamTag tag, Creature creature)  // attempt to remove the creature with the tag (dangerous, idk if i wanna add!) true: was removed, false otherwise
    //{
    //    if (!tagExists(tag))
    //        return false;

    //    //return teamsAndMembersDictionary[tag].Remove(creature);
    //}

    /// <summary>
    /// Removes a creature from the specified TeamTag
    /// </summary>
    /// <param name="creature"></param>
    /// <returns>True if the creature was removed, false otherwise.</returns>
    public bool removeCreature(Creature creature) // true: was removed, false otherwise
    {
        return teamsAndMembersDictionary[creature.GetCreatureTag()].Remove(creature);
        //// find the creature in the dictionary
        //foreach (KeyValuePair<string, List<Creature>> valuePair in teamsAndMembersDictionary)
        //{
        //    if (teamsAndMembersDictionary[valuePair.Key].Remove(creature))
        //        return true;    // creature was found and removed, return true.
        //}
        //return false;           // creature was not found, or removed.
    }

    /// <summary>
    ///[will be done in Tag class and will trigger an event that will make the creatures have the "no tag" tag] Removes the desired TeamTag from existing tags, and all Creatures within that tag will be re-tagged to "No Tag".
    /// </summary>
    /// <param name="tag"></param>
    /// <returns>True if the Tag was removed, false otherwise.</returns>
    public bool removeTag(CreatureTag tag)
    {
        if (!tagExists(tag))
            return false;

        List<Creature> list = teamsAndMembersDictionary[tag];
        teamsAndMembersDictionary[_noTag].AddRange(list);   // send the members of `tag` into the `no_tag` tag
        teamsAndMembersDictionary.Remove(tag);              // remove the tag
        return true;       
    }

    private bool tagExists(CreatureTag tag)
    {
        return teamsAndMembersDictionary.ContainsKey(tag);
    }

    // need to implment; rename a tag
}
