using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public sealed class MobTrackX : MonoBehaviour  
{
    public event Action<AchievementHandler.MobType> MobKilled;
    private AchievementHandler[] Mobs = Array.Empty<AchievementHandler>();

    const string PrefsPrefix = "mob-killed";

    private void Start()
    {
        var scene = SceneManager.GetActiveScene();
        Mobs = Resources.FindObjectsOfTypeAll<AchievementHandler>()
            .Where(m => m.gameObject.scene == scene)
            .ToArray();
        
        foreach (var mob in Mobs)
            mob.Died.AddListener(MobDiedEventHandler);
    }

    private void MobDiedEventHandler(AchievementHandler.MobType type)
    {
        MobKilled?.Invoke(type);

        int current = GetTotalKillCount(type);
        SetTotalKillCount(type, current + 1);
    }

    private void SetTotalKillCount(AchievementHandler.MobType type, int value)
    {
        PlayerPrefs.SetInt(PrefsPrefix + (int)type, value);
    }

    public int GetTotalKillCount(AchievementHandler.MobType type)
    {
        return PlayerPrefs.GetInt(PrefsPrefix + (int)type);
    }
}