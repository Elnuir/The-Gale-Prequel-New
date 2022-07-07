using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CheatOnOff : CheatBase
{
    public UnityEvent On;
    public UnityEvent Off;
    
    protected override void ActivateCheat()
    {
        base.ActivateCheat();
        On?.Invoke();
    }

    protected override void DeactivateCheat()
    {
        base.DeactivateCheat();
        Off?.Invoke();
    }
}
