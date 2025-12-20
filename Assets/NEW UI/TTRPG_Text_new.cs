using TMPro;
using UnityEngine;

public class TTRPG_Text_New : AbstractUI
{
    public TTRPG_TextSO textData;
    private TextMeshProUGUI tmpUGUI;

    public override void Configure()
    {
        tmpUGUI.font = textData.font;
        tmpUGUI.fontSize = textData.fontSize;
    }

    public override void Setup()
    {
        tmpUGUI = GetComponentInChildren<TextMeshProUGUI>();
    }
}
