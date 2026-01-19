using UnityEngine;

public static class DebugOut
{

    /// <summary>
    /// Calls the Unity `Debug.Log(...)` and structures the debug message.
    /// </summary>
    /// <param name="caller"></param>
    /// <param name="message"></param>
    /// <param name="disabled">True, logs the message, otherwise no log.</param>
    /// 
    public static void Log(object caller, string message, bool disabled)
    {
        if (!disabled)
            Debug.Log($"[{caller.GetType()}] {message}");
    }

}

