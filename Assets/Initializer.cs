using UnityEngine;
using System.Collections.Generic;

public class Initializer : MonoBehaviour
{
    public GameData gameData;           // is created during runtime
    public CameraData cameraData;       // is created during runtime
    public GameObject diskBasePrefab;
    public GameObject highlightRingPrefab;
    public GameEvent spawnedObjectEvent;


    private void Awake()
    {
        Highlight.Initialize(highlightRingPrefab);

        gameData = (GameData) ScriptableObject.CreateInstance("GameData");  // not recommended to use `new
        GeneralObject.gameData = gameData;
        GeneralObject.diskPrefab = diskBasePrefab;
        Creature.spawnedObjectEvent = spawnedObjectEvent;

        GameManagerScript.gameData = gameData;
        // and whoever else needing it

        cameraData = (CameraData) ScriptableObject.CreateInstance<CameraData>();
        CameraManager.cameraData = cameraData;

        Dictionary<string, CreatureTag> tagList = new();
        CreatureTag.Initialize(tagList);

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
