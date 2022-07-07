using System.Collections;
using System.Collections.Generic;
using CorgiWorks.Scripts;
using MoreMountains.CorgiEngine;
using MoreMountains.Tools;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class FallingDamageCancellation : CharacterAbility
{
    private bool _isWorking;
    private float _currDuration;

    public string AnimatorParameterName;
    public float Duration = 1f;
    public float Cooldown = 0.5f;
    public float SetFallingSpeed = 10;
    public float MaxDistanceToGround = 20;
    public float MinFallingSpeed = 0;
    public GameObject DamageArea;

    protected override void Start()
    {
        base.Start();
        DamageArea.SetActive(false);
    }

    private void CancelFallingDamage()
    {
        if (!ActionEx.CheckCooldown(CancelFallingDamage, Cooldown)) return;
        _isWorking = true;
        DamageArea.SetActive(true);
        _controller.AddVerticalForce(-SetFallingSpeed);
        _currDuration = Duration;
    }

    private void Update()
    {
        if (_currDuration > 0)
            _currDuration -= Time.deltaTime;
        else
        {
            _isWorking = false;
            DamageArea.SetActive(false);
        }
    }

    protected override void HandleInput()
    {
        var inputManager = (InputManagerX)_inputManager;

        if ((_inputManager.ShootButton.State.CurrentState == MMInput.ButtonStates.ButtonDown) || (_inputManager.ShootAxis == MMInput.ButtonStates.ButtonDown))
        {
            if (inputManager.DownButton.State.CurrentState == MMInput.ButtonStates.ButtonPressed)
            {
                bool isStateCorrect = _movement.CurrentState == CharacterStates.MovementStates.Falling || _movement.CurrentState == CharacterStates.MovementStates.Jumping;

                if (_controller.DistanceToTheGround < MaxDistanceToGround && isStateCorrect)
                    CancelFallingDamage();
            }
        }
    }


    protected override void InitializeAnimatorParameters()
    {
        RegisterAnimatorParameter(AnimatorParameterName, AnimatorControllerParameterType.Bool, out _);
    }

    /// <summary>
    /// At the end of each cycle, sends Jumping states to the Character's animator
    /// </summary>
    public override void UpdateAnimator()
    {
        if (_isWorking)
            _controller.GetComponent<FallingDamageX>().HardResetTakeOffAttitude();
        MMAnimatorExtensions.UpdateAnimatorBool(_animator, AnimatorParameterName, _isWorking);
    }
}