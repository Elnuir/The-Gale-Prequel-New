using System;
using System.Collections;
using System.Collections.Generic;
using MoreMountains.CorgiEngine;
using MoreMountains.Tools;
using UnityEngine;

public class AiActionShootX : AIActionShoot
{
    private float _canFaceTimer;
    private bool CanFace => _canFaceTimer <= 0;


    protected override void TestFaceTarget()
    {
        if (!FaceTarget || !CanFace)
        {
            return;
        }

        if (this.transform.position.x > _brain.Target.position.x)
        {
            _character.Face(Character.FacingDirections.Left);
        }
        else
        {
            _character.Face(Character.FacingDirections.Right);
        }
    }

    public void PreventFlip(float time)
    {
        _canFaceTimer = time;
    }

    private new void Update()
    {
        base.Update();
        if (_canFaceTimer >= 0)
            _canFaceTimer -= Time.deltaTime;
    }
}