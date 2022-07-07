using System.Collections;
using System.Collections.Generic;
using MoreMountains.CorgiEngine;
using MoreMountains.Tools;
using UnityEngine;

public class ChangeWeaponDecision : AIDecisionDistanceToTarget
{
    public Weapon TargetWeapon;

    private CharacterHandleWeapon _handleWeapon;

    public override void Initialization()
    {
        base.Initialization();
        _handleWeapon = GetComponentInParent<CharacterHandleWeapon>();
    }

    public override bool Decide()
    {
        bool b1 = base.Decide();
        bool b2 = !_handleWeapon.CurrentWeapon.name.Contains(TargetWeapon.name);
        return b1  && b2;
    }
}