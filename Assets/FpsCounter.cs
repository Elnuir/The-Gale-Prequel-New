using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FpsCounter : MonoBehaviour
{
    public Text display_Text;

    public void Update()
    {
        if (ActionEx.CheckCooldown(Update, 0.5f))
        {
            float current = (int)(1f / Time.unscaledDeltaTime);
            display_Text.text = current.ToString() ;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        QualitySettings.vSyncCount = 0;
    }
}