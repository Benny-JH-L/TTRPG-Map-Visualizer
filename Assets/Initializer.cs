using UnityEngine;

public class Initializer : MonoBehaviour
{
    public GameData gameData;           // is created during runtime
    public CameraData cameraData;       // is created during runtime
    public GameObject diskPrefab;
    public GameEvent spawnedObjectEvent;
    private void Awake()
    {
        gameData = (GameData) ScriptableObject.CreateInstance("GameData");  // not recommended to use `new
        Creature.gameData = gameData;
        Creature.diskPrefab = diskPrefab;
        Creature.spawnedObjectEvent = spawnedObjectEvent;

        GameManagerScript.gameData = gameData;
        // and whoever else needing it

        cameraData = (CameraData) ScriptableObject.CreateInstance<CameraData>();
        CameraManager.cameraData = cameraData;
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
