using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraManager : MonoBehaviour
{
    public GameEventListener onSelectedObject;
    public GameEventListener onCameraProjectionChange;

    public Camera mapCam;
    public Camera objectCam;    // for game object
    private Camera _currentCam;
    private List<Camera> _cameraList;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _cameraList = new List<Camera>();
        _cameraList.Add(mapCam);
        _cameraList.Add(objectCam);

        _currentCam = mapCam;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Tuple<string, Vector3> GetCurrMosePos()
    {
        //Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue()); // works with camera's with the tag "MainCamera"
        Ray ray = _currentCam.ScreenPointToRay(Mouse.current.position.ReadValue());
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Debug.Log("Mouse hit <" + hit.collider.name + "> at " + hit.point);
            return new Tuple<string, Vector3>(hit.collider.name, hit.point);
        }

        return null;
    }
}
