
using UnityEngine;

[System.Serializable]
public class CreatureSaveData
{
    public CreatureType creatureType;
    public string className;
    public int ac;
    public int speed;
    public int hp;

    // more stuff

    Vector3 position;
    Vector3 rotation;   // use: <gameobject>.transform.eulerAngles to get angle in degrees
    Vector3 scale;

    public CreatureSaveData()
    {
        className = ""; // use a param to decide class, ex: {1: wizard, 2:monk, etc.}
        ac = 0;
        speed = 0;
        hp = 0;
    }
}
