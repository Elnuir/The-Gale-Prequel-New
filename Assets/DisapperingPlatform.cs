using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class DisapperingPlatform : MonoBehaviour
{
    private BoxCollider2D boxTrigger2D;
    private BoxCollider2D boxCollider2D;
    private SpriteRenderer spriteRenderer;
    [SerializeField] private float moveSpeed;
    [SerializeField] private Vector3 movePosition; 
    private Vector3 startPosition;
    [Range(0, 1)] private float moveProgress;
    private bool isShaking;
    [SerializeField] private bool isAppearable;
    [SerializeField] private float shakeTime;
    private float shakeTimeLeft;
    private bool shaked;
    [SerializeField] private float notApearrenceCooldown;
    private float noAppearenceCooldownLeft;
    void Start()
    {
        boxTrigger2D = GetComponents<BoxCollider2D>().FirstOrDefault(c => c.isTrigger);
        boxCollider2D = GetComponents<BoxCollider2D>().FirstOrDefault(c => !c.isTrigger);
        spriteRenderer = GetComponent<SpriteRenderer>();
        shakeTimeLeft = shakeTime;
        noAppearenceCooldownLeft = notApearrenceCooldown;
        startPosition = transform.position;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isShaking = true;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            var climbing = other.GetComponent<PlayerClimbX>();
            if (climbing && climbing.isHanging && !isShaking)
            {
                isShaking = true;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        AfterShakeCheck();
    }

    void Move()
    {
        if (isShaking)
        {
            shakeTimeLeft -= Time.deltaTime;
            if(shakeTimeLeft >= 0)
            {
                moveProgress = Mathf.PingPong(Time.time * moveSpeed, 1);
                Vector3 offset = movePosition * moveProgress;
                transform.position = startPosition + offset;
            }
            else
            {
                shakeTimeLeft = shakeTime;
                isShaking = false;
                shaked = true;
            }
            
        }
    }

    void AfterShakeCheck()
    {
        if (isAppearable && shaked)
        {
            spriteRenderer.enabled = false; 
            boxCollider2D.enabled = false;
            boxTrigger2D.enabled = false;
            noAppearenceCooldownLeft -= Time.deltaTime;
            if (noAppearenceCooldownLeft <= 0)
            {
                spriteRenderer.enabled = true;
                boxCollider2D.enabled = true;
                boxTrigger2D.enabled = true;
                noAppearenceCooldownLeft = notApearrenceCooldown;
                shaked = false;
            }
        }
        else if (!isAppearable && shaked)
        {
            Destroy(gameObject);
        }
    }
}
