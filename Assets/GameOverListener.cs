using System.Collections;
using System.Collections.Generic;
using MoreMountains.CorgiEngine;
using MoreMountains.Tools;
using UnityEngine;
using UnityEngine.Events;

public class GameOverListener : MonoBehaviour, MMEventListener<CorgiEngineEvent>
{
    public UnityEvent Triggered;

    void OnEnable()
    {
        this.MMEventStartListening();
    }

    void OnDisable()
    {
        this.MMEventStopListening();
    }

    public void OnMMEvent(CorgiEngineEvent eventType)
    {
        if (eventType.EventType == CorgiEngineEventTypes.GameOver)
            Triggered?.Invoke();
    }
}