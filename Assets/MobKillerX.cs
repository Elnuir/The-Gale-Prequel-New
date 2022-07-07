using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using static AchievementHandler;

public class MobKillerX : AchievementX
{
    public int TargetKills;
    public char SpecialCharacter = '$';
    public MobTrackX MobTrack;

    public MobType[] WhoToKill;

    public override float Progress => Mathf.Clamp01((float)GetKillCount() / TargetKills);


    protected void Start()
    {
        MobTrack.MobKilled += MobKilledHandler;
        Description = Description.Replace(SpecialCharacter.ToString(), TargetKills.ToString());
    }

    private void MobKilledHandler(MobType type)
    {
        if (WhoToKill.Contains(type))
        {
            int totalKilled = GetKillCount();

            if (totalKilled == TargetKills)
                IsAchieved = true;
        }
    }

    private int GetKillCount()
    {
        int result = 0;
        foreach (var m in WhoToKill)
        {
            result += MobTrack.GetTotalKillCount(m);
        }

        return result;
    }
}