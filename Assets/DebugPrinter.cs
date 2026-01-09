using UnityEngine;

public static class DebugPrinter
{
    public static void printMessage(object obj, string message)
    {
        Debug.Log($"[{obj.GetType()}] {message}");
    }

}

