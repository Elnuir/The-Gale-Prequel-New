using System.Collections;
using System.Collections.Generic;
using MoreMountains.CorgiEngine;
using UnityEngine;

public class CharacterJumpX : CharacterJump
{
    public override bool JumpAuthorized
    {
        get
        {
            bool authorized = _movement.CurrentState != CharacterStates.MovementStates.LedgeHanging &&
            _movement.CurrentState != CharacterStates.MovementStates.LedgeClimbing;

            return authorized && base.JumpAuthorized;
        }
    }
}
