using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public void Fire()
    {
        StartCoroutine(IEBulletShot());
    }

    IEnumerator IEBulletShot()
    {
        float timer = 0;
        transform.parent = null;
        while (timer < 3)
        {
            timer += Time.deltaTime;
            transform.position += transform.forward * 30 * Time.deltaTime;
            yield return null;
        }

        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out ShipBase shipBase))
        {

            shipBase.hp -= 10;

        }
    }

}
