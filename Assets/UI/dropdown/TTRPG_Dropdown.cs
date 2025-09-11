using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TTRPG_Dropdown : AbstractUI
{
    public DropdownDataSO dropdownData;

    private TMP_Dropdown dropdown;

    public override void Configure()
    {
        ColorBlock colorBlock = dropdown.colors;
        colorBlock.normalColor = dropdownData.normal;
        colorBlock.selectedColor = dropdownData.selected;
        colorBlock.disabledColor = dropdownData.disabled;
        colorBlock.pressedColor = dropdownData.pressed;
        colorBlock.colorMultiplier = dropdownData.colorMultiplier;
        dropdown.colors = colorBlock;

    }

    public override void Setup()
    {
        dropdown = GetComponentInChildren<TMP_Dropdown>();
    }

    public void SetNewOptions(string[] optionNames)
    {
        dropdown.ClearOptions();
        List<TMP_Dropdown.OptionData> options = optionNames.Select(n => new TMP_Dropdown.OptionData(n)).ToList();
        dropdown.AddOptions(options);
    }
}
