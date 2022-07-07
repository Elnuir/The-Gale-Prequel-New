using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LuckyOneX : AchievementX
{
    public char SpecialCharacter = '$';
    public int TargetKillsInRow = 3;
    
    public MobTrackX MobTrack;
    private float previousHealth;
    private PlayerStats _stats;
    private int killsInRow;

    private int BestResult
    {
        get => PlayerPrefs.GetInt($"{Name}-best");
        set => PlayerPrefs.SetInt($"{Name}-best", value);
    }

    public override float Progress => Mathf.Clamp01((float)BestResult / TargetKillsInRow);

    protected void Start()
    {
        _stats = FindObjectOfType<PlayerStats>();
        MobTrack.MobKilled += OnMobKilled;
        previousHealth = _stats.currentHealth;
        Description = Description.Replace(SpecialCharacter.ToString(), TargetKillsInRow.ToString());
    }

    private void OnMobKilled(AchievementHandler.MobType obj)
    {
        if (Math.Abs(_stats.currentHealth - previousHealth) < 0.001f)
        {
            killsInRow++;
            if (killsInRow > BestResult)
                BestResult = killsInRow;
        }

        if (TargetKillsInRow == killsInRow)
            IsAchieved = true;
        previousHealth = _stats.currentHealth;
    }

}