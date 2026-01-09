using System;
using UnityEngine;
using UnityEngine.InputSystem;

[System.Serializable]
public class ObjectMovementManager : MonoBehaviour
{
    //GameEventListener<SelectedObject>;

    private static string _debugStart = "Object Movement Manager | ";
    
    //public GameEventSO objectMovedEvent;

    public float movementFactor = 30f;


    //[SerializeField] private bool _isUIFocused;
    //[SerializeField] private bool _isGameScreenFocused;

    // Smooth movement fields
    private Rigidbody _selectedRigidbody;
    private Vector3 _targetPosition;
    private Vector3 _smoothVelocity = Vector3.zero;
    public float smoothTime = 0.5f; // Adjust for smoothness (lower = snappier, higher = smoother)

    // Mouse movement fields
    private bool _isMovingToMouseTarget;
    private Vector3 _mouseTargetPosition;
    public float stopDistance = 0.5f; // Distance at which object stops at target

    public UtilityStorage utilStorage;
    [SerializeField] private MouseTracker mouseTracker;
    [SerializeField] private CameraManager cameraManager;
    [SerializeField] private TTRPG_SceneObjectBase _selectedGameObject;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _selectedGameObject = null;
        //_isUIFocused = false;
        //_isGameScreenFocused = false;
        _isMovingToMouseTarget = false;

        //if (mouseTracker == null)
        //    ErrorOutput.printError(this, "null mouse tracker!");

        utilStorage.CheckContents();
        mouseTracker = utilStorage.mouseTracker;
        cameraManager = utilStorage.cameraManager;
    }

    // Update is called once per frame
    void Update()
    {
        // should not be able to move object if user is interacting with (over the) UI
        if (mouseTracker.IsMouseOverUIElement())
        {
            //DebugPrinter.printMessage(this, "mouse over UI");
            return;
        }

        //// should not be able to move object if; interacting with the UI 
        //if (_isUIFocused)
        //    return;
        // can't move an object if no object is selected
        else if (_selectedGameObject == null)
            return;

        // Check for right mouse click (as long as mouse is focused on the game screen)
        if (Mouse.current.rightButton.wasPressedThisFrame) // && _isGameScreenFocused)
        {
            HandleMouseClick();
        }

        // Check for keyboard input
        bool keyboardInput = false;
        MovementValue movement = new(movementFactor);

        // Check if any of the movement keys are being held (Arrow keys)
        if (Keyboard.current.upArrowKey.isPressed)
        {
            movement.MoveNorth();
            keyboardInput = true;
        }
        else if (Keyboard.current.downArrowKey.isPressed)
        {
            movement.MoveSouth();
            keyboardInput = true;
        }
        if (Keyboard.current.leftArrowKey.isPressed)
        {
            movement.MoveWest();
            keyboardInput = true;
        }
        else if (Keyboard.current.rightArrowKey.isPressed)
        {
            movement.MoveEast();
            keyboardInput = true;
        }

        // If keyboard input detected, cancel mouse movement and use keyboard
        if (keyboardInput)
        {
            _isMovingToMouseTarget = false;

            Vector3 moveDirection = movement.GetMovement();

            // Rotate movement based on the orbit camera's rotation about the Y-axis
            if (cameraManager.GetCurrentCamera() is OrbitCam orbitCam)
            {
                // Get camera's forward and right directions, flattened on the horizontal plane
                Vector3 forward = orbitCam.GetCamera().transform.forward;
                forward.y = 0;
                Vector3 right = orbitCam.GetCamera().transform.right;
                right.y = 0;

                // Rotate the movement vector based on camera orientation
                if (forward.sqrMagnitude > 0.001f && right.sqrMagnitude > 0.001f)
                {
                    forward.Normalize();
                    right.Normalize();

                    // Rotate the movement vector based on camera orientation
                    moveDirection = (right * movement.GetMovement().x) + (forward * movement.GetMovement().z);
                }
                // angled 90 or -90 degrees resulting in no movement (ie facing directly up/down)
                else
                    moveDirection = movement.GetMovement(); // set to default movement
            }

            // Calculate target position for keyboard movement
            _targetPosition = _selectedGameObject.transform.position + moveDirection * Time.deltaTime;
        }
        // If moving to mouse target and close enough, stop
        else if (_isMovingToMouseTarget)
        {
            float distance = Vector3.Distance(_selectedGameObject.transform.position, _mouseTargetPosition);
            if (distance <= stopDistance)
            {
                _isMovingToMouseTarget = false;
                _targetPosition = _selectedGameObject.transform.position; // Stop moving
            }
            else
            {
                _targetPosition = _mouseTargetPosition;
            }
        }
    }

    /// <summary>
    /// Where smooth movement occurs.
    /// </summary>
    private void FixedUpdate()
    {
        if (_selectedGameObject == null)
            return;

        // Handle Rigidbody movement
        if (_selectedRigidbody != null)
        {
            // Smooth movement using SmoothDamp
            Vector3 newPosition = Vector3.SmoothDamp(
                _selectedRigidbody.position,
                _targetPosition,
                ref _smoothVelocity,
                smoothTime
            );

            _selectedRigidbody.MovePosition(newPosition);

            // Raise event with new position
            //Tuple<GameObject, Vector3> send = new Tuple<GameObject, Vector3>(_selectedGameObject, newPosition);
            //objectMovedEvent.Raise(this, send);
        }
        // Fallback for non-rigidbody objects
        else
        {
            Vector3 newPosition = Vector3.SmoothDamp(
                _selectedGameObject.transform.position,
                _targetPosition,
                ref _smoothVelocity,
                smoothTime
            );

            _selectedGameObject.transform.position = newPosition;

            //Tuple<GameObject, Vector3> send = new Tuple<GameObject, Vector3>(_selectedGameObject, newPosition);
            //objectMovedEvent.Raise(this, send);
        }

        // old
        //if (_selectedGameObject == null || _selectedRigidbody == null)
        //    return;

        //// Smooth movement using SmoothDamp
        //Vector3 newPosition = Vector3.SmoothDamp(
        //    _selectedRigidbody.position,
        //    _targetPosition,
        //    ref _smoothVelocity,
        //    smoothTime
        //);

        //_selectedRigidbody.MovePosition(newPosition);

        //// Raise event with new position
        //Tuple<GameObject, Vector3> send = new Tuple<GameObject, Vector3>(_selectedGameObject, newPosition);
        //objectMovedEvent.Raise(this, send);
    }

    /// <summary>
    /// handles right mouse click for moving the selected object.
    /// </summary>
    void HandleMouseClick()
    {
        Vector3 mouseWorldPos = cameraManager.GetMousePosInWorld();
        _mouseTargetPosition = mouseWorldPos;
        // Keep the object's current Y position (or use mouseWorldPos.y if you want it to match ground)
        //_mouseTargetPosition.y = _selectedGameObject.transform.position.y;    // match with current object's y position

        _isMovingToMouseTarget = true;
        _targetPosition = _mouseTargetPosition;
    }

    // to be removed/deleted
    //private void OLD_UPDATE()
    //{
    //    // In your Update method, replace the movement code with:
    //    // should not be able to move object if; interacting with the UI 
    //    if (_isUIFocused)
    //        return;
    //    // can't move an object if no object is selected
    //    else if (_selectedGameObject == null)
    //        return;

    //    //Vector3 moveBy = new(0f, 0f, 0f);
    //    MovementValue movement = new MovementValue(movementFactor);
    //    // Check if any of the movement keys are being held (Arrow keys)
    //    if (Keyboard.current.upArrowKey.isPressed)
    //    {
    //        movement.MoveNorth();
    //    }
    //    else if (Keyboard.current.downArrowKey.isPressed)
    //    {
    //        movement.MoveSouth();
    //    }
    //    if (Keyboard.current.leftArrowKey.isPressed)
    //    {
    //        movement.MoveWest();
    //    }
    //    else if (Keyboard.current.rightArrowKey.isPressed)
    //    {
    //        movement.MoveEast();
    //    }
    //    // Check if the game object was even moved
    //    if (movement.GetMovement().magnitude == 0)
    //        return;

    //    // Do movement
    //    Vector3 moveDirection = movement.GetMovement();

    //    // Rotate movement based on the orbit camera's rotation about the Y-axis
    //    if (cameraManager.GetCurrentCamera() is OrbitCam orbitCam)
    //    {
    //        // Get camera's forward and right directions, flattened on the horizontal plane
    //        Vector3 forward = orbitCam.GetCamera().transform.forward;
    //        forward.y = 0;
    //        forward.Normalize();
    //        Vector3 right = orbitCam.GetCamera().transform.right;
    //        right.y = 0;
    //        right.Normalize();
    //        // Rotate the movement vector based on camera orientation
    //        moveDirection = (right * movement.GetMovement().x) + (forward * movement.GetMovement().z);
    //    }

    //    // Calculate target position
    //    _targetPosition = _selectedGameObject.transform.position + moveDirection * Time.deltaTime;

    //    // initial
    //    //// should not be able to move object if; interacting with the UI 
    //    //if (_isUIFocused)
    //    //    return;
    //    //// can't move an object if no object is selected
    //    //else if (_selectedGameObject == null)
    //    //    return;

    //    ////Vector3 moveBy = new(0f, 0f, 0f);
    //    //MovementValue movement = new MovementValue(movementFactor);

    //    //// Check if any of the movement keys are being held (Arrow keys)
    //    //if (Keyboard.current.upArrowKey.isPressed)
    //    //{
    //    //    movement.MoveNorth();
    //    //}
    //    //else if (Keyboard.current.downArrowKey.isPressed)
    //    //{
    //    //    movement.MoveSouth();
    //    //}

    //    //if (Keyboard.current.leftArrowKey.isPressed)
    //    //{
    //    //    movement.MoveWest();
    //    //}
    //    //else if (Keyboard.current.rightArrowKey.isPressed)
    //    //{
    //    //    movement.MoveEast();
    //    //}

    //    //// Check if the game object was even moved
    //    //if (movement.GetMovement().magnitude == 0)
    //    //    return;

    //    //// Do movement
    //    ////Vector3 newPos = _selectedGameObject.transform.position + movement.GetMovement() * Time.deltaTime;

    //    //Vector3 moveDirection = movement.GetMovement();

    //    //// Rotate movement based on the orbit camera's rotation about the Y-axis
    //    //if (cameraManager.GetCurrentCamera() is OrbitCam orbitCam)
    //    //{
    //    //    // Get camera's forward and right directions, flattened on the horizontal plane
    //    //    Vector3 forward = orbitCam.GetCamera().transform.forward;
    //    //    forward.y = 0;
    //    //    forward.Normalize();

    //    //    Vector3 right = orbitCam.GetCamera().transform.right;
    //    //    right.y = 0;
    //    //    right.Normalize();

    //    //    // Rotate the movement vector based on camera orientation
    //    //    moveDirection = (right * movement.GetMovement().x) + (forward * movement.GetMovement().z);
    //    //}

    //    //Vector3 newPos = _selectedGameObject.transform.position + moveDirection * Time.deltaTime;
    //    //_selectedGameObject.transform.position = newPos;
    //    //Tuple<GameObject, Vector3> send = new Tuple<GameObject, Vector3>(gameObject, newPos);
    //    //objectMovedEvent.Raise(this, send);
    //}
    
    /// <summary>
    /// Sets the selected game object for movement.
    /// </summary>
    /// <param name="comp"></param>
    /// <param name="data"></param>
    public void OnSelectedObject(Component comp, object data)
    {
        Debug.Log(_debugStart + " Selected Object Event");

        if (data is TTRPG_SceneObjectBase)
        {
            Debug.Log(_debugStart + "Setting selected object");

            _selectedGameObject = ((TTRPG_SceneObjectBase)data);
            _selectedRigidbody = _selectedGameObject.GetComponent<Rigidbody>();

            // Initialize target position
            if (_selectedRigidbody != null)
            {
                _targetPosition = _selectedRigidbody.position;
            }
            else
            {
                _targetPosition = _selectedGameObject.transform.position;
            }

            _smoothVelocity = Vector3.zero;
            _isMovingToMouseTarget = false;
        }
    }

    /// <summary>
    /// Deselect's game object for movement.
    /// </summary>
    /// <param name="comp"></param>
    /// <param name="data"></param>
    public void OnDeselectObject(Component comp, object data)
    {
        Debug.Log(_debugStart + "deselected Object Event");
        _selectedGameObject = null;
        _selectedRigidbody = null;
        _smoothVelocity = Vector3.zero;
        _isMovingToMouseTarget = false;
    }

    //public void OnUIFocued(Component comp, object data)
    //{
    //    if (data is bool r)
    //        _isUIFocused = r;
    //}

    //public void OnGameScreenFocused(Component comp, object data)
    //{
    //    if (data is bool r)
    //        _isGameScreenFocused = r;
    //}

}
