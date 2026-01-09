using UnityEngine;

/// <summary>
/// Stores references to useful scripts.
/// </summary>
public class UtilityStorage : MonoBehaviour
{
    public CameraManager cameraManager;
    public MouseTracker mouseTracker;

    /// <summary>
    /// Checks if any of its variables are `null` references. Will print Errors if something is wrong.
    /// </summary>
    /// <returns>True if everything is in order with the storage, False otherwise.</returns>
    public bool CheckContents()
    {
        bool storageInOrder = true;

        if (cameraManager == null) { ErrorOutput.printError(this, "camera manager NULL"); storageInOrder = false; }
        if (mouseTracker == null) { ErrorOutput.printError(this, "mouse tracker NULL"); storageInOrder = false; }
            
        return storageInOrder;
    }
}
