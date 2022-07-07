using System.Collections;
using System.Collections.Generic;
using MoreMountains.Tools;
using UnityEngine;
using UnityEngine.Events;

public class AiActionRagemod : AIAction
{
    private AiRagemodAbility _aiRagemod;
    public UnityEvent ActionStarted;

    public override void Initialization()
    {
        base.Initialization();
        _aiRagemod = _brain.GetComponentInParent<AiRagemodAbility>();
    }

    // Start is called before the first frame update
    public override void PerformAction()
    {
    }

    public override void OnEnterState()
    {
        ActionStarted?.Invoke();
        _aiRagemod.SetRageActive(true);
        base.OnEnterState();
    }

    public override void OnExitState()
    {
        _aiRagemod.SetRageActive(false);
        base.OnExitState();
    }
}