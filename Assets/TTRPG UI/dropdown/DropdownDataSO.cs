using UnityEngine;

[CreateAssetMenu(fileName = "DropdownDataSO", menuName = "Scriptable Objects/DropdownDataSO")]
public class DropdownDataSO : ScriptableObject
{
    public Color normal;
    public Color highlighted;
    public Color pressed;
    public Color selected;
    public Color disabled;
    public float colorMultiplier;
    public float fadeDuration;
}
