using UnityEngine;
using UnityEngine.InputSystem;

public class Spawner : MonoBehaviour
{
    public CameraManager cameraManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        // should i have this in GameManagerScript instead?
        if (Keyboard.current.digit1Key.wasPressedThisFrame)
        {
            var tup = cameraManager.GetCurrMosePos();
            Vector3 pos = tup.Item2;
            Player.Create(pos);
        }
        else if (Keyboard.current.digit2Key.wasPressedThisFrame)
        {
            var tup = cameraManager.GetCurrMosePos();
            Vector3 pos = tup.Item2;
            Enemy.Create(pos);
        }
        else if (Keyboard.current.digit3Key.wasPressedThisFrame)
        {
            var tup = cameraManager.GetCurrMosePos();
            Vector3 pos = tup.Item2;
            Character.Create(pos);
        }
    }

}
