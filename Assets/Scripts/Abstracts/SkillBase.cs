using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public abstract class CollectableSkillBase : MonoBehaviour
{
    public virtual void Use(ShipBase ship)
    {

        //todo destroy

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out ShipBase shipBase))
        {

            Use(shipBase);

        }
    }

}
