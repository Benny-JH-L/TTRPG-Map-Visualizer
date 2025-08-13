
using UnityEngine;

[System.Serializable]

public class Enemy : Creature
{
    public static new Enemy Create(Vector3 pos)
    {
        CreatureSaveData saveData = new()
        {
            // set default values -> from a json file prolly
            creatureType = CreatureType.Enemy,
            ac = 10
            // ...
        };

        return Create(pos, saveData);
    }

    public static new Enemy Create(Vector3 pos, CreatureSaveData saveData)
    {
        Debug.Log("Creating enemy obj...");
        GameObject obj = Creature.CreateGameObject(pos);
        Enemy enemy = obj.AddComponent<Enemy>();
        enemy.Init(obj, saveData); // set values

        Creature.gameData.enemyList.Add(enemy);;

        Debug.Log("enemy obj created...");

        return enemy;
    }
}
