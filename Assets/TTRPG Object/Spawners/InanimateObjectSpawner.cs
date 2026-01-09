using System.Collections.Generic;
using UnityEngine;

public class InanimateObjectSpawner : MonoBehaviour
{
    [SerializeField] public static GameData gameData;   // static for now
    public MouseTracker mouseTracker;
    public Spawner diskBaseSpawner;
    //public List<GameObject> creatureList;  // each GameObject will have a reference to the prefabs and creature data

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
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

    public void Spawn(GameObject appearance)
    {
        Vector3 mousePosInWorld = mouseTracker.GetMousePositionInWorld();
        if (mouseTracker.IsMouseOverSceneObject())
        {
            DebugPrinter.printMessage(this, $"Couldn't Spawn object (Collision detected) | pos: {mousePosInWorld}");
            return;
        }
        if (mouseTracker.IsMouseOverUIElement())
        {
            DebugPrinter.printMessage(this, $"Couldn't Spawn object (Over UI element) | pos (world): {mousePosInWorld} | pos (screen): {MouseTracker.GetMousePosInScreen()}");
            return;
        }

        GameObject diskbase = diskBaseSpawner.Spawn(mousePosInWorld);
        diskbase.transform.SetParent(this.transform, true);                 // true -> stay in world pos

        Vector3 tmpOffset = new(0f, 2f, 0f);    // need to properly calculate how big the appearance is and ensure it rests ontop of the disk
        GameObject objAppearance = diskBaseSpawner.Spawn(appearance, mousePosInWorld + tmpOffset);
        objAppearance.transform.SetParent(diskbase.transform, true);   // true -> stay in world pos

        GameObject sceneObj = diskBaseSpawner.SpawnEmpty(mousePosInWorld);
        InanimateObj inanObj = sceneObj.AddComponent<InanimateObj>();
        inanObj.name = $"creature #{gameData.creatureList.Count}";
        inanObj.appearanceGameObj = objAppearance;
        inanObj.diskBase = diskbase;
        inanObj.data = ScriptableObject.CreateInstance<CreatureData>();
        inanObj.ConfirmInit();

        gameData.sceneObjectList.Add(inanObj);
    }
}
