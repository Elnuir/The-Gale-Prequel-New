using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder.MeshOperations;

public class TriggerPeriodicDamage : MonoBehaviour
{
    [SerializeField] float touchDamage;
    private PlayerStats playerStats;
    private float[] attackDetails = new float[2];
    [SerializeField] float touchDamageCooldownBase;

    private float
        touchDamageCooldown =
            0.3f; //Hardcoded value for 1st ever damage. Thus the player can firstly jumpy into water and get damage 100%, no bullshit. Otherwise he'll be able to climp up from pit of water not getting any damage, or be knocked back from pit, if cooldown is 0

    private bool isDamaging;
    private bool doDamage;

    private void Start()
    {
        playerStats = FindObjectOfType<PlayerStats>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
            isDamaging = true;
    }


    private void Update()
    {
        if (isDamaging)
        {
            // touchDamageCooldown -= Time.deltaTime;
            // if (touchDamageCooldown <= 0)
            // {
            //     DoDamage();
            //     touchDamageCooldown = touchDamageCooldownBase;
            // }
            if(ActionEx.CheckCooldown(DoDamage, touchDamageCooldownBase))
                DoDamage();
        }
        // else
        // {
        //     touchDamageCooldown = 0.3f;
        // }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
            isDamaging = false;
    }

    void DoDamage()
    {
        attackDetails[0] = touchDamage;
        attackDetails[1] = transform.position.x;
        playerStats.SendMessage("Damage", attackDetails);
    }
}