using System.Collections;
using System.Collections.Generic;
using MoreMountains.Tools;
using UnityEngine;

public class AiActionStun : AIAction
{
    private AiStunAbility _aiStun;

    public override void Initialization()
    {
        base.Initialization();
        _aiStun = _brain.GetComponentInParent<AiStunAbility>();

        if (!_aiStun)
            Debug.LogError($"Couldn't find {nameof(AiStunAbility)}");
    }

    // Start is called before the first frame update
    public override void PerformAction()
    {
    }

    public override void OnEnterState()
    {
        _aiStun.SetStunActive(true);
        base.OnEnterState();
    }

    public override void OnExitState()
    {
        _aiStun.SetStunActive(false);
        base.OnExitState();
    }
}