using System;
using UnityEngine;
using UnityEngine.Events;

public class MeleeAttackState : AnimatedEntityState
{
    public float Damage;
    public float Range;
    public float Cooldown;
    public bool InitFacingRight;

    public UnityEvent OnDeactivate;
    public TargetProviderBase TargetProvider;
    private bool IsFacingRight => InitFacingRight && transform.right.x > 0 || !InitFacingRight && transform.right.x < 0;

    public override bool IsAvailable => GetIsAvailable();

    private bool GetIsAvailable()
    {
        if (!enabled) return false;
        var target = TargetProvider.GetTarget();

        if (target == null) return false;
        if (Vector2.Distance(target.position, transform.position) > Range) return false;

        return ActionEx.CheckCooldown((Func<bool>)GetIsAvailable, Cooldown);
    }

    public void FlipFix(Transform target)
    {
        if (target == null) return;
        if (IsFacingRight && target.position.x < transform.position.x)
            transform.Rotate(0, 180, 0);

        if (!IsFacingRight && target.position.x > transform.position.x)
            transform.Rotate(0, 180, 0);
    }

    protected override void Update()
    {
        base.Update();
        if (IsActive && ActionEx.CheckCooldown(Update, 0.2f))
            FlipFix(TargetProvider.GetTarget());
    }

    public virtual void MeleeAttackAction()
    {
        if (TargetProvider == default) return;

        // var attackDetails = new float[2];
        // attackDetails[0] = Damage;
        // attackDetails[1] = transform.position.x;
        var attackDetails = new AttackDetails()
        {
            Attacker = transform,
            attackerX = transform.position.x,
            damageAmount = Damage
        };
        TargetProvider.GetTarget().SendMessage("NewDamage", attackDetails);
    }

    public override void DeactivateState()
    {
        base.DeactivateState();
        OnDeactivate?.Invoke();
    }
}