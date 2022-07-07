using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static KeySettingManager;

public class WallJumpX : MonoBehaviour
{
    private MovementStats _stats;
    private Rigidbody2D _physics;
    private Player _player;

    public bool isHooked;

    public Vector2 WallJumpDirection = new Vector2(-1, 1);
    public float WallJumpForse = 50000;
    public float Cooldown = 0.1f;
    public float FlipRestrictedTime= 0.2f;


    // Start is called before the first frame update
    void Start()
    {
        _stats = GetComponent<MovementStats>();
        _physics = GetComponent<Rigidbody2D>();
        _player = GetComponent<Player>();
    }

    // Update is called once per frame
    void OnGUI()
    {
        if (!_stats.GrabLedgeAirCheckStatus && _stats.GrabLedgeGroundCheckStatus && !_stats.IsGrounded)
        {
            if (Input.GetKeyDown(GetKeyCodeByName(JUMP_BUTTON)))
                PerformWallJump();
        }

        if (_player.SmoothMovement && _stats.IsGrounded)
            _player.SmoothMovement = false;
    }


    void PerformWallJump()
    {
        if (!ActionEx.CheckCooldown(PerformWallJump, Cooldown)) return;
        var normalized = WallJumpDirection.normalized;
        var actualDirection = new Vector2(normalized.x * transform.right.x, normalized.y);

        _physics.velocity = new Vector2(_physics.velocity.x, 0);
        _physics.AddForce(actualDirection * WallJumpForse, ForceMode2D.Impulse);


        _player.SmoothMovement = true;
        _player.Flip(true);
        _player.DisableFlip = true;
        Invoke(nameof(EnablePlayerFlip), FlipRestrictedTime);
    }

    void EnablePlayerFlip()
    {
        _player.DisableFlip = false;
    }
}