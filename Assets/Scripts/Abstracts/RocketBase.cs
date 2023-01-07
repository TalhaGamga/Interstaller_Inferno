using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class RocketBase : MonoBehaviour/*, IFirable*/
{
    public float damage;

    public virtual void Fire()
    {
        transform.parent = null;
        transform.position = transform.forward * 8;//todo
        //todo delete
    }

    public virtual void Use(ShipBase ship)
    {
        ship.hp -= damage;//todo
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out ShipBase shipBase))
        {

            Use(shipBase);
            //todo ??

        }
    }
}
