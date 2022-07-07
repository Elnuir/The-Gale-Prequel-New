using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlignWithMainCam : MonoBehaviour
{
    public float Frequency = 0.1f;

    void LateUpdate()
    {
        if (ActionEx.CheckCooldown(LateUpdate, Frequency))
        {
            var cam = Camera.main;
            var pos = cam.gameObject.transform.position;

            transform.position = new Vector3(pos.x, pos.y, transform.position.z);
        }
    }
}
