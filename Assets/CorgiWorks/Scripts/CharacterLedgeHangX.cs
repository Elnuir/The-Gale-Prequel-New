using System.Collections;
using System.Collections.Generic;
using MoreMountains.CorgiEngine;
using MoreMountains.Tools;
using UnityEngine;

public class CharacterLedgeHangX : CharacterLedgeHang
{
    new void Start()
    {
        base.Start();
    }

    protected override void HandleInput()
    {
        if (_movement.CurrentState != CharacterStates.MovementStates.LedgeHanging)
        {
            return;
        }

        _characterJump.AbilityPermitted = false;

        if (Time.time - _ledgeHangingStartedTimestamp < MinimumHangingTime)
        {
            return;
        }

        StartCoroutine(Climb());
    }


    protected override void HandleLedge()
    {
        if (_movement.CurrentState == CharacterStates.MovementStates.LedgeHanging)
        {
            _controller.SetForce(Vector2.zero);
            _controller.GravityActive(false);
            if (_characterJump != null)
            {
                _characterJump.ResetNumberOfJumps();
            }

            _characterHorizontalMovement.AbilityPermitted = false;
            _character.CanFlip = false;

            if (_ledge.GetType() == typeof(Ledge))
                _controller.transform.position = _ledge.transform.position + _ledge.HangOffset;

            if (_ledge.GetType() == typeof(Xyledge))
                _controller.transform.position = _controller.transform.position + _ledge.HangOffset;
        }
    }


    protected override IEnumerator Climb()
    {
        // we start to climb
        _movement.ChangeState(CharacterStates.MovementStates.LedgeClimbing);
        MMAnimatorExtensions.UpdateAnimatorBool(_animator, _ledgeClimbingAnimationParameter, true,
            _character._animatorParameters, _character.PerformAnimatorSanityChecks);
        // we prevent all other input
        _inputManager.InputDetectionActive = false;

        // we wait until the climb animation is complete
        yield return _climbingAnimationDelay;

        // we restore input and go to idle
        _inputManager.InputDetectionActive = true;
        MMAnimatorExtensions.UpdateAnimatorBool(_animator, _ledgeClimbingAnimationParameter, false,
            _character._animatorParameters, _character.PerformAnimatorSanityChecks);
        MMAnimatorExtensions.UpdateAnimatorBool(_animator, _idleAnimationParameter, true,
            _character._animatorParameters, _character.PerformAnimatorSanityChecks);
        _animator.Play(IdleAnimationName);

        if (_ledge.GetType() == typeof(Ledge))
            _character.transform.position = _ledge.transform.position + _ledge.ClimbOffset;

        if (_ledge.GetType() == typeof(Xyledge))
            _character.transform.position = _character.transform.position + _ledge.ClimbOffset;

        // we go back to idle and detach from the ledge
        _movement.ChangeState(CharacterStates.MovementStates.Idle);
        _controller.GravityActive(true);
        DetachFromLedge();
    }

    protected override void DetachFromLedge()
    {
        _ledge = null;
        _character.CanFlip = true;
        _characterHorizontalMovement.AbilityPermitted = true;
        _characterJump.AbilityPermitted = true;
        _controller.CollisionsOn();
        if (_startFeedbackIsPlaying)
        {
            StopStartFeedbacks();
            PlayAbilityStopFeedbacks();
        }
    }


    public override void StartGrabbingLedge(Ledge ledge)
    {
        if (ledge.GetType() != typeof(Xyledge))
            // we make sure we're facing the right direction
            if ((_character.IsFacingRight && (ledge.LedgeGrabDirection == Ledge.LedgeGrabDirections.Left))
                || (!_character.IsFacingRight && (ledge.LedgeGrabDirection == Ledge.LedgeGrabDirections.Right)))
            {
                return;
            }

        // we make sure we can grab the ledge
        if (!AbilityAuthorized
            || (_movement.CurrentState == CharacterStates.MovementStates.Jetpacking))
        {
            return;
        }

        // we start hanging from the ledge
        _ledgeHangingStartedTimestamp = Time.time;
        _ledge = ledge;
        _controller.CollisionsOff();
        PlayAbilityStartFeedbacks();
        _movement.ChangeState(CharacterStates.MovementStates.LedgeHanging);
    }
}