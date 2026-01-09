using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TTRPG_AttributeModifierText : TTRPG_Text
{
    public void OnAttributeChange(Component comp, object data)
    {
        UpdateModifierText(data);
    }

    private void UpdateModifierText(object data)
    {
        if (data is int val)
        {
            string newText = val >= 0 ? $"(+{val})" : $"({val})";
            textMeshProUGUI.text = newText;
        }
    }
}
