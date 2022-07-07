using System.Collections;
using System.Collections.Generic;
using MoreMountains.CorgiEngine;
using UnityEngine;

public class JumpBlocker : MonoBehaviour
{
    private CharacterJump Jump;
    private float blockTimeLeft;


    private void Start()
    {
        Jump = GetComponentInParent<CharacterJump>();
    }
    public void BlockJump(float time)
    {
        Jump.AbilityPermitted = false;

        if (time > blockTimeLeft)
            blockTimeLeft = time;
    }

    private void Update()
    {
        if (blockTimeLeft > 0)
            blockTimeLeft -= Time.deltaTime;
        else
            Jump.AbilityPermitted = true;
    }
}
