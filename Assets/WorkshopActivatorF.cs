using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WorkshopActivatorF : MonoBehaviour
{
    public bool isInsideTrigger;
    public UnityEvent Activated;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isInsideTrigger = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isInsideTrigger = false;
        }
    }

    private void OnGUI()
    {
        if (isInsideTrigger &&
            Input.GetKeyDown(KeySettingManager.GetKeyCodeByName(KeySettingManager.OPEN_HUB_ITEM)))
        {
            Activated?.Invoke();
        }
    }
}