using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TTRPG_Text : AbstractUI
{
    public TextDataSO textData;

    protected TextMeshProUGUI textMeshProUGUI;

    public override void Configure()
    {
        textMeshProUGUI.font = textData.font;
        textMeshProUGUI.fontSize = textData.size;
    }

    public override void Setup()
    {
        textMeshProUGUI = GetComponentInChildren<TextMeshProUGUI>();
    }
}
