using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class RocketX : RocketBase
{
    bool isTriger;
    public void Force(ShipBase shipBase,Vector3 vector3)
    {
        shipBase.Force(vector3);
    }
    protected override void OnTriggerEnter(Collider other)
    {
        if (isTriger)
        {
            return;
        }
        if (other.gameObject.TryGetComponent(out ShipBase shipBase))
        {
            isTriger = true;
            Use(shipBase);
            Force(shipBase, transform.position);
        }

    }
}
