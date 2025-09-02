using UnityEngine;

public abstract class GeneralObject : MonoBehaviour
{
    public static GameData gameData;
    public static GameObject diskPrefab;

    public static GameEvent spawnedObjectEvent; // do i need?

    public static Vector3 yOffsetDiskSpawn = new(0f, 0.5f, 0f);

    public GeneralObjectData saveData;  // will make it protected, public is to test/debug [in Unity editor, it will show as Creature save data for all sub classes]
    // disk and disk on model will be moved here
    public string objectName; //-???


}
