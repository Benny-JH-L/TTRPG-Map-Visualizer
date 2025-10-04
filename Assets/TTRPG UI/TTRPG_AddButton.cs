
using UnityEngine;

public class TTRPG_AddButton : AbstractUI
{
    private static string _debugStart = "TTRPG_AddButton | ";

    /// <summary>
    /// the prefab that is added when this button is pressed.
    /// </summary>
    public GameObject prefabToAdd;

    public ButtonDataSO data;

    public GameEvent clickedEvent;

    public override void Configure()
    {
        Debug.Log($"{_debugStart}Configure not implmented");
    }

    public override void Setup()
    {
        Debug.Log($"{_debugStart}Setup not implmented");
    }

    public void OnClick()
    {
        Debug.Log("Action add button clicked");
        clickedEvent.Raise(this, prefabToAdd);
    }
}
