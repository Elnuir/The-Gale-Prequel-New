using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableTriggerOnOff : OnOff
{
    public string ActionButton;
    private bool IsPlayerInside;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        IsPlayerInside = true;
    }


    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        IsPlayerInside = false;
    }
    

    private void OnGUI()
    {
        if (IsPlayerInside && Input.GetKeyDown(KeySettingManager.GetKeyCodeByName(KeySettingManager.LIGHT_TORCH_BUTTON)))
        {
            TurnOn();
        }
    }
}