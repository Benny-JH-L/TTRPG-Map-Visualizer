using UnityEngine;

public class ErrorOutput
{
    public static void printError(object caller, string msg)
    {
        throw new System.Exception($"[{caller.GetType()}] {msg}");
    }
}
