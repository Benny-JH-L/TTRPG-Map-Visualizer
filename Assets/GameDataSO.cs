
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameData", menuName = "Scriptable Objects/GameData")]
[System.Serializable]
public class GameData : ScriptableObject
{
    //GameEventListener onSpawnedObject;
    //public List<Creature_OLD> creatureList_OLD = new(); // everything, players, enemies, etc
    //public List<Character> characterList_OLD = new(); // turn them into GameObject lists?
    //public List<GeneralObject_OLD> generalObjectList_OLD = new();

    public List<Creature> creatureList = new();   // players, enemies, etc
    public List<TTRPG_SceneObjectBase> sceneObjectList = new(); // everything

    //public void OnSpawnedObject(Component component, object data)
    //{
    //    if (!(data is Tuple<CreatureType, Creature>))
    //    {
    //        Debug.Log("GameData: Incorrect data format | OnSpawnedObject");
    //        return;
    //    }

    //    Tuple<CreatureType, Creature> tup = (Tuple<CreatureType, Creature>)data;
    //    CreatureType creatureType = tup.Item1;

    //    switch (creatureType)
    //    {
    //        case CreatureType.Player:
    //            playerList.Add((Player)tup.Item2);
    //            break;

    //        case CreatureType.Enemy:
    //            enemyList.Add((Enemy)tup.Item2);
    //            break;

    //        case CreatureType.Other:
    //            creatureList.Add(tup.Item2);
    //            break;

    //    }
    //}

    //public void PrintGeneralObjects()
    //{
    //    string output = "";

    //    foreach (GeneralObject_OLD c in generalObjectList)
    //    {
    //        output += "General Object | " + c.GetPosition() + "\n";
    //    }
    //    Debug.Log(output);
    //}

    //public void PrintCharacters() // use tags
    //{
    //    string output = "";

    //    foreach (Character c in characterList)
    //    {
    //        output += "Character | " + c.GetPosition() + "\n";
    //    }
    //    Debug.Log(output);
    //}

    //public void PrintEnemies()    // use tags, printHOstiles to PCs
    //{
    //    string output = "";

    //    foreach (Enemy enemy in enemyList)
    //    {
    //        output += "Enemy | " + enemy.GetPosition() + "\n";
    //    }
    //    Debug.Log(output);
    //}

    //public void PrintCreatures()
    //{
    //    string output = "";

    //    foreach (Character creature in creatureList)
    //    {
    //        output += "Creature | " + creature.GetPosition() + "\n";
    //    }
    //    Debug.Log(output);
    //}
}
