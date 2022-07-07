using System.Collections;
using System.Collections.Generic;
using MoreMountains.CorgiEngine;
using UnityEngine;
using UnityEngine.UI;

public class RectSizeByPlayerHealth : MonoBehaviour
{
    private Health _playerHealth;
    public Image Target;
    public float maxHealth;
    public float minHealth;
    public float minSizeScale;
    public float maxSizeScale;

    private float InitWidth;
    private float InitHeight;

    public string PlayerTag;
    public Vector2 TargetRectSize;
    public Vector2 currVelocity;

    private void Start()
    {
        InitWidth = ((RectTransform)transform).localScale.x;
        InitHeight = ((RectTransform)transform).localScale.y;
    }
    // Update is called once per frame
    void Update()
    {
        if (!_playerHealth && ActionEx.CheckCooldown(Update, 0.3f))
            _playerHealth = GameObject.FindGameObjectWithTag(PlayerTag).GetComponentInChildren<Health>();

        if (_playerHealth)
        {
            float multiplier = GetOpacity(_playerHealth.CurrentHealth);
            TargetRectSize = new Vector2(InitWidth * multiplier, InitHeight * multiplier);
        }

        if (Vector2.Distance(Target.rectTransform.localScale, TargetRectSize) > 0.01f)
        {
            Target.rectTransform.localScale = Vector2.SmoothDamp(Target.rectTransform.localScale, TargetRectSize, ref currVelocity, 0.2f);
        }
    }


    float GetOpacity(float health)
    {
        if (health <= minHealth)
            return minSizeScale;

        if (health >= maxHealth)
            return maxSizeScale;

        var percent = (health - minHealth) / (maxHealth - minHealth);
        return minSizeScale + percent * (maxSizeScale - minSizeScale);
    }
}
