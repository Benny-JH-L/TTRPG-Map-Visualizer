
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[System.Serializable]
public class MouseTracker : MonoBehaviour
{
    public static GameObject screenSpaceGameObject; // UI component that occupies the screen space of the active game.
    public List<GameObject> gameObjectsOverScreenSpace;  // UI components that occupy the screen space over the `screenSpaceGameObject`
    private static string _debugStart = "MouseTracker | ";

    public GameEventSO gameScreenFocused;         // True: mouse is inside GameScreenSpace, False otherwise. 
    private bool _mouseInsideGameScreenSpace;   // Used so `gameScreenFocused` isn't called every `Update()`

    void Start()
    {
        _mouseInsideGameScreenSpace = false;
    }

    void Update()
    {
        // NOTE: funny result but if i tap on a input field and then move mouse over to game screen space i can type and move objects lol

        bool result = IsMousePositionInsideCameraRect();    

        if (result && !_mouseInsideGameScreenSpace)         // mouse moved inside GameScreenSpace
        {
            gameScreenFocused.Raise(this, true);
            _mouseInsideGameScreenSpace = true;
            Debug.Log($"{_debugStart}gameScreenFocused TRUE");
        }
        else if (!result && _mouseInsideGameScreenSpace)    // mouse moved outside of GameScreenSpace
        {
            gameScreenFocused.Raise(this, false);
            _mouseInsideGameScreenSpace = false;
            Debug.Log($"{_debugStart}gameScreenFocused FALSE");
        }
    }

    /// <summary>
    /// Checks if the mouse is over a certain portion of the screen which is defined by the UI GameObject `screenSpaceGameObject`s RectTransform.
    /// If there is a UI component that is over top the space defined by `screenSpaceGameObject` (and is active) then we do not consider the mouse position
    /// over top of `screenSpaceGameObject`.
    /// </summary>
    /// <returns>True if the mouse is over the specific portion of the screen, False otherwise.</returns>
    private bool IsMousePositionInsideCameraRect()
    {
        foreach (GameObject go in gameObjectsOverScreenSpace)
        {
            // if the mouse position is over a UI component that is on top of the screen space, then we do not consider it inside the Camera Rect
            if (go.activeSelf && RectTransformUtility.RectangleContainsScreenPoint(go.GetComponent<RectTransform>(), GetMousePos(), null))
                return false;
        }
        return RectTransformUtility.RectangleContainsScreenPoint(screenSpaceGameObject.GetComponent<RectTransform>(), GetMousePos(), null); // use `null` as camera since `screenSpaceGameObject` (the canvas) is a Screen Space - Overlay
    }

    /// <summary>
    /// Get's the Mouse position with `Mouse.current.position.ReadValue();`
    /// </summary>
    /// <returns>A Vector2 of the mouse position.</returns>
    public static Vector2 GetMousePos()
    {
        return Mouse.current.position.ReadValue();
    }

}
