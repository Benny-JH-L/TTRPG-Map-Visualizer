
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[System.Serializable]
public abstract class AbstractCamera : MonoBehaviour
{
    private static Rect _viewPortRect;

    protected Vector3 origin = Vector3.zero;

    public static CameraData cameraData;
    public static GameObject screenSpaceGameObject;  // space occupied by the actual game and not ScrenSpaceUI
    public static List<AbstractCamera> cameraList = new();

    /// <summary>
    /// camera instance used.
    /// </summary>
    protected Camera cam;

    private void Awake()
    {
        Init();
    }

    public void Init()
    {
        Setup();
        Configure();

        cam.enabled = false;
        cameraList.Add(this);
    }

    public abstract void Setup();
    public abstract void Configure();

    void Start()
    {
        // Please ensure this is called once UI has been set up... (ie if [DefaultExecutionOrder(N)] for this class is too high
        // this wont work :)
        if (_viewPortRect == Rect.zero)
            _SetViewPortRect(cam);

        if (_viewPortRect != Rect.zero)
            cam.rect = _viewPortRect;
    }

    private void OnDestroy()
    {
        if (cam != null)
            cameraList.Remove(this);
    }

    /// <summary>
    /// Moves the camera's distance into or away (closer or further) to something depending on context.
    /// </summary>
    protected abstract void MoveCameraDistance();

    /// <summary>
    /// Updates the camera when called.
    /// </summary>
    public abstract void UpdateCamera();

    /// <summary>
    /// Gets the Camera Rect (The area that the camera will render).
    /// Relative to the screen space occupied by `screenSpaceGameObject`.
    /// MUST ENSURE THERE IS AN ACTIVE CAMERA! -> Enables `cam` from parameter then disables it.
    /// </summary>
    protected static void _SetViewPortRect(Camera cam)
    {
        Debug.Log("Setting camera rect's");

        if (screenSpaceGameObject == null)
            Debug.Log("screenSpaceGameObject NULL");

        cam.enabled = true;
        Canvas.ForceUpdateCanvases();

        RectTransform rt = screenSpaceGameObject.GetComponent<RectTransform>();

        Debug.Log($"RectTransform sizeDelta: {rt.sizeDelta}");
        Debug.Log($"RectTransform rect: {rt.rect}");
        Debug.Log($"rt.name = {rt.name}");
        Debug.Log($"sizeDelta = {rt.sizeDelta}");
        Debug.Log($"rect = {rt.rect}");
        Debug.Log($"lossyScale = {rt.lossyScale}");
        Debug.Log($"activeInHierarchy = {rt.gameObject.activeInHierarchy}");

        // Get four corners of the UI object
        Vector3[] corners = new Vector3[4];
        rt.GetWorldCorners(corners);

        foreach (var v in corners)
            Debug.Log($"corner: {v}");

        Vector3 bottomLeft = corners[0];
        Vector3 topRight = corners[2];

        // Since Screen Space - Overlay, these are already in screen space
        Rect screenRect = new(
            bottomLeft.x,
            bottomLeft.y,
            topRight.x - bottomLeft.x,
            topRight.y - bottomLeft.y
        );

        // Normalize to viewport coordinates (0–1)
        Rect viewPortRect = new(
            screenRect.x / Screen.width,
            screenRect.y / Screen.height,
            screenRect.width / Screen.width,
            screenRect.height / Screen.height
        );

        Debug.Log($"ScreenRect: {screenRect} | ViewPortRect: {viewPortRect}");
        _viewPortRect = viewPortRect;
        cam.enabled = false;

        //foreach (Camera camera in _cameraList)
        //    camera.rect = viewPortRect;
    }

    /// <summary>
    /// Enables this camera. Disables all other cameras (done by default if not overriden by parameter).
    /// </summary>
    public void EnableCamera(bool disableAllOtherCams = true)
    {
        if (cam == null)
        {
            Debug.Log("CAMERA IS NULL, UNABLE TO ENABLE!");
            return;
        }
        else if (disableAllOtherCams)
        {
            foreach (AbstractCamera abstractCamera in cameraList)
            {
                //abstractCamera.cam.enabled = false;
                abstractCamera.DisableCamera();
            }

        }
        //string s = this is OrbitCam ? "ORBIT" : "MAP";
        //Debug.Log($"enabling camera: {s}");
        cam.enabled = true;
    }

    /// <summary>
    /// Disable's this camera.
    /// </summary>
    public void DisableCamera()
    {
        cam.enabled = false;
    }

    /// <summary>
    /// Toggles if this camrea is enabled or disabled. If it's enabled, sets itself to disabled, and viseversa.
    /// </summary>
    public void ToggleEnable()
    {
        cam.enabled = !cam.enabled;
    }

    /// <summary>
    /// Returns a boolean indicating whether or not this camrea is enabled.
    /// </summary>
    /// <returns>Boolean. true, camera is enabled. false otherwise.</returns>
    public bool CamEnabled()
    {
        return cam.enabled;
    }

    /// <summary>
    /// Try not to call this unless absolutely necessary :).
    /// </summary>
    /// <returns></returns>
    public Camera GetCamera()
    {
        return cam;
    }

    /// <summary>
    /// Enables/Disables the orthographoc view.
    /// True: enables orthographics view.
    /// False: enables perspective view.
    /// </summary>
    /// <param name="enableOrtho"></param>
    public void EnableOrthographicView(bool enableOrtho = true)
    {
        cam.orthographic = enableOrtho;
    }

    //public static void SetCameraData(CameraData data)
    //{
    //    cameraData = data;
    //}

    /// <summary>
    /// Zoom's the camera's Perspective FOV, or Ortho size with respect to mouse scroll input.
    /// </summary>
    protected void ZoomCamera()
    {
        float scrollDelta = Mouse.current.scroll.ReadValue().y; // zoom in +1, zoom out -1
        //Debug.Log("scroll data:"+scrollDelta);

        if (Mathf.Abs(scrollDelta) > 0.01f)
        {
            if (cam.orthographic)
            {
                cam.orthographicSize -= scrollDelta * (cameraData.zoomSpeed * 0.01f);
                cam.orthographicSize = Mathf.Clamp(cam.orthographicSize, cameraData.minOrthoSize, cameraData.maxOrthoSize);
            }
            else
            {
                cam.fieldOfView -= scrollDelta * cameraData.zoomSpeed * Time.deltaTime;
                cam.fieldOfView = Mathf.Clamp(cam.fieldOfView, cameraData.minZoom, cameraData.maxZoom);
            }
        }
    }

}
