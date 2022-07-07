using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class KeyBindingSetup : MonoBehaviour
{
    private bool isSetupMode;
    public string TargetKeyName;
    public Text WriteKeyTo;
    private List<KeyBindingSetup> others = new List<KeyBindingSetup>();

    private float CannotSetupTimer = 0;
    
    
    private void Start()
    {
        others.Clear();
        others.AddRange(FindObjectsOfType<KeyBindingSetup>());
    }

    private void OnEnable()
    {
        if (WriteKeyTo == null)
            WriteKeyTo = GetComponent<Text>();

        var key = KeySettingManager.GetKeyCodeByName(TargetKeyName);
        WriteKeyTo.text = key.ToString();
    }


    public void RunSetupMode()
    {
        if(isSetupMode || CannotSetupTimer > 0) return;
        
        foreach (var a in others)
            a.CancelSetupMode();

        
        isSetupMode = true;
        var key = KeySettingManager.GetKeyCodeByName(TargetKeyName);
        WriteKeyTo.text = key.ToString() + " - setup";
    }

    public void CancelSetupMode()
    {
        isSetupMode = false;
        var key = KeySettingManager.GetKeyCodeByName(TargetKeyName);
        WriteKeyTo.text = key.ToString();
    }

    private void Update()
    {
        if (CannotSetupTimer > 0)
            CannotSetupTimer -= Time.unscaledDeltaTime;
    }


    void OnGUI()
    {
        if (isSetupMode)
        {
            if (Event.current.isKey && Event.current.type == EventType.KeyDown)
            {
                if (Event.current.keyCode == KeyCode.Escape)
                {
                    CancelSetupMode();
                    return;
                }

                CompleteSetup(Event.current.keyCode);
            }

            if (Event.current.isMouse && Event.current.type == EventType.MouseDown)
            {
                CompleteSetup(KeyCode.Mouse0 + Event.current.button);
            }
        }
    }

    private bool CheckConflicts(KeyCode pressedKey)
    {
        foreach (var a in others)
        {
            if (a != this && KeySettingManager.GetKeyCodeByName(a.TargetKeyName) == pressedKey)
            {
                return false;
            }
        }

        return true;
    }

    private void CompleteSetup(KeyCode key)
    {
        CannotSetupTimer = 0.1f;

        if (CheckConflicts(key))
        {
            KeySettingManager.SetKeyByName(TargetKeyName, key);
            WriteKeyTo.text = key.ToString();
            isSetupMode = false;
        }
        else
        {
            WriteKeyTo.text = key + " - conflict!";
            isSetupMode = false;
            Invoke(nameof(OnEnable), 0.75f);
        }
    }

    private void SetSetupModeToFalse()
    {
        isSetupMode = false;
    }
}