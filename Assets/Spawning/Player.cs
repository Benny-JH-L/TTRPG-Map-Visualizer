using UnityEngine;

[System.Serializable]

public class Player : Creature
{
    public Player(Vector3 pos) : base(CreatureType.Player, pos)
    {
    }
}
