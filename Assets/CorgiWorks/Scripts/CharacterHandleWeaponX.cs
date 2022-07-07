using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using MoreMountains.CorgiEngine;
using MoreMountains.Tools;
using UnityEngine;

public class CharacterHandleWeaponX : CharacterHandleWeapon
{
    private const float TimeSinceButtonDown = 0.3f;
    private float weaponUseTimestamp;
    public float Cooldown = 0;
    public CharacterStates.MovementStates[] Whitelist;

    protected override void HandleInput()
    {
        if ((_inputManager.ShootButton.State.CurrentState == MMInput.ButtonStates.ButtonUp) ||
            (_inputManager.ShootAxis == MMInput.ButtonStates.ButtonUp))
        {
            if (_inputManager.ShootButton.TimeSinceLastButtonDown < TimeSinceButtonDown)
                ShootStart();
        }

        if (CurrentWeapon != null)
        {
            if (ContinuousPress && (CurrentWeapon.TriggerMode == Weapon.TriggerModes.Auto) &&
                (_inputManager.ShootButton.State.CurrentState == MMInput.ButtonStates.ButtonPressed))
            {
                ShootStart();
            }

            if (ContinuousPress && (CurrentWeapon.TriggerMode == Weapon.TriggerModes.Auto) &&
                (_inputManager.ShootAxis == MMInput.ButtonStates.ButtonPressed))
            {
                ShootStart();
            }
        }

        if (_inputManager.ReloadButton.State.CurrentState == MMInput.ButtonStates.ButtonUp)
        {
            Reload();
        }

        if ((_inputManager.ShootButton.State.CurrentState == MMInput.ButtonStates.ButtonDown) ||
            (_inputManager.ShootAxis == MMInput.ButtonStates.ButtonDown))
        {
            ShootStop();
        }

        if (CurrentWeapon != null)
        {
            if ((CurrentWeapon.WeaponState.CurrentState == Weapon.WeaponStates.WeaponDelayBetweenUses)
                && ((_inputManager.ShootAxis == MMInput.ButtonStates.Off) &&
                    (_inputManager.ShootButton.State.CurrentState == MMInput.ButtonStates.Off)))
            {
                CurrentWeapon.WeaponInputStop();
            }
        }
    }

    public override void ShootStart()
    {
        if (Whitelist.Any(w => _movement.CurrentState == w))
            if (ActionEx.CheckCooldown(ShootStart, Cooldown))
                base.ShootStart();
    }

    protected void ShootStartNoCheck()
    {
        base.ShootStart();
    }
}