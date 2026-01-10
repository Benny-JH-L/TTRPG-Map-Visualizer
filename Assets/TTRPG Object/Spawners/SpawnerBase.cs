using UnityEngine;

public abstract class SpawnerBase<T> : MonoBehaviour
    where T : TTRPG_SceneObjectBase
{
    [SerializeField] protected Vector3 spawnYOffset = new(0f, 0.5f, 0f);    // need to add a y-offset to the position or else the object will sometimes launch into the air (collides with the ground)
    [SerializeField] protected GameObject prefabToSpawn;
    //[SerializeField] protected List<GameObject> spawnList;                   // will contain only `prefabToSpawn`

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (prefabToSpawn == null)
            ErrorOut.Throw(this, "Prefab cannot be `null`");
        
        OnStart();
        Init();
    }

    public void Init()
    {
        Setup();
        Configure();
    }

    protected abstract void Setup();
    protected abstract void Configure();
    protected abstract void OnStart();


    /// <summary>
    /// Define where the GameObjects are stored in `GameData` with `T` component atached.
    /// </summary>
    /// <param name="gameData">GameData</param>
    /// <param name="gameObj">GameObject</param>
    protected abstract void DoAfterSpawn(GameData gameData, GameObject gameObj);

    /// <summary>
    /// Spawns (Instantiates) the prefab that was previously set at `position` with `T` component atached.
    /// </summary>
    /// <param name="position"></param>
    /// <returns>GameObject</returns>
    protected GameObject BaseSpawn(GameData gameData, Vector3 position)
    {
        return BaseSpawn(gameData, position, prefabToSpawn.transform.rotation); // use identity instead for rotation?
    }

    /// <summary>
    /// Spawns (Instantiates) the prefab that was previously set at `position` with `rotation` rotation with `T` component atached.
    /// </summary>
    /// <param name="position"></param>
    /// <param name="rotation"></param>
    /// <returns>GameObject</returns>
    protected GameObject BaseSpawn(GameData gameData, Vector3 position, Quaternion rotation)
    {
        return BaseSpawn(gameData, prefabToSpawn, position, rotation);
    }

    /// <summary>
    /// Spawns (Instantiates) a prefab from a path location at `position`
    /// </summary>
    /// <param name="pathToPrefab"></param>
    /// <param name="position"></param>
    /// <returns>GameObject</returns>
    /// 
    //public GameObject Spawn(string pathToPrefab, Vector3 position)
    //{
    // use Quartnion.identify for 0f angle.
    //}

    /// <summary>
    /// Spawns (Instantiates) a prefab from a path location at `position` with `rotation` rotation with `T` component atached.
    /// </summary>
    /// <param name="pathToPrefab"></param>
    /// <param name="position"></param>
    /// <param name="rotation"></param>
    /// <returns>GameObject</returns>
    /// 
    //public GameObject Spawn(string pathToPrefab, Vector3 position, Quaternion rotation)
    //{

    //}

    /// <summary>
    /// Spawns (Instantiates) a GameObject at `position` with `T` component atached.
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="position"></param>
    /// <returns>GameObject</returns>
    /// 
    protected GameObject BaseSpawn(GameData gameData, GameObject obj, Vector3 position)
    {
        return BaseSpawn(gameData, obj, position, Quaternion.identity);
    }

    /// <summary>
    /// Spawns (Instantiates) a GameObject at `position` with `rotation` rotation with `T` component atached.
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="position"></param>
    /// <param name="rotation"></param>
    /// <returns>GameObject</returns>
    /// 
    protected GameObject BaseSpawn(GameData gameData, GameObject obj, Vector3 position, Quaternion rotation)
    {
        GameObject spawnedObj = Instantiate(obj, position + spawnYOffset, rotation);
        spawnedObj.AddComponent<T>();
        DoAfterSpawn(gameData, spawnedObj);
        return spawnedObj;
    }

    /// <summary>
    /// Spawns (Instantiates) an Empty GameObject at `position` with `T` component atached. Will not call `DoAfterSpawn()`
    /// </summary>
    /// <param name="position"></param>
    /// <param name="rotation"></param>
    /// <returns>GameObject</returns>
    protected GameObject SpawnEmpty(Vector3 position)
    {
        return SpawnEmpty(position, Quaternion.identity);
    }

    /// <summary>
    /// Spawns (Instantiates) an Empty GameObject at `position` with `rotation` rotation with `T` component atached. Will not call `DoAfterSpawn()`
    /// </summary>
    /// <param name="position"></param>
    /// <param name="rotation"></param>
    /// <returns>GameObject</returns>
    protected GameObject SpawnEmpty(Vector3 position, Quaternion rotation)
    {
        GameObject obj = new();
        obj.transform.position = position;
        obj.transform.rotation = rotation;
        obj.AddComponent<T>();
        return obj;
    }
}