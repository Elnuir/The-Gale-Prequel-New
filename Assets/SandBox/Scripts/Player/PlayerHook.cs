using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerHook : MonoBehaviour
{
    private bool isTouchingHookPosition, isHookAboveCheck, isLegPosition;
    public Transform hookCheck, hookAboveCheck, legCheck;
    public bool isHooked, isSliding;
    [SerializeField] private float checkDistance;
    [SerializeField] private LayerMask whatIsGround;
    private Rigidbody2D rb;
    private Player player;

    public bool wallJumping;
    [SerializeField] private float reboundForceMultiplier, wallJumpTime;
    [SerializeField] private Vector2 jumpDirection;
    [SerializeField] public bool wallJumpPerformed; //FOR CHARACTER FLIPPING PURPOSE

    private float baseGravityScale;
    [SerializeField] private float baseWallHangingTime;
    private float leftWallHangingTime;
    private float moveInputY = 1f, moveInputX;
    [SerializeField] private float hookAgainTime;
    [SerializeField] private bool canHook = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GetComponent<Player>();
        baseGravityScale = rb.gravityScale;
        leftWallHangingTime = baseWallHangingTime;
    }

    private void OnGUI()
    {
        moveInputX = Input.GetAxisRaw("Horizontal");
        if (Input.GetKeyDown(KeyCode.Space) && isSliding)
        {
            wallJumping = true;
            Invoke(nameof(SetWallJumpingToFalse), wallJumpTime);
        }
    }

    void Update()
    {
        
        //jumpDirection = new Vector2(moveInputX, moveInputY);
    }

// Update is called once per frame
    void FixedUpdate()
    {
        isTouchingHookPosition = Physics2D.Raycast(hookCheck.position, transform.right, checkDistance, whatIsGround);
        isHookAboveCheck = Physics2D.Raycast(hookAboveCheck.position, transform.right, checkDistance, whatIsGround);
        isLegPosition = Physics2D.Raycast(hookAboveCheck.position, transform.right, checkDistance, whatIsGround);
        if (isTouchingHookPosition && !isHookAboveCheck && !player.isGrounded && !player.isDead &&
            canHook) //&& player.extraJumps > 0)
        {
            isHooked = true;
        }
        else if (isTouchingHookPosition && isLegPosition && isHookAboveCheck && !player.isDead && !player.isGrounded)
        {
            isSliding = true;
        }
// else if (isTouchingHookPosition && isLegPosition && isHookAboveCheck && !player.isGrounded && !player.isDead && canHook)
// {
// if (player.facingRight && player.moveInput > 0 && !player.facingRight && player.moveInput < 0)
// {
// isSliding = true;
// }
// }
        else
        {
            isHooked = false;
            isSliding = false;
            rb.gravityScale = baseGravityScale;
            leftWallHangingTime = Mathf.Clamp(leftWallHangingTime += Time.deltaTime, 0, baseWallHangingTime);
        }

        if (isHooked && leftWallHangingTime >= 0)
        {
            rb.velocity = Vector2.zero;
            print("5");
            rb.gravityScale = 0;
            leftWallHangingTime -= Time.deltaTime;
            if (Input.GetKeyDown(KeyCode.S))
            {
                leftWallHangingTime = 0;
                SetWallHookingFalse();
            }
        }
        //else if (isSliding)
        //{
        //    rb.velocity = new Vector2(rb.velocity.x, 0.5f);
       // }
        else
        {
            if (player.isGrounded)
                leftWallHangingTime = baseWallHangingTime;
            SetWallHookingFalse();
            SetWallSlidingFalse();
        }


        if (wallJumping)
        {
            // player.rb.velocity = Vector2.zero;
            print("6");
            rb.gravityScale = baseGravityScale;

            var actualJumpDirection = new Vector2(jumpDirection.x * transform.right.x, jumpDirection.y);

            if (ActionEx.CheckCooldown(FixedUpdate, 0.5f))
            {
                player.rb.AddForce(actualJumpDirection * reboundForceMultiplier * Time.fixedDeltaTime);

                if (Math.Sign(transform.right.x) != Math.Sign(actualJumpDirection.x))
                    player.Flip();
                player.flipRestricted = true;
            }

            wallJumpPerformed = true;
//canHook = false; Invoke(nameof(SetHookAgainTrue), hookAgainTime);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (wallJumping && LayerMask.LayerToName(other.gameObject.layer) == "Obstacle")
        {
 SetWallJumpingToFalse();
SetWallHookingFalse();
//SetWallSlidingFalse();
        }
    }

    void SetWallJumpingToFalse()
    {
        wallJumping = false;
        wallJumpPerformed = false;
    }

    void SetWallHookingFalse()
    {
        isHooked = false;
        rb.gravityScale = baseGravityScale;
    }

    void SetWallSlidingFalse()
    {
// isSliding = false;
        rb.gravityScale = baseGravityScale;
    }

    void SetHookAgainTrue()
    {
        canHook = true;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawRay(hookCheck.transform.position, transform.right * checkDistance);
        Gizmos.DrawRay(hookAboveCheck.transform.position, transform.right * checkDistance);
        Gizmos.DrawRay(legCheck.transform.position, transform.right * checkDistance);
    }
}