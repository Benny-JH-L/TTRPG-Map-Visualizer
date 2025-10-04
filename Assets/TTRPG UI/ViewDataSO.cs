using UnityEngine;

[CreateAssetMenu(fileName = "ViewDataSO", menuName = "Scriptable Objects/ViewDataSO")]
public class ViewDataSO : ScriptableObject
{
    public RectOffset padding;
    public float spacing = 0;

    public float flexibleHeight = 0;
    public float flexibleWidth = 0;


}
