using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnDrop : MonoBehaviour
{
    public GameObject[] Drops = new GameObject[0];

    public void DoShit()
    {
        var drop = Drops[Random.Range(0, Drops.Length)];
        drop.GetCloneFromPool(null, transform.position, Quaternion.identity);
    }
}
