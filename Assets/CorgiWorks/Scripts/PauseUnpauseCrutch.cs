using System.Collections;
using System.Collections.Generic;
using MoreMountains.CorgiEngine;
using MoreMountains.Tools;
using UnityEngine;

public class PauseUnpauseCrutch : MonoBehaviour, MMEventListener<CorgiEngineEvent>
{
    private void Update()
    {

    }

    private void OnEnable()
    {
        this.MMEventStartListening<CorgiEngineEvent>();
    }

    private void OnDisable()
    {
        this.MMEventStopListening<CorgiEngineEvent>();
    }

    public void OnMMEvent(CorgiEngineEvent engineEvent)
    {
        // switch (engineEvent.EventType)
        // {
        //     case CorgiEngineEventTypes.TogglePause:
        //         CorgiEngineEvent.Trigger(CorgiEngineEventTypes.Pause);
        //         break;
        //     case CorgiEngineEventTypes.UnPause:
        //         CorgiEngineEvent.Trigger(CorgiEngineEventTypes.Pause);
        //         break;
        // }
    }

    public void TriggerPause()
    {
        CorgiEngineEvent.Trigger(CorgiEngineEventTypes.TogglePause);
    }
}
