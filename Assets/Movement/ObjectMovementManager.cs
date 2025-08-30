using System;
using UnityEngine;
using UnityEngine.InputSystem;

[System.Serializable]
public class ObjectMovementManager : MonoBehaviour
{
    //GameEventListener<SelectedObject>;
    
    public GameEvent objectMovedEvent;

    private GameObject _selectedGameObject;

    private string _debugStart = "Object Movement Manager | ";

    public float movementFactor = 0.2f;


    //// Start is called once before the first execution of Update after the MonoBehaviour is created
    //void Start()
    //{

    //}


    // Update is called once per frame
    void Update()
    {
        if (_selectedGameObject == null)
            return;

        //Vector3 moveBy = new(0f, 0f, 0f);
        MovementValue movement = new MovementValue(movementFactor);

        // Check if any of the movement keys are being held (Arrow keys)
        if (Keyboard.current.upArrowKey.isPressed)
        {
            movement.MoveNorth();
        }
        else if (Keyboard.current.downArrowKey.isPressed)
        {
            movement.MoveSouth();
        }

        if (Keyboard.current.leftArrowKey.isPressed)
        {
            movement.MoveWest();
        }
        else if (Keyboard.current.rightArrowKey.isPressed)
        {
            movement.MoveEast();
        }

        // Check if the game object was even moved
        if (movement.GetMovement().magnitude == 0)
            return;

        // Do movement
        Vector3 newPos = _selectedGameObject.transform.position + movement.GetMovement();
        _selectedGameObject.transform.position = newPos;
        //Debug.Log("ewikjgbwebgwe");
        Tuple<GameObject, Vector3> send = new Tuple<GameObject, Vector3>(gameObject, newPos);
        //Debug.Log("ewikjgbwebgwe123");
        objectMovedEvent.Raise(this, send);
        //Debug.Log("ewikjgbwebgwe2141");

    }

    /// <summary>
    /// Sets the selected game object for movement.
    /// </summary>
    /// <param name="comp"></param>
    /// <param name="data"></param>
    public void OnSelectedObject(Component comp, object data)
    {
        Debug.Log(_debugStart + " Selected Object Event");

        if (data is Creature) // creatrure, player, etc.
        {
            Debug.Log(_debugStart + "Setting selected object");

            // could use either it seems
            //_selectedGameObject = ((Creature)data).creatureDisk;
            _selectedGameObject = ((Creature)data).gameObject;
        }
    }

    /// <summary>
    /// Deselectes game object for movement.
    /// </summary>
    /// <param name="comp"></param>
    /// <param name="data"></param>
    public void OnDeselectObject(Component comp, object data)
    {
        Debug.Log(_debugStart + "deselected Object Event");
        _selectedGameObject = null;
    }
}
