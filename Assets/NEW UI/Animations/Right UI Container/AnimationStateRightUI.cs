using UnityEngine;

public class AnimationStateRightUI : MonoBehaviour
{
    public GameEventSO onRightUIClosedAnimationFinished;

    public void OnClosed()
    {
        onRightUIClosedAnimationFinished.Raise(this, true);
        Debug.Log("Right UI finished `closed` animation");
    }
}
