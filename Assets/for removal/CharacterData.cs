
using UnityEngine;

// I wonder if i should just remove this class and use the `creaturesavedata` and if a thing doesn't
// use an atrribute it just wont show...
[System.Serializable]
public class CharacterData : CreatureData
{

    // more stuff

    Vector3 position;
    Vector3 rotation;   // use: <gameobject>.transform.eulerAngles to get angle in degrees
    Vector3 scale;

    public CharacterData() : base()
    {
        //className = ClassType.NA;

    }
    
    // constructor to create a save data from a JSON file.
    
    // outut to json

}
