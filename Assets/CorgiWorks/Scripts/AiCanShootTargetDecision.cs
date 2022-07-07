using System.Collections;
using System.Collections.Generic;
using MoreMountains.CorgiEngine;
using MoreMountains.Tools;
using UnityEngine;

public class AiCanShootTargetDecision : AIDecision
{
    public float RaycastOffset1;
    public float RaycastOffset2;
    public float RayLength = 5;
    public LayerMask ObstacleLayer;
    public AIDecision DetectTargetDecision;

    private Character _character;

    private void Start()
    {

        _character = _brain.gameObject.GetComponent<Character>();
    }
    public override bool Decide()
    {
        var origin1 = new Vector2(transform.position.x, transform.position.y + RaycastOffset1);
        var origin2 = new Vector2(transform.position.x, transform.position.y + RaycastOffset2);

        bool foundTarget = DetectTargetDecision.Decide();

        if(!foundTarget)
            return false;

        bool IsFacingRight = _brain.Target.transform.position.x > transform.position.x;
        var direction = IsFacingRight ? Vector2.right : Vector2.left;

        var hit1 = MMDebug.RayCast(origin1, direction, RayLength, ObstacleLayer, Color.yellow, true);
        var hit2 = MMDebug.RayCast(origin2, direction, RayLength, ObstacleLayer, Color.yellow, true);

        return !hit1 && !hit2;
    }
}
