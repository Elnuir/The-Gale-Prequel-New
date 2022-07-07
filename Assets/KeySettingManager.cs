using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class KeySettingManager : MonoBehaviour
{
    public const string JUMP_BUTTON = "JUMP_BUTTON";
    public const string LEFT_BUTTON = "LEFT_BUTTON";
    public const string RIGHT_BUTTON = "RIGHT_BUTTON";
    public const string UP_BUTTON = "UP_BUTTON";
    public const string DOWN_BUTTON = "DOWN_BUTTON";
    public const string DASH_BUTTON = "DASH_BUTTON";

    public const string SHIELD_BUTTON = "SHIELD_BUTTON";
    public const string MANUAL_SHIELD_BUTTON = "MANUAL_SHIELD_BUTTON";
    public const string ATTACK_BUTTON = "ATTACK_BUTTON";
    public const string LIGHT_TORCH_BUTTON = "LIGHT_TORCH_BUTTON";
    public const string GRENADE_BUTTON = "GRENADE_BUTTON";
    public const string HOLY_WATER_BUTTON = "HOLY_WATER_BUTTON";
    public const string OPEN_HUB_ITEM = "OPEN_HUB_ITEM"; 

    private static readonly Dictionary<string, KeyCode> defaults = new Dictionary<string, KeyCode>()
    {
        { JUMP_BUTTON, KeyCode.Space },
        { LEFT_BUTTON, KeyCode.A },
        { RIGHT_BUTTON, KeyCode.D },
        { UP_BUTTON, KeyCode.W },
        { DOWN_BUTTON, KeyCode.S },
        { DASH_BUTTON, KeyCode.LeftShift },
        { SHIELD_BUTTON, KeyCode.Alpha3 },
        { MANUAL_SHIELD_BUTTON, KeyCode.CapsLock },
        { ATTACK_BUTTON, KeyCode.Mouse0 },
        { LIGHT_TORCH_BUTTON, KeyCode.Alpha4 },
        { GRENADE_BUTTON, KeyCode.R },
        { HOLY_WATER_BUTTON, KeyCode.Mouse1 },
        { OPEN_HUB_ITEM, KeyCode.F  },
    };

    public static KeyCode GetKeyCodeByName(string name)
    {
        if (PlayerPrefs.HasKey(name))
            return (KeyCode)PlayerPrefs.GetInt(name);

        if (defaults.ContainsKey(name))
            return defaults[name];

        Debug.LogError($"БЛЯТЬ ЧЕ ЗА КНОПКА Я НЕ ЗНАЮ {name}");
        return default;
    }

    public static void SetKeyByName(string name, KeyCode newValue)
    {
        PlayerPrefs.SetInt(name, (int)newValue);
    }
}