//using UnityEngine;
//using UnityEngine.UIElements;

//// A wrapper for GameObjects
//[System.Serializable]
//public class Creature_OLD : GeneralObject_OLD
//{
//    private new static string _debugStart = "Creature | ";
//    public new CreatureData_OLD saveData;  // will make it protected, public is to test/debug [in Unity editor, it will show as Creature save data for all sub classes]
//    //GameObject modelOnDisk; // for later version (child of disk)

//    protected CreatureTag creatureTag;
//    protected TeamTag teamTag;

//    // ALL GAME EVENT LISTENERS WILL BE ON A GAME OBJECT
//    //public GameEventListener onMovement;
//    //public GameEventListener onMouseRightClick;
//    //public GameEventListener onSelectedObject;

//    public static Creature_OLD Create(Vector3 pos)
//    {
//        CreatureData_OLD saveData = new();
//        return Create(pos, saveData);
//    }

//    public static Creature_OLD Create(Vector3 pos, CreatureData_OLD saveData)
//    {
//        if (!IsPositionSpawnable(pos))
//        {
//            Debug.Log($"{_debugStart}Can't spawn, intersects with a GeneralObject");
//            return null;
//        }

//        GameObject obj = CreateGameObject(pos);
//        Creature_OLD creature = obj.AddComponent<Creature_OLD>();
//        creature.Init(obj, saveData); // set values

//        Debug.Log($"{_debugStart}Creature obj created...");

//        return creature;
//    }

//    /// <summary>
//    /// Sets values for Creature and derivatives, and adds itself to the list of Creatures 
//    /// </summary>
//    /// <param name="diskBase"></param>
//    /// <param name="data"></param>
//    protected void Init(GameObject diskBase, CreatureData_OLD data)
//    {
//        //Debug.Log("Creature init");
//        base.Init(diskBase, data);

//        saveData = data;

//        // wont be done directly in the future, will prolly use events.
//        gameData.creatureList_OLD.Add(diskBase.GetComponent<Creature_OLD>());
//        //creatureDisk = obj;
//        //saveData = data;
//        //gameData.creatureList.Add(obj.GetComponent<Creature>());
//        //Debug.Log("Player component to string: " + comp.ToString());
//    }

//    private void OnDestroy()
//    {
//        Debug.Log($"{_debugStart}OnDestroy");

//        gameData.creatureList_OLD.Remove(this);
//        base.RemoveFromGame();
//    }

//    public CreatureData_OLD GetSaveData()
//    {
//        return saveData;
//    }

//    public CreatureTag GetCreatureTag()
//    {
//        return creatureTag;
//    }
//}
