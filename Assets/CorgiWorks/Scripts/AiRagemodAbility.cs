using System.Collections;
using System.Collections.Generic;
using MoreMountains.CorgiEngine;
using MoreMountains.Tools;
using UnityEngine;

public class AiRagemodAbility : CharacterAbility
{
    public string RagemodAnimationParameterName;

    private int _animationPlaying;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    public void SetRageActive(bool value)
    {
        _animationPlaying = value ? 1 : 0;
    }

    protected override void InitializeAnimatorParameters()
    {
        base.InitializeAnimatorParameters();
        RegisterAnimatorParameter(RagemodAnimationParameterName, AnimatorControllerParameterType.Bool,
            out _animationPlaying);
    }

    public override void UpdateAnimator()
    {
        base.UpdateAnimator();

        MMAnimatorExtensions.UpdateAnimatorBool(_animator, RagemodAnimationParameterName,
            _animationPlaying == 1);
    }
}