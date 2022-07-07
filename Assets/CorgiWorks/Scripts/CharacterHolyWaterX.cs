using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using CorgiWorks.Scripts;
using MoreMountains.CorgiEngine;
using MoreMountains.Tools;
using UnityEngine;

public class CharacterHolyWaterX : CharacterAbility
{
    public float StaminaComsuption;
    public Transform SpawnPosition;
    public MMObjectPooler Pooler;
    public CharacterStaminaX Stamina;
    private Character _character;

    protected override void Initialization()
    {
        base.Initialization();

        if (!Pooler && !TryGetComponent<MMObjectPooler>(out Pooler))
            Debug.LogError("Добавь обжикт пулер, откуда я блять гранату брать должен");

        if (!Stamina && !TryGetComponent(out Stamina))
            Debug.LogWarning("Cun't get stamina component");

        _character = GetComponent<Character>();
    }

    protected override void HandleInput()
    {
        base.HandleInput();

        if (_inputManager is InputManagerX manager)
        {
            if (manager.HolyWaterButton.State.CurrentState == MMInput.ButtonStates.ButtonPressed)
            {
                bool canUse = !Stamina || Stamina.ConsumeStamina(StaminaComsuption);
                if (!canUse) return;

                var obj = Pooler.GetPooledGameObject();
                obj.transform.position = SpawnPosition.position;
                if (obj.TryGetComponent(out ThrownObject thrownObject))
                {
                    int myDirection = _character.IsFacingRight ? 1 : -1;

                    thrownObject.Direction =
                        new Vector2(myDirection, thrownObject.Direction.y);
                }

                obj.SetActive(true);
            }
        }
    }
}