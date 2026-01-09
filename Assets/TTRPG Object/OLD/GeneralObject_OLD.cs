//using UnityEngine;

//public abstract class GeneralObject_OLD : MonoBehaviour
//{
//    public static string _debugStart = "GeneralObject | ";
//    public static GameData gameData;
//    public static GameObject diskPrefab;

//    public static GameEventSO spawnedObjectEvent; // do i need?

//    public static Vector3 yOffsetDiskSpawn = new(0f, 0.5f, 0f);

//    public static int objectCount = 0;

//    [SerializeField] public float diskBaseRadius = 13f;  // NEED TO guess and check (its about 12.45f to be touching)
//    public GameObject diskBase; // should also be the var `gameObject` (they are the same)
//    //public GameObject modelOnBase;    // later implmentation

//    public GeneralObjectData_OLD saveData;  // will make it protected, public is to test/debug [in Unity editor, it will show as Creature save data for all sub classes]

//    //protected static GameObject CreateGameObject(CreatureType type, Vector3 position)
//    protected static GameObject CreateGameObject(Vector3 position)
//    {
//        //return CreateGameObject(position, Quaternion.identity);
//        return CreateGameObject(position, diskPrefab.transform.rotation);
//    }

//    protected static GameObject CreateGameObject(Vector3 position, Quaternion rotation)
//    {
//        //Debug.Log("Creating Creature GameObject");
//        objectCount++;
//        return Instantiate(diskPrefab, position + yOffsetDiskSpawn, rotation); // need to add a y-offset to the position or else the object will sometimes launch into the air (collides with the ground)
//        //GameObject obj = Instantiate(diskPrefab, position, rotation);

//        //object componentType = null;
//        //switch (creatureType)
//        //{
//        //    case CreatureType.Player:
//        //        componentType = obj.AddComponent<Player>();
//        //        ((Player)componentType).Init(obj);
//        //        gameData.playerList.Add((Player)componentType);
//        //        break;
//        //    case CreatureType.Enemy:
//        //        componentType = obj.AddComponent<Enemy>();
//        //        gameData.enemyList.Add((Enemy)componentType);
//        //        break;
//        //    case CreatureType.Other:
//        //        componentType = obj.AddComponent<Creature>();
//        //        break;
//        //}
//        //gameData.creatureList.Add((Creature)componentType);

//        //return obj;
//    }

//    // CAN'T BE DONE BECAUSE THIS IS A ABSTRACT CLASS
//    //public static GeneralObject Create(Vector3 pos)
//    //{
//    //    GeneralObjectData generalObjectData = new();
//    //    return Create(pos, generalObjectData);
//    //}

//    //public static GeneralObject Create(Vector3 pos, GeneralObjectData saveData)
//    //{
//    //    Debug.Log("Creating General obj...");
//    //    GameObject obj = CreateGameObject(pos);
//    //    GeneralObject generalObject = obj.AddComponent<GeneralObject>();
//    //    generalObject.Init(obj, saveData);
//    //    return generalObject;
//    //}

//    /// <summary>
//    /// Checks if the `spawnPosition` will collide with any GenrealObjects in the map within that object's `radius`.
//    /// </summary>
//    /// <param name="spawnPosition"></param>
//    /// <returns>Return True, if `spawnPosition` wont collide with anything, False otherwise.</returns>
//    protected static bool IsPositionSpawnable(Vector3 spawnPosition)
//    {
//        //Debug.Log($"spawn pos: {spawnPosition}");

//        foreach (GeneralObject_OLD generalObj in gameData.generalObjectList_OLD)
//        {
//            Vector3 generalObjPos = generalObj.GetPosition();
//            //float magnitudeFromSpawnPosToObjPos = (generalObjPos - spawnPosition).magnitude;
//            float magnitudeFromSpawnPosToObjPos = Vector3.Distance(generalObjPos, spawnPosition);
//            if (magnitudeFromSpawnPosToObjPos <= generalObj.diskBaseRadius)
//                return false;
//        }
//        return true;
//    }

//    protected void Init(GameObject diskBase, GeneralObjectData_OLD data)
//    {
//        this.diskBase = diskBase;
//        saveData = data;
//        saveData.objectName = "Object" + objectCount;

//        // wont be done directly in the future, will prolly use events.
//        gameData.generalObjectList_OLD.Add(diskBase.GetComponent<GeneralObject_OLD>());
//    }

//    public Vector3 GetPosition()
//    {
//        return diskBase.transform.position;
//    }

//    private void OnDestroy()
//    {
//        Debug.Log($"{_debugStart}OnDestroy");
//        RemoveFromGame();
//    }

//    protected void RemoveFromGame()
//    {
//        gameData.generalObjectList_OLD.Remove(this);
//        Destroy(diskBase);
//    }
//}
