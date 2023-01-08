using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class RocketBase : MonoBehaviour/*, IFirable*/
{
    public float damage;
    public bool isAdding=false;

    [SerializeField] private float rocketSpeed = 50;
    private void Start()
    {
        //Shot();
    }
    public virtual void Fire(ShipBase ship)
    {
        Shot();
    }
    public virtual void Use(ShipBase ship) 
    {
        gameObject.layer = 0;
        ship.hp -= damage;//todo
    }
    protected virtual void OnTriggerEnter(Collider other) //Roket shipe çarptýðýnda
    {
        if (other.gameObject.TryGetComponent(out ShipBase shipBase))
        {
            Use(shipBase);

            Destroy(gameObject);
        }
    }

    public virtual void Follow(Transform target)
    {
        StartCoroutine(IEFollow(target));
    }

    public IEnumerator IEFollow(Transform target)
    {
        float timer = 0;
        while (timer < 20)
        {
            timer += Time.deltaTime;
            Vector3 direction = (target.transform.position - transform.position).normalized;
            Quaternion targetRot = Quaternion.LookRotation(target.position - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, 1.5f * Time.deltaTime);
            transform.position += transform.forward * rocketSpeed * Time.deltaTime;
            yield return null;
        }

        Destroy(gameObject);
    }
    
    public virtual void Shot()
    {
        StartCoroutine(IERocketShot());
    }

    IEnumerator IERocketShot()
    {
        float timer = 0;
        while (timer < 3)
        {
            timer += Time.deltaTime;
            transform.position += transform.forward * rocketSpeed * Time.deltaTime;
            yield return null;
        }

        Destroy(gameObject);
    }

}
