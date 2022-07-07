using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SpriteFiller : MonoBehaviour
{
    private Abilities abilities;
    private Image image;
    private float fillAmount = 0f;
    private float maxFillAmount;

    private float FillAmount
    {
        get
        {
           return fillAmount;
        }
        set
        {
            fillAmount = value;
            image.fillAmount = FillAmount;
        }
    }

    public enum abilityTypeEnum
    {
        Shield,
        Grenage
    }

    public abilityTypeEnum abilityType;
    void Start()
    {
        abilities = FindObjectOfType<Abilities>();
        image = GetComponent<Image>();
        image.fillAmount = FillAmount;
        if (abilities == null)
        {
            Debug.LogError("Блять, филлер не нашел игрока, пиздец");
        }

        // if (abilityType.ToString() == abilityTypeEnum.Grenage.ToString())
        // {
        //
        // }
        // else if (abilityType.ToString() == abilityTypeEnum.Shield.ToString())
        // {
        //     
        // }
        
            
    }

    // Update is called once per frame
    void Update()
    {
        AbilityCheck(abilityType.ToString());
    }

    void AbilityCheck(string ability)
    {
        if (ability == abilityTypeEnum.Shield.ToString() && abilities.IsShieldUSAGECoolingDown)
        {
            FillAmount = 0f + abilities.shieldUSAGECoolDown / abilities.shieldUSAGECoolDownBase;
        }
        if (ability == abilityTypeEnum.Grenage.ToString() && abilities.IsGrenadeUSAGECoolingDown)
        {
            FillAmount = 0f + abilities.grenadeUSAGECoolDown / abilities.grenadeUSAGECoolDownBase;
        }
    }
}
