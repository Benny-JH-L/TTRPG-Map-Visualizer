using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Deprecate?
/// </summary>
public class TTRPG_Button : AbstractUI
{
    public TTRPG_ButtonSO buttonData;
    public GameEventSO clickedEvent;

    protected Button button;
    protected TextMeshProUGUI buttonText;

    public override void Configure()
    {
        // have a class that does this repetitive config stuff
        if (buttonData == null)
            ErrorOutput.printError(this, "button data null");
        if (buttonData.textData == null)
            ErrorOutput.printError(this, "buttonData.textData null");

        TTRPG_TextSO textData = buttonData.textData;
        buttonText.font = textData.font;
        buttonText.fontSize = textData.fontSize;
    }

    public override void Setup()
    {
        button = GetComponentInChildren<Button>();
        buttonText = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void OnClick()
    {
        Debug.Log("Button clicked");
        clickedEvent.Raise(this, null);
    }
}
