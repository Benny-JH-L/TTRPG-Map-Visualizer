using TMPro;
using UnityEngine;

public class TTRPG_NumericalInputManager : AbstractInputManager
{
    private string _debugStart = "NumericalInputHandler | ";
    private string _numericalStr = string.Empty;

    public GameEvent valueChanged;

    public override void Configure()
    {
        // Don't need to do this since I will be adding the Listeners via the Unity Inspector
        //RemoveAllListeners();
        //inputField.onValueChanged.AddListener(OnInputChanged);
    }

    public override void Setup()
    {
        _inputField = GetComponentInChildren<TMP_InputField>();
    }

    private void OnDestroy()
    {
        _inputField.onValueChanged.RemoveAllListeners();
    }
    public override void OnValueChanged(string text)
    {
        int val = ParseStringToInt(text);

        if (val > 0)
        {
            _numericalStr = val.ToString();
            _inputField.text = _numericalStr;
        }
        else 
        { 
            _inputField.text = _numericalStr;
        }
    }

    public override void OnEndEdit()
    {
        Debug.Log($"{_debugStart}Stopped editing, text entered: {_numericalStr}");

        if (_numericalStr == string.Empty)
            return;

        _inputField.text = _numericalStr;
        valueChanged.Raise(this, int.Parse(_numericalStr));
    }

    //    /// <summary>
    //  /// removes onValueChanged listeners at the moment
    ///// </summary>
    //private void RemoveAllListeners()
    //{
    //    //inputField.onValueChanged.RemoveAllListeners();
    //    //inputField.onValueChanged.RemoveListener(OnInputChanged);

    //}

    /// <summary>
    /// Will attempt to parse the string to an int. If it parses, will return the value as is (`absoluteValue` is False), or the absolute of it (`absoluteValue` is True).
    /// </summary>
    /// <param name="text"></param>
    /// <param name="absoluteValue"></param>
    /// <returns>Return -1 if it couldn't parse the string to an int.</returns>
    private int ParseStringToInt(string text, bool absoluteValue)
    {
        if (int.TryParse(text, out int value))
        {
            return value >= 0 ? value : (absoluteValue ? -value : value);
        }
        else
            return -1;
    }

    /// <summary>
    /// Will attempt to parse the string to an int. If it parses, will return the value as is.
    /// </summary>
    /// <param name="text"></param>
    /// <returns></returns>
    private int ParseStringToInt(string text)
    {
        return ParseStringToInt(text, true);
    }
}
