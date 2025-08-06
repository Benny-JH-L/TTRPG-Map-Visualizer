using UnityEngine;
using UnityEngine.Events;

[System.Serializable]   // need this or else we can't see it in the inspector
public class CustomGameEvent : UnityEvent<Component, object>
{

}

public class GameEventListener : MonoBehaviour
{

    public GameEvent gameEvent;
    public CustomGameEvent response;

    public void OnEventRaised(Component sender, object data)
    {
        response.Invoke(sender, data);
    }

    // when a GameEventListener is created, it will send its `gameEvent` to be registered 
    private void OnEnable()
    {
        gameEvent.RegisterListener(this);
    }

    private void OnDisable()
    {
        gameEvent.UnregisterListener(this);
    }
}
