using System.Collections;
using System.Collections.Generic;
using MoreMountains.CorgiEngine;
using UnityEngine;

public class Xyledge : Ledge
{

    protected override void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag(_playerTag))
        {
            if (collider.GetComponent<CorgiController>().Speed.y > 0)
                LedgeEvent.Trigger(collider, this);
        }
    }

}
