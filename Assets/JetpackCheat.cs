using System.Collections;
using System.Collections.Generic;
using MoreMountains.CorgiEngine;
using UnityEngine;

public class JetpackCheat : CheatBase
{
    private CharacterJetpack _jetpack;

    protected override void ActivateCheat()
    {
        base.ActivateCheat();
        _jetpack = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterJetpack>();
        _jetpack.enabled = true;
    }

    protected override void DeactivateCheat()
    {
        base.DeactivateCheat();
        _jetpack.enabled = false;
    }
}