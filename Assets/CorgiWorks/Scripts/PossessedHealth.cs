using System.Collections;
using System.Collections.Generic;
using MoreMountains.CorgiEngine;
using UnityEngine;
using UnityEngine.Events;

public class PossessedHealth : Health
{
    public UnityEvent ReadyToExorcism;

    public override void Damage(int damage, GameObject instigator, float flickerDuration, float invincibilityDuration,
        Vector3 damageDirection)
    {
        if (CurrentHealth > damage)
            base.Damage(damage, instigator, flickerDuration, invincibilityDuration, damageDirection);
        else
        {
            base.Damage(CurrentHealth - 1, instigator, flickerDuration, invincibilityDuration, damageDirection);
            Invulnerable = true;
            ReadyToExorcism?.Invoke();
        }
    }

    public void SendToHeaven()
    {
        Invulnerable = false;
        Kill();
    }

}