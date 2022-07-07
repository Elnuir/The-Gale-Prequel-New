using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DigInJumperState : AnimatedEntityState
{
    
    public float DigInForce;
    public Vector2 DigInOffset;
    private Rigidbody2D _physics;
    public TargetProviderBase PlayerTargetProvider;
    public LayerMask WhatIsGround;

    private DigOutJumperState _digOutState;

    public float MaxDistance = 10;
    public Collider2D[] DisableColliders;

    private Vector2 targetPosition;

    public UnityEvent DiggingIn;
    
    protected override void Start()
    {
        base.Start();
        _physics = GetComponent<Rigidbody2D>();
        _digOutState = GetComponent<DigOutJumperState>();

        if (PlayerTargetProvider == null)
            PlayerTargetProvider = GetComponent<TargetProviderBase>();
    }

    public override void ActivateState()
    {
        base.ActivateState();
        _digOutState.IsDiggedOut = false;
        targetPosition = (Vector2)_physics.position + (Vector2)DigInOffset;

        foreach (var col in DisableColliders)
            col.enabled = false;
        DiggingIn?.Invoke();
    }

    private bool CheckGroundPresence(Vector2 point)
    {
        return Physics2D.OverlapCircle(point, 1, WhatIsGround);
    }
    

    private bool CanMakeAvailable()
    {
        if (!_digOutState.IsDiggedOut) return false;
        var target = (Vector2)PlayerTargetProvider.GetTarget().position ;
        float distance = Mathf.Abs(transform.position.x- target.x);

        if (distance > MaxDistance)
            return true;
        return false;
    }

    public override void DeactivateState()
    {
        base.DeactivateState();
        foreach (var col in DisableColliders)
            col.enabled = true;
        GetComponent<SpriteRenderer>().enabled = false;
        _physics.position = targetPosition;
    }

    protected override void Update()
    {
        base.Update();

        if (IsActive)
            _physics.velocity = Vector2.down * DigInForce;
        
        if(!IsAvailable && CanMakeAvailable() )
            MakeAvailable();
    }
}
