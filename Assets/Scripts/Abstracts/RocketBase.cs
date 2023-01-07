using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class RocketBase : MonoBehaviour/*, IFirable*/
{
    public float damage;

    public virtual void Fire()
    {

    }

    public virtual void Use(ShipBase ship)
    {
        ship.TakeDamage(damage);
    }

    public virtual void Move(ShipBase Source)
    {
        throw new System.NotImplementedException();// Talha
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (TryGetComponent(out ShipBase shipBase))
        {
        }
    }
}
