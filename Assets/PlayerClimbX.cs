using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerClimbX : MonoBehaviour
{
    public bool isHanging;
    public bool isClimbing;
    private Animator _animator;
    private Rigidbody2D _physics;
    private MovementStats _stats;
    public Vector2 TeleportOffset;
    public float MaxHangingTime = 2f;
    private Player _player;

    public float ClimbingTime = 0.4f;
    public float HangingDisabledDuration = 0.5f;


    private float hangingTimer;
    public bool isHangingEnabled = true;

    private void Start()
    {
        _player = GetComponent<Player>();
        _physics = GetComponent<Rigidbody2D>();
        _stats = GetComponent<MovementStats>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (!isHanging && _stats.CanGrabLedge && isHangingEnabled && !_stats.IsGrounded)
        {
            if (_physics.velocity.y < 0 && _physics.velocity.y > -30f)
            {
                StartHanging();
            }
        }

        if (isHanging && !_stats.CanGrabLedge)
        {
            EndHanging();
        }

        if (isHanging)
        {
            hangingTimer -= Time.deltaTime;
            
            if(hangingTimer <= 0)
                EndHanging();
        }
    }

    private void OnGUI()
    {
        if (isHanging)
        {
            if (Input.GetKeyDown(
                KeySettingManager.GetKeyCodeByName(KeySettingManager.DOWN_BUTTON)))
            {
                EndHanging();
            }

            if (Input.GetKeyDown(
                KeySettingManager.GetKeyCodeByName(KeySettingManager.JUMP_BUTTON)))
            {
                StartClimbing();
            }
        }
    }

    private void StartHanging()
    {
        isHanging = true;
        _physics.bodyType = RigidbodyType2D.Static;
        _player.DisableFlip = true;
        hangingTimer = MaxHangingTime;
    }

    // is called from animation
    public void StartClimbing()
    {
        isHanging = false;
        isClimbing = true;
        Invoke(nameof(EndClimbing), ClimbingTime);

        DisableHanging();
        Invoke(nameof(EnableHanging), ClimbingTime + HangingDisabledDuration);
    }

    public void EndClimbing()
    {
        _physics.bodyType = RigidbodyType2D.Dynamic;
        _physics.position = new Vector2(transform.position.x + TeleportOffset.x * transform.right.x,
            transform.position.y + TeleportOffset.y);

        isClimbing = false;
        _physics.bodyType = RigidbodyType2D.Dynamic;
        _player.DisableFlip = false;
    }

    public void EndHanging()
    {
        _player.DisableFlip = false;
        _physics.bodyType = RigidbodyType2D.Dynamic;
        DisableHanging();
        Invoke(nameof(EnableHanging), HangingDisabledDuration);
    }

    private void DisableHanging()
    {
        isHanging = false;
        isHangingEnabled = false;
    }

    private void EnableHanging()
    {
        isHangingEnabled = true;
    }
}