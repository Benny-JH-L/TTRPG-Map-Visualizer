using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[System.Serializable]
public class CameraManager : MonoBehaviour
{
    // will be on the gameobject
    //public GameEventListener onSelectedObject;
    //public GameEventListener onCameraProjectionChange;
    private static string _cameraDebugStart = "Camera Manager | ";
    public static CameraData cameraData;

    private MapCam _mapCam;
    private OrbitCam _orbitCam; // actually used | for game object
    private AbstractCamera _currentCam;

    public bool _isUIFocused;   // only public so inspector can see it
    private bool _isGameScreenFocused; // most likely do not need anymore

    private void Awake()
    {
        _orbitCam = GetComponentInChildren<OrbitCam>();
        _mapCam = GetComponentInChildren<MapCam>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _isUIFocused = false;
        _mapCam.EnableCamera();
        _currentCam = _mapCam;

        if (_currentCam != null && _currentCam.CamEnabled())
        {
            Debug.Log("Map cam enabled");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_isUIFocused)   // Do not allow Camera adjustments while interacting with the UI
        {
            //Debug.Log($"{_cameraDebugStart}UI is focused");
            return;
        }

        UpdateCamera();
    }

    private void UpdateCamera()
    {
        if (Keyboard.current.backquoteKey.wasPressedThisFrame) // key: '`'
        {
            ToggleCam();
        }

        if (_mapCam.CamEnabled())
        {
            _mapCam.UpdateCamera();
            MovementValue movementValue = new(cameraData.mapMoveSensitivity);

            // use `wasd` or mouse input to move camera)
            // Check if any of the movement keys are being held
            if (Keyboard.current.wKey.isPressed)
            {
                movementValue.MoveNorth();
            }
            else if (Keyboard.current.sKey.isPressed)
            {
                movementValue.MoveSouth();
            }

            if (Keyboard.current.aKey.isPressed)
            {
                movementValue.MoveWest();
            }
            else if (Keyboard.current.dKey.isPressed)
            {
                movementValue.MoveEast();
            }

            if (Mouse.current.middleButton.isPressed)
            {
                movementValue = new(cameraData.mapMoveSensitivity + 0.2f);
                Vector2 mouseDelta = Mouse.current.delta.ReadValue();
                //Debug.Log($"{_cameraDebugStart}Mouse Delta: {mouseDelta}");
                // mouseDelta.x -> right (+) / left (-)
                // mouseDelta.y -> up (+) / down (-)
                movementValue.SetMove(-mouseDelta); // negate so the camera moves opposite of mouse movement
            }

            _mapCam.MoveMapCamBy(movementValue);
        }
        else if (_orbitCam.CamEnabled())
        {
            _orbitCam.UpdateCamera();
        }
    }

    /// <summary>
    /// Toggles between the map cam and orbit cam
    /// </summary>
    private void ToggleCam()
    {
        _mapCam.ToggleEnable();
        _orbitCam.ToggleEnable();

        foreach (AbstractCamera abstractCamera in AbstractCamera.cameraList)
        {
            if (abstractCamera.CamEnabled())
            {
                _currentCam = abstractCamera;
                break;
            }
        }

        if (_mapCam.CamEnabled())
            Debug.Log($"{_cameraDebugStart}Map cam enabled");
        else if (_orbitCam.CamEnabled())
            Debug.Log($"{_cameraDebugStart}Orbit cam enabled");
    }

    /// <summary>
    /// Returns a tuple with the `gameObject` the mouse is over and the mouse's location in the world.
    /// </summary>
    /// <returns>
    /// Tuple(GameObject, Vector3)
    /// Item 1: GameObject the mouse is over. Is Null if the mouse is not over a GameObject
    /// Item 2: Location of where the mouse is over. Is Null if the mouse is not over a GameObject
    /// </returns>
    public Tuple<GameObject, Vector3> GetGameObjectAtMousePos()
    {
        //Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue()); // works with camera's with the tag "MainCamera"
        Ray ray = _currentCam.GetCamera().ScreenPointToRay(MouseTracker.GetMousePos());
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            //Debug.Log("Camera | Mouse hit <" + hit.collider.gameObject + "> at " + hit.point);
            return new Tuple<GameObject, Vector3>(hit.collider.gameObject, hit.point);
        }

        return null;
    }

    public void OnSelectedObject(Component sender, object data)
    {
        if (data is GeneralObject)   // maybe have a condition to check whether or not we switch to player or go to map cam... (Eagel or creature view to moving..)
        {
            Debug.Log($"{_cameraDebugStart}Switching to orbit cam");
            GeneralObject creature = (GeneralObject)data;

            _orbitCam.EnableCamera(creature.diskBase);
            _currentCam = _orbitCam;
        }
    }

    public void OnDeselectObject(Component sender, object data)
    {
        _mapCam.EnableCamera();
    }

    public void OnUIFocued(Component comp, object data)
    {
        if (data is bool r)
            _isUIFocused = r;
    }

    public void OnGameScreenFocused(Component comp, object data) // most likely do not need anymore
    {
        if (data is bool r)
            _isGameScreenFocused = r;
    }
}
