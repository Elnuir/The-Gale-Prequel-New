using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class SpinningEntityState : AnimatedEntityState
{
    public float MinInterval;
    public float MaxInterval;
    public EntityHealthX Health;
    public float BlockAttackProbability;

    private float interval;
    private float _initBlockAttackProbability;


    protected override void Start()
    {
        base.Start();

        Health ??= GetComponent<EntityHealthX>();
        _initBlockAttackProbability = Health.BlockAttackProbability;
    }

    protected override void Update()
    {
        base.Update();
        interval -= Time.deltaTime;

        if (interval <= 0)
        {
            MakeAvailable();
            interval = GetRandomInterval();
        }
    }

    public override void ActivateState()
    {
        base.ActivateState();
        Health.BlockAttackProbability = BlockAttackProbability;
    }

    public override void DeactivateState()
    {
        base.DeactivateState();
        Health.BlockAttackProbability = _initBlockAttackProbability;
    }

    private float GetRandomInterval()
    {
        return Random.Range(MinInterval, MaxInterval);
    }
}