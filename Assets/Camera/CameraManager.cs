using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class OrbitCam
{
    /// <summary>
    /// Variable to set when a Orbit Camera is going to be used
    /// </summary>
    private Transform orbitTarget;
    /// <summary>
    /// Variable to set when a Orbit Camera is created
    /// </summary>
    public Camera orbitCam;

    private CameraData _cameraData;

    private float currentRotatX = 0f;   // theta
    private float currentRotatY = 20f;  // phi
    private float orbitDistance = 5f;   // how far camera is from object

    public OrbitCam(Camera orbitCam, CameraData cameraData)
    {
        orbitTarget = null;
        this.orbitCam = orbitCam;
        _cameraData = cameraData;
    }

    public void UpdateOrbitCam()
    {
        if (!Mouse.current.middleButton.isPressed)  // do orbit cam when holding middle mouse down
            return;

        OrbitCamLogic();
    }
    public void Enable(GameObject objToLookAt)
    {
        orbitTarget = objToLookAt.transform;
        orbitCam.enabled = true;
        UpdateOrbitCamOnce();
    }

    private void OrbitCamLogic()
    {
        float scrollDelta = Mouse.current.scroll.ReadValue().y; // zoom in +1, zoom out -1

        Vector2 lookDelta = Mouse.current.delta.ReadValue();
        currentRotatX += lookDelta.x * _cameraData.orbitSens * Time.deltaTime;
        currentRotatY -= lookDelta.y * _cameraData.orbitSens * Time.deltaTime;
        currentRotatY = Mathf.Clamp(currentRotatY, _cameraData.orbitMinVerticalClamp, _cameraData.orbitMaxVerticalClamp);

        // Zoom with scroll wheel -> needed here?, other parts of the code may do it already
        //orbitDistance -= scrollDelta * _cameraData.zoomSpeed * Time.deltaTime;
        orbitDistance = Mathf.Clamp(orbitDistance, _cameraData.minZoom, _cameraData.maxZoom);

        Quaternion rotation = Quaternion.Euler(currentRotatY, currentRotatX, 0);
        Vector3 direction = new Vector3(0, 0, -orbitDistance);
        Vector3 position = ((orbitTarget != null) ? orbitTarget.position : Vector3.zero) + rotation * direction;

        orbitCam.transform.position = position;
        orbitCam.transform.rotation = rotation;
    }

    private void UpdateOrbitCamOnce()
    {
        OrbitCamLogic();
    }

    public void Disable()
    {
        orbitCam.enabled = false;
    }
}

public class CameraManager : MonoBehaviour
{
    // will be on the gameobject
    //public GameEventListener onSelectedObject;
    //public GameEventListener onCameraProjectionChange;

    public static CameraData cameraData;

    public Camera mapCam;
    public Camera objectCam;    
    private OrbitCam _orbitCam; // actually used | for game object
    private Camera _currentCam;
    private List<Camera> _cameraList;

    /// <summary>
    /// per camera; stores their X and Y rotations for persepctive & ortho (rotations are shared between perspectives)
    /// </summary>
    private Dictionary<Camera, Vector2> cameraRotationDict;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _cameraList = new()
        {
            mapCam,
            objectCam
        };
        _DisableAllCams();

        cameraRotationDict = new Dictionary<Camera, Vector2>();

        foreach (Camera camera in _cameraList)
        {
            Vector2 vec2 = new(camera.transform.rotation.x, camera.transform.rotation.y);
            cameraRotationDict[camera] = vec2;
        }

        _orbitCam = new OrbitCam(objectCam, cameraData);

        _currentCam = mapCam;
        _currentCam.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateCamera();
    }


    private void UpdateCamera()
    {
        if (Keyboard.current.backquoteKey.wasPressedThisFrame) // '`'
        {
            ToggleCam();
        }

        if (Keyboard.current.shiftKey.isPressed && mapCam.enabled)
            MoveMapCameraInOut();
        else
            ZoomCurrentCamera();

        //if (_currentCam == mapCam)
        if (mapCam.enabled)
        {
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

            MoveMapCamBy(movementValue);
        }

        //else if (_currentCam == objectCam)
        else if (objectCam.enabled)
        {
            _orbitCam.UpdateOrbitCam();
        }
    }

    /// <summary>
    /// Toggles between the map cam and object cam
    /// </summary>
    private void ToggleCam()
    {
        mapCam.enabled = !mapCam.enabled;
        objectCam.enabled = !objectCam.enabled;

        foreach (Camera camera in _cameraList)
        {
            if (camera.enabled)
            {
                _currentCam = camera;
                break;
            }
        }        
        
        //if (mapCam.enabled)
        //    _currentCam = mapCam;
        //else if (objectCam.enabled)
        //    _currentCam = objectCam;
    }

    /// <summary>
    /// only for the map camera, may create Camera classes to differentiate map cam, object cam and whatever cams i may make.
    /// Takes in a `MovementValue` and uses it to move the map camera's position.
    /// </summary>
    /// <param name="moveBy"></param>
    private void MoveMapCamBy(MovementValue moveBy)
    {
        mapCam.transform.position += moveBy.GetMovement();
    }

    /// <summary>
    /// Zooms in/out with the current camera (Perspective FOV & Ortho size) with respect to mouse scroll input.
    /// </summary>
    private void ZoomCurrentCamera()
    {
        float scrollDelta = Mouse.current.scroll.ReadValue().y; // zoom in +1, zoom out -1
        //Debug.Log("scroll data:"+scrollDelta);

        if (Mathf.Abs(scrollDelta) > 0.01f)
        {
            if (_currentCam.orthographic)
            {
                _currentCam.orthographicSize -= scrollDelta * (cameraData.zoomSpeed * 0.01f);
                _currentCam.orthographicSize = Mathf.Clamp(_currentCam.orthographicSize, cameraData.minOrthoSize, cameraData.maxOrthoSize);
            }
            else
            {
                _currentCam.fieldOfView -= scrollDelta * cameraData.zoomSpeed * Time.deltaTime;
                _currentCam.fieldOfView = Mathf.Clamp(_currentCam.fieldOfView, cameraData.minZoom, cameraData.maxZoom);
            }
        }
    }

    /// <summary>
    /// Moves the map camera into the screen or away from the screen. ie changes the y value of the camera
    /// </summary>
    private void MoveMapCameraInOut()
    {
        float scrollDelta = Mouse.current.scroll.ReadValue().y; // zoom in +1, zoom out -1
        Vector3 pos = _currentCam.transform.position;
        pos.y -= (scrollDelta * cameraData.mapZoomInOutSensitivity * Time.deltaTime);
        _currentCam.transform.position = pos;
    }

    /// <summary>
    /// Returns a tuple with name of object the mouse is over and the mouses location in the world.
    /// </summary>
    /// <returns>
    /// Tuple(string, Vector3)
    /// Item 1: Name of the object the mouse is over. Is Null if the mouse is not over a GameObject
    /// Item 2: Location of where the mouse is over. Is Null if the mouse is not over a GameObject
    /// </returns>
    public Tuple<string, Vector3> GetCurrMosePos()
    {
        //Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue()); // works with camera's with the tag "MainCamera"
        Ray ray = _currentCam.ScreenPointToRay(Mouse.current.position.ReadValue());
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Debug.Log("Camera | Mouse hit <" + hit.collider.name + "> at " + hit.point);
            return new Tuple<string, Vector3>(hit.collider.name, hit.point);
        }

        return null;
    }

    private void _DisableAllCams()
    {
        foreach (Camera cam in _cameraList)
            cam.enabled = false;
    }

    private void _EnableObjectCam()
    {
        _DisableAllCams();
        _currentCam = objectCam;
        _currentCam.enabled = true;
    }

    private void _EnableMapCam()
    {
        _DisableAllCams();
        _currentCam = mapCam;
        _currentCam.enabled = true;
    }

    public void EnablePerspectiveView()
    {
        foreach (Camera cam in _cameraList)
         cam.orthographic = false;
    }

    public void EnableOrthographicView()
    {
        foreach (Camera cam in _cameraList)
            cam.orthographic = true;
    }

    public void OnSelectedCreature(Component sender, object data)
    {
        if (data is Character)   // maybe have a condition to check whether or not we switch to player or go to map cam... (Eagel or creature view to moving..)
        {
            Debug.Log("Switching to orbit cam");
            Character creature = (Character)data;
            _EnableObjectCam();
            _orbitCam.Enable(creature.creatureDisk);

            //_orbitCam.orbitTarget = creature.creatureDisk.transform;
            //_EnableObjectCam();
            //_orbitCam.UpdateOrbitCamOnce();
        }
    }
}
