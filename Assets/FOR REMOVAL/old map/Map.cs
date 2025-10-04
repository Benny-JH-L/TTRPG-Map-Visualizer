using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class Map : MonoBehaviour
{
    //public GameObject hexagonTile;
    private static string _debugStart = "Map | ";

    private const int SIX_CONSTANT = 6;
    private const int INITIAL_LEVEL = 0;


    public List<List<MapTile>> tileMap;
    public int totalLevels; // needed?
    public float inCircleRadius = 0;
    public float sideLength = 0;

    void Start()
    {
        Debug.Log($"{_debugStart}Map starting...");
        ResetMap();
    }

    public void ResetMap()
    {
        tileMap = new List<List<MapTile>>();
        totalLevels = 0;
        AddLayer(1);        // add the 0-th level
    }

    public void AddLayer(int additionalLevels)
    {
        Debug.Log($"{_debugStart}adding {additionalLevels} more levels");

        int targetLevel = additionalLevels + tileMap.Count;

        // case, add the starting tile
        if (tileMap.Count == 0)
            tileMap.Add(new List<MapTile> { MapTile.Create(INITIAL_LEVEL)});

        Debug.Log($"{_debugStart}tileMap size: {tileMap.Count}");

        MapTile originTile = tileMap[0][0];
        //MapTile prevTile = originTile;

        float hexRadius = GetHexRadius(originTile.mapTile); // for original hexagon, it should be ~0.7370...
        //float hexRadius = 0.63f;
        Debug.Log($"HEXRADIUS = {hexRadius}");
        
        float apothem = hexRadius * 0.866f;

        // generate tiles per level
        for (float currLevel = tileMap.Count; currLevel < targetLevel; currLevel++)
        {
            float neededTiles = SIX_CONSTANT * currLevel;
            float rotationSegment = 360f / neededTiles;
            float accumulatedRotations = 0f;
            List<MapTile> newLayer = new();

            for (int tileNum = 0; tileNum < neededTiles; tileNum++)
            {
                //float distance = 2f * 0.735f * currLevel;    // distance of new tile from the origin (or starting tile)
                //float distance = 2f * hexRadius * currLevel * (Mathf.Sqrt(3f) / 2f);    // distance of new tile from the origin (or starting tile)
                //float distance = 2f * hexRadius * currLevel * 0.866f;    // distance of new tile from the origin (or starting tile)

                float distance = 2f * apothem * currLevel;

                //float distance = 2f * inCircleRadius * currLevel;    // distance of new tile from the origin (or starting tile)
                //float distance = 2f * 0.735f * (Mathf.Sqrt(3f) / 2f) * currLevel;    // distance of new tile from the origin (or starting tile)
                //float distance = 2f * GetHexInradius(GetHexSideLength(originTile.mapTile)) * (Mathf.Sqrt(3) / 2f) * currLevel;    // distance of new tile from the origin (or starting tile)
                //Vector3 offset = new Vector3(0f, 0f, distance);
                Vector3 offset = new Vector3(distance, 0f, 0f);

                //Vector3 offset = new Vector3(tileNum, 0f, distance);    // debugging

                MapTile newTile = MapTile.Create(offset, (int)currLevel);

                RotateAroundTarget(newTile.mapTile, originTile.mapTile, accumulatedRotations);

                // i can create the tile straight up of distance `d` (translate up by `d`),
                // then need to calculate even rotation segments:
                //  360-degrees / # hexagns -> each hexagon will need to be rotated about the origin by that segement + prev rotation
                newLayer.Add(newTile);
                //prevTile = newTile;
                accumulatedRotations += rotationSegment;
            }

            if (accumulatedRotations != 360f)
            {
                Debug.Log("yo didn't rotate enough..");
            }

            tileMap.Add(newLayer);   // add a new layer
    }
}

    /// <summary>
    /// Rotates objToRotate around objTarget by a given angle in degrees. By default it will be about the Y-axis.
    /// Does not rotate `objToRotate`s own orientation/rotation.
    /// </summary>
    /// <param name="objToRotate">The GameObject to rotate.</param>
    /// <param name="objTarget">The target GameObject to rotate around.</param>
    /// <param name="angle">The angle in degrees.</param>
    /// <param name="axis">The axis of rotation (defaults to Vector3.up).</param>
    public void RotateAroundTarget(GameObject objToRotate, GameObject objTarget, float angle, Vector3? axis = null)
    {
        if (objToRotate == null || objTarget == null) return;

        Vector3 rotationAxis = axis ?? Vector3.up; // default axis is Y-axis

        Quaternion originalRotation = objToRotate.transform.rotation;   // Get original rotation

        objToRotate.transform.RotateAround(objTarget.transform.position, rotationAxis, angle);

        objToRotate.transform.rotation = originalRotation;  // Set original rotation
    }

    // might work? -> DOES WORK !!
    public float GetHexRadius(GameObject hexTile)
    {
        MeshCollider meshCollider = hexTile.GetComponent<MeshCollider>();
        if (meshCollider == null) return 1f;

        Bounds bounds = meshCollider.bounds;

        // For a regular hexagon: radius = width / 2
        // But we can also calculate from height: radius = height / sqrt(3)
        float radiusFromWidth = bounds.size.x / 2f;
        float radiusFromHeight = bounds.size.z / Mathf.Sqrt(3f); // assuming Z is the other horizontal axis

        // Use the width-based calculation as it's more reliable
        return radiusFromWidth;
    }
}
