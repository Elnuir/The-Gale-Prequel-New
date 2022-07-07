using System.Collections;
using System.Collections.Generic;
using MoreMountains.CorgiEngine;
using UnityEngine;

public class AlignWithFace : MonoBehaviour
{
    private Character _character;
    private bool _isFacingRight;

    // Start is called before the first frame update
    void Start()
    {
        _character = GetComponentInParent<Character>();
        if (_character.InitialFacingDirection == Character.FacingDirections.Right)
            _isFacingRight = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (_character.IsFacingRight != _isFacingRight)
        {
            Flip();
            _isFacingRight = !_isFacingRight;
        }
    }

    void Flip()
    {
        var t = transform;
        t.localPosition = new Vector2(-t.localPosition.x, t.localPosition.y);
    }
}
