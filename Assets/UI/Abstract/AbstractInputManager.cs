using TMPro;
using UnityEngine;

public abstract class AbstractInputManager : AbstractUI
{
    public GameEvent UIFocused;
    protected TMP_InputField _inputField;

    public abstract void OnValueChanged(string text);
    public abstract void OnEndEdit();

    public void OnSelect()
    {
        UIFocused.Raise(this, true);
        Debug.Log("UI focased event raised");
    }

    public void OnDeSelect()
    {
        UIFocused.Raise(this, false);
        Debug.Log("UI unfocased event raised");
    }

    public void InitializeInputField(Component comp, object data)
    {
        if (data is string val)
            _inputField.text = val;
        else if (data is int num)
            _inputField.text = num.ToString();
    }
}