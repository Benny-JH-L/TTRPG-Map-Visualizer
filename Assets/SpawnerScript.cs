using UnityEngine;

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
        Vector3 pos = new Vector3(1f, 1f, 1f);
        Player.Create(pos);

        cameraManager.GetCurrMosePos();
    }


}
