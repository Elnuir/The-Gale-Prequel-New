using System.Collections;
using System.Collections.Generic;
using MoreMountains.CorgiEngine;
using MoreMountains.Feedbacks;
using UnityEngine;

public class HealthX : Health
{
    public MMFeedbacks NoDamageHitFeedbacks;
    public int ArmorAmount;

    new void Start()
    {
        base.Start();
        OnHitZero += HandleHit;
    }

    public override void Damage(int damage, GameObject instigator, float flickerDuration, float invincibilityDuration, Vector3 damageDirection)
    {
        if (ArmorAmount >= damage)
        {
            OnHitZero?.Invoke();
            return;
        }
        else
            base.Damage(damage - ArmorAmount, instigator, flickerDuration, invincibilityDuration, damageDirection);
    }

    private void HandleHit()
    {
        if (!NoDamageHitFeedbacks) return;
        if (ArmorAmount != 0)
        {
            if (ActionEx.CheckCooldown(HandleHit, 0.3f))
            {
                NoDamageHitFeedbacks.PlayFeedbacks(transform.position);
            }
        }
    }
}