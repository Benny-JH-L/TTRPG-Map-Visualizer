using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject prefabToSpawn;
    public List<GameObject> spawnList;
    public Vector3 spawnYOffset = new(0f, 0.5f, 0f);    // need to add a y-offset to the position or else the object will sometimes launch into the air (collides with the ground)

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (prefabToSpawn == null)
            ErrorOutput.printError(this, "Prefab cannot be `null`");

        Init();
    }

    void Init()
    {
        Setup();
        Configure();
    }

    void Setup()
    {
        if (spawnList == null)
            spawnList = new List<GameObject>();
    }

    void Configure()
    {
        // nothing...
    }

    /// <summary>
    /// Spawns (Instantiates) the prefab that was previously set at `position`, and stores it in a GameObject List
    /// </summary>
    /// <param name="position"></param>
    /// <returns>GameObject</returns>
    public GameObject Spawn(Vector3 position)
    {
        return Spawn(prefabToSpawn, position, prefabToSpawn.transform.rotation); // use identity instead?
    }

    /// <summary>
    /// Spawns (Instantiates) the prefab that was previously set at `position` with `rotation` rotation, and stores it in a GameObject List
    /// </summary>
    /// <param name="position"></param>
    /// <param name="rotation"></param>
    /// <returns>GameObject</returns>
    public GameObject Spawn(Vector3 position, Quaternion rotation)
    {
        GameObject obj = Spawn(prefabToSpawn, position, rotation);
        spawnList.Add(obj);
        return obj;
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
    /// Spawns (Instantiates) a prefab from a path location at `position` with `rotation` rotation
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
    /// Spawns (Instantiates) a GameObject at `position`
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="position"></param>
    /// <returns>GameObject</returns>
    /// 
    public GameObject Spawn(GameObject obj, Vector3 position)
    {
        return Instantiate(obj, position + spawnYOffset, Quaternion.identity);
    }

    /// <summary>
    /// Spawns (Instantiates) a GameObject at `position` with `rotation` rotation
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="position"></param>
    /// <param name="rotation"></param>
    /// <returns>GameObject</returns>
    /// 
    public GameObject Spawn(GameObject obj, Vector3 position, Quaternion rotation)
    {
        return Instantiate(obj, position + spawnYOffset, rotation);
    }

    /// <summary>
    /// Spawns (Instantiates) an Empty GameObject at `position`
    /// </summary>
    /// <param name="position"></param>
    /// <param name="rotation"></param>
    /// <returns>GameObject</returns>
    public GameObject SpawnEmpty(Vector3 position)
    {
        return SpawnEmpty(position, Quaternion.identity);
    }

    /// <summary>
    /// Spawns (Instantiates) an Empty GameObject at `position` with `rotation` rotation
    /// </summary>
    /// <param name="position"></param>
    /// <param name="rotation"></param>
    /// <returns>GameObject</returns>
    public GameObject SpawnEmpty(Vector3 position, Quaternion rotation)
    {
        GameObject obj = new();
        obj.transform.position = position;
        obj.transform.rotation = rotation;
        return obj;
    }
}
