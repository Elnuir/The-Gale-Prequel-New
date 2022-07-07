using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using CorgiWorks.Scripts;
using MoreMountains.CorgiEngine;
using MoreMountains.Tools;
using UnityEngine;

public class CharacterWallClingingX : CharacterWallClinging
{
    public float SecondRaycastVerticalOffset;
    public float StaminaComsumption = 10f;

    public float StaminaComsimptionInterval = 0.5f;

    private CharacterStaminaX _stamina;
    private float _currentInterval;
    public LayerMask CustomPlatformMask;


    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
    }

    protected override void Initialization()
    {
        base.Initialization();
        _stamina = GetComponentInParent<CharacterStaminaX>();

        if (!_stamina)
            Debug.LogError("Can't find stamina component");
    }

    private void Update()
    {
        if (_movement.CurrentState == CharacterStates.MovementStates.WallClinging)
        {
            _currentInterval -= Time.deltaTime;
            if (_currentInterval <= 0f)
            {
                _stamina.ConsumeStamina(StaminaComsumption);
                _currentInterval = StaminaComsimptionInterval;
            }
        }
        else
            _currentInterval = 0;
    }

    protected override void WallClinging()
    {
        if (!AbilityAuthorized
            || (_condition.CurrentState != CharacterStates.CharacterConditions.Normal)
            || (_controller.State.IsGrounded)
            || (_controller.State.ColliderResized)
            || (_controller.Speed.y >= 0))
        {
            return;
        }

        if (InputIndependent)
        {
            if (TestForWall())
            {
                EnterWallClinging();
            }
        }
        else
        {
            if (((_controller.State.IsCollidingLeft) && (_horizontalInput <= -_inputManager.Threshold.x))
                || ((_controller.State.IsCollidingRight) && (_horizontalInput >= _inputManager.Threshold.x)))
            {
                if (TestWallX())
                    EnterWallClinging();
            }
        }
    }

    private bool TestWallX()
    {
        Vector3 raycastOrigin = transform.position;
        Vector3 raycastDirection;
        if (_character.IsFacingRight)
        {
            raycastOrigin = raycastOrigin + transform.right * _controller.Width() / 2 +
                            transform.up * RaycastVerticalOffset;
            raycastDirection = transform.right - transform.up;
        }
        else
        {
            raycastOrigin = raycastOrigin - transform.right * _controller.Width() / 2 +
                            transform.up * RaycastVerticalOffset;
            raycastDirection = -transform.right - transform.up;
        }

        var mask = CustomPlatformMask != default ? CustomPlatformMask : _controller.PlatformMask;

        // we cast our ray 
        RaycastHit2D _raycast = MMDebug.RayCast(raycastOrigin, raycastDirection, WallClingingTolerance,
            mask & ~(_controller.OneWayPlatformMask | _controller.MovingOneWayPlatformMask),
            Color.blue, _controller.Parameters.DrawRaycastsGizmos);

        raycastOrigin = new Vector3(raycastOrigin.x, raycastOrigin.y + SecondRaycastVerticalOffset, raycastOrigin.z);

        RaycastHit2D _second = MMDebug.RayCast(raycastOrigin, raycastDirection, WallClingingTolerance,
            mask & ~(_controller.OneWayPlatformMask | _controller.MovingOneWayPlatformMask),
            Color.blue, _controller.Parameters.DrawRaycastsGizmos);

        // we check if the ray hit anything. If it didn't, or if we're not moving in the direction of the wall, we exit
        return _raycast && _second;
    }

    protected override void ExitWallClinging()
    {
        if (_movement.CurrentState == CharacterStates.MovementStates.WallClinging)
        {
            // we prepare a boolean to store our exit condition value
            bool shouldExit = false;
            if ((_controller.State.IsGrounded) // if the character is grounded
                || (_controller.Speed.y >= 0)) // or if it's moving up
            {
                // then we should exit
                shouldExit = true;
            }

            // we then cast a ray to the direction's the character is facing, in a down diagonal.
            // we could use the controller's IsCollidingLeft/Right for that, but this technique 
            // compensates for walls that have small holes or are not perfectly flat
            Vector3 raycastOrigin = transform.position;
            Vector3 raycastDirection;
            if (_character.IsFacingRight)
            {
                raycastOrigin = raycastOrigin + transform.right * _controller.Width() / 2 +
                                transform.up * RaycastVerticalOffset;
                raycastDirection = transform.right - transform.up;
            }
            else
            {
                raycastOrigin = raycastOrigin - transform.right * _controller.Width() / 2 +
                                transform.up * RaycastVerticalOffset;
                raycastDirection = -transform.right - transform.up;
            }

            // we check if the ray hit anything. If it didn't, or if we're not moving in the direction of the wall, we exit
            if (!InputIndependent)
            {
                // we cast our ray 
                RaycastHit2D hit = MMDebug.RayCast(raycastOrigin, raycastDirection, WallClingingTolerance,
                    _controller.PlatformMask & ~(_controller.OneWayPlatformMask | _controller.MovingOneWayPlatformMask),
                    Color.black, _controller.Parameters.DrawRaycastsGizmos);

                if (_character.IsFacingRight)
                {
                    if ((!hit) || (_horizontalInput <= _inputManager.Threshold.x))
                    {
                        // shouldExit = true;
                    }
                }
                else
                {
                    if ((!hit) || (_horizontalInput >= -_inputManager.Threshold.x))
                    {
                        // shouldExit = true;
                    }
                }
            }
            else
            {
                if (_raycast.collider == null)
                {
                    shouldExit = true;
                }
            }

            if (_stamina.CurrentStamina < StaminaComsumption)
                shouldExit = true;

            if (shouldExit)
            {
                ProcessExit();
            }
        }

        if ((_stateLastFrame == CharacterStates.MovementStates.WallClinging)
            && (_movement.CurrentState != CharacterStates.MovementStates.WallClinging)
            && _startFeedbackIsPlaying)
        {
            // we play our exit feedbacks
            StopStartFeedbacks();
            PlayAbilityStopFeedbacks();
        }

        _stateLastFrame = _movement.CurrentState;
    }
}