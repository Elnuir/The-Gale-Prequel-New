using UnityEngine;

public class TimeOnOff : OnOff
{
    public float OnInSeconds = -1f;
    public float OffInSeconds = -1f;

    private float startTick;
    private void OnEnable()
    {
        startTick = Time.realtimeSinceStartup ;
    }

    private void OnGUI()
    {
        if (OnInSeconds > 0 && Time.realtimeSinceStartup >= startTick + OnInSeconds)
            TurnOn();

        if (OffInSeconds > 0 && Time.realtimeSinceStartup >= startTick + OffInSeconds)
            TurnOff();
    }
}