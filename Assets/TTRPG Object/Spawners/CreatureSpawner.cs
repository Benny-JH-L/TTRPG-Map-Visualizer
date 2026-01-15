using UnityEngine;

public class CreatureSpawner : SpawnerBase<Creature>
{
    public DiskBaseSpawner diskBaseSpawner;
    public ModelApperanceSpawner modelApperanceSpawner;

    protected override void Setup()
    {
        // nothing...
    }

    protected override void Configure()
    {
        // nothing...
    }

    protected override void OnStart()
    {
        //if (mouseTracker == null)
        //    ErrorOutput.printError(this, "`mouseTracker` cannot be `null`");
        if (diskBaseSpawner == null)
            ErrorOut.Throw(this, "`diskBaseSpawner` cannot be `null`");
        if (modelApperanceSpawner == null)
            ErrorOut.Throw(this, "`modelApperanceSpawner` cannot be `null`");
    }

    protected override void DoAfterSpawn(GameData gameData, GameObject gameObj)
    {
        if (gameData == null)
            ErrorOut.Throw(this, "game data null");

        if (gameObj.TryGetComponent<Creature>(out Creature creature))
        {
            if (creature ==  null)
                ErrorOut.Throw(this, " - DoAfterSpawn() - creature null");
            gameData.creatureList.Add(creature);
            gameData.sceneObjectList.Add(creature);
        }
    }

    // Update is called once per frame
    //void Update()
    //{
    // check for key press is done in GameManagerScript!
    //}

    public void Spawn(GameData gameData, MouseTracker mouseTracker, Vector3 mousePosInWorld, GameObject appearancePrefab)
    {
        //Vector3 mousePosInWorld = mouseTracker.GetMousePositionInWorld();
        if (mouseTracker.IsMouseOverSceneObject())
        {
            DebugOut.Log(this, $"Couldn't Spawn object (Collision detected) | pos: {mousePosInWorld}");
            return;
        }
        if (mouseTracker.IsMouseOverUIElement())
        {
            DebugOut.Log(this, $"Couldn't Spawn object (Over UI element) | pos (world): {mousePosInWorld} | pos (screen): {mouseTracker.GetMousePosInScreen()}");
            return;
        }

        GameObject diskbase = diskBaseSpawner.Spawn(mousePosInWorld);
        diskbase.transform.SetParent(this.transform, true);                 // true -> stay in world pos

        GameObject creatureModel = modelApperanceSpawner.Spawn(mousePosInWorld, appearancePrefab);
        creatureModel.transform.SetParent(diskbase.transform, true);        // true -> stay in world pos    (could make it `this.transform` too..)

        GameObject creatureObj = BaseSpawn(gameData, mousePosInWorld);
        Creature creature = creatureObj.GetComponent<Creature>();
        creature.name = $"creature #{gameData.creatureList.Count - 1}";
        creature.appearanceGameObj = creatureModel;
        creature.diskBase = diskbase;
        creature.data = ScriptableObject.CreateInstance<CreatureData>();
        creature.ConfirmInit();
    }


}



//using UnityEngine;
//using System.Collections.Generic;

//public class CreatureSpawner : MonoBehaviour
//{
//    [SerializeField] private GameObject creaturePrefab;
//    //[SerializeField] public static GameData gameData;   // static for now
//    public MouseTracker mouseTracker;
//    public DiskBaseSpawner diskBaseSpawner;
//    //public List<GameObject> creatureList;  // each GameObject will have a reference to the prefabs and creature data

//    // Start is called once before the first execution of Update after the MonoBehaviour is created
//    void Start()
//    {
//        if (creaturePrefab == null)
//            ErrorOutput.printError(this, "`creaturePrefab` cannot be `null`");
//        //if (gameData == null)
//        //    ErrorOutput.printError(this, "`gameData` cannot be `null`");
//        if (mouseTracker == null)
//            ErrorOutput.printError(this, "`mouseTracker` cannot be `null`");
//        if (diskBaseSpawner == null)
//            ErrorOutput.printError(this, "`diskBaseSpawner` cannot be `null`");

//        Init();
//    }

//    void Init()
//    {
//        Setup();
//        Configure();
//    }

//    void Setup()
//    {
//        // ensure these lists exist
//        //if (gameData.creatureList == null)
//        //    //gameData.creatureList = new List<GameObject>();
//        //    gameData.creatureList = new List<Creature>();
//        //if (gameData.sceneObjectList == null)
//        //    gameData.sceneObjectList = new List<TTRPG_SceneObjectBase>();
//    }

//    void Configure()
//    {
//        // nothing...
//    }

//    // Update is called once per frame
//    //void Update()
//    //{
//        // check for key press is done in GameManagerScript!
//    //}

//    public void Spawn(GameData gameData, Vector3 mousePosInWorld)
//    {
//        //Vector3 mousePosInWorld = mouseTracker.GetMousePositionInWorld();
//        //if (mouseTracker.IsMouseOverSceneObject()) 
//        //{ 
//        //    DebugPrinter.printMessage(this, $"Couldn't Spawn object (Collision detected) | pos: {mousePosInWorld}"); 
//        //    return;  
//        //}
//        if (mouseTracker.IsMouseOverUIElement())
//        {
//            DebugPrinter.printMessage(this, $"Couldn't Spawn object (Over UI element) | pos (world): {mousePosInWorld} | pos (screen): {MouseTracker.GetMousePosInScreen()}");
//            return;
//        }

//        GameObject diskbase = diskBaseSpawner.Spawn(mousePosInWorld);
//        diskbase.transform.SetParent(this.transform, true);                 // true -> stay in world pos

//        Vector3 tmpOffset = new(0f, 10f, 0f);    // need to properly calculate how big the appearance is and ensure it rests ontop of the disk
//        GameObject creatureAppearance = diskBaseSpawner.Spawn(creaturePrefab, mousePosInWorld + tmpOffset);
//        creatureAppearance.transform.SetParent(diskbase.transform, true);   // true -> stay in world pos

//        GameObject creatureObj = diskBaseSpawner.SpawnEmpty(mousePosInWorld);
//        Creature creature = creatureObj.AddComponent<Creature>();
//        creature.name = $"creature #{gameData.creatureList.Count}";
//        creature.appearanceGameObj = creatureAppearance;
//        creature.diskBase = diskbase;
//        creature.data = ScriptableObject.CreateInstance<CreatureData>();
//        creature.ConfirmInit();

//        //gameData.generalObjList.Add(creatureObj);
//        gameData.sceneObjectList.Add(creature);

//        //gameData.creatureList.Add(creatureObj);
//        gameData.creatureList.Add(creature);
//    }
//}
