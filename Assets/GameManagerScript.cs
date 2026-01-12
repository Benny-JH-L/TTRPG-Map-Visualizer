using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManagerScript : MonoBehaviour
{
    public static GameData gameData;
    public static GameObject screenSpaceGameObject;

    public TTRPG_SceneObjectBase selectedObject;

    public GameEventSO selectedObjectEvent;
    public GameEventSO deSelectedObjectEvent;
    public GameEventSO mouseRightClickEvent;
    public GameEventSO cameraChangedEvent;

    //private bool _isUIFocused;
    //private bool _isGameScreenFocused

    public CreatureSpawner creatureSpawner;
    public InanimateObjectSpawner inanimateObjectSpawner;

    public UtilityStorage utilStorage;

    // initialized in Start()
    [SerializeField] private MouseTracker mouseTracker;
    [SerializeField] private CameraManager cameraManager;


    // for testing
    public GameObject creatureAppearancePrefab;
    public GameObject inanimateAppearancePrefab;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //_isUIFocused = false;

        // checks should be made to see if we have any null vars 
        if (creatureSpawner == null)
            ErrorOut.Throw(this, "creature spawner cannot be null");
        if (inanimateObjectSpawner == null)
            ErrorOut.Throw(this, "inanimate object spawner cannot be null");
        if (creatureAppearancePrefab == null)
            ErrorOut.Throw(this, "creatureAppearancePrefab null");
        //if (utilStorage == null)
        //    ErrorOutput.printError(this, "UtilityStorage cannot be null");

        utilStorage.CheckContents();
        mouseTracker = utilStorage.mouseTracker;
        cameraManager = utilStorage.cameraManager;

        //if (inanimateObjectSpawner == null)
        //    ErrorOutput.printError(this, "inanimate Object Spawner cannot be null");

    }

    // Update is called once per frame
    void Update()
    {
        // check if the user is interacting with the UI or if the mouse is not over the `GameScreenSpace`
        if (mouseTracker.IsMouseOverUIElement())
        {
            //DebugPrinter.printMessage(this, " - Update() - mouse is over UI element, returning...");
            return;
        }
        //if (_isUIFocused || !_isGameScreenFocused)
        //{
        //    //Debug.Log($"mouse is interacting with UI OR mouse is not over `GameScreenSpace` | {MouseTracker.GetMousePos()}");
        //    return;
        //}

        CheckLeftMousePress();
        CheckSpawning();
    }

    /// <summary>
    /// Check for spawning via keyboard bind
    /// </summary>
    private void CheckSpawning()
    {
        //DebugPrinter.printMessage(this, "Checking Spawn");
        var tup = cameraManager.GetGameObjectAtMousePos();

        if (tup == null)  // check for non value
        {
            //DebugPrinter.printMessage(this, "null val... returning");
            return;
        }

        Vector3 mouseWorldPos = tup.Item2;

        if (Keyboard.current.digit1Key.wasPressedThisFrame)
        {
            //Creature_OLD.Create(pos);
            creatureSpawner.Spawn(gameData, mouseTracker, mouseWorldPos, creatureAppearancePrefab);
        }
        else if (Keyboard.current.digit2Key.wasPressedThisFrame)
        {
            inanimateObjectSpawner.Spawn(gameData, mouseTracker, mouseWorldPos, inanimateAppearancePrefab);
        }
        //// testing
        //else if (Keyboard.current.digit3Key.wasPressedThisFrame)
        //{
        //    map.AddLayer(1);    // old implmentation that does not work
        //}
    }

    /// <summary>
    /// Checks the mouse position if the left mouse button was pressed. 
    /// Raises `DeselectedObject` event if: 
    ///     1) pressed off the selected TTRPG_SceneObjectBase, 2) pressed the same TTRPG_SceneObjectBase twice.
    /// Raises `SelectedObject` event if:
    ///     1) pressed a TTRPG_SceneObjectBase and no TTRPG_SceneObjectBase was selected previously.
    /// Raises BOTH events if:
    ///     1) currently selected TTRPG_SceneObjectBase is different from the TTRPG_SceneObjectBase the mouse clicked.
    /// </summary>
    private void CheckLeftMousePress()
    {
        if (!Mouse.current.leftButton.wasPressedThisFrame)
            return;

        TTRPG_SceneObjectBase sceneObjAtMousePos = GetSceneObjectAtMousePos();

        if (selectedObject != null && sceneObjAtMousePos == null)     // pressed off the selected object
        {
            DeselectObject();
        }
        else if (selectedObject != null && selectedObject == sceneObjAtMousePos)  // pressed the same object twice
        {
            DeselectObject();
        }
        else if (selectedObject != null && sceneObjAtMousePos != null)            // pressed a different object while already selected an object
        {
            DeselectObject();                   // deselect the current object
            SelectObject(sceneObjAtMousePos);   // select the new object
        }
        else if (selectedObject == null && sceneObjAtMousePos != null)            // no object selected previously
        {
            SelectObject(sceneObjAtMousePos);
        }
    }

    /// <summary>
    /// Raises `selectedObjectEvent` GameEvent.
    /// </summary>
    /// <param name="selectedObj"></param>
    private void SelectObject(TTRPG_SceneObjectBase selectedObj)
    {
        DebugOut.Log(this, $"Raising SelectedObject Event | select TTRPG_SceneObjectBase: {selectedObj.name}");
        selectedObject = selectedObj;
        selectedObjectEvent.Raise(this, selectedObj);
    }

    /// <summary>
    /// Raises `deSelectedObjectEvent` GameEvent.
    /// </summary>
    private void DeselectObject()
    {
        DebugOut.Log(this, $"Raising deSelectedObject Event | deselect TTRPG_SceneObjectBase: {selectedObject.name}");
        TTRPG_SceneObjectBase old = selectedObject;
        selectedObject = null;  // reset
        deSelectedObjectEvent.Raise(this, old);
    }


    /// <summary>
    /// Gets the TTRPG_SceneObjectBase the mouse is on top of.
    /// Returns `null` if there are no TTRPG_SceneObjectBase's spawned, or the mouse is not directly atop of a TTRPG_SceneObjectBase.
    /// </summary>
    /// <returns>TTRPG_SceneObjectBase the mouse is over, null otherwise.</returns>
    private TTRPG_SceneObjectBase GetSceneObjectAtMousePos()
    {
        if (gameData.sceneObjectList.Count == 0)
        {
            DebugOut.Log(this, "Scene Object list is empty... Can not select any at mouse position...");
            return null;
        }

        // get information at the mouse position
        Tuple<GameObject, Vector3> x = cameraManager.GetGameObjectAtMousePos();
        GameObject rayHitGameObject = x.Item1;
        Vector3 mousePos = x.Item2;

        if (rayHitGameObject == null)
        {
            DebugOut.Log(this, $"Mouse position {mousePos} does not direcly overlap with any `TTRPG_SceneObjectBase`");
            return null;
        }

        // the mouse would have clicked the TTRPG_SceneObjectBase's disk base or appearance, so we need to get the parent to get the TTRPG_SceneObjectBase
        TTRPG_SceneObjectBase sceneObj = rayHitGameObject.GetComponentInParent<TTRPG_SceneObjectBase>();

        if (sceneObj == null)
            DebugOut.Log(this, $" | `{rayHitGameObject.name}`'s parent does not contain `TTRPG_SceneObjectBase` component!");

        DebugOut.Log(this, $"returning: {(sceneObj == null ? "`null`" : $"`{sceneObj.name}`") }");
        return sceneObj;
    }

    //public void OnUIFocued(Component comp, object data)
    //{
    //    if (data is bool r)
    //        _isUIFocused = r;
    //}

    //public void OnGameScreenFocused(Component comp, object data)
    //{
    //    if (data is bool r)
    //        _isGameScreenFocused = r;
    //}
}
