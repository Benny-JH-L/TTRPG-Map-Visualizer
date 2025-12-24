using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TTRPG_Dropdown : AbstractUI
{
    private static string _debugStart = "TTRPG_Dropdown | ";

    public DropdownDataSO dropdownData;

    public TMP_Dropdown dropdown;   // public for inspector

    public GameEventSO onValueChange;

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

    public void OnValueChanged(Int32 val)
    {
        onValueChange.Raise(this, val);
    }

    public void InitDropdownOptions(Component comp, object data) 
    { 
        if (data is string[] optionNames)
            SetNewOptions(optionNames);
    }

    public void InitActiveOption(Component comp, object data)
    {
        if (data is int val)
        {
            dropdown.value = val;
            dropdown.RefreshShownValue();
        }
        else
            Debug.Log($"{_debugStart}Init data is invalid: {data}");
    }

    public void SetNewOptions(string[] optionNames)
    {
        dropdown.ClearOptions();
        List<TMP_Dropdown.OptionData> options = optionNames.Select(n => new TMP_Dropdown.OptionData(n)).ToList();
        dropdown.AddOptions(options);
    }
}
