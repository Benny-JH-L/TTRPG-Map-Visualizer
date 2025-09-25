
using UnityEngine;
using UnityEngine.InputSystem;

[System.Serializable]
public class MapCam : AbstractCamera
{
    private static string _debugStart = "MapCam | ";

    public override void Setup()
    {
        cam = GetComponent<Camera>();
    }

    public override void Configure()
    {
        if (cameraData == null)
            Debug.Log($"{_debugStart}CAMERA DATA IS NULL");
    }

    public override void UpdateCamera()
    {
        if (Keyboard.current.shiftKey.isPressed)
            ZoomCamera();           // check for zoom input 
        else
            MoveCameraDistance();   // check for camera movement (along y-axis)
        
    }

    /// <summary>
    /// Moves the map camera into the screen or away from the screen, with respect to mouse scroll input. 
    /// Ie changes the y value of the camera.
    /// </summary>
    protected override void MoveCameraDistance()
    {
        float scrollDelta = Mouse.current.scroll.ReadValue().y; // zoom in +1, zoom out -1
        Vector3 pos = cam.transform.position;
        pos.y -= (scrollDelta * cameraData.mapZoomInOutSensitivity * Time.deltaTime);
        cam.transform.position = pos;
    }

    /// <summary>
    /// Takes in a `MovementValue` and uses it to move the map camera's position.
    /// </summary>
    /// <param name="moveBy"></param>
    public void MoveMapCamBy(MovementValue moveBy)
    {
        cam.transform.position += moveBy.GetMovement();
    }
}