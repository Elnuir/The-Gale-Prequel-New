using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DigOutJumperState : AnimatedEntityState
{
    public float DigOutForce;
    public Vector2 StartPositonDelta = new Vector2(0, -5);
    private Vector2 _digOutTarget;
    private Rigidbody2D _physics;
    public TargetProviderBase PlayerTargetProvider;
    public LayerMask WhatIsGround;
    public bool JumpAsideCrutch;
    public bool IsDiggedOut = false;

    public UnityEvent DiggedOut;

    public float MinInterval = 5;
    public float MaxInterval = 8;
    public float MaxDistanceX = 10;
    public Collider2D[] DisableColliders;

    private float interval;
    private Vector2 targetPosition;

    protected override void Start()
    {
        base.Start();
        _physics = GetComponent<Rigidbody2D>();

        if (PlayerTargetProvider == null)
            PlayerTargetProvider = GetComponent<TargetProviderBase>();
    }

    public override void ActivateState()
    {
        base.ActivateState();
        targetPosition = PlayerTargetProvider.GetTarget().position;
        _physics.position = targetPosition + StartPositonDelta;

        foreach (var col in DisableColliders)
            col.enabled = false;
        JumpAsideCrutch = true;
        GetComponent<SpriteRenderer>().enabled = true;
    }

    private bool CheckGroundPresence(Vector2 point)
    {
        return Physics2D.OverlapCircle(point, 1, WhatIsGround);
    }
    

    private bool CanMakeAvailable()
    {
        var target = (Vector2)PlayerTargetProvider.GetTarget().position + StartPositonDelta;
        float distance = Mathf.Abs(transform.position.x- target.x);

        if (distance > MaxDistanceX)
            return CheckGroundPresence(target);
        else
            return false;
    }

    public override void DeactivateState()
    {
        base.DeactivateState();
        _physics.velocity = Vector2.zero;

        foreach (var col in DisableColliders)
            col.enabled = true;
        _physics.position = targetPosition;
        IsDiggedOut = true;
        DiggedOut?.Invoke();
    }

    protected override void Update()
    {
        base.Update();

        interval -= Time.deltaTime;

        if (IsActive)
        {
            _physics.velocity = Vector2.up * DigOutForce;
        }

        if (interval <= 0)
        {
            if (CanMakeAvailable())
                MakeAvailable();
            interval = GetRandomInterval();
        }
    }

    private float GetRandomInterval()
    {
        return Random.Range(MinInterval, MaxInterval);
    }
}