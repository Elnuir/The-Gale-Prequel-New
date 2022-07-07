using System.Collections;
using System.Collections.Generic;
using MoreMountains.CorgiEngine;
using UnityEngine;

public class FallingDamage : MonoBehaviour
{
    private CorgiController _stats;
    private Rigidbody2D _physics;
    private Health _pstats;

    public float MinFallingVelocity;
    public float MaxFallingVelocity;

    [Space]
    public float MinDamage = 100;
    public float MaxDamage = 10;

    private float _prevVelocity;


    void FixedUpdate()
    {
        _stats = GetComponent<CorgiController>();
        _pstats = GetComponent<Health>();
        _physics = GetComponent<Rigidbody2D>();

        if (IsDamageRequired())
            DoDamage();

        _prevVelocity = -_stats.Speed.y;
    }

    bool IsDamageRequired()
    {
        return enabled && _stats.State.IsGrounded && _prevVelocity >= MinFallingVelocity;
    }

    void DoDamage()
    {
        _pstats.Damage((int)Mathf.Round(GetDamageAmount()), this.gameObject, 0.5f, 0.2f, Vector3.zero);
    }

    float GetDamageAmount()
    {
        if (_prevVelocity < MinFallingVelocity)
            return 0;

        if (_prevVelocity > MaxFallingVelocity)
            return MaxDamage;

        var percent = (_prevVelocity - MinFallingVelocity) / (MaxFallingVelocity - MinFallingVelocity);
        return MinDamage + percent * (MaxDamage - MinDamage);
    }
}