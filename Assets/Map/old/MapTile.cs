using UnityEngine;

[System.Serializable]
public class MapTile : MonoBehaviour
{
    private static string _debugStart = "MapTile | ";

    public static GameObject mapTilePrefab;
    //public static Vector3 yOffsetDiskSpawn = new(0f, 0.5f, 0f);

    public GameObject mapTile;
    public int atLevel = -1;

    public static MapTile Create(int level)
    {
        return Create(Vector3.zero, level);
    }

    public static MapTile Create(Vector3 pos, int level)
    {
        GameObject obj = Instantiate(mapTilePrefab, pos + GeneralObject.yOffsetDiskSpawn, mapTilePrefab.transform.rotation);
        MapTile mapTile = obj.AddComponent<MapTile>();
        mapTile.Init(obj, level);

        Debug.Log($"{_debugStart}obj created...");

        return mapTile;
    }

    protected void Init(GameObject tile, int level)
    {
        mapTile = tile;
        atLevel = level;
        GetComponent<Rigidbody>().isKinematic = true;  // does not move from collisions
    }
}
