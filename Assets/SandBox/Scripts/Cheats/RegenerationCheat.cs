using System;
using System.Collections;
using System.Collections.Generic;
using MoreMountains.CorgiEngine;
using UnityEngine;

public class RegenerationCheat : CheatBase
{
    public float Percent = 0.25f;
    public float Interval = 0.5f;
    public string PlayerTag = "Player";

    private Health _playerHealth;
    private Health PlayerHealth =>
     _playerHealth ?? (_playerHealth = GameObject.FindGameObjectWithTag(PlayerTag).GetComponentInChildren<Health>());


    private void Update()
    {
        if (IsActive && PlayerHealth.CurrentHealth < PlayerHealth.MaximumHealth)
            if (ActionEx.CheckCooldown(Update, Interval))
            {
                PlayerHealth.SetHealth((int)(PlayerHealth.CurrentHealth + (PlayerHealth.MaximumHealth - PlayerHealth.CurrentHealth) * Percent), gameObject);
            }
    }
}
