using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    private CinemachineVirtualCamera camera;
    private float shakeTimer;

    public void Awake()
    {
        camera = GetComponent<CinemachineVirtualCamera>();
    }

    public void Shake(float intensity, float time)
    {
        if (!camera)
        {
            camera = GetComponent<CinemachineVirtualCamera>();
        }
        var perlin = camera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        perlin.m_AmplitudeGain = intensity;
        shakeTimer = time;
    }

    private void Update()
    {
        if (shakeTimer > 0)
        {
            shakeTimer -= Time.deltaTime;
            if (shakeTimer <= 0)
            {
                var perlin = camera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
                perlin.m_AmplitudeGain = 0;
            }
        }
    }
}