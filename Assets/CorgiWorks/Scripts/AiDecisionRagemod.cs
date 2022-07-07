using System;
using System.Collections;
using System.Collections.Generic;
using MoreMountains.CorgiEngine;
using MoreMountains.Tools;
using UnityEngine;

public class AiDecisionRagemod : AIDecision
{
    public float Interval = 7;
    public float InitInterval;

    private float _currInterval;

    public override void Initialization()
    {
        base.Initialization();
        _currInterval = InitInterval;
    }

    private void Update()
    {
        if (_currInterval > 0)
            _currInterval -= Time.deltaTime;
        else
            _currInterval = Interval;
    }

    public override bool Decide()
    {
        return _currInterval <= Time.deltaTime;
    }
}