using System.Collections;
using System.Collections.Generic;
using MoreMountains.CorgiEngine;
using MoreMountains.Tools;
using UnityEngine;
using UnityEngine.EventSystems;

public class CharacterWalljumpX : CharacterWalljump
{
    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
    }

    protected override void HandleInput()
    {
        WallJumpHappenedThisFrame = false;

        if (_movement.CurrentState == CharacterStates.MovementStates.WallClinging)
            if (AbilityAuthorized && _inputManager.JumpButton.State.CurrentState == MMInput.ButtonStates.ButtonDown)
            {
                if (Mathf.Abs(_inputManager.PrimaryMovement.x) > 0.1f)
                {
                    Walljump();
                    _characterJump.NumberOfJumpsLeft = _characterJump.NumberOfJumps;
                }
                else
                {
                    var temp = WallJumpForce;
                    WallJumpForce = Vector2.up * GetComponent<CharacterJump>().JumpHeight;
                    Walljump();
                    WallJumpForce = temp;
                }
            }
    }
}