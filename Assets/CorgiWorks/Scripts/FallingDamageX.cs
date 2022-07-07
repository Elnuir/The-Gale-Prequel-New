using System;
using System.Collections;
using System.Collections.Generic;
using MoreMountains.CorgiEngine;
using MoreMountains.Tools;
using UnityEngine;

public class FallingDamageX : CharacterFallDamage
{
    public override void ProcessAbility()
    {
        base.ProcessAbility();

        if (_character.CharacterType != Character.CharacterTypes.AI && _inputManager.JumpButton.State.CurrentState == MMInput.ButtonStates.ButtonDown)
            _takeOffAltitude = transform.position.y;
    }

    public void HardResetTakeOffAttitude()
    {
        _takeOffAltitude = transform.position.y;
    }
}