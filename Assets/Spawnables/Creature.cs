using UnityEngine;

// A wrapper for GameObjects
[System.Serializable]
public class Creature : GeneralObject
{    
    public new CreatureData saveData;  // will make it protected, public is to test/debug [in Unity editor, it will show as Creature save data for all sub classes]
    //GameObject modelOnDisk; // for later version (child of disk)

    public int diskRadius = 1;  // NEED TO guess and check
    protected CreatureTag creatureTag;
    protected TeamTag teamTag;

    // ALL GAME EVENT LISTENERS WILL BE ON A GAME OBJECT
    //public GameEventListener onMovement;
    //public GameEventListener onMouseRightClick;
    //public GameEventListener onSelectedObject;

    public static Creature Create(Vector3 pos)
    {
        CreatureData saveData = new();
        
        return Create(pos, saveData);
    }

    public static Creature Create(Vector3 pos, CreatureData saveData)
    {
        Debug.Log("Creating pure creature obj...");
        GameObject obj = CreateGameObject(pos);
        Creature creature = obj.AddComponent<Creature>();
        creature.Init(obj, saveData); // set values

        Debug.Log("Pure creature obj created...");

        return creature;
    }

    /// <summary>
    /// Sets values for Creature and derivatives, and adds itself to the list of Creatures 
    /// </summary>
    /// <param name="diskBase"></param>
    /// <param name="data"></param>
    protected void Init(GameObject diskBase, CreatureData data)
    {
        //Debug.Log("Creature init");
        base.Init(diskBase, data);

        saveData = data;

        // wont be done directly in the future, will prolly use events.
        gameData.creatureList.Add(diskBase.GetComponent<Creature>());
        //creatureDisk = obj;
        //saveData = data;
        //gameData.creatureList.Add(obj.GetComponent<Creature>());
        //Debug.Log("Player component to string: " + comp.ToString());
    }

    public CreatureData GetSaveData()
    {
        return saveData;
    }

    public CreatureTag GetCreatureTag()
    {
        return creatureTag;
    }
}
