using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPointSaveCheat : CheatBase
{
    public const string PREFIX = "player-pos-";

    private void Start()
    {
    }

    protected override void ActivateCheat()
    {
        base.ActivateCheat();

        var player = GameObject.FindGameObjectWithTag("Player");
        SavePosition(player.transform.position);
    }

    protected override void DeactivateCheat()
    {
        base.DeactivateCheat();
        ActivateCheat();
    }

    private void SavePosition(Vector3 position)
    {
        PlayerPrefs.SetFloat(PREFIX + "X", position.x);
        PlayerPrefs.SetFloat(PREFIX + "Y", position.y);
        PlayerPrefs.SetFloat(PREFIX + "Z", position.z);
    }

    private void DeletePosition()
    {
        PlayerPrefs.DeleteKey(PREFIX + "X");
        PlayerPrefs.DeleteKey(PREFIX + "Y");
        PlayerPrefs.DeleteKey(PREFIX + "Z");
    }
}