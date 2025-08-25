using UnityEngine;

/// <summary>
/// Class to determine/record movement done with respect to the compass cardinal directions and derivatives.
/// </summary>
public class MovementValue
{
    private Vector3 _moveBy;
    private float _movementValue = 1f;      // can be any number and it wont affect distance moved, as we are normalizing it {if it's 1 or 1000 they will move the same distance}
    private float _movementFactor;          // applied to movementValue

    public MovementValue(float movementFactor)
    {
        this._movementFactor = movementFactor;
    }

    /// <summary>
    /// North movement (+z)
    /// </summary>
    public void MoveNorth()
    {
        _moveBy.z = _movementValue;
    }

    /// <summary>
    /// South movement (-z)
    /// </summary>
    public void MoveSouth()
    {
        _moveBy.z = -_movementValue;
    }

    /// <summary>
    /// West movement (-x)
    /// </summary>
    public void MoveWest()
    {
        _moveBy.x = -_movementValue;
    }

    /// <summary>
    /// East movement (+x)
    /// </summary>
    public void MoveEast()
    {
        _moveBy.x = _movementValue;
    }

    /// <summary>
    /// North East movement (+x, +z)
    /// </summary>
    public void MoveNorthEast()
    {
        MoveNorth();
        MoveEast();
    }

    /// <summary>
    /// North West movement (-x, +z)
    /// </summary>
    public void MoveNorthWest()
    {
        MoveNorth();
        MoveWest();
    }

    /// <summary>
    /// South East movement (+x, -z)
    /// </summary>
    public void MoveSouthEast()
    {
        MoveSouth();
        MoveEast();
    }

    /// <summary>
    /// South West movement (-x, -z)
    /// </summary>
    public void MoveSouthWest()
    {
        MoveSouth();
        MoveWest();
    }

    /// <summary>
    /// returns the movement value; Vector3.Normalized * movement factor
    /// </summary>
    /// <returns>Vector3 representation of the movement done</returns>
    public Vector3 GetMovement()
    {
        return (_moveBy.normalized) * _movementFactor;
    }
}

