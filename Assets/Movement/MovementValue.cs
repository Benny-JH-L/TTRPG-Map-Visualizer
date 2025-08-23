using UnityEngine;

/// <summary>
/// Class to determine/record movement done with respect to the compass directions
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

    public void MoveNorth()
    {
        _moveBy.z = _movementValue;
    }

    public void MoveSouth()
    {
        _moveBy.z = -_movementValue;
    }

    public void MoveWest()
    {
        _moveBy.x = -_movementValue;
    }

    public void MoveEast()
    {
        _moveBy.x = _movementValue;
    }

    public void MoveNorthEast()
    {
        MoveNorth();
        MoveEast();
    }

    public void MoveNorthWest()
    {
        MoveNorth();
        MoveWest();
    }

    public void MoveSouthEast()
    {
        MoveSouth();
        MoveEast();
    }

    public void MoveSouthWest()
    {
        MoveSouth();
        MoveWest();
    }

    /// <summary>
    /// returns the movement value, (Normalized) * movement factor
    /// </summary>
    /// <returns>Vector3 representation of the movement done</returns>
    public Vector3 GetMovement()
    {
        return (_moveBy.normalized) * _movementFactor;
    }
}

