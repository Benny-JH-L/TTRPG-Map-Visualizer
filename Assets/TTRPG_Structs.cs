
public struct ChangedObject
{
    public TTRPG_SceneObjectBase prevSelectedObj; // previously selected object
    public TTRPG_SceneObjectBase newSelectedObj;  // current/new selected object

    public ChangedObject(TTRPG_SceneObjectBase prevObj, TTRPG_SceneObjectBase newObj)
    {
        prevSelectedObj = prevObj;
        newSelectedObj = newObj;
    }

    public override string ToString()
    {
        return $"(prev selected obj: {(prevSelectedObj == null ? "<null>" : prevSelectedObj.name)}" +
            $" | new selected obj: {(newSelectedObj == null ? "<null>" : newSelectedObj.name)})";
    }
}

public struct ChangedTabButton
{
    public TabButton prevTabButton; // previously selected TabButton
    public TabButton newTabButton;  // newly selected TabButton

    public ChangedTabButton(TabButton prevButton, TabButton newButton)
    {
        prevTabButton = prevButton;
        newTabButton = newButton;
    }
}
