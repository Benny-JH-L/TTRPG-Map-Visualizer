using TMPro;
using UnityEngine;

public abstract class AbstractInputManager : AbstractUI
{
    public GameEvent UIFocused;
    public TextDataSO textData;
    public TMP_InputField _inputField;  // keep protected | public for inspector?

    public abstract void OnValueChanged(string text);
    public abstract void OnEndEdit();

    public abstract void InitializeInputField(Component comp, object data);

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


}