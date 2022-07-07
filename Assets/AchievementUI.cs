using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UI;

public class AchievementUI : MonoBehaviour
{
    public AchievementX Target;

    public Text WriteNameTo;
    public Text WriteDescriptionTo;
    public Slider WriteProgressTo;

    // Start is called before the first frame update
    void Start()
    {
        if (!Target)
            Target = GetComponentInChildren<AchievementX>();
        
        if (WriteNameTo)
            WriteNameTo.text = Target.Name;

        if (WriteDescriptionTo)
            WriteDescriptionTo.text = Target.Description;

        if (WriteProgressTo )
            WriteProgressTo.value = Target.Progress;
    }
}