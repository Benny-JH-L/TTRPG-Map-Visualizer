using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManagerScript : MonoBehaviour
{
    public static GameData gameData;
    private static string _debugStart = "Game Manager Script | ";

    public CameraManager cameraManager;

    public GameEvent selectedObjectEvent;
    public GameEvent deSelectedObjectEvent;
    public GameEvent mouseRightClickEvent;
    public GameEvent cameraChangedEvent;
    
    // old
    //public GameEvent spawnPlayerEvent;
    //public GameEvent spawnEnemyEvent;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    //void Start()
    //{
        
    //}

    int count = 0;
    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.backslashKey.wasPressedThisFrame)  // '\'
        {
            //gameData.PrintPlayers();
            gameData.PrintCharacters();
        }
        else if (Keyboard.current.qKey.wasPressedThisFrame)
        {
            count++;
            //gameData.playerList[0].saveData.className = $"woighowegwe + {count}";
            //gameData.playerList[0].GetComponent<Character>().saveData.className = ClassType.NONE;
            gameData.characterList[0].GetComponent<Character>().GetSaveData().className = ClassType.NONE;   // does not show in unity debug thing, really tempted to remvoe CharacterData and just use CreatureData... (Sept 8 9;44pm note)
            gameData.characterList[0].GetComponent<Character>().GetSaveData().coreStats.ac = 10000;
        }

        if (Mouse.current.leftButton.wasPressedThisFrame)
            CheckMousePos();
    }

    /// <summary>
    /// Checks the mouse position. 
    /// If is there a GeneralObject over it `SelectedObject` event is raised.
    /// Otherwise `DeselectedObject` event is raised.
    /// </summary>
    private void CheckMousePos()
    {
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

    
}
