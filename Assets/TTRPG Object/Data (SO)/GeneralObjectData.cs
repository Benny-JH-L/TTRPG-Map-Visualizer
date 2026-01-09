using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GeneralObjectData", menuName = "Scriptable Objects/GeneralObjectData")]
public abstract class GeneralObjectData : ScriptableObject
{
    [SerializeField] public float diskBaseRadius = 13f;  // NEED TO guess and check (its about 12.45f to be touching)

    // use `.name` for the object's name
    public TTRPG_HP hp;
    public int AC;
}
