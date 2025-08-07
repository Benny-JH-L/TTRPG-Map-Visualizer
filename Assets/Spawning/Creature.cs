
using System;
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
    public GameData gameData;
    public CreatureSaveData saveData;
    string diskPrefabName = "disk";
    public int diskRadius = 1;  // NEED TO guess and check

    public GameObject disk;
    //GameObject modelOnDisk; // for later version (child of disk)

    // ALL GAME EVENT LISTENERS WILL BE ON A GAME OBJECT
    //public GameEventListener onMovement;
    //public GameEventListener onMouseRightClick;
    //public GameEventListener onSelectedObject;

    public GameEvent spawnedObject;

    private string _debugbeginning = "Creature | ";

    public Creature(CreatureType creatureType, Vector3 position)
    {
        if (_CheckSpawnCollision(position))
        {
            Debug.Log(_debugbeginning + $"Can't Spawn at <{position}> game object detected there!");
            return;
        }

        saveData = new CreatureSaveData();
        saveData.creatureType = creatureType;

        // Set default creature values and other stuff
        switch (creatureType)
        {
            case CreatureType.Player:
                gameData.playerList.Add((Player)this);
                break;
            case CreatureType.Enemy:
                gameData.enemyList.Add((Enemy)this);
                break;
            case CreatureType.Other:
                gameData.creatureList.Add(this);
                break;
        }

        GameObject prefab = Resources.Load<GameObject>("Prefabs/" + diskPrefabName);
        disk = Instantiate(prefab, position, Quaternion.identity);
        Tuple<CreatureType, Creature> data = new Tuple<CreatureType, Creature>(creatureType, this);
        spawnedObject.Raise(this, data);
    }

    private bool _CheckSpawnCollision(Vector3 posToCheck)
    {
        for (int i = 0; i < gameData.playerList.Count; i++)
        {
            float distance = (posToCheck - gameData.playerList[i].disk.transform.position).magnitude;
            if (distance <= diskRadius)
                return true;
        }

        for (int i = 0; i < gameData.enemyList.Count; i++)
        {
            float distance = (posToCheck - gameData.enemyList[i].disk.transform.position).magnitude;
            if (distance <= diskRadius)
                return true;
        }

        for (int i = 0; i < gameData.creatureList.Count; i++)
        {
            float distance = (posToCheck - gameData.creatureList[i].disk.transform.position).magnitude;
            if (distance <= diskRadius)
                return true;
        }

        return false;   // no collision
    }

}
