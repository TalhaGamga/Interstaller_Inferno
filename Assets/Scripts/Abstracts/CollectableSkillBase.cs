using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public abstract class CollectableSkillBase : MonoBehaviour
{
    public virtual Tween Use(ShipBase ship)
    {
        transform.SetParent(ship.transform);
     return transform.DOLocalJump(Vector3.zero, 1f, 1, .5f);
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
