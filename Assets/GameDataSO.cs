using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameData", menuName = "Scriptable Objects/GameData")]
[System.Serializable]
public class GameData : ScriptableObject
{
    //GameEventListener onSpawnedObject;
    List<Player> playerList = new List<Player>();
    List<Enemy> enemyList = new List<Enemy>();
    List<Creature> creatureList = new List<Creature>();

    public void OnSpawnedObject(Component component, object data)
    {
        if (!(data is Tuple<CreatureType, Creature>))
        {
            Debug.Log("GameData: Incorrect data format | OnSpawnedObject");
            return;
        }

        Tuple<CreatureType, Creature> tup = (Tuple<CreatureType, Creature>)data;
        CreatureType creatureType = tup.Item1;

        switch (creatureType)
        {
            case CreatureType.Player:
                playerList.Add((Player)tup.Item2);
                break;

            case CreatureType.Enemy:
                enemyList.Add((Enemy)tup.Item2);
                break;

            case CreatureType.Other:
                creatureList.Add(tup.Item2);
                break;

        }
    }
}
