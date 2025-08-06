
using NUnit.Framework;
using System;
using UnityEditor;
using UnityEngine;

public enum CreatureType
{
    Player = 1,
    Enemy = 2,
    Other = 3 // will have more like horse, spirits, summons, etc.
}

[System.Serializable]
public class Creature : MonoBehaviour
{
    public CreatureSaveData saveData;
    public GameObject disk;
    //GameObject modelOnDisk; // for later version (child of disk)

    // ALL GAME EVENT LISTENERS WILL BE ON A GAME OBJECT
    //public GameEventListener onMovement;
    //public GameEventListener onMouseRightClick;
    //public GameEventListener onSelectedObject;

    public GameEvent spawnedObject;

    public Creature(CreatureType creatureType)
    {
        saveData = new CreatureSaveData();

        // Set default value
        switch (creatureType)
        {
            case CreatureType.Player:
                break;
            case CreatureType.Enemy:
                break;
            case CreatureType.Other:
                break;
        }

        Tuple<CreatureType, Creature> data = new Tuple<CreatureType, Creature>(creatureType, this);
        spawnedObject.Raise(this, data);
    }

}
