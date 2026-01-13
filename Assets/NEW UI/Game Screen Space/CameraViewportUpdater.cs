using UnityEngine;

/// <summary>
/// Updates all camera's `rect` values.
/// </summary>
public class CameraViewportUpdater : AbstractUI
{
    [SerializeField] public RectTransform screenSpaceGameObject;   // will be a UI GameObject, area that the camera's will render

    private void OnRectTransformDimensionsChange()
    {
        // Update the Viewport for all cameras (as long as `screenSpaceGameObject` exists)
        if (screenSpaceGameObject != null)
            UpdateViewportForAllCameras();
    }

    public override void Configure()
    {
        if (screenSpaceGameObject == null)
        {
            DebugOut.Log(this, "requires a target RectTransform to work.");
            return;
        }

        UpdateViewportForAllCameras();
    }

    public override void Setup()
    {
        screenSpaceGameObject = GetComponent<RectTransform>();
    }

    /// <summary>
    /// Updates the viewport for all camera's to `screenSpaceGameObject`.
    /// ie Sets all cameras (in AbstractCamera.cameraList) rect value to the screen space occupied by `screenSpaceGameObject`.
    /// </summary>
    private void UpdateViewportForAllCameras()
    {
        Canvas.ForceUpdateCanvases();   // force canvas update

        // debugging
        //Debug.Log($"RectTransform sizeDelta: {screenSpaceGameObject.sizeDelta}");
        //Debug.Log($"RectTransform rect: {screenSpaceGameObject.rect}");
        //Debug.Log($"rt.name = {screenSpaceGameObject.name}");
        //Debug.Log($"sizeDelta = {screenSpaceGameObject.sizeDelta}");
        //Debug.Log($"rect = {screenSpaceGameObject.rect}");
        //Debug.Log($"lossyScale = {screenSpaceGameObject.lossyScale}");
        //Debug.Log($"activeInHierarchy = {screenSpaceGameObject.gameObject.activeInHierarchy}");

        // Get four corners of the UI object
        Vector3[] corners = new Vector3[4];
        screenSpaceGameObject.GetWorldCorners(corners);

        Vector3 bottomLeft = corners[0];
        Vector3 topRight = corners[2];

        // Since it's "Screen Space - Overlay", these are already in screen space
        Rect screenRect = new Rect(
            bottomLeft.x,
            bottomLeft.y,
            topRight.x - bottomLeft.x,
            topRight.y - bottomLeft.y
        );

        // Normalize to viewport coordinates (0–1)
        Rect viewPortRect = new Rect(
            screenRect.x / Screen.width,
            screenRect.y / Screen.height,
            screenRect.width / Screen.width,
            screenRect.height / Screen.height
        );

        foreach (var cam in AbstractCamera.cameraList)
        {
            if (cam != null && cam.GetCamera() != null)
                cam.GetCamera().rect = viewPortRect;
        }
        //Debug.Log($"{_debugStart}Viewport updated: {viewPortRect}");
    }

    /// <summary>
    /// Gets the Camera Rect (The area that the camera will render).
    /// Relative to the screen space occupied by `screenSpaceGameObject`.
    /// MUST ENSURE THERE IS AN ACTIVE CAMERA! -> Enables `cam` from parameter then disables it.
    /// </summary>
    //protected static void _SetViewPortRect(Camera cam)
    //{
    //    Debug.Log("Setting camera rect's");

    //    if (screenSpaceGameObject == null)
    //        Debug.Log("screenSpaceGameObject NULL");

    //    bool isOriginallyEnabled = cam.enabled;
    //    if (!isOriginallyEnabled)
    //        cam.enabled = true;

    //    Canvas.ForceUpdateCanvases();

    //    RectTransform rt = screenSpaceGameObject.GetComponent<RectTransform>();

    //    Debug.Log($"RectTransform sizeDelta: {rt.sizeDelta}");
    //    Debug.Log($"RectTransform rect: {rt.rect}");
    //    Debug.Log($"rt.name = {rt.name}");
    //    Debug.Log($"sizeDelta = {rt.sizeDelta}");
    //    Debug.Log($"rect = {rt.rect}");
    //    Debug.Log($"lossyScale = {rt.lossyScale}");
    //    Debug.Log($"activeInHierarchy = {rt.gameObject.activeInHierarchy}");

    //    // Get four corners of the UI object
    //    Vector3[] corners = new Vector3[4];
    //    rt.GetWorldCorners(corners);

    //    foreach (var v in corners)
    //        Debug.Log($"corner: {v}");

    //    Vector3 bottomLeft = corners[0];
    //    Vector3 topRight = corners[2];

    //    // Since Screen Space - Overlay, these are already in screen space
    //    Rect screenRect = new(
    //        bottomLeft.x,
    //        bottomLeft.y,
    //        topRight.x - bottomLeft.x,
    //        topRight.y - bottomLeft.y
    //    );

    //    // Normalize to viewport coordinates (0–1)
    //    Rect viewPortRect = new(
    //        screenRect.x / Screen.width,
    //        screenRect.y / Screen.height,
    //        screenRect.width / Screen.width,
    //        screenRect.height / Screen.height
    //    );

    //    Debug.Log($"ScreenRect: {screenRect} | ViewPortRect: {viewPortRect}");
    //    _viewPortRect = viewPortRect;

    //    if (!isOriginallyEnabled)
    //        cam.enabled = false;

    //    //foreach (Camera camera in _cameraList)
    //    //    camera.rect = viewPortRect;
    //}
}
