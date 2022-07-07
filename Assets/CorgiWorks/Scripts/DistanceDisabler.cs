using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceDisabler : MonoBehaviour
{
    private List<GameObject> _targets = new List<GameObject>();
    private List<GameObject> _disabledTargets = new List<GameObject>();

    public float DisableDistance;
    public float EnableDistance;
    public string[] Tags;


    private void Start()
    {
        foreach (var t in Tags)
            _targets.AddRange(GameObject.FindGameObjectsWithTag(t));
    }

    private void Update()
    {
        foreach (GameObject t in _targets)
        {
            float distance = GetDistanceTo(t);

            if (distance >= DisableDistance && t.activeSelf)
            {
                t.SetActive(false);
                _disabledTargets.Add(t);
            }
            else if (distance <= EnableDistance && !t.activeSelf)
            {
                if (_disabledTargets.Contains(t))
                {
                    t.SetActive(true);
                    _disabledTargets.Remove(t);
                }
            }
        }
    }

    private float GetDistanceTo(GameObject other)
    {
        return Vector2.Distance(other.transform.position, transform.position);
    }
}
