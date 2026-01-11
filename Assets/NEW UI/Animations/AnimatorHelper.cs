using UnityEngine;

[RequireComponent (typeof(Animator))]
public class AnimatorHelper : MonoBehaviour
{
    [SerializeField] private Animator animator;     // animations for what ever 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        string msg = "Parameters:\n";
        foreach (var param in animator.parameters)
        {
            msg += $"{param.type} : '{param.name}'\n";
        }

        if (animator.parameterCount == 0)
            msg += "<empty param list>";
        DebugOut.Log(this, msg);
    }

    /// <summary>
    /// Calls `animator.SetTrigger(...)` with `triggerName` parameter.
    /// </summary>
    /// <param name="name"></param>
    public void CheckAnimationTrigger(string triggerName)
    {
        animator.SetTrigger(triggerName);   
    }
}
