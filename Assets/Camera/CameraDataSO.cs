using UnityEngine;

[CreateAssetMenu(fileName = "CameraDataSO", menuName = "Scriptable Objects/CameraDataSO")]
[System.Serializable]
public class CameraData : ScriptableObject
{
    public float zoomSpeed = 200f;

    // map
    public float mapMoveSensitivity = 0.5f; // range [0.25, 2], for moving the map camera
    public float mapZoomInOutSensitivity = 200f;    // range [50, 300] -> tentative values

    // orbit camera
    public float initXRotatation = 0f;      // theta
    public float initYRotatation = 45f;     // phi
    public float initOrbitDistance = 30f;
    public float orbitSens = 30f;
    public float orbitDistanceZoomSens = 2f;
    public float orbitMinDist = 10f;
    public float orbitMaxDist = 100f;
    public float orbitMinVerticalClamp = -20f;
    public float orbitMaxVerticalClamp = 90f;

    // perspective
    public float minZoom = 30f;
    public float maxZoom = 90f;

    // orthographic
    public float minOrthoSize = 2f;
    public float maxOrthoSize = 30f;

    // maybe a free cam?
    public float minVerticalClamp = -89f;
    public float maxVerticalClamp = 89f;
}
