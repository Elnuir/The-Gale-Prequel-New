using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTriggerEmmision : MonoBehaviour
{
    private BoxCollider2D boxCollider2D;
    private ParticleSystem particleSystem;
    private float rateoverTimeBase;
    [SerializeField] float onEnterClamp;
    private ParticleSystem.EmissionModule emissionModule;
    [SerializeField] float increaseMultiplier;
  //  [SerializeField] private float timeToIncreaseParticles;
  //  private float timeToIncreaseParticlesLeft;
    private bool isIncreasing;

    
    void Start()
    {
        particleSystem = GetComponentInChildren<ParticleSystem>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        boxCollider2D.size = new Vector2(particleSystem.shape.radius * 2f, boxCollider2D.size.y);
        emissionModule = particleSystem.emission;
        rateoverTimeBase = emissionModule.rateOverTimeMultiplier;
      //  timeToIncreaseParticlesLeft = timeToIncreaseParticles;
    }

    // Update is called once per frame
    void Update()
    {
        IncreaseCheck();
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isIncreasing = true;
        }
        //   o.rateOverTimeMultiplier = 2f;
    }

    void IncreaseCheck()
    {
        if (isIncreasing)
        {
            emissionModule.rateOverTimeMultiplier = 
                Mathf.Clamp(emissionModule.rateOverTimeMultiplier += increaseMultiplier*Time.deltaTime, rateoverTimeBase, onEnterClamp);
        }
        else
        {
            emissionModule.rateOverTimeMultiplier = 
                Mathf.Clamp(emissionModule.rateOverTimeMultiplier -= increaseMultiplier*Time.deltaTime, rateoverTimeBase, onEnterClamp);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isIncreasing = false;
        }
    }
}
