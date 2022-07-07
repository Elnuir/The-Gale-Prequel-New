using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LimitedPlayerTargetProvider : TargetProviderBase
{
    public string PlayerTag = "Player";
    public Rect Area;
    private GameObject _player;


    public override Transform GetTarget()
    {
        if (_player == null)
            return null;

        if (Area.Contains(_player.transform.position))
            return _player.transform;
        return null;
    }

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag(PlayerTag);
    }

    private void OnDrawGizmos()
    {
        var pos = transform.position;
        Gizmos.color = Color.gray;
        Gizmos.DrawWireCube(Area.center, Area.size);
    }
}