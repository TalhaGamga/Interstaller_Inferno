using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketPacket : MonoBehaviour
{
    [SerializeField] RocketBase rocket;
    RocketBase _rocket;
    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.TryGetComponent<ShipBase>(out ShipBase ship))
        {
            for (int i = 0; i < 3; i++)
            {
                Debug.Log("Collided");
                _rocket = Instantiate(rocket);
                if (ship.GetComponent<RocketLauncher>().Add(_rocket))
                {
                    Debug.Log("Add çalıştırıldı");
                }
            }

            Destroy(gameObject);
        }
    }
}
