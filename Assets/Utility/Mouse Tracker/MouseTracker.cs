
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

[System.Serializable]
public class MouseTracker : MonoBehaviour
{
    public static GameData gameData;    // static for now

    public static GameObject screenSpaceGameObject; // UI component that occupies the screen space of the active game.
    public List<GameObject> gameObjectsOverScreenSpace;  // UI components that occupy the screen space over the `screenSpaceGameObject`

    public GameEventSO gameScreenFocused;         // True: mouse is inside GameScreenSpace, False otherwise. 
    //private bool _mouseInsideGameScreenSpace;   // Used so `gameScreenFocused` isn't called every `Update()`
    
    [SerializeField] private CameraManager cameraManager;
    public bool debugDisabled = false;

    void Start()
    {
        //_mouseInsideGameScreenSpace = false;
    }

    void Update()
    {
        // NOTE: funny result but if i tap on a input field and then move mouse over to game screen space i can type and move objects lol

        bool result = IsMousePositionInsideCameraRect();    

        //if (result && !_mouseInsideGameScreenSpace)         // mouse moved inside GameScreenSpace
        //{
        //    //gameScreenFocused.Raise(this, true);
        //    //_mouseInsideGameScreenSpace = true;
        //    Debug.Log($"{_debugStart}gameScreenFocused TRUE");
        //}
        //else if (!result && _mouseInsideGameScreenSpace)    // mouse moved outside of GameScreenSpace
        //{
        //    //gameScreenFocused.Raise(this, false);
        //    //_mouseInsideGameScreenSpace = false;
        //    Debug.Log($"{_debugStart}gameScreenFocused FALSE");
        //}
    }

    /// <summary>
    /// Checks if the mouse is over a certain portion of the screen which is defined by the UI GameObject `screenSpaceGameObject`s RectTransform.
    /// If there is a UI component that is over top the space defined by `screenSpaceGameObject` (and is active) then we do not consider the mouse position
    /// over top of `screenSpaceGameObject`.
    /// </summary>
    /// <returns>True if the mouse is over the specific portion of the screen, False otherwise.</returns>
    private bool IsMousePositionInsideCameraRect()
    {
        foreach (GameObject go in gameObjectsOverScreenSpace)
        {
            // if the mouse position is over a UI component that is on top of the screen space, then we do not consider it inside the Camera Rect
            if (go.activeSelf && RectTransformUtility.RectangleContainsScreenPoint(go.GetComponent<RectTransform>(), GetMousePosInScreen(), null))
                return false;
        }
        return RectTransformUtility.RectangleContainsScreenPoint(screenSpaceGameObject.GetComponent<RectTransform>(), GetMousePosInScreen(), null); // use `null` as camera since `screenSpaceGameObject` (the canvas) is a Screen Space - Overlay
    }

    /// <summary>
    /// Checks if the mouse is over a TTRPG_SceneObjectBase.
    /// </summary>
    /// <returns>True if the mouse is over a TTRPG_SceneObjectBase and not over a UI element, False otherwise.</returns>
    public bool IsMouseOverSceneObject()
    {
        if (IsMouseOverUIElement())
        {
            DebugOut.Log(this, " - IsMouseOverSceneObject() - mouse is over a UI element!", debugDisabled);
            return false;
        }

        //foreach (GameObject gameObject in gameData.generalObjList)
        foreach (TTRPG_SceneObjectBase sceneObj in gameData.sceneObjectList)
        {
            Vector3 generalObjPos = sceneObj.transform.position;
            //DebugPrinter.printMessage(this, $"obj at pos: {generalObjPos}");
            //DebugPrinter.printMessage(this, $"mouse world pos: {GetMousePositionInWorld()}");

            //float magnitudeFromSpawnPosToObjPos = (generalObjPos - spawnPosition).magnitude;
            float magnitudeFromSpawnPosToObjPos = Vector3.Distance(generalObjPos, GetMousePositionInWorld());

            //DebugPrinter.printMessage(this, $"magnitudeFromSpawnPosToObjPos: {magnitudeFromSpawnPosToObjPos}");
            //DebugPrinter.printMessage(this, $"sceneObj.GetData.diskBaseRadius: {sceneObj.GetData.diskBaseRadius}");
            //DebugPrinter.printMessage(this, $"sceneObj.data: {sceneObj.GetData}");

            //if (magnitudeFromSpawnPosToObjPos <= generalObj.diskBaseRadius)
            if (magnitudeFromSpawnPosToObjPos <= sceneObj.GetData.diskBaseRadius)
                return true;
        }
        return false;
    }

    /// <summary>
    /// Uses `EventSystem.current.IsPointerOverGameObject()` to check if the mouse is over a UI Element.
    /// </summary>
    /// <returns>True if the mouse is over a UI element, False otherwise.</returns>
    public bool IsMouseOverUIElement()
    {
        return EventSystem.current.IsPointerOverGameObject();
    }

    /// <summary>
    /// Get's the Mouse position in the world.
    /// </summary>
    /// <returns>Vector3</returns>
    public Vector3 GetMousePositionInWorld()
    {
        return cameraManager.GetMousePosInWorld();
    }

    /// <summary>
    /// Get's the Mouse Screen Space position with `Mouse.current.position.ReadValue();`.
    /// Use `GetMousePosInWorld()` instead if you need the mouse position in the world
    /// </summary>
    /// <returns>A Vector2 of the mouse position.</returns>
    public Vector2 GetMousePosInScreen()
    {
        return Mouse.current.position.ReadValue();
    }

}
