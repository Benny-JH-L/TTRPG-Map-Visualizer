using UnityEngine;
using UnityEngine.InputSystem;

public class GameManagerScript : MonoBehaviour
{
    public static GameData gameData;

    public CameraManager cameraManager;

    public GameEvent selectedObjectEvent;
    public GameEvent mouseRightClickEvent;
    public GameEvent cameraChangedEvent;
    public GameEvent spawnPlayerEvent;
    public GameEvent spawnEnemyEvent;

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
            gameData.playerList[0].GetComponent<Creature>().saveData.className = $"woighowegwe + {count}";

        }

    }


}
