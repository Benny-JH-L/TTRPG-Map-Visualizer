using TMPro;
using UnityEngine;

public class TTRPG_TextInputManager : AbstractInputManager
{
    private string _debugStart = "TTRPG_TextInputManager | ";
    private string _textSoFar = string.Empty;

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
        _inputField.pointSize = textData.size;
        _inputField.characterLimit = textData.characterLimit;
    }

    private void OnDestroy()
    {
        _inputField.onValueChanged.RemoveAllListeners();
    }
    public override void OnValueChanged(string text)
    {
        _textSoFar = text;
    }

    public override void OnEndEdit()
    {
        Debug.Log($"{_debugStart}Stopped editing, text entered: {_textSoFar}");
        _inputField.text = _textSoFar;
        valueChanged.Raise(this, _textSoFar); // commented out during some testing
    }

    public override void InitializeInputField(Component comp, object data)
    {
        if (data is string s)
        {
            _inputField.text = s;
            //Debug.Log($"init text to {s}");
        }

        if (data is null)
            Debug.Log("Yo something ain't right with the data!");
    }

}
