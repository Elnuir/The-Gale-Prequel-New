using System.Collections;
using System.Collections.Generic;
using MoreMountains.CorgiEngine;
using UnityEngine;
using UnityEngine.Events;

public class PlayerHealthOnOff : OnOff
{
    public float MinPlayerHealth;
    public float MaxPlayerHealth;
    public string PlayerTag;
    private Health _playerHealth;


    // Update is called once per frame
    void Update()
    {
        if (_playerHealth && ShouldFlip())
        {
            Flip();
            return;
        }

        if (!_playerHealth && ActionEx.CheckCooldown(Update, 0.3f))
            _playerHealth = GameObject.FindGameObjectWithTag(PlayerTag).GetComponentInChildren<Health>();
    }

    private bool ShouldFlip()
    {
        float health = _playerHealth.CurrentHealth;
        bool shouldBeEnabled = health >= MinPlayerHealth && health <= MaxPlayerHealth;
        return shouldBeEnabled != IsEnabled;
    }
}
