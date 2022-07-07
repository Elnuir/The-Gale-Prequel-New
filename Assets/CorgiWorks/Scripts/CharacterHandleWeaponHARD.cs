using System.Collections;
using System.Collections.Generic;
using System.Linq;
using MoreMountains.CorgiEngine;
using MoreMountains.Tools;
using UnityEngine;

public class CharacterHandleWeaponHARD : CharacterHandleWeaponX
{
    private const float TimeSinceButtonDown = 0.3f;
    private bool CanShoot = true;


    public override void ShootStart()
    {
        if (Whitelist.Any(w => _movement.CurrentState == w))
            if (ActionEx.CheckCooldown(ShootStart, Cooldown))
            {
                CanShoot = false;
                ShootStartNoCheck();
            }
    }

    protected override void HandleInput()
    {
        if (_inputManager.ShootButton.State.CurrentState == MMInput.ButtonStates.ButtonUp)
            CanShoot = true;

        if ((_inputManager.ShootButton.State.CurrentState == MMInput.ButtonStates.ButtonPressed) ||
            (_inputManager.ShootAxis == MMInput.ButtonStates.ButtonDown))
        {
            if (CanShoot && _inputManager.ShootButton.TimeSinceLastButtonDown > TimeSinceButtonDown)
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

        if (_inputManager.ReloadButton.State.CurrentState == MMInput.ButtonStates.ButtonDown)
        {
            Reload();
        }

        if ((_inputManager.ShootButton.State.CurrentState == MMInput.ButtonStates.ButtonUp) ||
            (_inputManager.ShootAxis == MMInput.ButtonStates.ButtonUp))
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
}