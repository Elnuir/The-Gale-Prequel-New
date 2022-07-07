using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpAsideState : AnimatedEntityState
{
    private Rigidbody2D _physics;
    private TargetProviderBase provider;
    private DigOutJumperState _digOut;
    public Vector2 force = new Vector2(200, 200);
    private bool makedAvailableCrutch;

    protected override void Start()
    {
        base.Start();
        _physics = GetComponent<Rigidbody2D>();
        provider = GetComponent<TargetProviderBase>();
        _digOut = GetComponent<DigOutJumperState>();
    }

    public override void ActivateState()
    {
        base.ActivateState();
    }

    protected override void Update()
    {
        base.Update();
        if (IsActive)
        {
            var dX = transform.position.x - provider.GetTarget().position.x;
            var actualForce = new Vector2(force.x * Mathf.Sign(dX), force.y);
            _physics.AddForce(actualForce);
        }
    }

    public void MakeJumpAsideAvailable()
    {
        if (_digOut.JumpAsideCrutch)
        {
            _digOut.JumpAsideCrutch = false;
            base.MakeAvailable();
        }
    }
}