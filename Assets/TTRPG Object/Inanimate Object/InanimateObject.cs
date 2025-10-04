using UnityEngine;

public class InanimateObject : GeneralObject
{
    private static string _debugStart = "InanimateObject | ";
    public static InanimateObject Create(Vector3 pos)
    {
        GeneralObjectData generalObjectData = new();
        return Create(pos, generalObjectData);
    }

    public static InanimateObject Create(Vector3 pos, GeneralObjectData saveData)
    {
        if (!IsPositionSpawnable(pos))
        {
            Debug.Log($"{_debugStart}Can't spawn, intersects with a GeneralObject");
            return null;
        }

        GameObject obj = CreateGameObject(pos);
        InanimateObject generalObject = obj.AddComponent<InanimateObject>();
        generalObject.Init(obj, saveData);

        Debug.Log($"{_debugStart}Inanimate obj created...");

        return generalObject;
    }
}
