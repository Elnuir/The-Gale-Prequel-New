using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShakeProxy : MonoBehaviour
{
    private CameraShake shake;
    public float DamageShakeAmplitude = 4;
    public float DamageShakeTime = 0.1f;

    private void Start()
    {
        shake = FindObjectOfType<CameraShake>();
        if(!shake )
            Debug.LogWarning("Can't find camera shake");
    }

    public void DamageShake()
    {
       shake.Shake(DamageShakeAmplitude, DamageShakeTime); 
    }
}
