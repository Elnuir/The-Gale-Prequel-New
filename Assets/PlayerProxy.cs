using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProxy : MonoBehaviour
{
    private PlayerStats stats;
    
    // Start is called before the first frame update
    void Start()
    {
        stats = FindObjectOfType<PlayerStats>();
    }

    void Update()
    {
        if (!stats && !ActionEx.CheckCooldown(Update, 0.5f))
            stats = FindObjectOfType<PlayerStats>();
    }

    public void Die()
    {
       stats.DecreaseHealth(9999); 
    }
}
