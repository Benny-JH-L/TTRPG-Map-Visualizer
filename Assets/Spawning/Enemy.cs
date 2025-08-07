
using UnityEngine;

[System.Serializable]

public class Enemy : Creature
{
    public Enemy(Vector3 pos) : base(CreatureType.Enemy, pos)
    { }
}
