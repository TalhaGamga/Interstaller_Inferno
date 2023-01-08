using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketPacket : MonoBehaviour
{
    [SerializeField] RocketBase rocket;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<ShipBase>(out ShipBase ship))
        {
            ship.GetComponent<RocketLauncher>().Add(rocket);
        }
    }
}
