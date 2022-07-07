using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerDamageReceiver : MonoBehaviour
{
    public float ThornsDamage = 15;    
    bool isBlocking;

    private Player player;

    private PlayerStats playerStats;

    private Animator animator;

    public UnityEvent GotDamage;
    
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Player>();
        playerStats = GetComponent<PlayerStats>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        BlockCheck();
    }
    public void NewDamage(AttackDetails attackDetails)
        {
            if (!player.GetDashStatus() && !player.isDead && !isBlocking)
            {
                int direction;
                playerStats.DecreaseHealth(attackDetails.damageAmount); //YeAH RIGHT FOCKING HERE<-------
                GotDamage?.Invoke();
                if (attackDetails.attackerX < transform.position.x)
                {
                    direction = 1;
                }
                else
                {
                    direction = -1;
                }
                
    
                // THORNS
                if (GetComponent<Abilities>().IsShieldUSAGECoolingDown)
                    if (IsFacingAt(attackDetails.Attacker.gameObject))
                    {
                        float[] thornsDetails = new float[]
                        {
                            ThornsDamage,
                            transform.position.x
                        };
                        attackDetails.Attacker.BroadcastMessage("Damage", thornsDetails);
                    }
    
                player.KnockBack(direction);
            }
        }
    private void Damage(float[] attackDetails)
    {
        if (!player.GetDashStatus() && !player.isDead && !isBlocking)
        {
            int direction;
            playerStats.DecreaseHealth(attackDetails[0]); //YeAH RIGHT FOCKING HERE<-------
            GotDamage?.Invoke();
            if (attackDetails[1] < transform.position.x)
            {
                direction = 1;
            }
            else
            {
                direction = -1;
            }

            player.KnockBack(direction);
        }
    }

    private bool IsFacingAt(GameObject obj)
    {
        if (player.facingRight)
            return obj.transform.position.x > transform.position.x;
        else
            return obj.transform.position.x < transform.position.x;
    }
    void BlockCheck()
    {
        animator.SetBool("isBlocking", isBlocking);
        if (Input.GetKey(KeyCode.CapsLock))
            isBlocking = true;
        else
            isBlocking = false;
    }
}
