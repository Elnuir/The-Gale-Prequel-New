using System.Collections;
using System.Collections.Generic;
using MoreMountains.CorgiEngine;
using MoreMountains.Tools;
using UnityEngine;

public class AiStunAbility : CharacterAbility
{
    public string AnimationParameterName;

    private bool isPlaying;

    public void SetStunActive(bool value)
    {
        isPlaying = value;
    }

    protected override void InitializeAnimatorParameters()
    {
        base.InitializeAnimatorParameters();
        RegisterAnimatorParameter(AnimationParameterName, AnimatorControllerParameterType.Bool,
            out int _);
    }

    public override void UpdateAnimator()
    {
        base.UpdateAnimator();

        MMAnimatorExtensions.UpdateAnimatorBool(_animator, AnimationParameterName, isPlaying);
    }
}