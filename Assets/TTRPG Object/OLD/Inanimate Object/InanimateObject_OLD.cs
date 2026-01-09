//using UnityEngine;

//public class InanimateObject_OLD : GeneralObject_OLD
//{
//    private new static string _debugStart = "InanimateObject | ";
//    public static InanimateObject_OLD Create(Vector3 pos)
//    {
//        GeneralObjectData_OLD generalObjectData = new();
//        return Create(pos, generalObjectData);
//    }

//    public static InanimateObject_OLD Create(Vector3 pos, GeneralObjectData_OLD saveData)
//    {
//        if (!IsPositionSpawnable(pos))
//        {
//            Debug.Log($"{_debugStart}Can't spawn, intersects with a GeneralObject");
//            return null;
//        }

//        GameObject obj = CreateGameObject(pos);
//        InanimateObject_OLD generalObject = obj.AddComponent<InanimateObject_OLD>();
//        generalObject.Init(obj, saveData);

//        Debug.Log($"{_debugStart}Inanimate obj created...");

//        return generalObject;
//    }
//}
