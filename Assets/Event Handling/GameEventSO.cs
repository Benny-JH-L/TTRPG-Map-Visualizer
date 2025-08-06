using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "GameEvent", menuName = "Scriptable Objects/GameEvent")]
public class GameEvent : ScriptableObject
{
    public List<GameEventListener> listenerList = new List<GameEventListener>();

    public void Raise(Component sender, object data)
    {
        for (int i = 0; i < listenerList.Count; i++)
        {
            listenerList[i].OnEventRaised(sender, data);
        }
    }

    public void RegisterListener(GameEventListener gameEventListener)
    {
        if (!listenerList.Contains(gameEventListener))
        {
            listenerList.Add(gameEventListener);
        }
    }

    public void UnregisterListener(GameEventListener gameEventListener)
    {
        if (listenerList.Contains(gameEventListener))
        {
            listenerList.Remove(gameEventListener);
        }
    }
}

