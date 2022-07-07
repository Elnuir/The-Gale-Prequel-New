using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseSpeedOverTime : MonoBehaviour
{
    public WaypointMovement Target;
    public float Amount;
    public float Interval;
    private bool isFirstCall = true;
    public float MaxSpeed;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (ActionEx.CheckCooldown(Update, Interval))
            AddSpeed();
    }

    void AddSpeed()
    {
        if (isFirstCall)
        {
            isFirstCall = false;
            return;
        }

        if (Target.Speed < MaxSpeed)
            Target.Speed += Amount;
    }
}