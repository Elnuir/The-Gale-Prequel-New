using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class AchievementX : MonoBehaviour
{
    public string Name;
    public string Description;
    [HideInInspector] public abstract float Progress { get; }

    public UnityEvent Achieved;
    protected string IsAchievedPrefsKey => $"{Name}-achieved";

    public bool IsAchieved
    {
        get => PlayerPrefs.GetInt(IsAchievedPrefsKey) == 1;
        set
        {
            if (IsAchieved == value) return;
            PlayerPrefs.SetInt(IsAchievedPrefsKey, value ? 1 : 0); 
            Achieved?.Invoke();
        }
    }
}