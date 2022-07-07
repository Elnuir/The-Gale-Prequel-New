using System.Collections;
using System.Collections.Generic;
using CorgiWorks.Scripts;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class KeyBindingSetupX : MonoBehaviour
{
    private bool isSetupMode;
    public string TargetKeyName;
    public Text WriteKeyTo;
    private List<KeyBindingSetupX> others = new List<KeyBindingSetupX>();

    private float CannotSetupTimer = 0;
    private InputManagerX _inputManager;
    
    
    private void Start()
    {
       
        others.Clear();
        others.AddRange(FindObjectsOfType<KeyBindingSetupX>());
    }

    private void OnEnable()
    {
        _inputManager = FindObjectOfType<InputManagerX>();
        if(!_inputManager)
            Debug.Log("Блять где инпут манагер Х?");
        
        if (WriteKeyTo == null)
            WriteKeyTo = GetComponent<Text>();

        var key = _inputManager.KeyButtonMap[TargetKeyName];
        WriteKeyTo.text = key.ToString();
    }


    public void RunSetupMode()
    {
        if(isSetupMode || CannotSetupTimer > 0) return;
        
        foreach (var a in others)
            a.CancelSetupMode();

        
        isSetupMode = true;
        var key = _inputManager.KeyButtonMap[TargetKeyName];
        WriteKeyTo.text = key.ToString() + " - setup";
    }

    public void CancelSetupMode()
    {
        isSetupMode = false;
        var key = _inputManager.KeyButtonMap[TargetKeyName];
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
            if (a != this && _inputManager.KeyButtonMap[a.TargetKeyName] == pressedKey)
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
            _inputManager.KeyButtonMap[TargetKeyName] = key;
            _inputManager.Save();
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
