using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpCheat : CheatBase
{
    public int ExtraJumps = 100;

    private PlayerJump _playerJump;
    private int _initExtraJumps;
    private int _initExtraJumpsValue;

    private void Start()
    {
        _playerJump = FindObjectOfType<PlayerJump>();
        _initExtraJumpsValue = _playerJump.extraJumpsValue;
        _initExtraJumps = _playerJump.extraJumps;
    }

    protected override void ActivateCheat()
    {
        base.ActivateCheat();
        _playerJump.extraJumpsValue = ExtraJumps;
        _playerJump.extraJumps = ExtraJumps;
    }

    protected override void DeactivateCheat()
    {
        base.DeactivateCheat();
        _playerJump.extraJumps = _initExtraJumps;
        _playerJump.extraJumpsValue = _initExtraJumpsValue;
    }
}
