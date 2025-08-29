
using UnityEngine;

[System.Serializable]

public class Enemy : Character
{
    public static new Enemy Create(Vector3 pos)
    {
        CharacterSaveData saveData = new()
        {
            // set default values -> from a json file prolly
            //creatureType = CreatureType.Enemy,
            ac = 10
            // ...
        };

        return Create(pos, saveData);
    }

    public static new Enemy Create(Vector3 pos, CharacterSaveData saveData)
    {
        Debug.Log("Creating enemy obj...");
        GameObject obj = Character.CreateGameObject(pos);
        Enemy enemy = obj.AddComponent<Enemy>();
        enemy.Init(obj, saveData); // set values

        Character.gameData.enemyList.Add(enemy);;

        Debug.Log("enemy obj created...");

        return enemy;
    }
}
