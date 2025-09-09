using UnityEngine;

public class InanimateObject : GeneralObject
{
    public static InanimateObject Create(Vector3 pos)
    {
        GeneralObjectData generalObjectData = new();
        return Create(pos, generalObjectData);
    }

    public static InanimateObject Create(Vector3 pos, GeneralObjectData saveData)
    {
        Debug.Log("Creating Inanimate obj...");
        GameObject obj = CreateGameObject(pos);
        InanimateObject generalObject = obj.AddComponent<InanimateObject>();
        generalObject.Init(obj, saveData);
        return generalObject;
    }
}
