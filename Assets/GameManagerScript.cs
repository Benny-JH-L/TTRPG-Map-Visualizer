using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManagerScript : MonoBehaviour
{
    public static GameData gameData;

    public CameraManager cameraManager;

    public GameEvent selectedObjectEvent;
    public GameEvent mouseRightClickEvent;
    public GameEvent cameraChangedEvent;
    
    // old
    //public GameEvent spawnPlayerEvent;
    //public GameEvent spawnEnemyEvent;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    int count = 0;
    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.backslashKey.wasPressedThisFrame)  // '\'
        {
            gameData.PrintPlayers();
        }
        else if (Keyboard.current.qKey.wasPressedThisFrame)
        {
            count++;
            //gameData.playerList[0].saveData.className = $"woighowegwe + {count}";
            //gameData.playerList[0].GetComponent<Character>().saveData.className = ClassType.NONE;
            gameData.playerList[0].GetComponent<Character>().GetSaveData().className = ClassType.NONE;
        }

        if (Mouse.current.leftButton.wasPressedThisFrame)
            SelectCreature();
    }

    private Character SelectCreature()
    {
        Character creature = GetCreatureAtMousePos();
        Debug.Log("Raising SelectedObject Event");
        selectedObjectEvent.Raise(this, creature);
        return creature;
    }

    /// <summary>
    /// Gets the creature (Player, Enemy, Other, etc.) the mouse is on top of.
    /// Returns `null` if there are no Creatures spawned, or the mouse is not directlt atop of a Creature.
    /// </summary>
    /// <returns></returns>
    private Character GetCreatureAtMousePos()
    {
        if (gameData.creatureList.Count == 0)
        {
            Debug.Log("Creature list is empty... Can not select any at mouse position...");
            return null;
        }

        Tuple<string, Vector3> x = cameraManager.GetCurrMosePos();
        string rayHitGameObject = x.Item1;
        Vector3 mousePos = x.Item2;

        string gameObjectName = gameData.creatureList[0].name;  // should be the same for all those inside -> "disk(Clone)"
        if (rayHitGameObject != gameObjectName)
        {
            Debug.Log("Mouse position does not direcly overlap with any `Creature` given with name<" + gameObjectName + ">");
            return null;
        }

        Character closestToMouse = null;
        float cloestDistanceSoFar = float.MaxValue;
        //Vector3 cloestDistanceSoFar = new Vector3(float.MaxValue, float.MaxValue, float.MaxValue);

        // Find the game object closest to the mouses position
        foreach (Character gameObject in gameData.creatureList)
        {
            //Vector3 center = gameObject.GetComponent<Collider>().bounds.center; // accounts for size shape and transformations (for more complex shapes)
            //Vector3 center2 = gameObject.transform.position; // for simpler shapes? or for those without a collider

            Vector3 gameObjectCenter = gameObject.GetComponent<Collider>().bounds.center;
            float distance = (mousePos - gameObjectCenter).magnitude;   // distance of mouse position and game object
            if (distance < cloestDistanceSoFar)
            {
                cloestDistanceSoFar = distance;
                closestToMouse = gameObject;
            }
        }

        Debug.Log("Gameobject<" + rayHitGameObject + "> under mouse pos is: " + closestToMouse.GetPosition());
        return closestToMouse;
    }

    
}
