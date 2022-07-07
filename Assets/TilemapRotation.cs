using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilemapRotation : MonoBehaviour
{
    private Animator animator;
    private Animation animation;
    void Start()
    {
        animator = GetComponent<Animator>();
        animation = GetComponent<Animation>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            animation.Play("TileMapRotation");
        }
        if (Input.GetKeyDown(KeyCode.J))
        {
            animation.Play("TileMapRotationBack");
        }
    }
}
