using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Abilities : MonoBehaviour
{
    private Player player;
    Throw throw1;
    private StaminaBar staminaBar;
    [SerializeField] float stamina;
    [SerializeField] private float maxStamina = 100;
    private float Stamina
    {
        get => stamina;
        set
        {
            stamina = value;
            staminaBar.SetStamina(stamina);
            
        }

    }

    #region Shield

    [SerializeField] GameObject shieldVFX;
    public float shieldACTIVECountDownBase;
    [SerializeField] float shieldCost;
    public float shieldUSAGECoolDownBase;
    bool isShieldUSAGECoolingDown, isShieldACTIVECountdown;
    public float shieldUSAGECoolDown;
    float shieldACTIVECountdown;
    
    // USED IN Combat manager
    public bool IsShieldUSAGECoolingDown
    {
        get => isShieldUSAGECoolingDown;
        set => isShieldUSAGECoolingDown = value;
    }
    private bool IsShieldACTIVECountingDown
    {
        get => isShieldACTIVECountdown;
        set => isShieldACTIVECountdown = value;
    }
    #endregion

    #region StickyGrenade

    public float grenadeUSAGECoolDownBase;
    public float grenadeCost;
    private bool isGrenadeUSAGECoolingDown;
    public float grenadeUSAGECoolDown;
    public bool IsGrenadeUSAGECoolingDown
    {
        get => isGrenadeUSAGECoolingDown;
        set => isGrenadeUSAGECoolingDown = value;
    }

    #endregion
    
    
    void Start()
    {
        player = GetComponent<Player>();
        staminaBar = FindObjectOfType<StaminaBar>();
        throw1 = FindObjectOfType<Throw>();
        staminaBar.SetMaxStamina(maxStamina);
        
        shieldUSAGECoolDown = shieldUSAGECoolDownBase;
        shieldACTIVECountdown = shieldACTIVECountDownBase;

        grenadeUSAGECoolDown = grenadeUSAGECoolDownBase;
    }

    // Update is called once per frame
    void Update()
    {
        GetStaminaToItsMax();
        CheckSetShieldCOOLDOWNToFalse();
        CheckSetShieldACTIVEToFalse();
        CheckSetGrenageUSAGEToFalse();
        ActivateShieldAbility();
        ActivateGrenadeAbility();

    }

    #region ActivationAbilities

    void ActivateGrenadeAbility()
    {

        if (!IsGrenadeUSAGECoolingDown && Input.GetKeyUp(KeyCode.R) && grenadeCost < Stamina)
        {
            Debug.Log("Grenade");
            //Invoke(nameof(CheckSetGrenageUSAGEToFalse), grenadeUSAGECoolDown);
            
            throw1.DirectionCheckGrenade();
            
        }
    }

    void ActivateShieldAbility()
    {
        if (!IsShieldUSAGECoolingDown && Input.GetKeyDown(KeyCode.E) && shieldCost < Stamina)
        {
            Debug.Log("enemy got damage");
           // Invoke(nameof(SettingShieldCOOLDOWNToFalse), shieldUSAGECoolDown);
          //  Invoke(nameof(SettingShieldACTIVEToFalse), shieldACTIVECountdown);
          shieldVFX.SetActive(true);
            IsShieldUSAGECoolingDown = true;
            IsShieldACTIVECountingDown = true;
            WithdrawStamina(shieldCost);
        }
    }

    #endregion
    
    #region SettingFalse

    #region Shield

    void CheckSetShieldCOOLDOWNToFalse()
    {
        if (IsShieldUSAGECoolingDown == true)
        {
            shieldUSAGECoolDown -= Time.deltaTime;
            if (shieldUSAGECoolDown <= 0f)
            {
                IsShieldUSAGECoolingDown = false;
                shieldUSAGECoolDown = shieldUSAGECoolDownBase;
            }
        }
       // IsShieldUSAGECoolingDown = false;
    }
    void CheckSetShieldACTIVEToFalse()
    {
        if (IsShieldACTIVECountingDown == true)
        {
            shieldACTIVECountdown -= Time.deltaTime;
            if (shieldACTIVECountdown <= 0f)
            {
                shieldVFX.SetActive(false);
                IsShieldACTIVECountingDown = false;
                shieldACTIVECountdown = shieldACTIVECountDownBase;
            }
        }
    }

    #endregion
    
    #region Grenade

    void CheckSetGrenageUSAGEToFalse()
    {
        if (IsGrenadeUSAGECoolingDown == true)
        {
            grenadeUSAGECoolDown -= Time.deltaTime;
            if (grenadeUSAGECoolDown <= 0f)
            {
                IsGrenadeUSAGECoolingDown = false;
                grenadeUSAGECoolDown = grenadeUSAGECoolDownBase;
            }
        }
    }

    #endregion
    
    #endregion
    public void WithdrawStamina(float cost)
    {
        Stamina = Mathf.Clamp(Stamina - cost, 0f, maxStamina);
    }

    void GetStaminaToItsMax()
    {
        Stamina = Mathf.Clamp(Stamina + Time.deltaTime, 0f, maxStamina);
    }
    
}
