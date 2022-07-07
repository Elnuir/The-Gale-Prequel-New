using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTriggerAnimation : MonoBehaviour
{
    private Animator animator;
    [SerializeField] private BoxCollider2D boxCollider2D;
    [SerializeField] private float spikesUpTime;
    private float spikesUpTimeLeft;
    [SerializeField] private float spikesDownTime;
    float spikesDownTimeLeft;
    private bool isDownCoolingDown = true;
    private bool isActive;
    void Start()
    {
        animator = GetComponent<Animator>();
       // boxCollider2D = GetComponentInParent<BoxCollider2D>();
        spikesDownTimeLeft = spikesDownTime;
        spikesUpTimeLeft = spikesUpTime;
    }

    // Update is called once per frame
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isActive = true;
        }

    }

    void Update()
    {
        if (isActive)
        {
            if (!isDownCoolingDown)
            {
                animator.SetBool("IsUp", true);
                spikesUpTimeLeft -= Time.deltaTime;
                boxCollider2D.enabled = true;
            }
            if (spikesUpTimeLeft <= 0 && !isDownCoolingDown)
            {
//                print("SPIKESRRR");
                animator.SetBool("IsUp", false);
                boxCollider2D.enabled = false;
                isDownCoolingDown = true;
                spikesUpTimeLeft = spikesUpTime;
            }

            else if (isDownCoolingDown)
            {
                spikesDownTimeLeft -= Time.deltaTime;
                if (spikesDownTimeLeft <= 0)
                {
                   // print("SPIKEOOOO");
                    isDownCoolingDown = false;
                    spikesDownTimeLeft = spikesDownTime;
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        isActive = false;
        isDownCoolingDown = false;
        spikesDownTimeLeft = spikesDownTime;
        spikesUpTimeLeft = spikesUpTime;
        animator.SetBool("IsUp", false);
    }

}
