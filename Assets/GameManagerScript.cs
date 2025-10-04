using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManagerScript : MonoBehaviour
{
    public static GameData gameData;
    public static GameObject screenSpaceGameObject;
    private static string _debugStart = "Game Manager Script | ";

    public CameraManager cameraManager;
    public Map map; // for testing

    public GameEvent selectedObjectEvent;
    public GameEvent deSelectedObjectEvent;
    public GameEvent mouseRightClickEvent;
    public GameEvent cameraChangedEvent;

    private bool _isUIFocused;
    private bool _isGameScreenFocused;

    // old
    //public GameEvent spawnPlayerEvent;
    //public GameEvent spawnEnemyEvent;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _isUIFocused = false;
    }

    int count = 0;
    // Update is called once per frame
    void Update()
    {
        // check if the user is interacting with the UI or if the mouse is not over the `GameScreenSpace`
        if (_isUIFocused || !_isGameScreenFocused)
        {
            //Debug.Log($"mouse is interacting with UI OR mouse is not over `GameScreenSpace` | {MouseTracker.GetMousePos()}");
            return;
        }

        CheckLeftMousePress();
        CheckSpawning();
    }

    /// <summary>
    /// Check for spawning via keyboard bind
    /// </summary>
    private void CheckSpawning()
    {
        var tup = cameraManager.GetGameObjectAtMousePos();
        Vector3 pos = tup.Item2;

        if (Keyboard.current.digit1Key.wasPressedThisFrame)
        {
            Creature.Create(pos);
        }
        else if (Keyboard.current.digit2Key.wasPressedThisFrame)
        {
            InanimateObject.Create(pos);
            Debug.Log("NEED UI IMPLMENTATION!");
        }
        // testing
        else if (Keyboard.current.digit3Key.wasPressedThisFrame)
        {
            map.AddLayer(1);
        }
    }

    /// <summary>
    /// Checks the mouse position if the left mouse button was pressed. 
    /// If is there a GeneralObject over it `SelectedObject` event is raised.
    /// Otherwise `DeselectedObject` event is raised.
    /// </summary>
    private void CheckLeftMousePress()
    {
        if (!Mouse.current.leftButton.wasPressedThisFrame)
            return;

        GeneralObject generalObj = GetGeneralObjectAtMousePos();

        if (generalObj == null)
        {
            Debug.Log($"{ _debugStart}Raising deSelectedObject Event | General Object (should be null): <{generalObj}>");
            deSelectedObjectEvent.Raise(this, null); // `generalObj` is null
            return;
        }

        Debug.Log(_debugStart + "Raising SelectedObject Event | creature: " + generalObj);
        selectedObjectEvent.Raise(this, generalObj);
    }

    /// <summary>
    /// Selects the GeneralObject at the mouse position, if it exists.
    /// </summary>
    /// <returns>The GeneralObject the mouse is over, if it exists. Null otherwise.</returns>
    private GeneralObject GetGeneralObjectAtMousePos()
    {
        return GetGeneralObjectAtMousePosHelper();
    }

    /// <summary>
    /// Gets the GeneralObject the mouse is on top of.
    /// Returns `null` if there are no GeneralObject's spawned, or the mouse is not directly atop of a GeneralObject.
    /// </summary>
    /// <returns>GeneralObject the mouse is over, null otherwise.</returns>
    private GeneralObject GetGeneralObjectAtMousePosHelper()
    {
        if (gameData.generalObjectList.Count == 0)
        {
            Debug.Log("General Object list is empty... Can not select any at mouse position...");
            return null;
        }

        // get information at the mouse position
        Tuple<GameObject, Vector3> x = cameraManager.GetGameObjectAtMousePos();
        GameObject rayHitGameObject = x.Item1;
        Vector3 mousePos = x.Item2;

        // check if the mouse pressed a GeneralObject
        if (!rayHitGameObject.TryGetComponent<GeneralObject>(out GeneralObject generalObject))
        {
            // if entered; `generalObject` var will be null (ie `TryGetComponent(...)` returned false
            Debug.Log($"{_debugStart}Mouse position {mousePos} does not direcly overlap with any `GeneralObject` | Hit object name <{rayHitGameObject.name}>");
            return null;
        }

        GeneralObject generalObjectClosestToMouse = null;
        float cloestDistanceSoFar = float.MaxValue;
        //Vector3 cloestDistanceSoFar = new Vector3(float.MaxValue, float.MaxValue, float.MaxValue);

        // Find the game object closest to the mouse's position
        foreach (GeneralObject gameObject in gameData.generalObjectList)
        {
            //Vector3 center = gameObject.GetComponent<Collider>().bounds.center; // accounts for size shape and transformations (for more complex shapes)
            //Vector3 center2 = gameObject.transform.position; // for simpler shapes? or for those without a collider

            Vector3 gameObjectCenter = gameObject.GetComponent<Collider>().bounds.center;
            float distance = (mousePos - gameObjectCenter).magnitude;   // distance of mouse position and game object
            if (distance < cloestDistanceSoFar)
            {
                cloestDistanceSoFar = distance;
                generalObjectClosestToMouse = gameObject;
            }
        }

        //Debug.Log("Gameobject<" + rayHitGameObject + "> under mouse pos is: " + generalObjectClosestToMouse.GetPosition());
        Debug.Log($"{_debugStart}GeneralObject<{generalObjectClosestToMouse}> with pos <{generalObjectClosestToMouse.GetPosition()}> is under the mouse pos.");
        return generalObjectClosestToMouse;
    }

    public void OnUIFocued(Component comp, object data)
    {
        if (data is bool r)
            _isUIFocused = r;
    }

    public void OnGameScreenFocused(Component comp, object data)
    {
        if (data is bool r)
            _isGameScreenFocused = r;
    }
}
