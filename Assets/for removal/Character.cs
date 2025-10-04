
using UnityEngine;

/// <summary>
/// NPCS, PCS, and whatever characters. A wrapper for GameObjects
/// </summary>
[System.Serializable]
public class Character : Creature
{
    private static string _debugStart = "Character Class | ";

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

    public static new Character Create(Vector3 pos)
    {
        CharacterData saveData = new();

        return Create(pos, saveData);
    }

    public static Character Create(Vector3 pos, CharacterData saveData)
    {
        Debug.Log(_debugStart + "Creating character...");
        GameObject obj = CreateGameObject(pos);
        Character character = obj.AddComponent<Character>();
        character.Init(obj, saveData); // set values

        gameData.characterList.Add(character);

        return character;
    }

    /// <summary>
    /// if i use `new` runtime polymorphism wont work, if i call this with `Creature c = new Character; c.GetSaveData()` it will give me the creature save data
    /// </summary>
    /// <returns></returns>
    public new CharacterData GetSaveData() 
    {
        return (CharacterData)saveData;
    }

 }
