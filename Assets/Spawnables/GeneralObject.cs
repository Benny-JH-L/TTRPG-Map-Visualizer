using UnityEngine;

public abstract class GeneralObject : MonoBehaviour
{
    public static GameData gameData;
    public static GameObject diskPrefab;

    public static GameEvent spawnedObjectEvent; // do i need?

    public static Vector3 yOffsetDiskSpawn = new(0f, 0.5f, 0f);

    public float diskBaseRadius = 1.7f;  // NEED TO guess and check
    public GameObject diskBase; // should also be the var `gameObject` (they are the same)
    //public GameObject modelOnBase;    // later implmentation

    public GeneralObjectData saveData;  // will make it protected, public is to test/debug [in Unity editor, it will show as Creature save data for all sub classes]
    //public string objectName = "NAME"; // here or put into savedata?
    public string objectName = string.Empty; // here or put into savedata?


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

    // CAN'T BE DONE BECAUSE THIS IS A ABSTRACT CLASS
    //public static GeneralObject Create(Vector3 pos)
    //{
    //    GeneralObjectData generalObjectData = new();
    //    return Create(pos, generalObjectData);
    //}

    //public static GeneralObject Create(Vector3 pos, GeneralObjectData saveData)
    //{
    //    Debug.Log("Creating General obj...");
    //    GameObject obj = CreateGameObject(pos);
    //    GeneralObject generalObject = obj.AddComponent<GeneralObject>();
    //    generalObject.Init(obj, saveData);
    //    return generalObject;
    //}

    /// <summary>
    /// Checks if the `spawnPosition` will collide with any GenrealObjects in the map within that object's `radius`.
    /// </summary>
    /// <param name="spawnPosition"></param>
    /// <returns>Return True, if `spawnPosition` wont collide with anything, False otherwise.</returns>
    protected static bool IsPositionSpawnable(Vector3 spawnPosition)
    {
        foreach (GeneralObject generalObj in gameData.generalObjectList)
        {
            Vector3 generalObjPos = generalObj.GetPosition();
            float magnitudeFromSpawnPosToObjPos = (generalObjPos - spawnPosition).magnitude;
            if (magnitudeFromSpawnPosToObjPos <= generalObj.diskBaseRadius)
                return false;
        }
        return true;
    }

    protected void Init(GameObject diskBase, GeneralObjectData data)
    {
        this.diskBase = diskBase;
        saveData = data;

        // wont be done directly in the future, will prolly use events.
        gameData.generalObjectList.Add(diskBase.GetComponent<GeneralObject>());
    }

    public Vector3 GetPosition()
    {
        return diskBase.transform.position;
    }
}
