using System.Collections;
using System.Collections.Generic;
using MoreMountains.CorgiEngine;
using MoreMountains.Tools;
using UnityEngine;

public class AiDecisionChooseBestWeapon : AIDecision
{
    public float MeleeWeponRange;

    protected CharacterHandleWeapon _characterHandleWeapon;


    public override void Initialization()
    {
        base.Initialization();
        _characterHandleWeapon =
            this.gameObject.GetComponentInParent<Character>()?.FindAbility<CharacterHandleWeapon>();
    }

    public override bool Decide()
    {
        float distance = GetDistanceToTarget();

        if (distance <= MeleeWeponRange && !IsMeleeWeaponSelected())
            return true;
        if (distance > MeleeWeponRange && IsMeleeWeaponSelected())
            return true;
        return false;
    }

    protected bool IsMeleeWeaponSelected()
    {
        if (!_characterHandleWeapon.CurrentWeapon)
            return false;
        return _characterHandleWeapon.CurrentWeapon.TryGetComponent<MeleeWeapon>(out _);
    }

    protected virtual float GetDistanceToTarget()
    {
        if (_brain.Target == null)
            return float.MaxValue;

        return Vector3.Distance(this.transform.position, _brain.Target.position);
    }
}