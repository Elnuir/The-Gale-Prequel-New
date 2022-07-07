using System.Collections;
using System.Collections.Generic;
using MoreMountains.CorgiEngine;
using MoreMountains.Tools;
using UnityEngine;

public class DamageOnTouchX : DamageOnTouch
{
    protected override void Colliding(Collider2D collider)
    {
        if (!this.isActiveAndEnabled)
        {
            return;
        }

        // if the object we're colliding with is part of our ignore list, we do nothing and exit
        if (_ignoredGameObjects.Contains(collider.gameObject))
        {
            return;
        }

        // if what we're colliding with isn't part of the target layers, we do nothing and exit
        if (!MMLayers.LayerInLayerMask(collider.gameObject.layer,TargetLayerMask))
        {
            return;
        }

        _collidingCollider = collider;
        _colliderHealth = collider.gameObject.MMGetComponentNoAlloc<PossessedHealth>();

        OnHit?.Invoke();
			
        // if what we're colliding with is damageable
        if ((_colliderHealth != null) && (_colliderHealth.enabled))
        {
            if(_colliderHealth.Invulnerable || _colliderHealth.ImmuneToDamage)
            {
                _colliderHealth.Invulnerable = false;
                _colliderHealth.ImmuneToDamage = false;
                _colliderHealth.Kill();
             //   OnCollideWithDamageable(_colliderHealth);
            }
        }
        // if what we're colliding with can't be damaged
        else
        {
            OnCollideWithNonDamageable();
        }
    }
}
