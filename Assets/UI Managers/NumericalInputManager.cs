using TMPro;
using UnityEngine;

public class NumericalInputManager : AbstractUI
{
    private string _debugStart = "NumericalInputHandler | ";
    private string _numericalStr = string.Empty;
    private TMP_InputField _inputField;

    public GameEvent _valueChanged;

    public override void Configure()
    {
        // Don't need to do this since I will be adding the Listeners via the Unity Inspector
        //RemoveAllListeners();
        //inputField.onValueChanged.AddListener(OnInputChanged);

        _inputField.ActivateInputField();
    }

    public override void Setup()
    {
        _inputField = GetComponentInChildren<TMP_InputField>();
    }

    private void OnDestroy()
    {
        _inputField.onValueChanged.RemoveAllListeners();
    }
    public void OnValueChanged(string text)
    {
        string f = $"you entered: {text}\n";

        int wef = ParseStringToInt(text);

        if (wef > 0)
        {
            _numericalStr = wef.ToString();
            _inputField.text = _numericalStr;
        }
        else { 
            _inputField.text = _numericalStr;
        }
    }

    public void OnEndEdit()
    {
        Debug.Log($"Stopped editing, text entered: {_numericalStr}");
        _inputField.text = _numericalStr;
        _valueChanged.Raise(this, int.Parse(_numericalStr));
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
    /// Will attempt to parse the string to an int. If it parses, will return the value as is or the absolute of it.
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
