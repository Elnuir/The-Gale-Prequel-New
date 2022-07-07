using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedAttackState : AnimatedEntityState
{
    public float MinDistance = 0;
    public float MaxDistance = 8;
    public float Cooldown = 1f;
    public float MaxDeltaY = 3;
    public TargetProviderBase TargetProvider;
    public GameObject projectileFireBall;
    public Transform fireBallSpawnTransform;


    public bool CanAttack(Transform target)
    {
        if (!target) return false;
        float distance = Vector2.Distance(transform.position, target.position);
        
        if (distance < MinDistance || distance > MaxDistance) return false;
        if (Mathf.Abs(target.transform.position.y - transform.position.y) > MaxDeltaY) return false;
        if (target.position.x < transform.position.x &&  transform.right.x < 0 ) return false;
        if (target.position.x > transform.position.x && transform.right.x > 0 ) return false;
        return ((Func<Transform, bool>) CanAttack).CheckCooldown(Cooldown);
    }
    
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        TargetProvider ??= GetComponent<TargetProviderBase>();
    }

    private float GetDistanceToTarget()
    {
        return Vector3.Distance(TargetProvider.GetTarget().position, transform.position);
    }

    // CALLS FROM ANIMATION
    public void ThrowFireball()
    {
        var p = projectileFireBall.GetCloneFromPool(null);
        p.transform.position = fireBallSpawnTransform.position;
        
        if (p.TryGetComponent(out FireballRedeer fireballR))
            fireballR.flyRight = transform.right.x > 0;

        if (p.TryGetComponent(out Fireball fb))
            fb.flyRight = transform.right.x > 0;
    }
    
    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        if (CanAttack(TargetProvider.GetTarget()))
            if (ActionEx.CheckCooldown(Update, Cooldown))
                MakeAvailable();
    }
}
