using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketPacket : MonoBehaviour
{
    [SerializeField] RocketBase rocket;
    RocketBase _rocket;
    private void OnTriggerEnter(Collider collision)
    {

        if (collision.gameObject.TryGetComponent<ShipBase>(out ShipBase ship))
        {
            for (int i = 0; i < 3; i++)
            {
                Debug.Log("Collided");
                ship.FireTimer();
                _rocket = Instantiate(rocket);
                gameObject.layer = 0;

                if (ship.GetComponent<RocketLauncher>().Add(_rocket))
                {
                    Debug.Log("Add çalýþtýrýldý");
                }
            }
            Destroy(gameObject);
        }
    }
}
