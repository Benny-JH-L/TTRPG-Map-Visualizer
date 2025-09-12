using System;
using UnityEngine;
using UnityEngine.InputSystem;

[System.Serializable]
public class ObjectMovementManager : MonoBehaviour
{
    //GameEventListener<SelectedObject>;
    
    public GameEvent objectMovedEvent;

    private GameObject _selectedGameObject;

    public float movementFactor = 0.2f;

    private string _debugStart = "Object Movement Manager | ";
    private bool _isUIFocused;
    private bool _isGameScreenFocused;  // most likely do not need anymore


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _isUIFocused = false;
    }


    // Update is called once per frame
    void Update()
    {
        if (_isUIFocused)  // should not be able to move object if; interacting with the UI 
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
        Tuple<GameObject, Vector3> send = new Tuple<GameObject, Vector3>(gameObject, newPos);
        objectMovedEvent.Raise(this, send);

    }

    /// <summary>
    /// Sets the selected game object for movement.
    /// </summary>
    /// <param name="comp"></param>
    /// <param name="data"></param>
    public void OnSelectedObject(Component comp, object data)
    {
        Debug.Log(_debugStart + " Selected Object Event");

        if (data is GeneralObject)
        {
            Debug.Log(_debugStart + "Setting selected object");

            // could use either it seems
            //_selectedGameObject = ((Creature)data).creatureDisk;
            _selectedGameObject = ((GeneralObject)data).gameObject;
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

    public void OnUIFocued(Component comp, object data)
    {
        if (data is bool r)
            _isUIFocused = r;
    }

    public void OnGameScreenFocused(Component comp, object data)    // most likely do not need anymore
    {
        if (data is bool r)
            _isGameScreenFocused = r;
    }

}
