using System;
using System.Collections;
using System.Collections.Generic;
using CorgiWorks.Scripts;
using MoreMountains.CorgiEngine;
using MoreMountains.Tools;
using UnityEngine;

public class CharacterShieldX : CharacterAbility
{
    private bool IsActive
    {
        get => Shield.activeSelf;
        set
        {
            Shield.SetActive(value);
            _health.ArmorAmount = value ? AmountAbsorbed : 0;
        }
    }

    private CharacterStaminaX _stamina;
    private new HealthX _health;
    public float StaminaConsumption;
    public int AmountAbsorbed;

    public GameObject Shield;

    private new void Start()
    {
        base.Start();
        _stamina = GetComponentInChildren<CharacterStaminaX>();
        _health = GetComponentInChildren<HealthX>();
    }

    protected override void HandleInput()
    {
        base.HandleInput();

        if (_inputManager.RunButton.State.CurrentState == MMInput.ButtonStates.ButtonDown)
            IsActive = !IsActive;
    }

    private void Update()
    {
        if (IsActive && ActionEx.CheckCooldown(Update, 0.5f))
        {
            bool canUse = _stamina.ConsumeStamina(StaminaConsumption);
            if (!canUse)
                IsActive = false;
        }
    }
}