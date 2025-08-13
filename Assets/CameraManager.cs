using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraManager : MonoBehaviour
{
    // will be on the gameobject
    //public GameEventListener onSelectedObject;
    //public GameEventListener onCameraProjectionChange;

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
        _DisableAllCams();

        _currentCam = mapCam;
        _currentCam.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        
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
            Debug.Log("Mouse hit <" + hit.collider.name + "> at " + hit.point);
            return new Tuple<string, Vector3>(hit.collider.name, hit.point);
        }

        return null;
    }

    private void _DisableAllCams()
    {
        foreach (Camera cam in _cameraList)
            cam.enabled = false;
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
}
