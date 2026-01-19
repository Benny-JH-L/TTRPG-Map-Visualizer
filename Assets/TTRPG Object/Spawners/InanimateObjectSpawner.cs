using UnityEngine;

public class InanimateObjectSpawner : SpawnerBase<InanimateObj>
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
        if (diskBaseSpawner == null)
            ErrorOut.Throw(this, "`diskBaseSpawner` cannot be `null`");
        if (modelApperanceSpawner == null)
            ErrorOut.Throw(this, "`modelApperanceSpawner` cannot be `null`");
    }

    protected override void DoAfterSpawn(GameData gameData, GameObject gameObj)
    {
        if (gameData == null)
            ErrorOut.Throw(this, "game data null");

        if (gameObj.TryGetComponent<TTRPG_SceneObjectBase>(out TTRPG_SceneObjectBase sceneObj))
        {
            if (sceneObj == null)
                ErrorOut.Throw(this, " - DoAfterSpawn() - sceneObj null");
            gameData.sceneObjectList.Add(sceneObj);
        }
    }

    // Update is called once per frame
    //void Update()
    //{
    // check for key press is done in GameManagerScript!
    //}

    public void Spawn(GameData gameData, MouseTracker mouseTracker, Vector3 mousePosInWorld, GameObject appearance)
    {
        //Vector3 mousePosInWorld = mouseTracker.GetMousePositionInWorld();
        if (mouseTracker.IsMouseOverSceneObject())
        {
            DebugOut.Log(this, $"Couldn't Spawn object (Collision detected) | pos: {mousePosInWorld}", debugDisabled);
            return;
        }
        if (mouseTracker.IsMouseOverUIElement())
        {
            DebugOut.Log(this, $"Couldn't Spawn object (Over UI element) | pos (world): {mousePosInWorld} | pos (screen): {mouseTracker.GetMousePosInScreen()}", debugDisabled);
            return;
        }

        GameObject diskbase = diskBaseSpawner.Spawn(mousePosInWorld);
        diskbase.transform.SetParent(this.transform, true);                 // true -> stay in world pos

        GameObject objModel = modelApperanceSpawner.Spawn(mousePosInWorld, appearance);
        objModel.transform.SetParent(diskbase.transform, true);             // true -> stay in world pos    (could make it `this.transform` too..)

        GameObject sceneObj = BaseSpawn(gameData, mousePosInWorld);
        InanimateObj inanObj = sceneObj.GetComponent<InanimateObj>();
        DebugOut.Log(this, $"gameData.sceneObjectList.Count: {gameData.sceneObjectList.Count}", debugDisabled);

        inanObj.name = $"inanimate Obj #{gameData.sceneObjectList.Count - gameData.creatureList.Count - 1}";
        inanObj.appearanceGameObj = objModel;
        inanObj.diskBase = diskbase;
        inanObj.data = ScriptableObject.CreateInstance<InanimateObjectData>();
        inanObj.ConfirmInit();
    }
}
