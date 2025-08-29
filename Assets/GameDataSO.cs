
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameData", menuName = "Scriptable Objects/GameData")]
[System.Serializable]
public class GameData : ScriptableObject
{
    //GameEventListener onSpawnedObject;
    public List<Character> creatureList = new(); // everything, players, enemies, etc
    public List<Player> playerList = new(); // turn them into GameObject lists?
    public List<Enemy> enemyList = new();

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

    public List<Character> characterList = new(); 

    public void PrintPlayers()
    {
        string output = "";

        foreach (Player player in playerList)
        {
            output += "Player | " + player.GetPosition() + "\n";
        }
        Debug.Log(output);
    }

    public void PrintEnemies()
    {
        string output = "";

        foreach (Enemy enemy in enemyList)
        {
            output += "Enemy | " + enemy.GetPosition() + "\n";
        }
        Debug.Log(output);
    }

    public void PrintCreatures()
    {
        string output = "";

        foreach (Character creature in creatureList)
        {
            output += "Creature | " + creature.GetPosition() + "\n";
        }
        Debug.Log(output);
    }
}
