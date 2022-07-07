using System.Collections;
using System.Collections.Generic;
using MoreMountains.CorgiEngine;
using UnityEngine;

public class MainCameraBlinder : MonoBehaviour
{
    public LayerMask BlindMode;
    private LayerMask _normalMode;

    private void Start()
    {
    }

    public void EnableBlindMode()
    {
        _normalMode = Camera.main.cullingMask;
        Camera.main.cullingMask = BlindMode;
        GameObject.FindGameObjectWithTag("Player").GetComponent<CorgiController>().enabled = false;
    }

    public void DisableBlindMode()
    {
        Camera.main.cullingMask = _normalMode;
        GameObject.FindGameObjectWithTag("Player").GetComponent<CorgiController>().enabled = true;
    }
}
