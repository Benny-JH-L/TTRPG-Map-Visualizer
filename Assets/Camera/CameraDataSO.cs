using UnityEngine;

[CreateAssetMenu(fileName = "CameraDataSO", menuName = "Scriptable Objects/CameraDataSO")]
[System.Serializable]
public class CameraData : ScriptableObject
{
    public float zoomSpeed = 200f;

    // map
    public float mapMoveSensitivity = 0.01f; // range [0.01, 1], for moving the map camera
    public float mapZoomInOutSensitivity = 200f;    // range [50, 300] -> tentative values

    // orbit camera
    public float orbitSens = 30f;
    public float orbitMinDist = 2f;
    public float orbitMaxDist = 10f;
    public float orbitMinVerticalClamp = -20f;
    public float orbitMaxVerticalClamp = 90f;

    // perspective
    public float minZoom = 5f;
    public float maxZoom = 90f;

    // orthographic
    public float minOrthoSize = 2f;
    public float maxOrthoSize = 30f;

    // maybe a free cam?
    public float minVerticalClamp = -89f;
    public float maxVerticalClamp = 89f;
}
