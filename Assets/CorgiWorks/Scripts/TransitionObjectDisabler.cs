using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TransitionObjectDisabler : MonoBehaviour
{
    public string[] TargetObjects = { };

    private GameObject[] _targetObjects;
    private List<GameObject> _disabledOnes = new List<GameObject>();



    public void EnableAll()
    {
        _disabledOnes.ForEach(d => d?.SetActive(true));
        _disabledOnes.Clear();
    }

    public void DisableAll()
    {
        _targetObjects = TargetObjects.Select(GameObject.Find).ToArray();
        foreach (var o in _targetObjects)
        {
            o?.SetActive(false);
            if (o != null)
                _disabledOnes.Add(o);
        }
    }
}
