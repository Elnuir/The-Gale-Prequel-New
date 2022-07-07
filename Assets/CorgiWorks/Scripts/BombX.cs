using System.Collections;
using System.Collections.Generic;
using MoreMountains.CorgiEngine;
using UnityEngine;

public class BombX : Bomb
{
    public bool DestroyOnCollision;
    public LayerMask TargetLayer;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (DestroyOnCollision)
            if (Contains(TargetLayer, other.gameObject.layer))
                _timeSinceStart = TimeBeforeExplosion;

    }
    private bool Contains(LayerMask mask, int layer)
    {
        return mask == (mask | (1 << layer));
    }
}