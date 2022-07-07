using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using MoreMountains.CorgiEngine;
using MoreMountains.Tools;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnerX : MonoBehaviour
{
    public MMObjectPooler ObjectPooler { get; set; }
    public Transform[] Points;
    public float Interval;
    public bool RandomOrder;

    private float _currInterval;
    private int _currIndex = int.MaxValue;

    protected virtual void Start()
    {
        Initialization();
    }

    protected virtual void Initialization()
    {
        if (GetComponent<MMMultipleObjectPooler>() != null)
        {
            ObjectPooler = GetComponent<MMMultipleObjectPooler>();
        }

        if (GetComponent<MMSimpleObjectPooler>() != null)
        {
            ObjectPooler = GetComponent<MMSimpleObjectPooler>();
        }

        if (ObjectPooler == null)
        {
            Debug.LogWarning(this.name +
                             " : no object pooler (simple or multiple) is attached to this Projectile Weapon, it won't be able to shoot anything.");
            return;
        }
    }

    public void Spawn()
    {
        _currIndex = 0;
        _currInterval = 0;
        if (RandomOrder)
        {
            Points = Points.OrderBy(_ => Random.Range(0, 1)).ToArray();
        }
    }

    private void Update()
    {
        if (_currInterval > 0)
        {
            _currInterval -= Time.deltaTime;
            return;
        }

        if (_currIndex < Points.Length)
        {
            Spawn(Points[_currIndex++].position);
            _currInterval = Interval;
        }
    }

    private void Spawn(Vector3 position)
    {
        GameObject nextGameObject = ObjectPooler.GetPooledGameObject();

        // mandatory checks
        if (nextGameObject == null)
        {
            return;
        }

        if (nextGameObject.GetComponent<MMPoolableObject>() == null)
        {
            throw new Exception(gameObject.name +
                                " is trying to spawn objects that don't have a PoolableObject component.");
        }

        // we position the object
        nextGameObject.transform.position = position;

        // we activate the object
        nextGameObject.gameObject.SetActive(true);
        nextGameObject.gameObject.MMGetComponentNoAlloc<MMPoolableObject>().TriggerOnSpawnComplete();

        // we check if our object has an Health component, and if yes, we revive our character
        Health objectHealth = nextGameObject.gameObject.MMGetComponentNoAlloc<Health>();
        if (objectHealth != null)
        {
            objectHealth.Revive();
        }
    }
}