
using UnityEngine;

/// <summary>
/// NPCS, PCS, and whatever characters. A wrapper for GameObjects
/// </summary>
[System.Serializable]
public class Character : Creature
{
    //public static GameData gameData;
    //public static GameObject diskPrefab;

    //public static GameEvent spawnedObjectEvent;

    //public static Vector3 yOffsetDiskSpawn = new Vector3(0f, 0.5f, 0f);

    //public CharacterSaveData saveData;
    //public GameObject creatureDisk; // should also be the var `gameObject`
    ////GameObject modelOnDisk; // for later version (child of disk)

    //public int diskRadius = 1;  // NEED TO guess and check


    // ALL GAME EVENT LISTENERS WILL BE ON A GAME OBJECT
    //public GameEventListener onMovement;
    //public GameEventListener onMouseRightClick;
    //public GameEventListener onSelectedObject;

    // a way to associate a pet/owner relationship? (using a list)

    public static Character Create(Vector3 pos)
    {
        CharacterSaveData saveData = new()
        {
            // set default values -> from a json file prolly
            ac = 10
            // ...
        };

        return Create(pos, saveData);
    }

    public static Character Create(Vector3 pos, CharacterSaveData saveData)
    {
        Debug.Log("Creating pure creature obj...");
        GameObject obj = CreateGameObject(pos);
        Character creature = obj.AddComponent<Character>();
        creature.Init(obj, saveData); // set values

        Debug.Log("Pure creature obj created...");

        return creature;
    }

    /// <summary>
    /// if i use `new` runtime polymorphism wont work, if i call this with `Creature c = new Character; c.GetSaveData()` it will give me the creature save data
    /// </summary>
    /// <returns></returns>
    public new CharacterSaveData GetSaveData() 
    {
        return (CharacterSaveData)saveData;
    }

 }
