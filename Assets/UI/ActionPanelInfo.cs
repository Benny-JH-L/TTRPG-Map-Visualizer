
using TMPro;
using UnityEngine;

[System.Serializable]
public class ActionPanelInfo : AbstractUI
{
    public string actionName;
    public string actionDescription;

    private TMP_InputField inputName;
    private TMP_InputField inputDescription;

    public override void Setup()
    {
        actionName = string.Empty;
        actionDescription = string.Empty;

        TMP_InputField[] inputs = GetComponentsInChildren<TMP_InputField>();

        inputName = inputs[0];
        inputDescription = inputs[1];
    }

    public override void Configure()
    {

    }


    public void OnActionNameChange()
    {
        actionName = inputName.text;
    }

    public void OnActionDescriptionChange()
    {
        actionDescription = inputDescription.text;
    }
}
