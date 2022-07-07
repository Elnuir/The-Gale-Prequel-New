using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    public int extraJumps; //Resposible for double-triple-etc. jumps
    public int extraJumpsValue; // 
    public bool isSecondJump;
    private Player player;
    private Rigidbody2D rb;
    [SerializeField] float jumpForce; //
    private bool isJumpRestricted;
    private PlayerClimbX playerClimbX;

    // Start is called before the first frame update
    void Start()
    {
        extraJumps = extraJumpsValue;
        player = GetComponent<Player>();
        rb = GetComponent<Rigidbody2D>();
        playerClimbX = GetComponent<PlayerClimbX>();
    }

    // Update is called once per frame
    void Update()
    {
        if (extraJumps == 0 && !player.isDead && !isJumpRestricted && !playerClimbX.isHanging && !playerClimbX.isClimbing)
        {
            isSecondJump = true;
        }

        if (Input.GetKeyDown(KeySettingManager.GetKeyCodeByName(KeySettingManager.JUMP_BUTTON)) && extraJumps > 0 && !player.isDead && !isJumpRestricted && !playerClimbX.isHanging && !playerClimbX.isClimbing)
        {
            JumpPerform();
            ;
        }
        else
            JumpChecker();
    }

    void JumpChecker()
    {
        if (!GameManager.gameIsPaused)
        {
            if ((player.isGrounded && extraJumps != extraJumpsValue && rb.velocity.y < 0.1f) || playerClimbX.isHanging || playerClimbX.isClimbing) //|| playerClimb.isHooked)
            {
                extraJumps = extraJumpsValue; //Sets amount of extra jumps when you are on the ground
                isSecondJump = false;
            }
        }
    }

    public void JumpPerform()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce); //Vector2.up * jumpForce; //makes extra jump
        //if(!playerHook.isSliding)
        extraJumps--;
    }
}