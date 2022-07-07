using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerCombatManager : MonoBehaviour
{
    public static PlayerCombatManager instance;
    private BuffManager _manager;

    public bool canBeUsed;
    public bool canReceiveInput;
    public bool inputReceived;
   

    public bool isHardAttacked;


    public Transform attackPosLeftUp, attackPosRightDown, jumpAttackPosLeftUp, jumpAttackPosRightDown;
    public LayerMask whatIsEnemies;

    private Player player;
    private PlayerClimbX playerClimbX;
    private DashMove dashMove;
    private PlayerStats playerStats;
    private Rigidbody2D rb;

    public float baseAttackDamage;
    public float baseHardAttackDamage;
    public float baseQuickAttackDamage;
    public float attackDamage;
    private float[] attackDetails = new float[2];


    private Collider2D[] enemiesToDamage, enemiesToDamageJump;
    Collider2D checkEnemiesToDamageJump;
    [SerializeField] float reboundForce;

    // [SerializeField] float hardAttackTimeBase;
    float mouseDownTime;
    [SerializeField] private float hardAttackTimeAfter;
    private Animator animator;

//Pushing player forward when attack
    [SerializeField] private float pushForwardMultiplier;
    private Vector2 sped;
    public bool isPushingForward; //To not allow Player.cs control x velocity
    [SerializeField] private float pushForwardTime;
    private float pushForwardTimeLeft;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
        player = GetComponent<Player>();
        playerClimbX = GetComponent<PlayerClimbX>();
        dashMove = GetComponent<DashMove>();
        playerStats = GetComponent<PlayerStats>();
        rb = GetComponent<Rigidbody2D>();
        _manager = FindObjectOfType<BuffManager>();
        animator = GetComponent<Animator>();
        attackDamage = baseAttackDamage;
        pushForwardTimeLeft = pushForwardTime;
    }

    // Update is called once per frame
    void Update()
    {
        PushForward();
        AttackCheck();
        animator.SetBool("IsPushingForward", isPushingForward);
        //   JumpKill();
    }

    

    void AttackCheck()
    {
        // if (Input.GetMouseButton(0))
        // {
        //     hardAttackTimeLeft += Time.deltaTime;
        //     while (hardAttackTimeLeft <= hardAttackTimeBase)
        //     {
        //         HardAttackAnim();
        //         return;
        //     }
        //
        // }
        // else
        //     hardAttackTimeLeft = hardAttackTimeBase;
        if (Input.GetKeyDown(KeyCode.Q))
        {
            QuickAttackAnim();
        }

        if (mouseDownTime > hardAttackTimeAfter && !isHardAttacked)
        {
            HardAttackAnim();
            isHardAttacked = true;
        }

        if (mouseDownTime > 0 && !Input.GetMouseButton(0) && !isHardAttacked)
        {
            WeakAttackAnim();
            mouseDownTime = 0;
        }

        if (!Input.GetMouseButton(0))
        {
            mouseDownTime = 0;
            isHardAttacked = false;
        }

        if (Input.GetMouseButton(0)) mouseDownTime += Time.deltaTime;
    }

    public void HardAttackAnim()
    {
        if (!player.isDead &&  !dashMove.isDashing && !GameManager.gameIsPaused && canBeUsed &&
            !player.isRunning && !playerClimbX.isHanging)
        {
            animator.SetTrigger("HardAttacking");
            
        }
    }

    public void QuickAttackAnim()
    {
        if (!player.isDead && !playerClimbX.isHanging && !dashMove.isDashing && !GameManager.gameIsPaused && canBeUsed &&
            !player.isRunning)
        {
            animator.SetTrigger("QuickAttacking");
        }
    }

    public void WeakAttackAnim()
    {
        if (!player.isDead &&  !dashMove.isDashing && !GameManager.gameIsPaused && canBeUsed &&
            !player.isRunning && !playerClimbX.isHanging)
        {
            if (canReceiveInput)
            {
                // print("shit");
                inputReceived = true;
                canReceiveInput = false;
                isPushingForward = true;
                //hardAttackTimeLeft = hardAttackTimeBase;
            }
            else
            {
                return;
            }
        }
    }

    public void InputManager()
    {
        if (!canReceiveInput)
        {
            canReceiveInput = true;
        }
        else
        {
            canReceiveInput = false;
        }
    }

    public void HardAttack()
    {
        enemiesToDamage =
            Physics2D.OverlapAreaAll(attackPosLeftUp.position, attackPosRightDown.position, whatIsEnemies);
        attackDetails[0] = baseHardAttackDamage;
        attackDetails[1] = transform.position.x;
        foreach (Collider2D collider in enemiesToDamage)
        {
            collider.transform.SendMessage("Damage", attackDetails);
            AddBurningIfRequired(collider.transform);
            AddFatalLightingIfRequired(collider.transform);
        }
    }


    public void Attack()
    {
        enemiesToDamage =
            Physics2D.OverlapAreaAll(attackPosLeftUp.position, attackPosRightDown.position, whatIsEnemies);
        attackDetails[0] = baseAttackDamage;
        attackDetails[1] = transform.position.x;
        
        foreach (Collider2D collider in enemiesToDamage)
        {
            collider.transform.SendMessage("Damage", attackDetails);
            AddBurningIfRequired(collider.transform);
            AddFatalLightingIfRequired(collider.transform);
        }
    }

    public void QuickAttack()
    {
        enemiesToDamage =
            Physics2D.OverlapAreaAll(attackPosLeftUp.position, attackPosRightDown.position, whatIsEnemies);
        attackDetails[0] = baseQuickAttackDamage;
        attackDetails[1] = transform.position.x;
        foreach (Collider2D collider in enemiesToDamage)
        {
            if (collider.gameObject.layer != 22) //Layer Of Fireball
            {
                collider.transform.SendMessage("Damage", attackDetails);
                AddBurningIfRequired(collider.transform);
                AddFatalLightingIfRequired(collider.transform);
            }
            else
            {
                Rigidbody2D rbFireBall = collider.gameObject.GetComponent<Rigidbody2D>();
                float xSpeedOfBall = rbFireBall.velocity.x;
                rbFireBall.velocity = new Vector2(-xSpeedOfBall, rbFireBall.velocity.y);
                print("DDDDDDFFF");
            }
        }
    }

    private void AddBurningIfRequired(Transform target)
    {
        if (!_manager.CheckActive("fire-fury")) return;
        var damager = target.GetComponents<PeriodicDamage>()
            .FirstOrDefault(c => c.DamagerId == PeriodicDamage.DamageTypes.Fire);

        if (damager != null)
            damager.Impose();
        else
            Debug.Log("Бля сука не могу найти периодический дамагер");
    }

    private void AddFatalLightingIfRequired(Transform target)
    {
        if (!_manager.CheckActive("fatal-lighting")) return;
        var lighting = GetComponent<FatalLighting>();
        lighting.Emit(target);
    }

    void JumpKill()
    {
        checkEnemiesToDamageJump =
            Physics2D.OverlapArea(jumpAttackPosLeftUp.position, jumpAttackPosRightDown.position, whatIsEnemies);
        enemiesToDamageJump =
            Physics2D.OverlapAreaAll(jumpAttackPosLeftUp.position, jumpAttackPosRightDown.position, whatIsEnemies);
        if (checkEnemiesToDamageJump && !player.isGrounded && !player.isDead && !playerClimbX.isHanging &&
            rb.velocity.y < -10f)
        {
            attackDetails[0] = baseAttackDamage;
            attackDetails[1] = transform.position.x;
            foreach (var collider in enemiesToDamageJump)
            {
                if (collider.TryGetComponent(out NJumpDamageReceiver receiver))
                {
                    if (receiver.JumpDamage(attackDetails))
                        rb.velocity = Vector2.up * reboundForce;
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(
            new Vector2((attackPosLeftUp.position.x + attackPosRightDown.position.x) / 2,
                (attackPosLeftUp.position.y + attackPosRightDown.position.y) / 2),
            new Vector2((attackPosRightDown.position.x - attackPosLeftUp.position.x),
                (attackPosLeftUp.position.y - attackPosRightDown.position.y)));
        Gizmos.DrawWireCube(
            new Vector2((jumpAttackPosLeftUp.position.x + jumpAttackPosRightDown.position.x) / 2,
                (jumpAttackPosLeftUp.position.y + jumpAttackPosRightDown.position.y) / 2),
            new Vector2((jumpAttackPosRightDown.position.x - jumpAttackPosLeftUp.position.x),
                (jumpAttackPosLeftUp.position.y -
                 jumpAttackPosRightDown.position
                     .y))); //draws a rectangle inside of which enemies get their asses whuped;
    }

    void PushForward()
    {
        //var a = Vector2.SmoothDamp(rb.velocity, transform.right * pushForwardMultiplier, ref sped, 0.2f);
        if (isPushingForward)
        {
            pushForwardTimeLeft -= Time.deltaTime;
            rb.velocity = new Vector2(transform.right.x * pushForwardMultiplier * Time.deltaTime, -0.5f);
            if (pushForwardTimeLeft <= 0)
            {
                isPushingForward = false;
                pushForwardTimeLeft = pushForwardTime;
            }
        }
        // print(rb.velocity);
        // print("10");
        //print(a);
    }
    
    
    

    
}