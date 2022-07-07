using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OpenDoorByKills : MonoBehaviour
{
    public NEnemyHealth[] Enemies = Array.Empty<NEnemyHealth>();
    public int KillCount = 1;
    public UnityEvent Completed;

    // Start is called before the first frame update
    void Start()
    {
        foreach (var enemy in Enemies)
        {
           enemy.OnDeath += DecreasekKillCount; 
        }
    }

    private void DecreasekKillCount()
    {
        KillCount--;
        if(KillCount == 0)
            Completed?.Invoke();
    }

    // Update is called once per frame
    void Update()
    {
    }
}