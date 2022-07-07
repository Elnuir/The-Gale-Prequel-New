using System.Collections;
using System.Collections.Generic;
using MoreMountains.CorgiEngine;
using UnityEngine;
using UnityEngine.Events;

public class DestroyByDash : MonoBehaviour
{
    public float DelayBeforeDestruction;
    public GameObject ObjectToDestroy;
    public UnityEvent OnDestruction;
    private bool _isDestroying;

    private void OnTriggerStay2D(Collider2D other)
    {
        OnTriggerEnter2D(other);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (_isDestroying)
            return;

        if (other.gameObject.tag == "Player")
        {
            var p = other.gameObject.GetComponent<Character>();
            if (CheckFacing(p) && p.MovementState.CurrentState == CharacterStates.MovementStates.Dashing)
            {
                OnDestruction?.Invoke();
                _isDestroying = true;
                Invoke(nameof(SelfDestroy), DelayBeforeDestruction);
            }

        }
    }

    private bool CheckFacing(Character character)
    {
        if (transform.position.x > character.transform.position.x)
            return character.IsFacingRight;
        else
            return !character.IsFacingRight;
    }

    private void SelfDestroy()
    {
        GameObject.Destroy(ObjectToDestroy);
    }
}
