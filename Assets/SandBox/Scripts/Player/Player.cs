using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // private Animator animator;                                    //Animations and their names
    // private string currentState;                                  //
    // private const string PLAYER_IDLE = "CharacterIdle";           //
    // private const string PLAYER_JUMP = "CharacterJump";           //
    // private const string PLAYER_JUMPFLY = "CharacterJumpFly";     //
    // private const string PLAYER_LANDING = "CharacterLanding";    //
    // private const string PLAYER_RUN = "CharacterRun";             //
    // private const string PLAYER_DEATH = "CharacterDeath";          //
    // private const string PLAYER_HIT = "CharacterHit";             //
    public enum TypeOfPlayer
    {
        Swordsman,
        Dagger
    }

    public TypeOfPlayer PlayerType;


    [HideInInspector] public Rigidbody2D rb;
    //  private PlayerClimb playerClimb;


    public float speed; // Movement shit

    [HideInInspector] public float moveInput; //

    // Ground Checking
    //[SerializeField] Transform groundCheckLeftUp, groundCheckRightDown; //
    // [SerializeField] float checkRadius; // 

    [SerializeField] LayerMask whatIsGround; //


    private DashMove dashmove; //ForAnimationOfDashing


    private float knockbackStartTime;
    [SerializeField] private float knockbackDuration;
    [SerializeField] private Vector2 knockbackSpeed;
    private GameManager gameManager; //TO CHECK  if game is paused

    //DELETE TODO:ddd
    private Animator animator;
    public bool facingRight = true; // Responsible for facing right
    public bool isGrounded;

    public bool isRunning;

    // public bool isIdling = true;
    public bool isDead; //for animations, called in PlayerStats
    public bool isHitted; //for animations, called in PlayerStats
    public bool knockback;
    public bool isAttacking; // TODO: remove this
    public bool DisableFlip;
    [HideInInspector] public bool flipRestricted;

    [HideInInspector] public float timeFlipRestricted = 0.3f;

    //private bool secondJumpIsDone;
    private Throw throw1;
    private MovementStats movementStats;
    private PlayerJump playerJump;
    private PlayerCombatManager playerCombatManager;
    private PlayerClimbX playerClimbX;

    private Vector2 refVelocity;
    public bool SmoothMovement;

    private void Start()
    {
        animator = GetComponent<Animator>();
        playerClimbX = GetComponent<PlayerClimbX>();
        rb = GetComponent<Rigidbody2D>();
        dashmove = GetComponent<DashMove>();
        gameManager = FindObjectOfType<GameManager>();
        throw1 = GetComponentInChildren<Throw>();
        movementStats = GetComponent<MovementStats>();
        playerJump = GetComponent<PlayerJump>();
        playerCombatManager = GetComponent<PlayerCombatManager>();
    }

    public void PlayerStates()
    {
        if (moveInput != 0 && isGrounded && !playerClimbX.isHanging && !GameManager.gameIsPaused) //Mathf.Abs(rb.velocity.x) > 1e-5f && isGrounded && moveInput != 0)
        {
            isRunning = true;
            //  isIdling = false;
        }
        else if (moveInput == 0 && isGrounded && !playerClimbX.isHanging && !GameManager.gameIsPaused) //Mathf.Abs(rb.velocity.x) < 1e-5f && moveInput == 0)
        {
            isRunning = false;
            // isIdling = true;
        }


        // else if (extraJumps == extraJumpsValue) //|| isGrounded)
        // {
        // isSecondJump = false;

        // }

        animator.SetBool("hit", knockback);
        animator.SetBool("die", isDead);
        animator.SetBool("move", isRunning);
        animator.SetBool("isGrounded", isGrounded);
        animator.SetBool("attack", isAttacking);
        animator.SetBool("isHanging", playerClimbX.isHanging);
        animator.SetBool("isClimbing", playerClimbX.isClimbing);
        animator.SetBool("isDashing", dashmove.isDashing);
        animator.SetFloat("yVelocity", rb.velocity.y);
        animator.SetBool("isSecondJump", playerJump.isSecondJump);
    }


    void Update()
    {
        moveInput = GetMoveInput();
        PlayerStates();
        CheckKnockBack();
        FlipRestrictedCheck();
        // JumpChecker();
    }

    private float GetMoveInput()
    {
        if (Input.GetKey(
            KeySettingManager.GetKeyCodeByName(KeySettingManager.LEFT_BUTTON)))
            return -1f;

        if (Input.GetKey(
            KeySettingManager.GetKeyCodeByName(KeySettingManager.RIGHT_BUTTON)))
            return 1f;

        return 0f;
    }

    private void FixedUpdate()
    {
        isGrounded = movementStats.IsGrounded;
        //  JumpChecker();
        //isGrounded = Physics2D.OverlapArea(groundCheckLeftUp.position, groundCheckRightDown.position, whatIsGround); //creates circle which suppose to be at feet
        if (!knockback && !isDead &&
            !playerCombatManager.isPushingForward) //&& moveInput != 0) //&& !playerClimb.isHooked
        {
            if (!SmoothMovement)
                rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
            else
                rb.velocity = Vector2.SmoothDamp(rb.velocity, new Vector2(moveInput * speed, rb.velocity.y),
                    ref refVelocity, 0.4f);
//            print("1");
            //print("VELOCITY");
        }


        if (facingRight == false && moveInput > 0) // !playerClimb.isHooked 
        {
            if (!isDead)
                Flip();
        }
        else if (facingRight && moveInput < 0) // !playerClimb.isHooked //Flips character
        {
            if (!isDead)
                Flip();
        }
    }

    void FlipRestrictedCheck()
    {
        if (flipRestricted)
        {
            timeFlipRestricted -= Time.deltaTime;
            if (timeFlipRestricted <= 0f)
            {
                flipRestricted = false;
                timeFlipRestricted = 0.3f;
            }
        }
    }


    public void Flip(bool ignoreDisableFlip = false)
    {
        if(!ignoreDisableFlip && DisableFlip) return;
        if (!flipRestricted)
        {
            facingRight = !facingRight;
            transform.Rotate(0f, 180f, 0f); // flips character
        }
    }

    private void OnDrawGizmos()
    {
        // Gizmos.DrawWireCube(new Vector2((groundCheckLeftUp.position.x + groundCheckRightDown.position.x)/2, (groundCheckLeftUp.position.y + groundCheckRightDown.position.y)/2),
        //     new Vector2((groundCheckRightDown.position.x - groundCheckLeftUp.position.x), (groundCheckLeftUp.position.y - groundCheckRightDown.position.y)));
    }

    public void ChangeAnimationState(string newState)
    {
        //  if (currentState == newState) return;              //Used to call anims from animator, kinda useful shit
        // animator.Play(newState);
        // currentState = newState;
    }

    public void StopAttack()
    {
        isAttacking = false; //Crutch, used as an event in animation time line
    }

    public void StopBeingHit()
    {
        isHitted = false; //Crutch, used as an event in animation time line
    }

    public void StopLanding()
    {
//        animator.SetBool("JumpDown", false);
    }

    public void StopJumpUp()
    {
        //  animator.SetBool("JumpUp", false);
    }

    // public void StopSecondJump()
    // {
    //     isSecondJump = false;
    //     animator.SetBool("isSecondJump", isSecondJump);
    //     secondJumpIsDone = true;
    // }

    public void KnockBack(int direction)
    {
        isAttacking = false;
        knockback = true;
        animator.SetBool("hit", knockback);
        knockbackStartTime = Time.time;
        rb.velocity = new Vector2(knockbackSpeed.x * direction, knockbackSpeed.y);
        print("3");
    }

    void CheckKnockBack()
    {
        if (Time.time > knockbackStartTime + knockbackDuration && knockback)
        {
            knockback = false;
            animator.SetBool("hit", knockback);
            rb.velocity = new Vector2(0.0f, rb.velocity.y);
            print("4");
            // print("VELOCITY");
        }
    }

    public bool GetDashStatus()
    {
        return dashmove.isDashing;
    }

    public void ShootKnife()
    {
        throw1.ShootKnife();
    }

    public void ShootSaintWater()
    {
        throw1.ShootSaintWater();
    }

    public void ShootGrenade()
    {
        throw1.ShootGrenade();
    }
}