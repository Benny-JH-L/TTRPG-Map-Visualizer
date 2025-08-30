using UnityEngine;

// A wrapper for GameObjects
[System.Serializable]
public class Creature : GeneralObject
{    
    public static GameEvent spawnedObjectEvent;

    public static Vector3 yOffsetDiskSpawn = new(0f, 0.5f, 0f);

    protected CreatureSaveData saveData;  // will make it protected, public is to test/debug [in Unity editor, it will show as Creature save data for all sub classes]
    public GameObject creatureDisk; // should also be the var `gameObject` (they are the same)
    //GameObject modelOnDisk; // for later version (child of disk)

    public int diskRadius = 1;  // NEED TO guess and check
    protected CreatureTag creatureTag;
    protected TeamTag teamTag;

    // ALL GAME EVENT LISTENERS WILL BE ON A GAME OBJECT
    //public GameEventListener onMovement;
    //public GameEventListener onMouseRightClick;
    //public GameEventListener onSelectedObject;

    //protected static GameObject CreateGameObject(CreatureType type, Vector3 position)
    protected static GameObject CreateGameObject(Vector3 position)
    {
        //return CreateGameObject(position, Quaternion.identity);
        return CreateGameObject(position, diskPrefab.transform.rotation);
    }

    protected static GameObject CreateGameObject(Vector3 position, Quaternion rotation)
    {
        //Debug.Log("Creating Creature GameObject");
        return Instantiate(diskPrefab, position + yOffsetDiskSpawn, rotation); // need to add a y-offset to the position or else the object will sometimes launch into the air (collides with the ground)
        //GameObject obj = Instantiate(diskPrefab, position, rotation);

        //object componentType = null;
        //switch (creatureType)
        //{
        //    case CreatureType.Player:
        //        componentType = obj.AddComponent<Player>();
        //        ((Player)componentType).Init(obj);
        //        gameData.playerList.Add((Player)componentType);
        //        break;
        //    case CreatureType.Enemy:
        //        componentType = obj.AddComponent<Enemy>();
        //        gameData.enemyList.Add((Enemy)componentType);
        //        break;
        //    case CreatureType.Other:
        //        componentType = obj.AddComponent<Creature>();
        //        break;
        //}
        //gameData.creatureList.Add((Creature)componentType);

        //return obj;
    }
    public static Creature Create(Vector3 pos)
    {
        CreatureSaveData saveData = new()
        {
            // set default values -> from a json file prolly
            ac = 10
            // ...
        };

        return Create(pos, saveData);
    }

    public static Creature Create(Vector3 pos, CreatureSaveData saveData)
    {
        Debug.Log("Creating pure creature obj...");
        GameObject obj = CreateGameObject(pos);
        Creature creature = obj.AddComponent<Character>();
        creature.Init(obj, saveData); // set values

        Debug.Log("Pure creature obj created...");

        return creature;
    }

    /// <summary>
    /// Sets values for Creature and derivatives, and adds itself to the list of Creatures 
    /// </summary>
    /// <param name="diskBase"></param>
    /// <param name="data"></param>
    protected void Init(GameObject diskBase, CreatureSaveData data)
    {
        //Debug.Log("Creature init");

        creatureDisk = diskBase;
        saveData = data;

        gameData.creatureList.Add(diskBase.GetComponent<Creature>());
        
        //creatureDisk = obj;
        //saveData = data;
        //gameData.creatureList.Add(obj.GetComponent<Creature>());
        //Debug.Log("Player component to string: " + comp.ToString());
    }

    public Vector3 GetPosition()
    {
        return creatureDisk.transform.position;
    }

    public CreatureSaveData GetSaveData()
    {
        return saveData;
    }

    public CreatureTag GetCreatureTag()
    {
        return creatureTag;
    }
}
