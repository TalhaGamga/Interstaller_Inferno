using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class DroneController : MonoBehaviour
{

    [SerializeField] Transform parent;
    [SerializeField] Vector3 distance;
    [SerializeField] Transform droidMesh;
    [SerializeField] RocketLauncher myRocketLauncher;
    [SerializeField] RocketLauncher enemyShip;

    [SerializeField] GameObject bulletPrefab;
    public bool detect = false;

    private void Update()
    {

        if (detect)
        {
            print("detect");
            droidMesh.DOLookAt(enemyShip.transform.position, 0.4f);
            transform.position = Vector3.Lerp(transform.position, parent.position + distance, Time.deltaTime * 5);
            return;
        }
        transform.position = Vector3.Lerp(transform.position, parent.position + distance, Time.deltaTime * 5);
        transform.DORotate(new Vector3(parent.eulerAngles.x, parent.eulerAngles.y, parent.eulerAngles.z), 0.4f); ;
    }

    public void Shoot()
    {
        StartCoroutine(IEShootInterval());
    }

    IEnumerator IEShootInterval()
    {
        while (detect)
        {

            Bullet bullet = Instantiate(bulletPrefab).GetComponent<Bullet>();
            bullet.transform.parent = transform;
            bullet.transform.localPosition = Vector3.zero;
            transform.rotation=new Quaternion(parent.eulerAngles.x, parent.eulerAngles.y, parent.eulerAngles.z, 0);
            bullet.Fire();

            yield return new WaitForSeconds(1f);

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out RocketLauncher detectShip) && detectShip != myRocketLauncher)
        {
            enemyShip = detectShip;
            detect = true;
            Shoot();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent(out RocketLauncher detectShip))
        {
            enemyShip = null;
            detect = false;
        }
    }

}

