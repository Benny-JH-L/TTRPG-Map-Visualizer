using UnityEngine;
using System.Collections.Generic;

public class CreatureSpawner : MonoBehaviour
{
    [SerializeField] private GameObject creaturePrefab;
    [SerializeField] public static GameData gameData;   // static for now
    public MouseTracker mouseTracker;
    public Spawner diskBaseSpawner;
    //public List<GameObject> creatureList;  // each GameObject will have a reference to the prefabs and creature data

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (creaturePrefab == null)
            ErrorOutput.printError(this, "`creaturePrefab` cannot be `null`");
        if (gameData == null)
            ErrorOutput.printError(this, "`gameData` cannot be `null`");
        if (mouseTracker == null)
            ErrorOutput.printError(this, "`mouseTracker` cannot be `null`");
        if (diskBaseSpawner == null)
            ErrorOutput.printError(this, "`diskBaseSpawner` cannot be `null`");

        Init();
    }

    void Init()
    {
        Setup();
        Configure();
    }

    void Setup()
    {
        // ensure these lists exist
        if (gameData.creatureList == null)
            //gameData.creatureList = new List<GameObject>();
            gameData.creatureList = new List<Creature>();
        if (gameData.sceneObjectList == null)
            gameData.sceneObjectList = new List<TTRPG_SceneObjectBase>();
    }

    void Configure()
    {
        // nothing...
    }

    // Update is called once per frame
    //void Update()
    //{
        // check for key press is done in GameManagerScript!
    //}

    public void Spawn()
    {
        Vector3 mousePosInWorld = mouseTracker.GetMousePositionInWorld();
        if (mouseTracker.IsMouseOverSceneObject()) 
        { 
            DebugPrinter.printMessage(this, $"Couldn't Spawn object (Collision detected) | pos: {mousePosInWorld}"); 
            return;  
        }
        
        GameObject diskbase = diskBaseSpawner.Spawn(mousePosInWorld);
        diskbase.transform.SetParent(this.transform, true);                 // true -> stay in world pos

        Vector3 tmpOffset = new(0f, 2f, 0f);    // need to properly calculate how big the appearance is and ensure it rests ontop of the disk
        GameObject creatureAppearance = diskBaseSpawner.Spawn(creaturePrefab, mousePosInWorld + tmpOffset);
        creatureAppearance.transform.SetParent(diskbase.transform, true);   // true -> stay in world pos
        
        GameObject creatureObj = diskBaseSpawner.SpawnEmpty(mousePosInWorld);
        Creature creature = creatureObj.AddComponent<Creature>();
        creature.name = $"creature #{gameData.creatureList.Count}";
        creature.appearanceGameObj = creatureAppearance;
        creature.diskBase = diskbase;
        creature.data = ScriptableObject.CreateInstance<CreatureData>();
        creature.ConfirmInit();

        //gameData.generalObjList.Add(creatureObj);
        gameData.sceneObjectList.Add(creature);

        //gameData.creatureList.Add(creatureObj);
        gameData.creatureList.Add(creature);
    }
}
