using UnityEngine;

public class TTRPG_ConfirmationPopupMenu : AbstractUI
{
    private static string _debugStart = "TTRPG_ConfirmationPopupMenu | ";
    public GeneralObject selectedObject;

    public GameObject popupMenu;

    public GameEventSO UIFocused;
    public GameEventSO GeneralObjectDestroyed;

    public override void Configure()
    {
        //throw new System.NotImplementedException();
    }

    public override void Setup()
    {
        //throw new System.NotImplementedException();
        //popupMenu = GetComponentInChildren<GameObject>(); //idk do not work
    }

    public void OnSelectObject(Component comp, object data)
    {
        // if the menu is active, keep the current selectedObject
        if (popupMenu.gameObject.activeSelf)
            return;

        Debug.Log($"{_debugStart}selected obj");
        if (data is GeneralObject)
        {
            Debug.Log($"{_debugStart}selected obj IS GENERAL OBJECT");

            selectedObject = (GeneralObject)data;
        }
        else if (data is Creature)
        {
            Debug.Log($"{_debugStart}selected obj IS Creature");

        }
    }

    public void OnDeselectObject(Component comp, object data)
    {
        // if the menu is active, keep the current selectedObject
        if (popupMenu.gameObject.activeSelf)
            return;

        selectedObject = null;
        Debug.Log($"{_debugStart}deselected obj");
    }

    public void ActivateMenu(Component comp, object data)
    {
        popupMenu.gameObject.SetActive(true);
        UIFocused.Raise(this, true);
    }

    public void OnConfirm(Component comp, object data)
    {
        Debug.Log($"{_debugStart}OnConfirm: {selectedObject}");

        if (selectedObject != null)
        {
            Debug.Log($"{_debugStart}Destroying: {selectedObject}");
            Destroy(selectedObject);
            selectedObject = null;
            GeneralObjectDestroyed.Raise(this, null);
        }
        DeActivateMenu();
    }

    public void OnCancel(Component comp, object data)
    {
        Debug.Log($"{_debugStart}OnCancel: {data}");

        DeActivateMenu();
    }

    private void DeActivateMenu()
    {
        popupMenu.gameObject.SetActive(false);
        UIFocused.Raise(this, false);
    }

}

