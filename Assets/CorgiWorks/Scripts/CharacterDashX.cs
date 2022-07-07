using System.Collections;
using System.Collections.Generic;
using MoreMountains.CorgiEngine;
using MoreMountains.Tools;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class CharacterDashX : CharacterDash
{
    private float _initObstacleTolerance;
    public float ObstacleHeightTolerance;
    public float UnfoldMinHeight;

    protected override void Start()
    {
        base.Start();
        _initObstacleTolerance = _controller.ObstacleHeightTolerance;
    }

    public override void InitiateDash()
    {
        _controller.ObstacleHeightTolerance = ObstacleHeightTolerance;
        base.InitiateDash();
    }

    public override void StopDash()
    {
        base.StopDash();
        _controller.ObstacleHeightTolerance = _initObstacleTolerance;
    }


    public override bool DashAuthorized()
    {
        if (_controller.State.IsCollidingLeft || _controller.State.IsCollidingRight)
            return false;
        return base.DashAuthorized();
    }

    public bool CanUnfold()
    {
        float offsetX = _controller.Width() / 2f + 0.7f;
        var v1 = new Vector2(transform.position.x + offsetX, transform.position.y);
        var v2 = new Vector2(transform.position.x - offsetX, transform.position.y);

        var hit1 = MMDebug.RayCast(v1, Vector2.up, UnfoldMinHeight, _controller.PlatformMask, Color.green, true);
        var hit2 = MMDebug.RayCast(v2, Vector2.up, UnfoldMinHeight, _controller.PlatformMask, Color.green, true);

        return !hit1 && !hit2;
    }


    protected override IEnumerator Dash()
    {
        // if the character is not in a position where it can move freely, we do nothing.
        if (!AbilityAuthorized
            || (_condition.CurrentState != CharacterStates.CharacterConditions.Normal))
        {
            yield break;
        }

        // we keep dashing until we've reached our target distance or until we get interrupted
        while ((_distanceTraveled < DashDistance || !CanUnfold())
            && _shouldKeepDashing
            && !_controller.State.TouchingLevelBounds
            && _movement.CurrentState == CharacterStates.MovementStates.Dashing)
        {
            _distanceTraveled = Vector3.Distance(_initialPosition, this.transform.position);



            // if we collide with something on our left or right (wall, slope), we stop dashing, otherwise we apply horizontal force
            if ((_controller.State.IsCollidingLeft && _dashDirection.x < 0f)
                || (_controller.State.IsCollidingRight && _dashDirection.x > 0f)
                || (_controller.State.IsCollidingAbove && _dashDirection.y > 0f)
                || (_controller.State.IsCollidingBelow && _dashDirection.y < 0f))
            {
                _shouldKeepDashing = false;
                _controller.SetForce(Vector2.zero);
            }
            else
            {
                _controller.GravityActive(false);
                _controller.SetForce(_dashDirection * DashForce);
            }
            yield return null;
        }
        StopDash();
    }
}
