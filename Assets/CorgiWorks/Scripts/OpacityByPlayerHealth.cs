using System.Collections;
using System.Collections.Generic;
using MoreMountains.CorgiEngine;
using UnityEngine;
using UnityEngine.UI;

public class OpacityByPlayerHealth : MonoBehaviour
{
   private Health _playerHealth;
    public Image Target;
    public float maxHealth;
    public float minHealth;
    public float minOpacity;
    public float maxOpacity;

    public string PlayerTag;

    // Update is called once per frame
    void Update()
    {
        if (!_playerHealth && ActionEx.CheckCooldown(Update, 0.3f))
            _playerHealth = GameObject.FindGameObjectWithTag(PlayerTag).GetComponentInChildren<Health>();

        if (_playerHealth)
        {
            var tempColor = Target.color;
            tempColor.a = 1 - GetOpacity(_playerHealth.CurrentHealth);
            Target.color = tempColor;
        }
    }


    float GetOpacity(float health)
    {
        if (health <= minHealth)
            return minOpacity;

        if (health >= maxHealth)
            return 1;

        var percent = (health - minHealth) / (maxHealth - minHealth);
        return minOpacity + percent * (maxOpacity - minOpacity);
    }}
