using UnityEngine;
using System.Collections.Generic;

[DefaultExecutionOrder(-1000)]  // execute first
public class Initializer : MonoBehaviour
{
    public GameData gameData;           // is created during runtime
    //public CameraData cameraData;       // is created during runtime -> done in abstract cam class

    public GameObject diskBasePrefab;
    public GameObject highlightRingPrefab;
    public GameObject screenSpaceGameObject;

    public GameEventSO spawnedObjectEvent;
    public GameEventSO strModChanged;
    public GameEventSO dexModChanged;
    public GameEventSO conModChanged;
    public GameEventSO intModChanged;
    public GameEventSO wisModChanged;
    public GameEventSO chaModChanged;

    private void Awake()
    {
        Canvas.ForceUpdateCanvases();

        //Highlight.Initialize(highlightRingPrefab);

        gameData = (GameData) ScriptableObject.CreateInstance("GameData");  // not recommended to use `new
        
        //CreatureSpawner.gameData = gameData;
        //InanimateObjectSpawner.gameData = gameData;
        MouseTracker.gameData = gameData;
        TTRPG_SceneObjectBase.gameData = gameData;
        
        //GeneralObject_OLD.gameData = gameData;
        //GeneralObject_OLD.diskPrefab = diskBasePrefab;
        //GeneralObject_OLD.spawnedObjectEvent = spawnedObjectEvent;

        GameManagerScript.gameData = gameData;
        // and whoever else needing it

        //cameraData = (CameraData) ScriptableObject.CreateInstance<CameraData>();
        //CameraManager.cameraData = cameraData;
        //AbstractCamera.cameraData = cameraData;
        //AbstractCamera.screenSpaceGameObject = screenSpaceGameObject;

        Dictionary<string, CreatureTag> tagList = new();
        CreatureTag.Initialize(tagList);

        TTRPG_CoreStats.strModChanged = strModChanged;
        TTRPG_CoreStats.dexModChanged = dexModChanged;
        TTRPG_CoreStats.conModChanged = conModChanged;
        TTRPG_CoreStats.intModChanged = intModChanged;
        TTRPG_CoreStats.wisModChanged = wisModChanged;
        TTRPG_CoreStats.chaModChanged = chaModChanged;

        MouseTracker.screenSpaceGameObject = screenSpaceGameObject;
    }


    //// Start is called once before the first execution of Update after the MonoBehaviour is created
    //void Start()
    //{

    //}

    ////Update is called once per frame
    //void Update()
    //{
    //    AbstractCamera.screenSpaceGameObject = screenSpaceGameObject;

    //}
}
