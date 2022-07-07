using System.Collections;
using System.Collections.Generic;
using MoreMountains.Feedbacks;
using UnityEngine;
using UnityEngine.Events;

public class PressAnyKey : MonoBehaviour
{
    public UnityEvent Triggered;

    // Update is called once per frame
    void OnGUI()
    {
        if (Input.anyKeyDown)
        {
            Triggered?.Invoke();
        }
    }
}