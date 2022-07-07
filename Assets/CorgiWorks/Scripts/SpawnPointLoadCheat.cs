using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPointLoadCheat : CheatBase
{
    private void Start()
    {
    }

    protected override void ActivateCheat()
    {
        base.ActivateCheat();

        var player = GameObject.FindGameObjectWithTag("Player");

        var position = LoadPosition();
        if (position != null)
            player.transform.position = position.Value;
    }

    protected override void DeactivateCheat()
    {
        base.DeactivateCheat();
        ActivateCheat();
    }

    private Vector3? LoadPosition()
    {
        if (!PlayerPrefs.HasKey(SpawnPointSaveCheat.PREFIX + "X")) return null;

        float x = PlayerPrefs.GetFloat(SpawnPointSaveCheat.PREFIX + "X");
        float y = PlayerPrefs.GetFloat(SpawnPointSaveCheat.PREFIX + "Y");
        float z = PlayerPrefs.GetFloat(SpawnPointSaveCheat.PREFIX + "Z");
        return new Vector3(x, y, z);
    }
}