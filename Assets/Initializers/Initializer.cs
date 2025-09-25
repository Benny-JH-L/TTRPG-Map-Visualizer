using UnityEngine;
using System.Collections.Generic;

[DefaultExecutionOrder(-1000)]  // execute first
public class Initializer : MonoBehaviour
{
    public GameData gameData;           // is created during runtime
    public CameraData cameraData;       // is created during runtime

    public GameObject diskBasePrefab;
    public GameObject highlightRingPrefab;
    public GameObject screenSpaceGameObject;

    public GameEvent spawnedObjectEvent;
    public GameEvent strModChanged;
    public GameEvent dexModChanged;
    public GameEvent conModChanged;
    public GameEvent intModChanged;
    public GameEvent wisModChanged;
    public GameEvent chaModChanged;

    private void Awake()
    {
        Canvas.ForceUpdateCanvases();

        Highlight.Initialize(highlightRingPrefab);

        gameData = (GameData) ScriptableObject.CreateInstance("GameData");  // not recommended to use `new
        GeneralObject.gameData = gameData;
        GeneralObject.diskPrefab = diskBasePrefab;
        GeneralObject.spawnedObjectEvent = spawnedObjectEvent;

        GameManagerScript.gameData = gameData;
        // and whoever else needing it

        cameraData = (CameraData) ScriptableObject.CreateInstance<CameraData>();
        CameraManager.cameraData = cameraData;
        CameraManager.screenSpaceGameObject = screenSpaceGameObject;
        AbstractCamera.cameraData = cameraData;
        AbstractCamera.screenSpaceGameObject = screenSpaceGameObject;

        Dictionary<string, CreatureTag> tagList = new();
        CreatureTag.Initialize(tagList);

        CoreStats.strModChanged = strModChanged;
        CoreStats.dexModChanged = dexModChanged;
        CoreStats.conModChanged = conModChanged;
        CoreStats.intModChanged = intModChanged;
        CoreStats.wisModChanged = wisModChanged;
        CoreStats.chaModChanged = chaModChanged;

        MouseTracker.screenSpaceGameObject = screenSpaceGameObject;

        MapTile.mapTilePrefab = diskBasePrefab;
    }


    //// Start is called once before the first execution of Update after the MonoBehaviour is created
    //void Start()
    //{
        
    //}

    //// Update is called once per frame
    //void Update()
    //{
        
    //}
}
