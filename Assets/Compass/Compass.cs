
using TMPro;
using UnityEngine;

/// <summary>
/// Updates and tracks the rotations done from cameras (map and orbit) to update compass UI.
/// </summary>
public class CompassUI : AbstractUI
{
    [Header("Camera Used to keep track of rotations")]
    public OrbitCam orbitCam;
    public MapCam mapCam;

    [Header("UI References")]
    public TextMeshProUGUI degreeText;         // Text showing the degree value
    public TextMeshProUGUI directionText;      // Text showing N/E/S/W direction

    [SerializeField] private float currentAngle = 0f;   // uses the Y-axis with respect to euler angles

    public override void Setup()
    {
        
    }

    public override void Configure()
    {
        
    }

    void Update()
    {
        // Update the compass values with respect to the active camera
        if (orbitCam.IsCamEnabled())
        {
            currentAngle = orbitCam.transform.eulerAngles.y;
            UpdateCompassUI();
        }
        else if (mapCam.IsCamEnabled())
        {
            currentAngle = mapCam.transform.eulerAngles.y;
            UpdateCompassUI();
        }
    }

    /// <summary>
    /// Returns the associated cardinal direction with the given `angle`.
    /// </summary>
    /// <param name="angle"></param>
    /// <returns></returns>
    private string GetCardinalDirection(float angle)
    {
        // Map angles to cardinal directions
        if (angle >= 337.5f || angle < 22.5f) return "N";
        if (angle >= 22.5f && angle < 67.5f) return "NE";
        if (angle >= 67.5f && angle < 112.5f) return "E";
        if (angle >= 112.5f && angle < 157.5f) return "SE";
        if (angle >= 157.5f && angle < 202.5f) return "S";
        if (angle >= 202.5f && angle < 247.5f) return "SW";
        if (angle >= 247.5f && angle < 292.5f) return "W";
        return "NW";
    }

    /// <summary>
    /// Updates the UI components `degreeText` and `directionText` using `currentAngle`.
    /// </summary>
    private void UpdateCompassUI()
    {
        // Update degree text
        degreeText.text = Mathf.RoundToInt(currentAngle) + "°";

        // Update direction text
        directionText.text = GetCardinalDirection(currentAngle);
    }
}
