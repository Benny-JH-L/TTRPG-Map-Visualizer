using UnityEngine;

public class AnimationStateRightUI : MonoBehaviour  // need to deprecate this!
{
    public GameEventSO onRightUIClosedAnimationFinished;

    public void OnClosed()
    {
        onRightUIClosedAnimationFinished.Raise(this, true);
        Debug.Log("Right UI finished `closed` animation");
    }
}
