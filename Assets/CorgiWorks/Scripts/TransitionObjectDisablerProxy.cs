using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionObjectDisablerProxy : MonoBehaviour
{
    public void EnableAll()
    {
        var disabler = GameObject.FindObjectOfType<TransitionObjectDisabler>();
        disabler.EnableAll();
    }

    public void DisableAll()
    {
        var disabler = GameObject.FindObjectOfType<TransitionObjectDisabler>();
        disabler.DisableAll();
    }
}
