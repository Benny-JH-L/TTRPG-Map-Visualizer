using TMPro;
using UnityEngine;

public class TTRPG_Button : AbstractUI
{
    private static string _debugStart = "TTRPG_Button | ";

    public ButtonDataSO data;

    public GameEvent clickedEvent;

    protected TextMeshProUGUI textMeshProUGUI;

    public override void Configure()
    {
        Debug.LogWarning($"{_debugStart}Configure not implmented fully");
        //textMeshProUGUI.fontSize = data.textData.size;
    }

    public override void Setup()
    {
        Debug.LogWarning($"{_debugStart}Setup not implmented fully");
        //textMeshProUGUI = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void OnClick()
    {
        Debug.Log("Button clicked");
        clickedEvent.Raise(this, null);
    }
}
